using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CommInfo.Models
{
    public class RegistrationViewModel
    {
        [Required]
        [StringLength(60, ErrorMessage = "The name you have entered is too long. Please enter a shorter name.")]
        [Display(Name = "First Name", Order = 15000)]
        public string NameFirst { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Please enter a last name that is at least two characters long.")]
        [Display(Name = "Last Name", Order = 15001)]
        public string NameLast { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}