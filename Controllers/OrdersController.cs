using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShopNet6.Entities;

namespace MyShopNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MyShopContext _context;

        public OrdersController(MyShopContext ctx)
        {
            _context = ctx;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Orders.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPut("Update/{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateOrder(int id, Order updatedOrder)
        {
            if (id != updatedOrder.Id)
            {
                return BadRequest();
            }

            var existingOrder = await _context.Orders!.FindAsync(id);

            if (existingOrder == null)
            {
                return NotFound();
            }

            existingOrder.OrderDate = updatedOrder.OrderDate;
            existingOrder.Receiver = updatedOrder.Receiver;
            existingOrder.Address = updatedOrder.Address;
            existingOrder.Description = updatedOrder.Description;
            existingOrder.Amount = updatedOrder.Amount;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Update successful" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<Order>> PostOrder(Order Order)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'OrderStoreContext.Orders'  is null.");
            }
            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = Order.Id }, Order);
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Delete successful" });
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}