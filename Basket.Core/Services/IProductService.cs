using System;
using System.Threading.Tasks;
using Basket.Core.Domain.Models;

namespace Basket.Core.Services
{
    public interface IProductService
    {
        Task DecreaseStock(Product product, int quantity);
    }
}