using ecommerce.API.Models;
using ecommerce.application.Interfaces;
using ecommerce.domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Serilog;

namespace ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("redis-test")]
        public async Task<IActionResult> RedisTest([FromServices] IDistributedCache cache)
        {
            await cache.SetStringAsync("test-key", "Hello Redis");
            var value = await cache.GetStringAsync("test-key");
            return Ok(value); // => "Hello Redis"
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
        {
            var products = await _productService.GetAllAsync();
            Log.Information("Retrieved {ProductCount} products", products.Count()); // Log thông tin khi lấy sản phẩm
            return Ok(products);
        }

        [HttpGet("{id:guid}", Name = "GetProductById")] // Thêm route name
        public async Task<ActionResult<Product>> GetByIdAsync(Guid id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                Log.Information("Retrieved product with id: {ProductId}", id); // Log khi lấy sản phẩm thành công
                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            {
                Log.Warning(ex, "Product not found: {ProductId}", id); // Log cảnh báo khi không tìm thấy sản phẩm
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProductAsync([FromBody] CreateProductRequest request)
        {
            var product = new Product(
                id: Guid.NewGuid(),
                name: request.Name,
                description: request.Description,
                price: request.Price,
                stockQuantity: request.StockQuantity,
                category: request.Category
            );

            await _productService.AddAsync(product);
            Log.Information("Product created: {ProductId}", product.Id); // Log khi tạo sản phẩm mới

            // Sửa thành CreatedAtRoute và thêm route name
            return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProductAsync(Guid id, [FromBody] CreateProductRequest request) // Sử dụng DTO thay vì Entity
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Log.Warning("Invalid model state for update product: {ProductId}", id); // Log cảnh báo khi model không hợp lệ
                    return BadRequest(ModelState);
                }

                var product = new Product(
                    id: id,
                    name: request.Name,
                    description: request.Description,
                    price: request.Price,
                    stockQuantity: request.StockQuantity,
                    category: request.Category
                );

                await _productService.UpdateAsync(product);
                Log.Information("Product updated: {ProductId}", id); // Log khi cập nhật sản phẩm
                return NoContent(); // 204 No Content là chuẩn cho PUT thành công
            }
            catch (KeyNotFoundException ex)
            {
                Log.Warning(ex, "Product not found: {ProductId}", id); // Log khi không tìm thấy sản phẩm
                return NotFound();
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProductAsync(Guid id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                Log.Information("Product deleted: {ProductId}", id); // Log khi xóa sản phẩm
                return NoContent(); // 204 No Content là chuẩn cho DELETE thành công
            }
            catch (KeyNotFoundException ex)
            {
                Log.Warning(ex, "Product not found: {ProductId}", id); // Log khi không tìm thấy sản phẩm
                return NotFound();
            }
        }
    }
}
