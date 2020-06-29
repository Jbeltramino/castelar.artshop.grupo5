using ArtShop.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ArtShop.Data.Services
{
    public class InMemoryProductData : BaseDataService<Product>
    {
        readonly List<Product> Products;


        public InMemoryProductData()
        {
            Products = new List<Product>()
            {
                new Product(){Title="Pintura 1",ArtistID=1,Description="Descripcion Pintura1",Image="Imagen1",Price=123.2f,QuantitySold=5,AvgStars=2},
                new Product(){Title="Pintura 2",ArtistID=1,Description="Descripcion Pintura2",Image="Imagen1",Price=200.2f,QuantitySold=5,AvgStars=2},
                new Product(){Title="Pintura 3",ArtistID=1,Description="Descripcion Pintura3",Image="Imagen1",Price=4312.2f,QuantitySold=5,AvgStars=2},
                new Product(){Title="Pintura 4",ArtistID=1,Description="Descripcion Pintura4",Image="Imagen1",Price=523.2f,QuantitySold=5,AvgStars=2},
                new Product(){Title="Pintura 5",ArtistID=1,Description="Descripcion Pintura5",Image="Imagen1",Price=64.2f,QuantitySold=5,AvgStars=2},
                new Product(){Title="Pintura 6",ArtistID=1,Description="Descripcion Pintura6",Image="Imagen1",Price=623.2f,QuantitySold=5,AvgStars=2}
            };
        }

        public override List<Product> Get(Expression<Func<Product, bool>> whereExpression = null, Func<IQueryable<Product>, IOrderedQueryable<Product>> orderFunction = null, string includeEntities = "")
        {
            return Products.ToList();
        }
        // public IEnumerable<Product> GetAll()
        //{
        //    return Products;
        //}
    }
}
