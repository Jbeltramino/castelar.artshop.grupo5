using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ArtShop.Data.Model
{
    public class Order : IdentityBase
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        [Required]
        public int OrderNumber { get; set; }
        [Required]
        public int ItemCount { get; set; }
    }
}
