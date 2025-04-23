using ecommerce.domain.Entities;

namespace ecommerce.domain.Interface
{
    public interface IProductRepository
    {
        Task<Product?> GetByidAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);

    }
}
