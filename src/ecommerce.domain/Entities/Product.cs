namespace ecommerce.domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }                 // Mã sản phẩm
        public string Name { get; private set; }             // Tên sản phẩm
        public string Description { get; private set; }      // Mô tả
        public decimal Price { get; private set; }           // Giá
        public int StockQuantity { get; private set; }       // Tồn kho
        public string Category { get; private set; }         // Danh mục
        public DateTime CreatedAt { get; private set; }      // Ngày tạo
        public DateTime UpdatedAt { get; private set; }      // Ngày cập nhật

        public Product(Guid id, string name, string description, decimal price, int stockQuantity, string category)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            Category = category;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        // Method để giảm tồn kho khi có đơn hàng
        public void DecreaseStock(int quantity)
        {

            if (quantity > StockQuantity)
            {
                throw new InvalidOperationException("Not enogh stock");
            }
            StockQuantity -= quantity;
            UpdatedAt = DateTime.UtcNow;
        }

        // Method để tăng tồn kho (trong trường hợp trả hàng, nhập hàng...)
        public void IncreaseStock(int quantity)
        {

           
            StockQuantity += quantity;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
