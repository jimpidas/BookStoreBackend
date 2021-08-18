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
    public class AddressController : Controller
    {
        IAddressBL addressBL;
        public AddressController( IAddressBL addressBL)
        {
           
            this.addressBL = addressBL;
        }
        [HttpPost]
        public IActionResult AddAddress(AddressRequest address)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));

                if (idClaim != null)
                {
                    int UserId = Convert.ToInt32(idClaim.Value);
                    var data = this.addressBL.AddAddress(UserId, address);
                    return this.Ok(new { status = "True", message = "Address Added Successfully", data });
                }
                else
                {

                    return this.BadRequest(new { status = "False", message = "Failed To Add Address", message1 = "Please Login User " });
                }

            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }
        [HttpGet]
        public IActionResult GetListOfAddress()
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));

                if (idClaim != null)
                {
                    int UserId = Convert.ToInt32(idClaim.Value);
                    var data = this.addressBL.GetListOfAddress(UserId);
                    return this.Ok(new { status = "True", message = "list Of Address Display Successfully", data });
                }
                else
                {

                    return NotFound(new { success = true, message = "No Address Found" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        [HttpGet("{addressId}")]
        public IActionResult GetListOfAddressid(int addressId)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));

                if (idClaim != null)
                {
                    int UserId = Convert.ToInt32(idClaim.Value);
                    var data = this.addressBL.GetListOfAddressid(UserId, addressId);
                    return this.Ok(new { status = "True", message = "Address Display Successfully", data });
                }
                else
                {

                    return NotFound(new { success = false, message = "No Address Found" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }


        [HttpPut]
        public IActionResult UpdateAddress(int AddressId, AddressRequest address)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));

                if (idClaim != null)
                {
                    int UserId = Convert.ToInt32(idClaim.Value);
                    var data = this.addressBL.UpdateAddress(UserId, AddressId, address);
                    return this.Ok(new { status = "True", message = "Address update Successfully", data });
                }
                else
                {
                    return this.BadRequest(new { status = "False", message = "Failed Address update", message1 = "Please login user" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }
        [HttpDelete]
        public IActionResult DeleteAddressById(int addressid)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));

                if (idClaim != null)
                {
                    int UserId = Convert.ToInt32(idClaim.Value);
                    bool result = addressBL.DeleteAddressById(UserId, addressid);
                    return this.Ok(new { success = true, message = " Address Delete Successfully" });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "No such Address Exist", message1 = "Please login User" });
                }
            }
            catch (Exception ex)
            {
                
                return BadRequest(new { ex.Message });
            }
        }
    }
}
