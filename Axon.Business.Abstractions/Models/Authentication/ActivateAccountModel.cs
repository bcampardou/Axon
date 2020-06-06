using System;
using System.Collections.Generic;
using System.Text;

namespace Axon.Business.Abstractions.Models.Authentication
{
    public class ActivateAccountModel
    {
        public string Username { get; set; }
        public string CallbackUrl { get; set; }
        public void OnGet()
        {

        }
    }
}
