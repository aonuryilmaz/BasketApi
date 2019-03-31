namespace Basket.Api.Models
{
    public class CreateProductRequestModel
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public int UnitPrice { get; set; }
        public int InStock { get; set; }
    }
}