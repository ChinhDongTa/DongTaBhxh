using DongTaBhxh.Domain.Data;
using DongTaBhxh.Domain.Models.EmployeeDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DongTaBhxh.ApiGateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuarterEmployeeRanksController(DongTaBhxhDbContext context) : ControllerBase {

    // GET: api/QuarterEmployeeRanks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuarterEmployeeRank>>> GetQuarterEmployeeRanks()
    {
        return await context.QuarterEmployeeRanks.ToListAsync();
    }

    // GET: api/QuarterEmployeeRanks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<QuarterEmployeeRank>> GetQuarterEmployeeRank(int id)
    {
        var quarterEmployeeRank = await context.QuarterEmployeeRanks.FindAsync(id);

        if (quarterEmployeeRank == null)
        {
            return NotFound();
        }

        return quarterEmployeeRank;
    }

    // PUT: api/QuarterEmployeeRanks/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutQuarterEmployeeRank(int id, QuarterEmployeeRank quarterEmployeeRank)
    {
        if (id != quarterEmployeeRank.QuarterEmployeeRankId)
        {
            return BadRequest();
        }

        context.Entry(quarterEmployeeRank).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!QuarterEmployeeRankExists(id))
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

    // POST: api/QuarterEmployeeRanks
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<QuarterEmployeeRank>> PostQuarterEmployeeRank(QuarterEmployeeRank quarterEmployeeRank)
    {
        context.QuarterEmployeeRanks.Add(quarterEmployeeRank);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (QuarterEmployeeRankExists(quarterEmployeeRank.QuarterEmployeeRankId))
            {
                return Conflict();
            }
            else
            {
                throw;
            }
        }

        return CreatedAtAction("GetQuarterEmployeeRank", new { id = quarterEmployeeRank.QuarterEmployeeRankId }, quarterEmployeeRank);
    }

    // DELETE: api/QuarterEmployeeRanks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuarterEmployeeRank(int id)
    {
        var quarterEmployeeRank = await context.QuarterEmployeeRanks.FindAsync(id);
        if (quarterEmployeeRank == null)
        {
            return NotFound();
        }

        context.QuarterEmployeeRanks.Remove(quarterEmployeeRank);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool QuarterEmployeeRankExists(int id)
    {
        return context.QuarterEmployeeRanks.Any(e => e.QuarterEmployeeRankId == id);
    }
}