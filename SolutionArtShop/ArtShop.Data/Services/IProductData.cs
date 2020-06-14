using ArtShop.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArtShop.Data.Services
{
    public interface IProductData
    {
        IEnumerable<Product> GetAll();
    }
}
