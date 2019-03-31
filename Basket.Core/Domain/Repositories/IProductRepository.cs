using System.Threading.Tasks;
using Basket.Core.Domain.Models;

namespace Basket.Core.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetProductBySku(string sku);
        Task<bool> Update(Product product);
        Task Create(Product product);
    }
}