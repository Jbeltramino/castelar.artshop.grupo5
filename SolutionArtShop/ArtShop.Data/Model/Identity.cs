using System;
using System.Collections.Generic;
using System.Text;

namespace ArtShop.Data.Model
{
    public class Identity
    {
        public int ID { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ChangedOn { get; set; }
        public string ChangedBy { get; set; }
    }
}
