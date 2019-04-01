using System;
using System.Threading.Tasks;
using Basket.Core.Domain.Models;

namespace Basket.Core.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> Get(string email);
        Task Create(User user);
    }
}