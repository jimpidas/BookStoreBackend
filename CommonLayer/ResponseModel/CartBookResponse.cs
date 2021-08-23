using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class CartBookResponse
    {
        public int CartId { get; set; }
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public string Category { get; set; }
        public string Pages { get; set; }
        public int Price { get; set; }
        public int OrderQuantity { get; set; }
        public int TotalPrice { get; set; }
    }
}
