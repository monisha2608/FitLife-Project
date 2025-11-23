using FitLife.Api.Data;
using FitLife.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Api.Controllers
{
    // Handles API requests for workouts
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutsController : ControllerBase
    {
        // Database context to access workouts table
        private readonly FitLifeContext _context;

        public WorkoutsController(FitLifeContext context)
        {
            _context = context;
        }

        // Get all workouts or filter by serviceId
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutEntity>>> GetAll([FromQuery] int? serviceId)
        {
            IQueryable<WorkoutEntity> query = _context.Workouts;

            // Apply filter if serviceId is provided
            if (serviceId.HasValue)
            {
                query = query.Where(w => w.ServiceId == serviceId.Value);
            }

            var items = await query
                .OrderBy(w => w.Title)
                .ToListAsync();

            return Ok(items);
        }

        // Get a single workout by ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<WorkoutEntity>> GetById(int id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            if (workout == null)
                return NotFound();

            return Ok(workout);
        }

        // Create a new workout
        [HttpPost]
        public async Task<ActionResult<WorkoutEntity>> Create(WorkoutEntity dto)
        {
            _context.Workouts.Add(dto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        // Update an existing workout
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, WorkoutEntity dto)
        {
            if (id != dto.Id)
                return BadRequest("Id mismatch.");

            var existing = await _context.Workouts.FindAsync(id);
            if (existing == null)
                return NotFound();

            // Update main fields
            existing.ServiceId = dto.ServiceId;
            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.Category = dto.Category;
            existing.DurationMins = dto.DurationMins;
            existing.Calories = dto.Calories;
            existing.Level = dto.Level;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Delete a workout by ID
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Workouts.FindAsync(id);
            if (existing == null)
                return NotFound();

            _context.Workouts.Remove(existing);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
