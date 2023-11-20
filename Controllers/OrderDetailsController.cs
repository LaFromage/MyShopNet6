using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShopNet6.Dtos;
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

        // PUT: api/OrderDetails/Update/id
        [HttpPut("Update/{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateOrderDetail(int id, OrderDetailSimple updatedOrderDetail)
        {
            if (id != updatedOrderDetail.Id)
            {
                return BadRequest();
            }

            var existingOrderDetail = await _context.OrderDetails!.FindAsync(id);

            if (existingOrderDetail == null)
            {
                return NotFound();
            }

            existingOrderDetail.ProductId = updatedOrderDetail.ProductId;
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Update successfully!" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
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
            return Ok(new { Message = "Delete successfully!" });
        }

        private bool OrderDetailExists(int id)
        {
            return (_context.OrderDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}