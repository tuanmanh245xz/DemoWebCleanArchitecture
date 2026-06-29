namespace Demo.Web.Models.Products
{
    //Dùng để hiển thị từng dòng trong danh sách sản phẩm.
    public class ProductListItemViewModel
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
    }
}
