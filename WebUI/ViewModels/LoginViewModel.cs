using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Write your username.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The username can be at least 2 and at most 16 characters. ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Write your password.")]
        [StringLength(16, MinimumLength = 2, ErrorMessage = "The password can be at least 2 and at most 16 characters.")]
        public string UserPassword { get; set; }
        public bool UserRememberMe { get; set; }
    }
}