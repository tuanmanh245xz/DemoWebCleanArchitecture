namespace Demo.Application.Requests.Products
{
    public class AddProductRequest
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
    }
}
