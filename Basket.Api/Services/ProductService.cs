using System;
using System.Threading.Tasks;
using Basket.Core.Domain.Models;
using Basket.Core.Domain.Repositories;

namespace Basket.Api.Services
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

        public async Task CreateProduct(Product product)
        {
            if (!await IsExist(product.Sku))
            {
                throw new Exception("Product is already created");
            }
            await _productRepository.Create(product);
        }

        private async Task<bool> IsExist(string sku)
        {
            return await _productRepository.GetProductBySku(sku) != null;
        }
    }
}