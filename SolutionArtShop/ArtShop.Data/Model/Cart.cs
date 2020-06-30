using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ArtShop.Data.Model
{
    public class Cart : IdentityBase
    {

        [Required]
        public string Cookie { get; set; }
        [Required]
        public DateTime CartDate { get; set; }
        [Required]
        public int ItemCount { get; set; }
    }
}
