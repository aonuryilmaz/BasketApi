namespace Basket.Core.Domain.Models
{
    public class Product:Entity
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public int UnitPrice { get; set; }
        public int InStock { get; set; }

        protected Product()
        {
            
        }

        public Product(string sku, string name, string brand, int unitPrice, int inStock)
        {
            Sku = sku;
            Name = name;
            Brand = brand;
            UnitPrice = unitPrice;
            InStock = inStock;
        }
    }
}