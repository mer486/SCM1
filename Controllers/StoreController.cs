using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCM1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StoreController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Store
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetStores()
        {
            if (_context.Stores == null)
            {
                return NotFound("The Stores DbSet is not initialized.");
            }

            var stores = await _context.Stores.ToListAsync();
            if (!stores.Any())
            {
                return NotFound("No stores found.");
            }

            return Ok(stores);
        }

        // GET: api/Store/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStore(int id)
        {
            if (_context.Stores == null)
            {
                return NotFound("The Stores DbSet is not initialized.");
            }

            var store = await _context.Stores.FindAsync(id);

            if (store == null)
            {
                return NotFound($"Store with ID {id} not found.");
            }

            return Ok(store);
        }

        // POST: api/Store
        [HttpPost]
        public async Task<ActionResult<Store>> CreateStore([FromBody] Store store)
        {
            if (_context.Stores == null)
            {
                return Problem("The Stores DbSet is not initialized.");
            }

            _context.Stores.Add(store);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStore), new { id = store.BusinessEntityID }, store);
        }

        // PUT: api/Store/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(int id, [FromBody] Store store)
        {
            if (id != store.BusinessEntityID)
            {
                return BadRequest("The provided ID does not match the store's ID.");
            }

            if (_context.Stores == null)
            {
                return Problem("The Stores DbSet is not initialized.");
            }

            _context.Entry(store).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
                {
                    return NotFound($"Store with ID {id} not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Store/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            if (_context.Stores == null)
            {
                return Problem("The Stores DbSet is not initialized.");
            }

            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound($"Store with ID {id} not found.");
            }

            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoreExists(int id)
        {
            if (_context.Stores == null)
            {
                return false;
            }

            return _context.Stores.Any(e => e.BusinessEntityID == id);
        }
    }
}

