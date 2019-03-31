using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Basket.Api.Models;
using Basket.Core.Domain.Models;
using Basket.Core.Domain.Repositories;
using Basket.Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Basket.Api.Controllers
{
    [Route("Basket")]
    public class BasketController:ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        public BasketController(
            IUserRepository userRepository, 
            IProductRepository productRepository, 
            IBasketService basketService, 
            IProductService productService)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _basketService = basketService;
            _productService = productService;
        }
        // POST api/values
        [HttpPost]
        public async Task<IActionResult> AddToBasket([FromBody]AddToBasketItemRequestModel model)
        {
            var user = await _userRepository.GetUserByEmail(model.UserEmail);
            if (user==null)
            {
                return BadRequest("User not found");
            }
            
            var basket = await _basketService.GetOrCreateBasket(user.Id);
            var product = await _productRepository.GetProductBySku(model.Sku);
            if (!IsAvailableStock(product,model.Quantity))
            {
                return BadRequest("Product stock is not available");
            }
            
            var basketItem = BasketItem.FromProduct(product,model.Quantity);
            await _basketService.AddToBasket(basket,basketItem);
            await _productService.DecreaseStock(product, model.Quantity);
            return Ok();
        }

        private bool IsAvailableStock(Product product, int quantity)
        {
            return product.InStock >= quantity;
        }
    }
}