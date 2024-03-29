﻿using Basket.API.Models;
using Basket.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreApiResponse;
using System.Net;
using Basket.API.GrpcServices;

namespace Basket.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBusketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;
        public BasketController(IBusketRepository basketRepository, DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService; 
        }
        [HttpGet]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket(string userName)
        {
            try
            {
                var basket = await _basketRepository.GetBasket(userName);
                return CustomResult("Basket data load successfully",basket);  
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart basket)
        {
            try
            {
                foreach (var item in basket.Items)
                {
                    var discount = await _discountGrpcService.GetDiscount(item.ProductId);
                    if (discount != null)
                        item.Price -= discount.Amount;
                }
                var basketResponse = await _basketRepository.UpdateBasket(basket);
                return CustomResult("Basket modified done.", basketResponse);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpDelete]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            try
            {
                await _basketRepository.DeleteBasket(userName);
                return CustomResult("Basket has been deleted.");
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
