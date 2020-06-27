using System;
using System.Collections.Generic;
using System.Text;

namespace ArtShop.Data.Model
{
    public class Order : IdentityBase
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalPrice { get; set; }
        public int OrderNumber { get; set; }
        public int ItemCount { get; set; }
    }
}
