using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class MockProductRepository : IProductRepository
    {
        public IQueryable<Product> Products => new List<Product>
        {
            new Product { Name = "Piłka nożna", Price = 125 },
            new Product { Name = "Rower górski", Price = 5500 },
            new Product { Name = "Strój pływacki męski", Price = 329 }
        }.AsQueryable();
    }
}
