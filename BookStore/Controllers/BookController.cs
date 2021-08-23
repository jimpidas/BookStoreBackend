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
    [Authorize]
    [Route("[controller]")]
    [ApiController]

    public class BookController : Controller
    {
        

        IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        //[Authorize(Roles = Role.Admin)]
        [HttpPost]
        public IActionResult AddBook(AddBooks adminbookData)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(AdminId => AdminId.Type.Equals("AdminId", StringComparison.InvariantCultureIgnoreCase));
                int adminId = Convert.ToInt32(idClaim.Value);
                AdminBookResponseData datas = bookBL.AddBook(adminId, adminbookData);
                
                if (idClaim == null)
                {
                    return Ok(new { success=false, message = $"Book Added Failed"});
                }
                else
                {
                    return Ok(new { success=true, message=$"Book Added Successfully", data= datas });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetListOfBooks()
        {
            try
            {
                var data = bookBL.GetListOfBooks();
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

        [AllowAnonymous]
        [HttpGet("{bookId}")]
        public IActionResult GetListOfBooksid(int bookId)
        {
            try
            {
                var data = bookBL.GetListOfBooksid(bookId);
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

        [HttpPut("{bookId}")]
        public IActionResult UpdateBook(int bookId, AddBooks adminbookData)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(AdminId => AdminId.Type.Equals("AdminId", StringComparison.InvariantCultureIgnoreCase));
                int adminId = Convert.ToInt32(idClaim.Value);
                AdminBookResponseData data = bookBL.UpdateBook(bookId, adminId, adminbookData);
                bool success = false;
                string message;
                if (adminbookData == null)
                {
                   
                    message = $"Book Update Failed";
                    return Ok(new { success, message });

                }
                else
                {
                   
                    success = true;
                    message = $"Book Update Successfully {bookId}";
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete("{bookId}")]
        public IActionResult DeleteBookById(int bookId)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(AdminId => AdminId.Type.Equals("AdminId", StringComparison.InvariantCultureIgnoreCase));
                int adminId = Convert.ToInt32(idClaim.Value);
                bool result = bookBL.DeleteBookById(adminId, bookId);
                if (!result.Equals(false))
                {
                   
                    return this.Ok(new { success = true, message = " Books Delete Successfully" });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "No such Books Id Exist" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }

}
