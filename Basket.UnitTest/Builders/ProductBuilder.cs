using Basket.Core.Domain.Models;

namespace Basket.UnitTest.Builders
{
    public class ProductBuilder
    {
        private string sku = "";
        private string name = "";
        private string brand = "";
        private int unitPrice = 0;
        private int inStock = 0;

        public ProductBuilder WithSku(string value)
        {
            sku = value;
            return this;
        }

        public ProductBuilder WithName(string value)
        {
            name = value;
            return this;
        }

        public ProductBuilder WithBrand(string value)
        {
            brand = value;
            return this;
        }

        public ProductBuilder WithUnitPrice(int value)
        {
            unitPrice = value;
            return this;
        }

        public ProductBuilder WithStock(int value)
        {
            inStock = value;
            return this;
        }

        public Product Build()
        {
            return new Product(
                    sku,
                    name,
                    brand,
                    unitPrice,
                    inStock
                );
        }
    }
}