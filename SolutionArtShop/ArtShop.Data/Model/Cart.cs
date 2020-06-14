using System;
using System.Collections.Generic;
using System.Text;

namespace ArtShop.Data.Model
{
    public class Cart : Identity
    {
        public string Cookie { get; set; }
        public DateTime CartDate { get; set; }
        public int ItemCount { get; set; }
    }
}
