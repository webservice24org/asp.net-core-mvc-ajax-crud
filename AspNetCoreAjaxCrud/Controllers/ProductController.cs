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
        public JsonResult EditProducct(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            return Json(product);
        }

        [HttpPost]
        public JsonResult UpdateProducct([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return Json("Product Updated Successfully");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { message = "Model Validation Failed", errors = errors });
            }
        }



    }
}
