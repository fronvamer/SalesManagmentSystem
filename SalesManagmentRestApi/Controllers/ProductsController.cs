using Microsoft.AspNetCore.Mvc;
using SalesManagmentSystem.Models.Store;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SalesManagmentRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ModelStore _context;

        public ProductsController(ModelStore context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.Products.Select(p => new
            {
                category = p.Category,
                name = p.Name,
                price = p.RetailPrice
            }).ToList();

            return Ok(products);
        }
        [HttpPost("upload")]
        public IActionResult UploadProducts([FromBody] List<Products> products)
        {
            if (products == null || !products.Any())
            {
                return BadRequest("Список товаров не может быть пустым.");
            }

            foreach (var productDto in products)
            {
                var existingProduct = _context.Products.FirstOrDefault(p => p.Code == productDto.Code);
                if (existingProduct != null)
                {
                    existingProduct.RetailPrice = productDto.RetailPrice;
                    // Проверка на необходимость обновления
                    if (existingProduct.RetailPrice != productDto.RetailPrice)
                    {
                        _context.Entry(existingProduct).State = EntityState.Modified;
                    }
                }
                else
                {
                    var newProduct = new Products
                    {
                        Code = productDto.Code,
                        Name = productDto.Name,
                        Brand = productDto.Brand,
                        Category = productDto.Category,
                        RetailPrice = productDto.RetailPrice
                    };
                    _context.Products.Add(newProduct);
                }
            }

            _context.SaveChanges();
            return Ok("Товары загружены успешно!");
        }
        [HttpPost("validate")]
        public IActionResult ValidateProducts([FromBody] List<Products> products)
        {
            var errors = new List<string>();

            foreach (var productDto in products)
            {
                if (string.IsNullOrEmpty(productDto.RetailPrice.ToString()))  
                {
                    errors.Add($"Отсутствует цена для товара: {productDto.Name}");
                }
            }

            if (errors.Any())
            {
                return BadRequest(new { errors });
            }

            return Ok("Данные корректны!");
        }


    }
}
