using System.ComponentModel.DataAnnotations;

namespace ecommerce.API.Models
{
    public class AdjustStockRequest
    {
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
