using System;
using System.Linq;
using System.Threading.Tasks;
using Basket.Core.Domain.Models;
using Basket.Core.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Basket.Core.Repositories
{
    public class BasketRepository:IBasketRepository
    {
        private IMongoDatabase _database;
        public BasketRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<CustomerBasket> GetBasketByUserId(Guid userId)
        {
            return await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(f => f.UserId == userId);
        }

        public async Task<CustomerBasket> GetBasketByBasketId(Guid basketId)
        {
            return await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(f=>f.Id==basketId);
        }

        public async Task Create(CustomerBasket basket)
        {
            await Collection.InsertOneAsync(basket);
        }

        public async Task<bool> Update(CustomerBasket basket)
        {
            ReplaceOneResult updateResult =
                await Collection
                    .ReplaceOneAsync(
                        filter: f => f.Id == basket.Id,
                        replacement: basket);
            return updateResult.IsAcknowledged
                   && updateResult.ModifiedCount > 0;
        }

        private IMongoCollection<CustomerBasket> Collection =>
            _database.GetCollection<Domain.Models.CustomerBasket>("Baskets");
    }
}