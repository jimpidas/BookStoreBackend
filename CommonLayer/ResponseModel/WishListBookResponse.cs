using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class WishListBookResponse
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int WishListId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public string Category { get; set; }
        public string Pages { get; set; }
        public string Price { get; set; }
    }
}
