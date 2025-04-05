using Microsoft.AspNetCore.Mvc;
using SalesManagmentSystem.Models.Store;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class OrderStatusController : ControllerBase
{
    private readonly ModelStore _context;

    public OrderStatusController(ModelStore context)
    {
        _context = context;
    }

    [HttpGet("{orderId}")]
    public IActionResult GetOrderStatus(int orderId)
    {
        var order = _context.Sales.FirstOrDefault(o => o.SaleID == orderId);
        if (order == null)
        {
            return NotFound();
        }

        return Ok(new
        {
            orderId = order.SaleID,
            status = "В пути", 
            deliveryDate = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd") 
        });
    }
}
