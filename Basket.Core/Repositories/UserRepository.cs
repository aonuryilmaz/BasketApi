using System;
using System.Linq;
using System.Threading.Tasks;
using Basket.Core.Domain.Models;
using Basket.Core.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Basket.Core.Repositories
{
    public class UserRepository:IUserRepository
    {
        private IMongoDatabase _database;

        public UserRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<User> Get(string email)
        {
            return await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(f => f.Email == email);
        }

        public async Task Create(User user)
        {
            await Collection.InsertOneAsync(user);
        }

        public async Task<User> GetUserById(Guid userId)
        {
            return await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(f => f.Id.Equals(userId));
        }

        private IMongoCollection<User> Collection => _database.GetCollection<User>("Users");
        
    }
}