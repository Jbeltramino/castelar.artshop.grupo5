using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ArtShop.Data.Model
{
    public class OrderDetail : IdentityBase
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}
