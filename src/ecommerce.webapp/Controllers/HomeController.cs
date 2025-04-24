using ecommerce.webapp.Models;
using ecommerce.webapp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ecommerce.webapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService     _product;

        public HomeController(ILogger<HomeController> logger, IProductService product)
        {
            _logger = logger;
            _product = product;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var products =  await _product.GetAllProduct();
                return View(products);  // Tr? v? View v?i d? li?u s?n ph?m
            }
            catch (Exception ex)
            {
                // X? lý l?i n?u có
                ViewBag.ErrorMessage = "Có l?i x?y ra khi g?i API: " + ex.Message;
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
