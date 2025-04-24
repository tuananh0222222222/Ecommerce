using ecommerce.webapp.Models;
using Newtonsoft.Json;

namespace ecommerce.webapp.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }

        public async Task<List<Product>> GetAllProduct()
        {
            try
            {
                var respone = await _httpClient.GetStringAsync("api/product");
                return JsonConvert.DeserializeObject<List<Product>>(respone);


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
