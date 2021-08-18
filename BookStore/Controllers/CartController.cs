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
    public class CartController : Controller
    {
       
        private ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [HttpPost("Add")]
        public IActionResult AddBookToCart(int BookId)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                if (idClaim != null)
                {
                    int UserId = Convert.ToInt32(idClaim.Value);
                    var data = this.cartBL.AddBookToCart(UserId, BookId);
                    return this.Ok(new { status = "True", message = "Book Added To Cart Successfully", data });
                }
                else
                {

                    return BadRequest(new { status = "False", message = "Failed To Add Cart", message1 = "Please Login User " });
                }

            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [HttpGet("GetCart")]
        public IActionResult GetListOfBooksInCart()
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Convert.ToInt32(idClaim.Value);
                var data = cartBL.GetListOfBooksInCart(UserId);
                if (data != null)
                {

                    return Ok(new { success = true, message = "List of Books Fetched Successfully", data });
                }
                else
                {

                    return NotFound(new { success = true, message = "No Books Found" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCartById(string id)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Convert.ToInt32(idClaim.Value);
                bool result = cartBL.DeleteCartById(UserId, id);
                if (!result.Equals(false))
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
