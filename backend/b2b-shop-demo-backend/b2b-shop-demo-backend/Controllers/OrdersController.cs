using b2b_shop_demo_backend.Data;
using b2b_shop_demo_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace b2b_shop_demo_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _db;
        public OrdersController(AppDbContext db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderRequest request)
        {
            var companyId = int.Parse(User.FindFirstValue("companyId")!);

            var products = await _db.Products
                .Where(p => request.Items.Select(i => i.ProductId).Contains(p.Id))
                .ToListAsync();

            if (!products.Any())
                return BadRequest(new { message = "No valid products" });

            var order = new Order
            {
                CompanyId = companyId,
                Items = request.Items.Select(i =>
                {
                    var product = products.First(p => p.Id == i.ProductId);
                    return new OrderItem
                    {
                        ProductId = product.Id,
                        Quantity = i.Quantity,
                        UnitPrice = product.Price
                    };
                }).ToList()
            };

            var thisMonthStart = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            var monthlyTotal = await _db.Orders
                .Where(o => o.CompanyId == companyId && o.CreatedAt >= thisMonthStart)
                .Select(o => o.Items.Sum(i => i.Quantity * i.UnitPrice))
                .SumAsync();

            var limit = await _db.PurchaseLimits.FirstOrDefaultAsync(l => l.CompanyId == companyId);

            var newTotal = monthlyTotal + order.Items.Sum(i => i.Quantity * i.UnitPrice);

            if (limit != null && newTotal > limit.MonthlyLimitAmount)
            {
                return BadRequest(new
                {
                    message = "Monthly purchase limit exceeded."
                });
            }

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                orderId = order.Id,
                total = order.TotalAmount,
                status = order.Status
            });
        }
    }
}
