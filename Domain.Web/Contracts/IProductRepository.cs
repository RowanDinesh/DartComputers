using Domain.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Web.Contracts
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task UpdateAsync(Product product);

        Task<List<Product>> GetAllProductAsync();

        Task<Product> GetProductByIdAsync(int id);
    }
}
