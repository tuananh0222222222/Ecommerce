using ecommerce.application.Interfaces;
using ecommerce.domain.Entities;
using ecommerce.domain.Interface;
using Serilog;
using System.Text.Json;

namespace ecommerce.application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICacheService _cache;

        public ProductService(IProductRepository productRepository, ICacheService cacheService)
        {
            _productRepository = productRepository;
            _cache = cacheService;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            // 1. Kiểm tra cache trước
            var cacheKey = $"product:{id}";
            var cachedProduct = await _cache.GetAsync<Product>(cacheKey);

            if (cachedProduct != null)
            {
                Log.Information("Product {ProductId} retrieved from cache", id);
                return cachedProduct;
            }
            // 2. Nếu không có trong cache, lấy từ repository (DB)
            var product = await _productRepository.GetByidAsync(id);

            if (product != null)
            {
                //  var serializedProduct = JsonSerializer.Serialize(product);
                await _cache.SetAsync(cacheKey, product, TimeSpan.FromMinutes(5));
                Log.Information("Product {ProductId} retrieved from database and cached", id);
            }

            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task AddAsync(Product product)
        {
            if (product.Price < 0) throw new Exception("Price must be positive");
            // Lưu sản phẩm vào cache sau khi thêm vào DB
            await _productRepository.AddAsync(product);

            var cacheKey = $"product:{product.Id}";
            var serializedProduct = JsonSerializer.Serialize(product);
            await _cache.SetAsync(cacheKey, serializedProduct, TimeSpan.FromMinutes(5));

            Log.Information("Product {ProductId} added and cached", product.Id);
        }

        public async Task UpdateAsync(Product product)
        {

            await _productRepository.UpdateAsync(product);
            var cacheKey = $"product:{product.Id}";
            await _cache.RemoveAsync(cacheKey);

            var serializedProduct = JsonSerializer.Serialize(product);
            await _cache.SetAsync(cacheKey, serializedProduct, TimeSpan.FromMinutes(5));

            Log.Information("Product {ProductId} updated and cache invalidated", product.Id);

        }

        public async Task DeleteAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);

            var caheKey = $"product: {id}";
            await _cache.RemoveAsync(caheKey);

            Log.Information("Product {ProductId} deleted and cache invalidated", id);
        }

        public async Task DecreaseStockAsync(Guid id, int quantity)
        {
            var product = await _productRepository.GetByidAsync(id);

            if (quantity <= 0)
                throw new Exception("Quantity must be positive");

            product.DecreaseStock(quantity);
            await _productRepository.UpdateAsync(product);
        }

        public async Task IncreaseStockAsync(Guid id, int quantity)
        {
            var product = await _productRepository.GetByidAsync(id);
            if (quantity <= 0)
                throw new Exception("Quantity must be positive");
            product.IncreaseStock(quantity);
            await _productRepository.UpdateAsync(product);
        }
    }
}