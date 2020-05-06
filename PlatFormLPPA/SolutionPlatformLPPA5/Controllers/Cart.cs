using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolutionPlatformLPPA5.Controllers
{
    public class Cart
    {
        public int Id_Cart { get; set; }
        public string Cookie { get; set; }
        public DateTime CartDate { get; set; }
        public int ItemCount { get; set; }
    }
}