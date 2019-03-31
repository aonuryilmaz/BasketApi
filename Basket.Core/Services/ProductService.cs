using System;
using System.Threading.Tasks;
using Basket.Core.Domain.Models;
using Basket.Core.Domain.Repositories;

namespace Basket.Core.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task DecreaseStock(Product product, int quantity)
        {
            if (product.InStock < quantity)
            {
                throw new Exception("Product stock is not available");
            }
            product.InStock = product.InStock - quantity;
            await _productRepository.Update(product);
        }
    }
}