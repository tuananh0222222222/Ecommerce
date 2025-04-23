using ecommerce.domain.Entities;

namespace ecommerce.application.Interfaces
{
    public interface IProductService
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
        Task DecreaseStockAsync(Guid id, int quantity);
        Task IncreaseStockAsync(Guid id, int quantity);
    }
}
