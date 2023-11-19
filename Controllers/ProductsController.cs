using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShopNet6.Entities;

namespace MyShopNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyShopContext _context;

        public ProductsController(MyShopContext ctx)
        {
            _context = ctx;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var Product = await _context.Products.FindAsync(id);

            if (Product == null)
            {
                return NotFound();
            }

            return Product;
        }

        [HttpPut("Update/{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateProduct(int id, Product updatedProduct)
        {
            if (id != updatedProduct.Id)
            {
                return BadRequest();
            }

            var existingProduct = await _context.Products!.FindAsync(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.UnitPrice = updatedProduct.UnitPrice;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.CategoryId = updatedProduct.CategoryId;
            existingProduct.Quantity = updatedProduct.Quantity;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Update successful" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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
        public async Task<ActionResult<Product>> PostProduct(Product Product)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ProductStoreContext.Products'  is null.");
            }
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = Product.Id }, Product);
        }

        // DELETE: api/Products/id
        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var Product = await _context.Products.FindAsync(id);
            if (Product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(Product);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Delete successful" });
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}