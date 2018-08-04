using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBookStore
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please Enter Email Address")]
        [Display(Name = "Email Address")]
        public string EmailAddress
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string Password
        {
            get;
            set;
        }
        public bool RememberMe
        {
            get;
            set;
        }
    }
}