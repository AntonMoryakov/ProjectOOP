using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOP_PROJECT.Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EventsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventsModel>>> GetProducts()
    {
        return await _context.Events.ToListAsync();
    }

    // GET: api/Products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EventsModel>> GetEvent(int id)
    {
        var events = await _context.Events.FindAsync(id);

        if (events == null)
        {
            return NotFound();
        }

        return events;
    }

    // PUT: api/Products
    [HttpPut]
    public async Task<IActionResult> PutEvent([FromBody] EventsModel events)
    {
        if (events == null)
        {
            return BadRequest();
        }

        _context.Events.Add(events);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (ProductExists(events.Id))
            {
                return Conflict();
            }
            else
            {
                throw;
            }
        }

        return CreatedAtAction("GetEvent", new { id = events.Id }, events);
    }

    // POST: api/Products
    [HttpPost]
    public async Task<ActionResult<EventsModel>> PostProduct(EventsModel events)
    {
        _context.Events.Add(events);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProduct", new { id = events.Id }, events);
    }

    // DELETE: api/Products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var events = await _context.Events.FindAsync(id);
        if (events == null)
        {
            return NotFound();
        }

        _context.Events.Remove(events);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductExists(int id)
    {
        return _context.Events.Any(e => e.Id == id);
    }
}

