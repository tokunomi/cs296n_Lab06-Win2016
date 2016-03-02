using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommInfo.Models
{
    public class LogInViewModel
    {
        //[Required]
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [HiddenInput(DisplayValue=false)]  // hides control when viewed
        public string ReturnUrl { get; set; }
    }
}