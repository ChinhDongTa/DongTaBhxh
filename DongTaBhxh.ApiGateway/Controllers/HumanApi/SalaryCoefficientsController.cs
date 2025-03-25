using DongTa.ResponseResult;
using DongTaBhxh.DataTranfer.Mapping;
using DongTaBhxh.Domain.Data;
using DongTaBhxh.Domain.Models.EmployeeDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DongTaBhxh.ApiGateway.Controllers.HumanApi;

[Route("[controller]")]
[ApiController]
public class SalaryCoefficientsController(DongTaBhxhDbContext context) : ControllerBase {

    // GET: api/SalaryCoefficients
    [HttpGet("All")]
    public async Task<IActionResult> All()
    {
        return Ok(ResultExtension.GetResult(await context.SalaryCoefficients.Select(x => x.ToDto()).ToListAsync()));
    }

    // GET: SalaryCoefficients/5
    [HttpGet("GetOne/{id}")]
    public async Task<ActionResult<SalaryCoefficient>> GetOne(int id)
    {
        return Ok(ResultExtension.GetResult(await context.SalaryCoefficients.FindAsync(id)));
    }

    // PUT: SalaryCoefficients/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update(int id, SalaryCoefficient salaryCoefficient)
    {
        if (id != salaryCoefficient.SalaryCoefficientId)
        {
            return BadRequest();
        }

        context.Entry(salaryCoefficient).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SalaryCoefficientExists(id))
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

    // POST: api/SalaryCoefficients
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("Add")]
    public async Task<ActionResult<SalaryCoefficient>> Add(SalaryCoefficient salaryCoefficient)
    {
        context.SalaryCoefficients.Add(salaryCoefficient);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetOne", new { id = salaryCoefficient.SalaryCoefficientId }, salaryCoefficient);
    }

    // DELETE: api/SalaryCoefficients/5
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var salaryCoefficient = await context.SalaryCoefficients.FindAsync(id);
        if (salaryCoefficient == null)
        {
            return NotFound();
        }

        context.SalaryCoefficients.Remove(salaryCoefficient);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool SalaryCoefficientExists(int id)
    {
        return context.SalaryCoefficients.Any(e => e.SalaryCoefficientId == id);
    }
}