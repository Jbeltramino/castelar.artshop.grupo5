using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtShop.Data.Model
{
    public class Artist : IdentityBase
    {
        [Required]
        [DisplayName("Nombre")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Apellido")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Fecha Nacimiento")]
        public string LifeSpan { get; set; }

        [Required]
        [DisplayName("País")]
        public string Country { get; set; }

        [Required]
        [DisplayName("Descripción")]
        public string Description { get; set; }

        [DisplayName("Total de Pinturas")]
        public int TotalProducts { get; set; }

        [NotMapped]
        [DisplayName("Nombre Completo")]
        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }
        public virtual ICollection<Product> Product { get; set; }

    }
}
