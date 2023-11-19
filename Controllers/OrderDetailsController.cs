using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShopNet6.Entities;

namespace MyShopNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly MyShopContext _context;

        public OrderDetailsController(MyShopContext ctx)
        {
            _context = ctx;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.OrderDetails.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
            if (_context.OrderDetails == null)
            {
                return NotFound();
            }
            var orderdetail = await _context.OrderDetails.FindAsync(id);

            if (orderdetail == null)
            {
                return NotFound();
            }

            return orderdetail;
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            if (_context.OrderDetails == null)
            {
                return NotFound();
            }
            var orderdetail = await _context.OrderDetails.FindAsync(id);
            if (orderdetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderdetail);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Delete successful" });
        }

        private bool OrderDetailExists(int id)
        {
            return (_context.OrderDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}