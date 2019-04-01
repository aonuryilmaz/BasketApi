using System.Threading.Tasks;
using Basket.Api.Models;
using Basket.Api.Services;
using Basket.Core.Domain.Models;
using Basket.Core.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    [Route("Product")]
    public class ProductController:ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductRepository productRepository, IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequestModel model)
        {
            var product = new Product(
                model.Sku,
                model.Name,
                model.Brand,
                model.UnitPrice,
                model.InStock
            );
            await _productService.CreateProduct(product);
            return Ok(product);
        }
    }
}