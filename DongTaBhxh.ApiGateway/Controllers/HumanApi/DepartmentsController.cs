using DongTa.BaseDapper;
using DongTa.ResponseResult;
using DongTaBhxh.DataTranfer.Dtos;
using DongTaBhxh.DataTranfer.Mapping;
using DongTaBhxh.DefaultValue;
using DongTaBhxh.Domain.Data;
using DongTaBhxh.Domain.Models.EmployeeDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DongTaBhxh.ApiGateway.Controllers.HumanApi;

[Route("[controller]")]
[ApiController]
public class DepartmentsController(DongTaBhxhDbContext context, IGenericDapper dapper) : ControllerBase {

    // GET: Departments/All
    [HttpGet("All")]
    public async Task<IActionResult> All()
    {
        return Ok(ResultExtension.GetResult(
            await context.Departments.OrderBy(x => x.SortOrder)
                         .Select(x => x.ToDto()).ToListAsync())
            );
    }

    // GET: Departments/GetOne/5
    [HttpGet("GetOne/{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var department = await context.Departments.FindAsync(id);
        return Ok(ResultExtension.GetResult(department?.ToDto()));
    }

    // GET: Departments/GetOne/5
    [HttpGet("GetOneByEmployeeId/{employeeId}")]
    public async Task<IActionResult> GetOneByEmployeeId(int employeeId)
    {
        return Ok(ResultExtension.GetResult(await dapper.GetOneAsync<DepartmentDto>(SqlQueryString.SelectDepartmentDtoByEmployeeId(employeeId))));
    }

    // PUT: api/Departments/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update(int id, DepartmentDto department)
    {
        if (id != department.DepartmentId)
        {
            return Ok(Result<bool>.Failure("Bad request"));
        }

        try
        {
            var dept = context.Departments.Find(id);
            if (dept is not null)
            {
                context.Entry(dept).State = EntityState.Modified;
                dept = department.ToEntity(dept);
                return Ok(ResultExtension.GetResult(await context.SaveChangesAsync() > 0, "Cập nhập phòng thành công"));
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DepartmentExists(id))
            {
                return Ok(Result<bool>.Failure("Not found"));
            }
            else
            {
                throw;
            }
        }
        return Ok(Result<bool>.Failure("Not Content"));
    }

    // POST: api/Departments
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("Add")]
    public async Task<ActionResult<Department>> Add(DepartmentDto department)
    {
        context.Departments.Add(department.ToEntity());
        return Ok(Result<bool>.BoolResult(await context.SaveChangesAsync() > 0));
    }

    // DELETE: Departments/5
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var department = await context.Departments.FindAsync(id);
        if (department == null)
        {
            return Ok(Result<bool>.Failure("Not Fountd"));
        }
        context.Departments.Remove(department);
        return Ok(Result<bool>.BoolResult(await context.SaveChangesAsync() > 0));
    }

    private bool DepartmentExists(int id)
    {
        return context.Departments.Any(e => e.DepartmentId == id);
    }
}