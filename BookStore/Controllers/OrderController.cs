using BusinessLayer.Interfaces;
using CommonLayer.RequestModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        
        private readonly IOrderBL orderBL;
        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }

        [HttpPost]
        public IActionResult AddOrder(OrderRequest order)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                if (idClaim != null)
                {
                    int UserId = Convert.ToInt32(idClaim.Value);
                    var data = this.orderBL.AddOrder(UserId, order.CartId, order.AddressId);
                    return this.Ok(new { status = "True", message = "Order Successfull", data });
                }
                else
                {

                    return this.BadRequest(new { status = "False", message = "Failed To Order", message1 = "Please Login User " });
                }

            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }
        [HttpGet("All")]
        public IActionResult GetListOfOrders()
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));

                if (idClaim != null)
                {
                    int UserId = Convert.ToInt32(idClaim.Value);
                    var data = orderBL.GetListOfOrders(UserId);
                    return Ok(new { success = true, message = "Get List Of Orders", data });
                }
                else
                {

                    return NotFound(new { success = true, message = "Please Login User And Then Access" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetOrders(int CartId)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));

                if (idClaim != null)
                {
                    int UserId = Convert.ToInt32(idClaim.Value);
                    var data = orderBL.GetOrders(UserId, CartId);
                    return Ok(new { success = true, message = "Order Successfully", data });
                }
                else
                {

                    return NotFound(new { success = true, message = "Please Login User And Then Access" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete]
        public IActionResult DeleteOrderById(int OrderId)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));

                if (idClaim != null)
                {
                    int UserId = Convert.ToInt32(idClaim.Value);
                    bool result = orderBL.DeleteOrderById(UserId, OrderId);
                    return this.Ok(new { success = true, message = " Order Delete Successfully" });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "No such Order Exist", message1 = "Please login User" });
                }
            }
            catch (Exception ex)
            {
                bool success = false;
                return BadRequest(new { ex.Message });
            }
        }


    }
}
