using System;
using System.Threading.Tasks;
using Basket.Core.Domain.Models;

namespace Basket.Core.Domain.Repositories
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketByUserId(Guid userId);
        Task<CustomerBasket> GetBasketByBasketId(Guid basketId);
        Task Create(CustomerBasket basket);
        Task<bool> Update(CustomerBasket basket);
    }
}