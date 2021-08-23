using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class ReviewListBookResponse
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public string Category { get; set; }
        public string Pages { get; set; }
        public string Price { get; set; }
        public int Review { get; set; }
        public string Feedback { get; set; }
    }
}
