using DongTa.ResponseResult;
using DongTaBhxh.DataTranfer.Mapping;
using DongTaBhxh.Domain.Data;
using DongTaBhxh.Domain.Models.EmployeeDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DongTaBhxh.ApiGateway.Controllers.HumanApi;

[Route("[controller]")]
[ApiController]
public class PositionsController(DongTaBhxhDbContext context) : ControllerBase {

    // GET: api/Positions
    [HttpGet("All")]
    public async Task<IActionResult> All()
    {
        return Ok(ResultExtension.GetResult(await context.Positions.Select(x => x.ToDto()).ToListAsync()));
    }

    // GET: api/Positions/5
    [HttpGet("GetOne/{id}")]
    public async Task<ActionResult<Position>> GetOne(int id)
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
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update(int id, Position position)
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
    [HttpPost("Add")]
    public async Task<ActionResult<Position>> Add(Position position)
    {
        context.Positions.Add(position);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetPosition", new { id = position.PositionId }, position);
    }

    // DELETE: api/Positions/5
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
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