using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class OrderResponse
    {
        public int UserId { get; set; }
        public int CartId { get; set; }
        public int AddressId { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public string CustomerName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public string MobileNumber { get; set; }
        public int OrderQuantity { get; set; }
        public int TotalPrice { get; set; }
    }
}
