using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Web.Mvc;
using System.IO;

namespace ArtShop.Data.Model
{
    public class Product : IdentityBase
    {
        [Required]
        [DisplayName("Título")]
        public string Title { get; set; }
        [Required]
        [DisplayName("Descripción")]
        public string Description { get; set; }
        [Required]
        [DisplayName("ID Artista")]
        public int? ArtistID { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        [DisplayName("Precio")]
        public double Price { get; set; }
        [DisplayName("Cantidad Vendida")]
        public int QuantitySold { get; set; }
        [DisplayName("Promedio Estrellas")]
        public double AvgStars { get; set; }

        [NotMapped]
        public List<Artist> Artistas { get; set; }
        [NotMapped]
        public Artist Artista { get; set; }

    }
}
