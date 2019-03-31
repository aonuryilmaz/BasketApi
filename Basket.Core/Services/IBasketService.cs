using System;
using System.Threading.Tasks;
using Basket.Core.Domain.Models;

namespace Basket.Core.Services
{
    public interface IBasketService
    {
        Task<CustomerBasket> GetOrCreateBasket(Guid userId);
        Task AddToBasket(CustomerBasket basket,BasketItem basketItem);
    }
}