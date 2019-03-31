using System;

namespace Basket.Core.Domain.Models
{
    public class BasketItem
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }

        public static BasketItem FromProduct(Product product,int quantity=1)
        {
            return new BasketItem
            {
                Sku = product.Sku,
                Name = product.Name,
                UnitPrice = product.UnitPrice,
                Quantity = quantity
            };
        }
    }
}