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
    [Route("[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdminBL adminBL; 
        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
            
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult RegisterAdmin(AdminRegisterRequest admin)
        {
            try
            {
                this.adminBL.RegisterAdmin(admin);
                string userFullName = admin.FullName;

                return this.Ok(new { success = true, message = $"Hello {userFullName} Your Account Created Successfully {admin.Email}" });


            }
            catch (Exception e)
            {

                return this.BadRequest(new { success = false, message = $"Registration Failed {e.Message}" });

              
            }
        }

       
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(AdminLogin cred)
        {
            var token = this.adminBL.Login(cred.Email, cred.Password);

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
               
                success = true;
                userFullName = data.FullName;
                message = "Hello " + userFullName + ", You Logged in Successfully";
                return this.Ok(new { success = true, token = token, message = $"Login {cred.Email}" });
            }
        }

        [AllowAnonymous]
        [HttpPost("Forgot Password")]
        public ActionResult ForgotPassword(ForgotPassword user)
        {
            try
            {
                bool isExist = this.adminBL.ForgotPassword(user.Email);
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


        //  Change Password

        [HttpPut("Reset Password")]
        public ActionResult ResetPassword(UserRestPassword user)
        {
            try
            {
                if (user.NewPassword == user.ConfirmPassword)
                {
                    var EmailClaim = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("Email", StringComparison.InvariantCultureIgnoreCase));
                    this.adminBL.ChangePassword(EmailClaim.Value, user.NewPassword);
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
