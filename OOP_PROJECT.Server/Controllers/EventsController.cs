//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using OOP_PROJECT.Server.Models;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using OOP_PROJECT.Server.Models;

//[Route("api/[controller]")]
//[ApiController]
//public class EventsController : ControllerBase
//{
//    private readonly ApplicationDbContext _context;

//    public EventsController(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    // GET: api/Products
//    [HttpGet]
//    public async Task<ActionResult<IEnumerable<EventsModel>>> GetProducts()
//    {
//        return await _context.Events.ToListAsync();
//    }

//    // GET: api/Products/5
//    [HttpGet("{id}")]
//    public async Task<ActionResult<EventsModel>> GetEvent(int id)
//    {
//        var events = await _context.Events.FindAsync(id);

//        if (events == null)
//        {
//            return NotFound();
//        }

//        return events;
//    }

//    // PUT: api/Products
//    [HttpPut]
//    public async Task<IActionResult> PutEvent([FromBody] EventsModel events)
//    {
//        if (events == null)
//        {
//            return BadRequest();
//        }

//        _context.Events.Add(events);

//        try
//        {
//            await _context.SaveChangesAsync();
//        }
//        catch (DbUpdateException)
//        {
//            if (ProductExists(events.Id))
//            {
//                return Conflict();
//            }
//            else
//            {
//                throw;
//            }
//        }

//        return CreatedAtAction("GetEvent", new { id = events.Id }, events);
//    }

//    // POST: api/Events
//    [HttpPost]
//    public async Task<ActionResult<EventsModel>> PostEvent(EventsModel events)
//    {
//        // Добавляем новое событие в базу данных
//        _context.Events.Add(events);
//        await _context.SaveChangesAsync();

//        return CreatedAtAction("GetEvent", new { id = events.Id }, events);
//    }

//    // DELETE: api/Products/5
//    [HttpDelete("{id}")]
//    public async Task<IActionResult> DeleteProduct(int id)
//    {
//        var events = await _context.Events.FindAsync(id);
//        if (events == null)
//        {
//            return NotFound();
//        }

//        _context.Events.Remove(events);
//        await _context.SaveChangesAsync();

//        return NoContent();
//    }

//    private bool ProductExists(int id)
//    {
//        return _context.Events.Any(e => e.Id == id);
//    }
//}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOP_PROJECT.Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _hostEnvironment;

    public EventsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
    {
        _context = context;
        _hostEnvironment = hostEnvironment;
    }

    // GET: api/Events
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventsModel>>> GetEvents()
    {
        return await _context.Events.ToListAsync();
    }

    // GET: api/Events/5
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

    // PUT: api/Events
    [HttpPut]
    public async Task<IActionResult> PutEvent([FromForm] EventsModel events, IFormFile image)
    {
        if (events == null)
        {
            return BadRequest();
        }

        if (image != null)
        {
            events.ImagePath = await SaveImage(image);
        }

        _context.Events.Update(events);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (EventExists(events.Id))
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

    // POST: api/Events
    [HttpPost]
    public async Task<ActionResult<EventsModel>> PostEvent([FromForm] EventsModel events, IFormFile image)
    {
        if (image != null)
        {
            events.ImagePath = await SaveImage(image);
        }

        _context.Events.Add(events);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetEvent", new { id = events.Id }, events);
    }

    // DELETE: api/Events/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var events = await _context.Events.FindAsync(id);
        if (events == null)
        {
            return NotFound();
        }

        // Удаление изображения с сервера
        if (!string.IsNullOrEmpty(events.ImagePath))
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", events.ImagePath);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }

        _context.Events.Remove(events);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EventExists(int id)
    {
        return _context.Events.Any(e => e.Id == id);
    }

    private async Task<string> SaveImage(IFormFile image)
    {
        // Создаем путь для хранения изображений
        var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images");
        if (!Directory.Exists(imagePath))
        {
            Directory.CreateDirectory(imagePath);
        }

        // Генерируем уникальное имя файла
        var fileName = Path.GetRandomFileName() + Path.GetExtension(image.FileName);
        var fullPath = Path.Combine(imagePath, fileName);

        // Сохраняем файл
        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        // Возвращаем имя файла для хранения в базе данных
        return fileName;
    }
}

