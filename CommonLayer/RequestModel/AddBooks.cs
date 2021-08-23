using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class AddBooks
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public string Category { get; set; }
        public string Pages { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

       // public bool InStock {get;set;}
    }
}
