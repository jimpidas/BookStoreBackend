using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class OrderRequest
    {
        [Required]
        public int CartId { get; set; }
        [Required]
        public int AddressId { get; set; }
    }
}
