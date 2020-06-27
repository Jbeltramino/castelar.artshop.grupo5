using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ArtShop.Data.Model
{
    public class IdentityBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        [Required]
        public DateTime ChangedOn { get; set; }
        public string ChangedBy { get; set; }
    }
}
