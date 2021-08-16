using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class AdminBookResponseData
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public string Category { get; set; }
        public string Pages { get; set; }
        public string Price { get; set; }
    }
}
