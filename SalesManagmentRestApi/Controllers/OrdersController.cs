using Microsoft.AspNetCore.Mvc;
using SalesManagmentSystem.Models.Store;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly ModelStore _context;

    public OrdersController(ModelStore context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
    {
        var sale = new Sales
        {
            CustomerID = request.CustomerId,
            SaleDate = DateTime.Now,
            TotalAmount = 0 
        };

        _context.Sales.Add(sale);
        _context.SaveChanges();

        foreach (var item in request.Items)
        {
            var saleItem = new SaleItems
            {
                SaleID = sale.SaleID,
                ProductID = item.ProductId,
                Quantity = item.Quantity,
                SalePrice = _context.Products.FirstOrDefault(p => p.ProductID == item.ProductId).RetailPrice
            };
            sale.TotalAmount += saleItem.SalePrice * item.Quantity;
            _context.SaleItems.Add(saleItem);
        }

        _context.SaveChanges();

        return Ok(new { orderId = sale.SaleID, status = "Создан" });
    }

    [HttpGet("order/{orderId}")]
    public IActionResult GetOrderStatus(int orderId)
    {
        var order = _context.Sales.FirstOrDefault(s => s.SaleID == orderId);

        if (order == null)
        {
            return NotFound("Заказ не найден.");
        }


        var saleItems = _context.SaleItems
            .Where(si => si.SaleID == order.SaleID)
            .ToList();

        var status = new
        {
            orderId = order.SaleID,
            status = "В процессе", 
            deliveryDate = DateTime.Now.AddDays(3), 
            items = saleItems.Select(si => new {
                productId = si.ProductID,
                quantity = si.Quantity
            }).ToList()
        };

        return Ok(status);
    }


}

public class CreateOrderRequest
{
    public int CustomerId { get; set; }
    public OrderItem[] Items { get; set; }
}

public class OrderItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
