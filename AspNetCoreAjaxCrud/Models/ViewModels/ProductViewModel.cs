using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace AspNetCoreAjaxCrud.Models.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Qty { get; set; }

        public IFormFile ProductImageFile { get; set; }

        public string ProductImage { get; set; }
    }
}
