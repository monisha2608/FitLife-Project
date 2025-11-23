using FitLife.Api.Data;
using FitLife.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Api.Controllers
{
    // Handles all meal related API requests
    [ApiController]
    [Route("api/[controller]")]
    public class MealsController : ControllerBase
    {
        // Database context for accessing meals table
        private readonly FitLifeContext _context;

        public MealsController(FitLifeContext context)
        {
            _context = context;
        }

        // Get meals by service ID
        [HttpGet("by-service/{serviceId:int}")]
        public async Task<ActionResult<IEnumerable<MealEntity>>> GetByService(int serviceId)
        {
            if (serviceId <= 0)
                return Ok(Array.Empty<MealEntity>());

            var meals = await _context.Meals
                .Where(m => m.ServiceId == serviceId)
                .ToListAsync();

            return Ok(meals);
        }

        // Get all meals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealEntity>>> GetAll()
        {
            return Ok(await _context.Meals.ToListAsync());
        }

        // Get a meal by its ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MealEntity>> GetById(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            if (meal == null) return NotFound();

            return Ok(meal);
        }

        // Create a new meal
        [HttpPost]
        public async Task<ActionResult<MealEntity>> Create(MealEntity meal)
        {
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = meal.Id }, meal);
        }

        // Update an existing meal
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, MealEntity dto)
        {
            if (id != dto.Id) return BadRequest();

            var existing = await _context.Meals.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Name = dto.Name;
            existing.Type = dto.Type;
            existing.Calories = dto.Calories;
            existing.Description = dto.Description;
            existing.DayOfWeek = dto.DayOfWeek;
            existing.TimeOfDay = dto.TimeOfDay;
            existing.ServiceId = dto.ServiceId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Delete a meal by ID
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            if (meal == null) return NotFound();

            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
