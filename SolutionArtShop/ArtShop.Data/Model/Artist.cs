using System;
using System.Collections.Generic;
using System.Text;

namespace ArtShop.Data.Model
{
    public class Artist : Identity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LifeSpan { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public int TotalProducts { get; set; }
    }
}
