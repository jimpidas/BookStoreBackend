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
            if (user == null)
            {
                return BadRequest("user is null.");
            }
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
                userFullName = data.FullName;      
                return this.Ok(new { success = true, token = token, message = "Hello " + userFullName + ", You Logged in Successfully" });
            }
        }

        [AllowAnonymous]
        [HttpPost("Forgot Password")]
        public ActionResult ForgotPassword(ForgotPassword user)
        {
            try
            {
                bool isExist = this.userBL.ForgotPassword(user.Email);
                if (isExist)
                {
                   
                    Console.WriteLine($"Email User Exist with {user.Email}");
                    return Ok(new { success = true, message = $"Reset Link sent to {user.Email}" });
                }
                else
                {
                   
                    return BadRequest(new { success = false, message = $"No user Exist with {user.Email}" });

                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Authorize]
        [HttpPut("Reset Password")]
        public ActionResult ResetPassword(UserRestPassword user)
        {
            try
            {
                if (user.NewPassword == user.ConfirmPassword)
                {
                    var EmailClaim = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("Email", StringComparison.InvariantCultureIgnoreCase));
                    this.userBL.ChangePassword(EmailClaim.Value, user.NewPassword);
                    return Ok(new { success = true, message = "Your Account Password Changed Successfully", Email = $"{EmailClaim.Value}" });
                }
                else
                {  
                    return Ok(new { success = false, message = "New Password and Confirm Password are not equal." });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
