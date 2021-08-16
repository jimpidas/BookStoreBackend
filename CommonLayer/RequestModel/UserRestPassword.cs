using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class UserRestPassword
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
