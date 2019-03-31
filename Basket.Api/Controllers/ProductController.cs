using System.Threading.Tasks;
using Basket.Api.Models;
using Basket.Core.Domain.Models;
using Basket.Core.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    [Route("Product")]
    public class ProductController:ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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
            await _productRepository.Create(product);
            return Ok(product);
        }
    }
}