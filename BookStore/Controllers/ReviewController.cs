using BusinessLayer.Interfaces;
using CommonLayer.RequestModel;
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
    public class ReviewController : Controller
    {
       
        private IReviewBL reviewBL;
        public ReviewController(IReviewBL reviewBL)
        {
            this.reviewBL = reviewBL;
          
        }

        [HttpPost]
        public IActionResult AddReview(int BookId, ReviewRequest review)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Convert.ToInt32(idClaim.Value);
                var data = this.reviewBL.AddReview(UserId, BookId, review);
                if (data != null)
                {
                  
                    return this.Ok(new { status = "True", message = "Review Added To Book Successfully", data });
                }
                else
                {
                  
                    return this.BadRequest(new { status = "False", message = "Failed To Add Review" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [HttpGet]
        public IActionResult GetListOfReview()
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Convert.ToInt32(idClaim.Value);
                var data = reviewBL.GetListOfReview(UserId);
                if (data != null)
                {
                  
                    return Ok(new { success = true, message = "List of Review Fetched Successfully", data });
                }
                else
                {
                   
                    return NotFound(new { success = true, message = "List of Review Fetched Field" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
