using BusinessLayer.Interfaces;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class CartController : Controller
    {
       
        private readonly ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [HttpPost]
        public IActionResult AddBookToCart(int BookId)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Convert.ToInt32(idClaim.Value);
                var data = this.cartBL.AddBookToCart(UserId, BookId);
                if (idClaim != null)
                {
                         
                    return this.Ok(new { status = "True", message = "Book Added To Cart Successfully", data });
                }
                else
                {
                   
                    return this.BadRequest(new { status = "False", message = "Failed To Add Cart" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [HttpGet]
        public IActionResult GetListOfBooksInCart()
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Convert.ToInt32(idClaim.Value);
                var data = cartBL.GetListOfBooksInCart(UserId);
                if (data != null)
                {
                              
                    return Ok(new { success = true, message = "List of Carts Fetched Successfully", data });
                }
                else
                {
                   
                    return NotFound(new { success = true, message = "Cart Fetched Failed" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPut]
        public IActionResult AddBookQuantityintoCart(int BookId, int quantity)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Convert.ToInt32(idClaim.Value);
                var data = this.cartBL.AddBookQuantityintoCart(UserId, BookId, quantity);
                if (idClaim != null)
                {
                      
                    return this.Ok(new { status = "True", message = "Quantity Add Cart Successfully", data });
                }
                else
                {
                   
                    return this.BadRequest(new { status = "False", message = "Failed To Quantity Add To Cart", message1 = "Please login user" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [HttpPut("Increase")]
        public IActionResult IncreaseBookQuantityintoCart(int BookId)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Convert.ToInt32(idClaim.Value);
                var data = this.cartBL.IncreaseBookQuantityintoCart(UserId, BookId);
                if (idClaim != null)
                {
                            
                    return this.Ok(new { status = "True", message = "Quantity Increase Cart Successfully", data=data });
                }
                else
                {
                   
                    return this.BadRequest(new { status = "False", message = "Failed To Quantity Increase To Cart", message1 = "Please login user" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }


        [HttpPut("Decrease")]
        public IActionResult DecreaseBookQuantityintoCart(int BookId)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Convert.ToInt32(idClaim.Value);
                var data = this.cartBL.DecreaseBookQuantityintoCart(UserId, BookId);
                if (idClaim != null)
                {
                   
                    return this.Ok(new { status = "True", message = "Quantity Decrease Cart Successfully", data });
                }
                else
                {
                   
                    return this.BadRequest(new { status = "False", message = "Failed To Quantity Decrease To Cart", message1 = "Please login user" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCartById(int id)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Convert.ToInt32(idClaim.Value);
                bool result = cartBL.DeleteCartById(UserId, id);
                if (idClaim != null)
                {
                                       
                    return this.Ok(new { success = true, message = " Card Delete Successfully" });
                }
                else
                {
                   
                    return this.NotFound(new { success = false, message = "No such CartId Exist" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
