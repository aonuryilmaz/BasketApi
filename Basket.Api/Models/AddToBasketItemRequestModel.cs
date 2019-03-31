namespace Basket.Api.Models
{
    public class AddToBasketItemRequestModel
    {
        public string UserEmail { get; set; }
        public string Sku { get; set; }
        public int Quantity { get; set; }
    }
}   