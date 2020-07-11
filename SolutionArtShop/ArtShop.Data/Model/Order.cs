using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ArtShop.Data.Model
{
    public class Order : IdentityBase
    {
        [Required]
        [DisplayName("Id Usuario")]
        public string UserId { get; set; }
        [Required]
        [DisplayName("Fecha")]
        public DateTime OrderDate { get; set; }
        [Required]
        [DisplayName("Precio Total")]
        public double TotalPrice { get; set; }
        [Required]
        public int OrderNumber { get; set; }
        [Required]
        [DisplayName("Cantidad Items")]
        public int ItemCount { get; set; }

    }
}
