using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ArtShop.Data.Model
{
    public class CartItem : IdentityBase
    {
        [Required]
        public int CartId { get; set; }
        [Required]
        [DisplayName("Producto")]
        public int ProductId { get; set; }
        [Required]
        [DisplayName("Precio")]
        public double Price { get; set; }
        [Required]
        [DisplayName("Cantidad")]
        public int Quantity { get; set; }
    }

    public class ListItems
    {
        public List<CartItem> itemCartList { get; set; }
    }
}
