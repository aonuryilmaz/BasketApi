using System;
using System.Linq;
using System.Threading.Tasks;
using Basket.Core.Domain.Models;
using Basket.Core.Domain.Repositories;

namespace Basket.Core.Services
{
    public class BasketService:IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;
        public BasketService(IProductRepository productRepository, IBasketRepository basketRepository)
        {
            _productRepository = productRepository;
            _basketRepository = basketRepository;
        }


        public async Task<CustomerBasket> GetOrCreateBasket(Guid userId)
        {
            var basket = await _basketRepository.GetBasketByUserId(userId);
            if (basket==null)
            {
                var newBasket = new CustomerBasket(userId);
                await _basketRepository.Create(newBasket);
                return newBasket;
            }

            return basket;
        }

        public async Task AddToBasket(CustomerBasket customerBasket,BasketItem basketItem)
        {
            var basket = await _basketRepository.GetBasketByBasketId(customerBasket.Id);
            var item = basket.ItemList.FirstOrDefault(f => f.Sku == basketItem.Sku);
            if (item!=null)
            {
                item.Quantity += basketItem.Quantity;
            }
            else
            {
                basket.ItemList.Add(basketItem);
            }

            await _basketRepository.Update(basket);
        }
    }
}