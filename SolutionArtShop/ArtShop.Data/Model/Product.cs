using System;
using System.Collections.Generic;
using System.Text;

namespace ArtShop.Data.Model
{
    public class Product : IdentityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int ArtistID { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public int QuantitySold { get; set; }
        public float AvgStarts { get; set; }
        public virtual Artist Artist {get;set;}
    }
}
