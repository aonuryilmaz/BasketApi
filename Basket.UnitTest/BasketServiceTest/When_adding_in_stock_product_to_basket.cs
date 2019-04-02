using System;
using System.Linq;
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
    public class When_adding_baskektitem_to_basket
    {
        private readonly Mock<IBasketRepository> _basketRepository = new Mock<IBasketRepository>();
        private readonly Mock<IProductService> _productService = new Mock<IProductService>();
        private BasketService _basketService;
        private readonly Guid userId = new Guid("93648BFC-1A27-40F0-A707-F567F337EFC7");
        private Product _product;
        private CustomerBasket _basket;
        private string sku = "product1";
        private string name = "product1";
        private string brand = "product1";
        private int stock = 5;
        private int unitPrice = 10;
        
        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            CreateProduct();
            _productService.Setup(x => x.GetProductBySku(sku)).Returns(Task.FromResult(_product));
            _basketService=new BasketService(_productService.Object, _basketRepository.Object);

            _basket=await _basketService.AddToBasket(userId, sku, 4);
        }

        [Test]
        public void product_should_added_to_basket()
        {
            _basketRepository.Verify(x=>x.Update(It.IsAny<CustomerBasket>()),Times.Once);
        }
        
        [Test]
        public void basket_should_be_created()
        {
            _basketRepository.Verify(x=>x.Create(It.IsAny<CustomerBasket>()),Times.Once);
        }
        
        [Test]
        public void product_stock_should_be_decreased()
        {
            _productService.Verify(x=>x.DecreaseStock(It.IsAny<Product>(),It.IsAny<int>()),Times.Once);
        }
        
        [Test]
        public void basket_item_should_be_correctly_added()
        {
            _basket.ItemList.Any(x => x.Sku == sku).Should().BeTrue();
        }
        
        [Test]
        public void basket_should_be_created_with_user_id()
        {
            _basket.UserId.Should().Be(userId);
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