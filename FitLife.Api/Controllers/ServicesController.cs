using FitLife.Api.Data;
using FitLife.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Api.Controllers
{
    // Handles API requests related to services
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        // Database context for accessing services table
        private readonly FitLifeContext _context;

        public ServicesController(FitLifeContext context)
        {
            _context = context;
        }

        // Get all services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceEntity>>> GetAll()
        {
            var services = await _context.Services
                .OrderBy(s => s.DayOfWeek)
                .ThenBy(s => s.StartTime)
                .ToListAsync();

            return Ok(services);
        }

        // Get a service by ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceEntity>> GetById(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return NotFound();

            return Ok(service);
        }

        // Create a new service
        [HttpPost]
        public async Task<ActionResult<ServiceEntity>> Create(ServiceEntity dto)
        {
            _context.Services.Add(dto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        // Update an existing service
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ServiceEntity dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var existing = await _context.Services.FindAsync(id);
            if (existing == null)
                return NotFound();

            // Update main fields
            existing.Name = dto.Name;
            existing.Type = dto.Type;
            existing.Description = dto.Description;
            existing.Trainer = dto.Trainer;
            existing.DayOfWeek = dto.DayOfWeek;
            existing.StartTime = dto.StartTime;
            existing.DurationMins = dto.DurationMins;
            existing.Price = dto.Price;
            existing.Level = dto.Level;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Delete a service by ID
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return NotFound();

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
