using Microsoft.AspNetCore.Mvc;
using SalesManagmentSystem.Models.Store;
using System.Collections.Generic;
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
    }
}
