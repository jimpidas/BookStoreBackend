using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class AddressRequest
    {
        public string CustomerName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public string MobileNumber { get; set; }
    }
}
