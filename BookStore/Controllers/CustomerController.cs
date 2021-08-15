using BusinessLayer.Interfaces;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private IUserBL userBL;
        public CustomerController(IUserBL userBL)
        {
            this.userBL = userBL;

        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult RegisterUser(RegisterUserRequest user)
        {
            try
            {
                this.userBL.RegisterUser(user);
                string userFullName = user.FullName;//+ " " + user.LastName;

                return this.Ok(new { success = true, message = $"Hello {userFullName} Your Account has been Created Successfully {user.Email}" });

            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = $"Registration Failed {e.Message}" }); 
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(Login cred)
        {
            var token = this.userBL.Login(cred.Email, cred.Password);

            UserResponse data = new UserResponse();

            string message, userFullName;
            bool success = false;
            if (token == null)
            {
              
                message = "Enter Valid Email & Password";
                return Ok(new { success, message });

            }
            else
            {
                userFullName = data.FullName;       //data.FirstName + " " + data.LastName;
                return this.Ok(new { success = true, token = token, message = "Hello " + userFullName + ", You Logged in Successfully" });
            }
        }
    }
}
