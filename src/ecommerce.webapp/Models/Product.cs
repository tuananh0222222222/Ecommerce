namespace ecommerce.webapp.Models
{
    public class Product
    {
        public Guid Id { get; set; }                 // Mã sản phẩm
        public string Name { get; set; }             // Tên sản phẩm
        public string Description { get; set; }      // Mô tả
        public decimal Price { get; set; }           // Giá
        public int StockQuantity { get; set; }       // Tồn kho
        public string Category { get; set; }         // Danh mục
        public DateTime CreatedAt { get; set; }      // Ngày tạo
        public DateTime UpdatedAt { get; set; }      // Ngày cập nhật
    }
}
