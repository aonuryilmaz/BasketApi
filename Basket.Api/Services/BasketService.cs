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
        private readonly IProductService _productService;
        public BasketService(IProductService productService, IBasketRepository basketRepository)
        {
            _productService = productService;
            _basketRepository = basketRepository;
        }
        public async Task<CustomerBasket> AddToBasket(Guid userId, string sku, int quantity)
        {
            var basket = await GetOrCreateBasket(userId);
            var product = await _productService.GetProductBySku(sku);
            if (product == null)
            {
                throw new Exception("Product was not found");
            }
            var basketItem = BasketItem.FromProduct(product,quantity);
            await _productService.DecreaseStock(product, quantity);
            return await AddBasketItemToBasket(basket, basketItem);
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
        private async Task<CustomerBasket> AddBasketItemToBasket(CustomerBasket basket,BasketItem basketItem)
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
            return basket;
        }
    }
}