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
                AdminBookResponseData data = bookBL.AddBook(adminId, adminbookData);
                
                if (adminbookData == null)
                {
                    return Ok(new { success=false, message = $"Book Added Failed"});
                }
                else
                {
                    return Ok(new { success=true, message=$"Book Added Successfully {adminId}", data });
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

        //[Authorize(Roles = Role.Admin)]
        [HttpDelete]
        public IActionResult DeleteBookById(string id)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(AdminId => AdminId.Type.Equals("AdminId", StringComparison.InvariantCultureIgnoreCase));
                int adminId = Convert.ToInt32(idClaim.Value);
                bool result = bookBL.DeleteBookById(adminId, id);
                if (!result.Equals(false))
                {
                    return this.Ok(new { success = true, message = " Books Delete Successfully" });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "No such BooksId Exist" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }


    }

}
