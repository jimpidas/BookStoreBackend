using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class CartRequest
    {
        [Required]
        public int BookId { get; set; }

    }
}
