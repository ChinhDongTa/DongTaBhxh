using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DongTaBhxh.Domain.Data;
using DongTaBhxh.Domain.Models.EmployeeDb;
using DongTaBhxh.DataTranfer.Mapping;
using DongTa.ResponseResult;
using DongTaBhxh.DataTranfer.Dtos;

namespace DongTaBhxh.ApiGateway.Controllers.HumanApi;

[Route("[controller]")]
[ApiController]
public class EmployeesController(DongTaBhxhDbContext context) : ControllerBase {

    // GET: api/Employees
    [HttpGet("All")]
    public async Task<IActionResult> All()
    {
        return Ok(ResultExtension.GetResult(
            await context.Employees.Where(x => x.IsQuitJob == false)
                                    .OrderBy(x => x.SortOrder).ThenBy(x => x.Birthdate)
                                   .Select(x => x.ToDto()).ToListAsync())
            );
    }

    [HttpGet("FindByName/{name}/{quitJob}")]
    public async Task<IActionResult> FindByName(string name, bool quitJob = false)
    {
        if (quitJob)
        {
            return Ok(ResultExtension.GetResult(
            await context.Employees.Where(x => x.IsQuitJob == true && (x.FirstName.Contains(name) || x.LastName.Contains(name)))
                                    .OrderBy(x => x.SortOrder).ThenBy(x => x.Birthdate)
                                   .Select(x => x.ToDto()).ToListAsync())
            );
        }
        return Ok(ResultExtension.GetResult(
            await context.Employees.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name))
                                    .OrderBy(x => x.SortOrder).ThenBy(x => x.Birthdate)
                                   .Select(x => x.ToDto()).ToListAsync())
            );
    }

    // GET: api/Employees/5
    [HttpGet("GetOne/{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var employee = await context.Employees.FindAsync(id);
        return Ok(ResultExtension.GetResult(employee?.ToDto()));
    }

    [HttpGet("GetByDeptId/{deptId}")]
    public async Task<IActionResult> GetByDeptId(int deptId)
    {
        return Ok(ResultExtension.GetResult(
            await context.Employees.Where(x => x.DeptId == deptId && x.IsQuitJob == false)
                                    .OrderBy(x => x.SortOrder).ThenBy(x => x.Birthdate)
                                    .Select(x => x.ToDto()).ToListAsync())
            );
    }

    // PUT: api/Employees/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update(int id, EmployeeDto dto)
    {
        if (id != dto.EmployeeId)
        {
            return Ok(Result<bool>.Failure(DongTa.ResponseMessage.Message.Notfound));
        }

        try
        {
            var employee = context.Employees.Find(id);
            if (employee is not null)
            {
                employee = dto.ToEntity(employee);
                context.Entry(employee).State = EntityState.Modified;
                return Ok(Result<bool>.BoolResult(await context.SaveChangesAsync() > 0));
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmployeeExists(id))
            {
                return Ok(Result<bool>.Failure(DongTa.ResponseMessage.Message.Notfound));
            }
            else
            {
                throw;
            }
        }
        return Ok(Result<bool>.Failure(DongTa.ResponseMessage.Message.Notfound));
    }

    // POST: api/Employees
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("Add")]
    public async Task<IActionResult> Add(Employee employee)
    {
        context.Employees.Add(employee);
        return Ok(Result<bool>.BoolResult(await context.SaveChangesAsync() > 0));
    }

    // DELETE: api/Employees/5
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await context.Employees.FindAsync(id);
        if (employee == null)
        {
            return Ok(Result<bool>.Failure(DongTa.ResponseMessage.Message.Notfound));
        }
        context.Employees.Remove(employee);
        return Ok(Result<bool>.BoolResult(await context.SaveChangesAsync() > 0));
    }

    private bool EmployeeExists(int id)
    {
        return context.Employees.Any(e => e.EmployeeId == id);
    }
}