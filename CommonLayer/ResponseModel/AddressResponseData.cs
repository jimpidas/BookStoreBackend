using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class AddressResponseData
    {
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public string CustomerName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public string MobileNumber { get; set; }
    }
}
