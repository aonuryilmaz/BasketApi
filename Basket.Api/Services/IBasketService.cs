using System;
using System.Threading.Tasks;
using Basket.Core.Domain.Models;

namespace Basket.Api.Services
{
    public interface IBasketService
    {
        Task AddToBasket(Guid userId, string sku, int quantity);
    }
}