using Microsoft.AspNetCore.Mvc;
using SalesManagmentSystem.Models.Store;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class InventoryController : ControllerBase
{
    private readonly ModelStore _context;

    public InventoryController(ModelStore context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetInventory(int? storeId, int? productId)
    {

        var inventory = _context.Warehouses
            .Where(w => !storeId.HasValue || w.StoreID == storeId) 
            .Select(w => new
            {
                store = w.Stores.Name, 
                products = _context.Inventory
                    .Where(i => i.WarehouseID == w.WarehouseID && (!productId.HasValue || i.ProductID == productId)) 
                    .Select(i => new
                    {
                        productName = i.Products.Name, 
                        quantity = i.ActualQuantity 
                    }).ToList()
            }).ToList();

   
        return Ok(inventory);
    }
}
