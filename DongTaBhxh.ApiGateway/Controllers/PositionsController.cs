using DongTaBhxh.Domain.Data;
using DongTaBhxh.Domain.Models.EmployeeDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DongTaBhxh.ApiGateway.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController(DongTaBhxhDbContext context) : ControllerBase {

        // GET: api/Positions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions()
        {
            return await context.Positions.ToListAsync();
        }

        // GET: api/Positions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> GetPosition(int id)
        {
            var position = await context.Positions.FindAsync(id);

            if (position == null)
            {
                return NotFound();
            }

            return position;
        }

        // PUT: api/Positions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosition(int id, Position position)
        {
            if (id != position.PositionId)
            {
                return BadRequest();
            }

            context.Entry(position).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Positions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Position>> PostPosition(Position position)
        {
            context.Positions.Add(position);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetPosition", new { id = position.PositionId }, position);
        }

        // DELETE: api/Positions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(int id)
        {
            var position = await context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            context.Positions.Remove(position);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool PositionExists(int id)
        {
            return context.Positions.Any(e => e.PositionId == id);
        }
    }
}