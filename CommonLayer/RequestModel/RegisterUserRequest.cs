using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class RegisterUserRequest
    {
        [Required]
        //[RegularExpression("^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "Your First Name should  contain only Alphabets!")]
        public string FullName { get; set; }
        [RegularExpression("^[a-zA-Z0-9]+([._+-][a-zA-Z0-9]+)*@[a-zA-Z0-9]+.[a-zA-Z]{2,4}([.][a-zA-Z]{2,})?$")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 8 and 255 characters", MinimumLength = 8)]
        public string Password { get; set; }

        public string MobileNo { get; set; }
    }
}
