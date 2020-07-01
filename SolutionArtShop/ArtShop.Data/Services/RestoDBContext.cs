using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ArtShop.Data.Model;

namespace ArtShop.Data.Services
{
    public class RestoDBContext : DbContext
    {
        public RestoDBContext() : base("name=DefaultConnection")
        {
            Database.SetInitializer<RestoDBContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public virtual DbSet<Artist> Artist { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Error> Error { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartItem> CartItem { get; set; }
        public virtual DbSet<Order> Order { get; set; }

    }
}
