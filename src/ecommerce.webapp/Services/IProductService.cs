using ecommerce.webapp.Models;

namespace ecommerce.webapp.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProduct();
    }
}
