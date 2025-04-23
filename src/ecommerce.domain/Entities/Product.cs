namespace ecommerce.domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int StockQuantity { get; set; }
        // Constructor
        public Product(int id, string name, decimal price, string description, string category, int stockQuantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
            Category = category;
            StockQuantity = stockQuantity;
        }
        // Method to update stock quantity
        public void UpdateStock(int quantity)
        {
            StockQuantity += quantity;
        }
    }
}
