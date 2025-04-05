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
