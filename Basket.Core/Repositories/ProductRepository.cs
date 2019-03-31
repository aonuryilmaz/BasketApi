using System.Threading.Tasks;
using Basket.Core.Domain.Models;
using Basket.Core.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Basket.Core.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private IMongoDatabase _database;
        public ProductRepository(IMongoDatabase database)
        {
            _database = database;
        }
        public async Task<Product> GetProductBySku(string sku)
        {
            return await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(f => f.Sku == sku);
        }

        public async Task<bool> Update(Product product)
        {
            ReplaceOneResult updateResult =
                await Collection
                    .ReplaceOneAsync(
                        filter: f => f.Id == product.Id,
                        replacement: product);
            return updateResult.IsAcknowledged
                   && updateResult.ModifiedCount > 0;
                
        }

        public async Task Create(Product product)
        {
            await Collection.InsertOneAsync(product);
        }

        private IMongoCollection<Product> Collection => _database.GetCollection<Product>("Products");
    }
}