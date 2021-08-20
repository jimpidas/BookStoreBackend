using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class WishListController : Controller
    {
       
        private IWishListBL wishListBL;
        public WishListController(IWishListBL wishListBL)
        {
            this.wishListBL = wishListBL;
            
        }

        [HttpPost]
        public IActionResult AddBookToWishList(int BookId)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Convert.ToInt32(idClaim.Value);
                var data = this.wishListBL.AddBookToWishList(UserId, BookId);
                if (data != null)
                {
                  
                    return this.Ok(new { status = "True", message = "Book Added To WishList Successfully", data });
                }
                else
                {
                   
                    return this.BadRequest(new { status = "False", message = "Failed To Add WishList" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }


        [HttpGet]
        public IActionResult GetListOfBooksInWishlist()
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Convert.ToInt32(idClaim.Value);
                var data = wishListBL.GetListOfBooksInWishlist(UserId);
                if (idClaim != null)
                {
                              
                    return Ok(new { success = true, message = "List of Wishlist Fetched Successfully", data });
                }
                else
                {
                   
                    return NotFound(new { success = true, message = "Failed To Fetch WishList" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

       
    }
}
