using System;
using System.Linq;
using System.Threading.Tasks;
using Basket.Core.Domain.Models;
using Basket.Core.Domain.Repositories;

namespace Basket.Api.Services
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
        public async Task AddToBasket(Guid userId, string sku, int quantity)
        {
            var basket = await GetOrCreateBasket(userId);
            var product = await _productRepository.GetProductBySku(sku);
            if (product == null)
            {
                throw new Exception("Product was not found");
            }

            if (!IsInStock(product,quantity))
            {
                throw new Exception("Product is not in stock");
            }

            var basketItem = BasketItem.FromProduct(product);
            AddBasketItemToBasket(basket, basketItem);
        }    
        private async Task<CustomerBasket> GetOrCreateBasket(Guid userId)
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
        private bool IsInStock(Product product, int quantity)
        {
            return product.InStock >= quantity;
        }
        private async void AddBasketItemToBasket(CustomerBasket basket,BasketItem basketItem)
        {
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