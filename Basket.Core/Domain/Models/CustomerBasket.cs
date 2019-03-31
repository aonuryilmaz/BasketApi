using System;
using System.Collections.Generic;

namespace Basket.Core.Domain.Models
{
    public class CustomerBasket : Entity
    {
        public Guid UserId { get; set; }
        public List<BasketItem> ItemList { get; set; }

        protected CustomerBasket()
        {
            
        }

        public CustomerBasket(Guid userId)
        {
            UserId = userId;
            ItemList= new List<BasketItem>();
        }
    }
}