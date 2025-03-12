using DongTaBhxh.Domain.Data;
using DongTaBhxh.Domain.Models.EmployeeDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DongTaBhxh.ApiGateway.Controllers.HumanApi;

[Route("[controller]")]
[ApiController]
public class QuarterDepartmentRanksController(DongTaBhxhDbContext context) : ControllerBase {

    // GET: api/QuarterDepartmentRanks
    [HttpGet("All")]
    public async Task<ActionResult<IEnumerable<QuarterDepartmentRank>>> All()
    {
        return await context.QuarterDepartmentRanks.ToListAsync();
    }

    // GET: api/QuarterDepartmentRanks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<QuarterDepartmentRank>> GetQuarterDepartmentRank(int id)
    {
        var quarterDepartmentRank = await context.QuarterDepartmentRanks.FindAsync(id);

        if (quarterDepartmentRank == null)
        {
            return NotFound();
        }

        return quarterDepartmentRank;
    }

    // PUT: api/QuarterDepartmentRanks/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutQuarterDepartmentRank(int id, QuarterDepartmentRank quarterDepartmentRank)
    {
        if (id != quarterDepartmentRank.QuarterDepartmentRankId)
        {
            return BadRequest();
        }

        context.Entry(quarterDepartmentRank).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!QuarterDepartmentRankExists(id))
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

    // POST: api/QuarterDepartmentRanks
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<QuarterDepartmentRank>> PostQuarterDepartmentRank(QuarterDepartmentRank quarterDepartmentRank)
    {
        context.QuarterDepartmentRanks.Add(quarterDepartmentRank);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (QuarterDepartmentRankExists(quarterDepartmentRank.QuarterDepartmentRankId))
            {
                return Conflict();
            }
            else
            {
                throw;
            }
        }

        return CreatedAtAction("GetQuarterDepartmentRank", new { id = quarterDepartmentRank.QuarterDepartmentRankId }, quarterDepartmentRank);
    }

    // DELETE: api/QuarterDepartmentRanks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuarterDepartmentRank(int id)
    {
        var quarterDepartmentRank = await context.QuarterDepartmentRanks.FindAsync(id);
        if (quarterDepartmentRank == null)
        {
            return NotFound();
        }

        context.QuarterDepartmentRanks.Remove(quarterDepartmentRank);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool QuarterDepartmentRankExists(int id)
    {
        return context.QuarterDepartmentRanks.Any(e => e.QuarterDepartmentRankId == id);
    }
}