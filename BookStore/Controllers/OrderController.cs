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

       
        private IOrderBL orderBL;
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
                int UserId = Convert.ToInt32(idClaim.Value);
                var data = this.orderBL.AddOrder(UserId, order.CartId, order.AddressId);
                if (idClaim != null)
                {
                   
                    return this.Ok(new { status = "True", message = "Order Successfull", data });
                }
                else
                {
                              
                    return this.BadRequest(new { status = "False", message = "Failed To Order" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [HttpGet]
        public IActionResult GetListOfOrders()
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Convert.ToInt32(idClaim.Value);
                var data = orderBL.GetListOfOrders(UserId);
                if (idClaim != null)
                {
                    
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

        [HttpGet("CartId")]
        public IActionResult GetOrders(int CartId)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Convert.ToInt32(idClaim.Value);
                var data = orderBL.GetOrders(UserId, CartId);
                if (idClaim != null)
                {
                           
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

        [HttpDelete("OrderId")]
        public IActionResult DeleteOrderById(int OrderId)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Convert.ToInt32(idClaim.Value);
                bool result = orderBL.DeleteOrderById(UserId, OrderId);
                if (idClaim != null)
                {
                             
                    return this.Ok(new { success = true, message = " Order Delete Successfully" });
                }
                else
                {
                   
                    return this.NotFound(new { success = false, message = "No such Order Exist" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

    }
}
