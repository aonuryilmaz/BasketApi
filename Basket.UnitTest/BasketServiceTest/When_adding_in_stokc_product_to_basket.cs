using System;
using System.Threading.Tasks;
using Basket.Api.Services;
using Basket.Core.Domain.Models;
using Basket.Core.Domain.Repositories;
using Basket.UnitTest.Builders;
using Moq;
using NUnit.Framework;

namespace Basket.UnitTest.BasketServiceTest
{
    public class When_adding_baskektitem_to_basket
    {
        private readonly Mock<IBasketRepository> _basketRepository = new Mock<IBasketRepository>();
        private readonly Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
        private BasketService _basketService;
        private readonly Guid userId = new Guid("93648BFC-1A27-40F0-A707-F567F337EFC7");
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
            _productRepository.Setup(x => x.GetProductBySku(sku)).Returns(Task.FromResult(_product));
            _basketService=new BasketService(_productRepository.Object, _basketRepository.Object);

            await _basketService.AddToBasket(userId, sku, 4);
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