using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class SqlProductRepository : IProductRepository
    {
        private AppDbContext dbContext;
        
        public SqlProductRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Product> Products => dbContext.Products;
    }
}
