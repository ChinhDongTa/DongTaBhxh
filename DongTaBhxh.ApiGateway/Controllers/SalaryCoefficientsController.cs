using DongTaBhxh.Domain.Data;
using DongTaBhxh.Domain.Models.EmployeeDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DongTaBhxh.ApiGateway.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class SalaryCoefficientsController(DongTaBhxhDbContext context) : ControllerBase {

        // GET: api/SalaryCoefficients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaryCoefficient>>> GetSalaryCoefficients()
        {
            return await context.SalaryCoefficients.ToListAsync();
        }

        // GET: api/SalaryCoefficients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalaryCoefficient>> GetSalaryCoefficient(int id)
        {
            var salaryCoefficient = await context.SalaryCoefficients.FindAsync(id);

            if (salaryCoefficient == null)
            {
                return NotFound();
            }

            return salaryCoefficient;
        }

        // PUT: api/SalaryCoefficients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalaryCoefficient(int id, SalaryCoefficient salaryCoefficient)
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
        [HttpPost]
        public async Task<ActionResult<SalaryCoefficient>> PostSalaryCoefficient(SalaryCoefficient salaryCoefficient)
        {
            context.SalaryCoefficients.Add(salaryCoefficient);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetSalaryCoefficient", new { id = salaryCoefficient.SalaryCoefficientId }, salaryCoefficient);
        }

        // DELETE: api/SalaryCoefficients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalaryCoefficient(int id)
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
}