using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductWithCategoryAsync()
        {
            //EagerLoading  : Ürüne bağlı kategorileri çektiğimiz için EagerLoading
            //LazyLoading   : Daha sonra ürüne bağlı kategorileri çekersek LazyLoading

            return await _context.Products.Include(c => c.Category).ToListAsync();
        }
    }
}
