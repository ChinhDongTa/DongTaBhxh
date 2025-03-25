using DongTa.ResponseResult;
using DongTaBhxh.DataTranfer.Dtos;
using DongTaBhxh.DataTranfer.Mapping;
using DongTaBhxh.Domain.Data;
using DongTaBhxh.DefaultValue;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DongTa.BaseDapper;

namespace DongTaBhxh.ApiGateway.Controllers.HumanApi;

[Route("[controller]")]
[ApiController]
public class EmployeesController(DongTaBhxhDbContext context, IGenericDapper dapper) : ControllerBase {

    // GET: api/Employees
    [HttpGet("All")]
    public async Task<IActionResult> All()
    {
        return Ok(ResultExtension.GetResult(
            await context.Employees.Where(x => x.IsQuitJob == false)
            .Include(x => x.SalaryCoefficient).Include(x => x.Dept).Include(x => x.Post)
                                    .OrderBy(x => x.SortOrder).ThenBy(x => x.Birthdate)
                                   .Select(x => x.ToDto()).ToListAsync())
            );
    }

    [HttpGet("FindByName/{name}/{quitJob}")]
    public async Task<IActionResult> FindByName(string name, bool quitJob = false)
    {
        return quitJob
            ? Ok(ResultExtension.GetResult(await context.Employees
                    .Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name))
                    .Include(x => x.SalaryCoefficient)
                    .Include(x => x.Dept)
                    .Include(x => x.Post)
                    .OrderBy(x => x.SortOrder).ThenBy(x => x.Birthdate)
                    .Select(x => x.ToDto()).ToListAsync())
            )
            : Ok(ResultExtension.GetResult(await context.Employees
                    .Where(x => x.IsQuitJob == false && (x.FirstName.Contains(name) || x.LastName.Contains(name)))
                    .Include(x => x.SalaryCoefficient)
                    .Include(x => x.Dept)
                    .Include(x => x.Post)
                    .OrderBy(x => x.SortOrder).ThenBy(x => x.Birthdate)
                    .Select(x => x.ToDto()).ToListAsync())
            );
    }

    // GET: api/Employees/5
    [HttpGet("GetOne/{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var employee = await context.Employees.Include(x => x.SalaryCoefficient)
            .Include(x => x.Dept).Include(x => x.Post)
                                .FirstOrDefaultAsync(x => x.EmployeeId == id);

        return Ok(ResultExtension.GetResult(employee?.ToDto()));
    }

    [HttpGet("GetEmployeeDto")]
    public async Task<IActionResult> GetEmployeeDto()
    {
        var dto = await dapper.GetOneAsync<EmployeeDto>(SqlQueryString.SelectEmployeeDto);
        //FormattableString query = ;

        return Ok(ResultExtension.GetResult(dto));
    }

    [HttpGet("GetByUserId/{userId}")]
    public async Task<IActionResult> GetoByUserId(string userId) => Ok(ResultExtension.GetResult(
            await dapper.GetOneAsync<EmployeeDto>(SqlQueryString.SelectEmployeeDtoByUserId(userId)))
            );

    /// <summary>
    /// Lấy người đứng đầu phòng ban theo userId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("GetDeptHeadByUserId/{userId}")]
    public async Task<IActionResult> GetDeptHeadByUserId(string userId)
    {
        return Ok(ResultExtension.GetResult(
            await dapper.GetOneAsync<EmployeeDto>(SqlQueryString.GetDeptHeadByUserId(userId))));
    }

    [HttpGet("GetByDeptId/{deptId}")]
    public async Task<IActionResult> GetByDeptId(int deptId)
    {
        return Ok(ResultExtension.GetResult(
            await context.Employees.Where(x => x.DeptId == deptId && x.IsQuitJob == false)
                                    .Include(x => x.SalaryCoefficient).Include(x => x.Dept).Include(x => x.Post)
                                    .OrderBy(x => x.SortOrder).ThenBy(x => x.Birthdate)
                                    .Select(x => x.ToDto()).ToListAsync())
            );
    }

    [HttpGet("GetEmployeeDtoForListBox")]
    public async Task<IActionResult> GetEmployeeDtoForListBox()
    {
        var result = await context.Employees.Where(s => s.IsQuitJob == false)
            .Include(s => s.Dept)
            .AsSplitQuery()
            .OrderBy(s => s.DeptId).ThenBy(s => s.SortOrder).ThenBy(s => s.LastName)
            .Select(s => new EmployeeDtoForListBox(s.EmployeeId, $"{s.FirstName} {s.LastName}-{s.Dept!.ShortName}"))
            .ToListAsync();
        //FormattableString query = ;

        return Ok(ResultExtension.GetResult(result));
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
    public async Task<IActionResult> Add(EmployeeDto dto)
    {
        context.Employees.Add(dto.ToEntity());
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