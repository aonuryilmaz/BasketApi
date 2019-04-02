using System.Threading.Tasks;
using Basket.Api.Services;
using Basket.Core.Domain.Models;
using Basket.Core.Domain.Repositories;
using Basket.UnitTest.Builders;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Basket.UnitTest.ProductServiceTest
{
    public class When_decreasing_non_in_stock_product
    {
        private Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
        private ProductService _productService;
        private Product _product;
        private string sku = "product1";
        private string name = "product1";
        private string brand = "product1";
        private int stock = 5;
        private int unitPrice = 10;
        
        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            CreateProduct();
            _productService=new ProductService(_productRepository.Object);
        }
        
        [Test]
        public void should_throw_exception_product_not_in_stock()
        {
            var message = _productService.DecreaseStock(_product, 6).Exception?.Message;
            message.Should().Contain("Product is not in stock");
        }
        private void CreateProduct()
        {
            _product = new ProductBuilder()
                .WithSku(sku)
                .WithName(name)
                .WithBrand(brand)
                .WithStock(stock)
                .WithUnitPrice(unitPrice)
                .Build();
        }
    }
}