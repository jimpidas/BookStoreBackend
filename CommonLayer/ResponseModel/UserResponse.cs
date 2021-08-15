using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class UserResponse
    {
        public int UserId { set; get; }

        public string FullName { set; get; }

       public string MobileNo { get; set; }

        public string Email { set; get; }
        public string Password { get; set; }
    }
}
