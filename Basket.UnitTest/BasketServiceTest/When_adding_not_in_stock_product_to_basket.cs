using System;
using System.Threading.Tasks;
using Basket.Api.Services;
using Basket.Core.Domain.Models;
using Basket.Core.Domain.Repositories;
using Basket.UnitTest.Builders;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Basket.UnitTest.BasketServiceTest
{
    public class When_adding_not_in_stock_product_to_basket
    {
        private readonly Mock<IBasketRepository> _basketRepository = new Mock<IBasketRepository>();
        private readonly Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
        private BasketService _basketService;
        private readonly Guid userId = new Guid("93648BFC-1A27-40F0-A707-F567F337EFC7");
        private Product _product;
        private string sku = "product1";
        private string name = "product1";
        private string brand = "product1";
        private int stock = 3;
        private int unitPrice = 10;
        
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            CreateProduct();
            _productRepository.Setup(x => x.GetProductBySku(sku)).Returns(Task.FromResult(_product));
            _basketService=new BasketService(_productRepository.Object, _basketRepository.Object);

        }

        [Test]
        public void basket_service_should_throw_exception()
        {
            var message= _basketService.AddToBasket(userId, sku, 4).Exception.Message;
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