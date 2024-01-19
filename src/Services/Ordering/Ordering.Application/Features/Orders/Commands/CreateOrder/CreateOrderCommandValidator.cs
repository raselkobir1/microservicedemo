﻿using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder
{
    public class UpdateOrderCommandValidator: AbstractValidator<CreateOrderCommand> 
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().MaximumLength(50)
                .WithMessage("Please enter user name.")
                .EmailAddress()
                .WithMessage("User name should be valid email address");

            RuleFor(x => x.EmailAddress).NotEmpty();

            RuleFor(x=> x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x=> x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x=> x.TotalPrice).GreaterThan(0);

        }
    }
}