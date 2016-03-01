using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CommInfo.Models
{
    public class Member : IdentityUser
    {
        List<Message> message = new List<Message>();
        public int MemberID { get; set; }  // Was going to use Identity's MemberID, but MessageController has issues
        public int MessageID { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The name you have entered is too long. Please enter a shorter name.")]
        [Display(Name = "First Name", Order = 15000)]
        public string NameFirst { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Please enter a last name that is at least two characters long.")]
        [Display(Name = "Last Name", Order = 15001)]
        public string NameLast { get; set; }
        ////[StringLength(20, MinimumLength = 2, ErrorMessage = "Please enter a username that is between 2 to 20 characters long.")]
        ////public string Username { get; set; }  // using Identity's Username
        //[Required]
        //[DataType(DataType.EmailAddress)]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage="Please check your Email Address; it does not appear valid.")]
        //public string email { get; set; }

        //public void dummy() { this.} // to test what is in Identity
    }
}