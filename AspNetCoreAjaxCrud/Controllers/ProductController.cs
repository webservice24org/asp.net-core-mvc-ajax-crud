using AspNetCoreAjaxCrud.DAL;
using AspNetCoreAjaxCrud.Models.DBEntities;
using AspNetCoreAjaxCrud.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;

namespace AspNetCoreAjaxCrud.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AspAjaxCrudContext _context;

        public ProductController(IWebHostEnvironment webHostEnvironment, AspAjaxCrudContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Insert([FromForm] ProductViewModel model)
        {
            try
            {
                if (model.ProductImageFile != null && model.ProductImageFile.Length > 0)
                {
                    string fileName = Path.GetFileName(model.ProductImageFile.FileName);
                    string webRootPath = _webHostEnvironment.WebRootPath;
                    string imagePath = Path.Combine(webRootPath, "Images", fileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        model.ProductImageFile.CopyTo(stream);
                    }

                    model.ProductImage = "/Images/" + fileName;
                }


                var product = new Product
                {
                    ProductName = model.ProductName,
                    Price = model.Price,
                    Qty = model.Qty,
                    ProductImage = model.ProductImage
                };

                _context.Products.Add(product);
                _context.SaveChanges();

                return Json(product);
            }
            catch (Exception ex)
            {
                return Json(new { message = "Error while saving data!", error = ex.Message });
            }

        }
        public JsonResult GetProducts()
        {
            var products = _context.Products.ToList();
            return Json(products);
        }

        [HttpGet]
        public JsonResult EditProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return Json(new { message = "Product not found!" });
            }

            var productViewModel = new ProductViewModel
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                Qty = product.Qty,
                ProductImage = product.ProductImage
            };

            return Json(productViewModel);
<<<<<<< HEAD
        }


        [HttpPost]
        public JsonResult UpdateProducct([FromForm] ProductViewModel model)
        {

            var product = _context.Products.Find(model.Id);

            if (product == null)
            {
                return Json(new { message = "Product not found" });
            }

            if (model.ProductImageFile != null && model.ProductImageFile.Length > 0)
            {
                string fileName = Path.GetFileName(model.ProductImageFile.FileName);
                string webRootPath = _webHostEnvironment.WebRootPath;
                string imagePath = Path.Combine(webRootPath, "Images", fileName);

                if (!string.IsNullOrEmpty(product.ProductImage))
                {
                    string oldImagePath = Path.Combine(webRootPath, product.ProductImage.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    model.ProductImageFile.CopyTo(stream);
                }

                product.ProductImage = "/Images/" + fileName; 
            }

            product.ProductName = model.ProductName;
            product.Price = model.Price;
            product.Qty = model.Qty;

            _context.Products.Update(product);
            _context.SaveChanges();

            return Json("Product Updated Successfully");

=======
>>>>>>> 81bd07ccf1098332ee5dd3c51a35f8c423d9375a
        }


        [HttpPost]
<<<<<<< HEAD
        public JsonResult DeleteProduct(int id)
        {
            try
            {
                var product = _context.Products.Find(id);

                if (product == null)
                {
                    return Json(new { message = "Product not found" });
                }

                if (!string.IsNullOrEmpty(product.ProductImage))
                {
                    string webRootPath = _webHostEnvironment.WebRootPath;
                    string oldImagePath = Path.Combine(webRootPath, product.ProductImage.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                _context.Products.Remove(product);
                _context.SaveChanges();

                return Json(new { message = "Product deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { message = "Error while deleting data!", error = ex.Message });
=======
        public JsonResult UpdateProducct([FromForm] ProductViewModel model)
        {

            var product = _context.Products.Find(model.Id);

            if (product == null)
            {
                return Json(new { message = "Product not found" });
            }

            if (model.ProductImageFile != null && model.ProductImageFile.Length > 0)
            {
                string fileName = Path.GetFileName(model.ProductImageFile.FileName);
                string webRootPath = _webHostEnvironment.WebRootPath;
                string imagePath = Path.Combine(webRootPath, "Images", fileName);

                // Delete the old image if it exists
                if (!string.IsNullOrEmpty(product.ProductImage))
                {
                    string oldImagePath = Path.Combine(webRootPath, product.ProductImage.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    model.ProductImageFile.CopyTo(stream);
                }

                product.ProductImage = "/Images/" + fileName; // Update image path
>>>>>>> 81bd07ccf1098332ee5dd3c51a35f8c423d9375a
            }

            product.ProductName = model.ProductName;
            product.Price = model.Price;
            product.Qty = model.Qty;

            _context.Products.Update(product);
            _context.SaveChanges();

            return Json("Product Updated Successfully");

        }


    }
}
