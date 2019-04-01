using System.Threading.Tasks;
using Basket.Core.Domain.Models;

namespace Basket.Api.Services
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string email);
        Task CreateUser(string email);
    }
}