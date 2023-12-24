﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Models
{
    public class Email
    {
        public Email() 
        {
            To = new List<string>();
            Cc = new List<string>();
            AttachmentFiles = new List<IFormFile>();
        }
        public List<string> To { get; set; }
        public List<string>? Cc { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public List<IFormFile>? AttachmentFiles { get; set; }
    }
}