using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolutionPlatformLPPA5.Controllers
{
    public class Order
    {
        public int Id_Order { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalPrice { get; set; }
        public int OrderNumber { get; set; }
        public int ItemCount { get; set; }
    }
}