using DongTa.BaseDapper;
using DongTa.QuarterInYear;
using DongTa.TypeExtension;
using DongTaBhxh.DataTranfer.Dtos;
using DongTaBhxh.DataTranfer.Mapping;
using DongTaBhxh.DefaultValue;
using DongTaBhxh.Domain.Data;
using DongTaBhxh.Domain.Models.EmployeeDb;
using Microsoft.EntityFrameworkCore;

namespace DongTaBhxh.ApiGateway.Helpers;

public static class DbContextExt {

    public static async Task<QuarterDepartmentRankDto> GetCurrentQuarterDepartmentRankDtoByUserId(this DongTaBhxhDbContext context, IGenericDapper dapper, string userId)
    {
        int deptId = await dapper.GetDeptIdByUserId(userId);
        return await context.GetCurrentQuarterDepartmentRankDtoByDeptId(deptId);
    }

    public static async Task<QuarterDepartmentRankDto> GetCurrentQuarterDepartmentRankDtoByDeptId(this DongTaBhxhDbContext context, int deptId)
    {
        var current = QuarterInYear.Before(DateTime.Now.AddDays(ReadOnlyValue.SubDay));
        var result = await context.QuarterDepartmentRanks
                    .Where(x => x.DeptId == deptId && x.Quarter == current.Quarter && x.Year == current.Year)
                    .Select(x => x.ToDto()).FirstAsync();
        if (result is not null)
        {
            return result;
        }
        else//khởi tạo xếp loại tất cả đơn vị cho quý hiện tại
        {
            foreach (var dept in context.Departments)
            {
                var quarterDepartmentRank = new QuarterDepartmentRank
                {
                    DeptId = dept.DepartmentId,
                    Quarter = current.Quarter,
                    Year = current.Year,
                    RewardId = 22// loại A
                };
                context.QuarterDepartmentRanks.Add(quarterDepartmentRank);
            }
            await context.SaveChangesAsync();

            return await context.QuarterDepartmentRanks
                    .Where(x => x.DeptId == deptId && x.Quarter == current.Quarter && x.Year == current.Year)
                    .Select(x => x.ToDto()).FirstAsync();
        }
    }

    public static async Task<IEnumerable<QuarterEmployeeRankDto>> GetQuarterEmployeeRankDtoByUserId(
        this DongTaBhxhDbContext context,
        IGenericDapper dapper, string userId,
        QuarterInYear quarterInYear)
    {
        int deptId = await dapper.GetDeptIdByUserId(userId);
        return await context.GetQuarterEmployeeRankDtoByDeptId(deptId, quarterInYear);
    }

    public static async Task<IEnumerable<QuarterEmployeeRankDto>> GetQuarterEmployeeRankDtoByDeptId(
        this DongTaBhxhDbContext context,
        int deptId,
        QuarterInYear quarter)
    {
        List<QuarterEmployeeRankDto> currentQuarterEmployeeRankDto = await context.GetQuarterEmployeeRankDtos(deptId, quarter);

        if (currentQuarterEmployeeRankDto.HasItems())
        {
            //lấy danh sách Id nhân viên của phòng ban
            var employeIdOfDept = await context.Employees.Where(x => x.DeptId == deptId && x.IsQuitJob == false)
                                   .Select(x => x.EmployeeId).ToListAsync();
            //Nếu 2 ds trùng nhau thì trả về current
            if (employeIdOfDept.MatchAll(currentQuarterEmployeeRankDto.Select(x => x.EmployeeId)))
            {
                return currentQuarterEmployeeRankDto;
            }
            //Nếu số lương ds xếp loại khác với số nhân viên của đơn vị thì xóa hết và tạo lại
            //Trường hợp này xảy ra khi điều chuyển, tuyển mới, nghỉ hưu,...
            else
            {
                await context.QuarterEmployeeRanks.Where
                    (x => x.Employee.DeptId == deptId
                    && x.Quarter == quarter.Quarter
                    && x.Year == quarter.Year)
                    .ForEachAsync(x => context.QuarterEmployeeRanks.Remove(x));
                await context.SaveChangesAsync();
                await context.InitQuarterEmployeeRank(deptId, quarter);
                return await context.GetQuarterEmployeeRankDtos(deptId, quarter);
            }
        }
        else
        {
            //Nếu không có ds xếp loại thì tạo mới
            await context.InitQuarterEmployeeRank(deptId, quarter);
            return await context.GetQuarterEmployeeRankDtos(deptId, quarter);
        }
    }

    private static async Task<List<QuarterEmployeeRankDto>> GetQuarterEmployeeRankDtos(
        this DongTaBhxhDbContext context,
        int deptId,
        QuarterInYear quarter)
    {
        //xếp loại cho quý của 1 đơn vị
        return await context.QuarterEmployeeRanks
            .Where(x => x.Employee.DeptId == deptId && x.Quarter == quarter.Quarter && x.Year == quarter.Year)
            .Select(x => x.ToDto()).ToListAsync();
    }

    private static async Task<bool> InitQuarterEmployeeRank(
        this DongTaBhxhDbContext context,
        int deptId,
        QuarterInYear quarter)
    {
        var currentQuarter = new QuarterInYear();
        if (quarter.Year == currentQuarter.Year && quarter.Quarter > currentQuarter.Quarter)
            return true;
        var employeeIds = await context.Employees.Where(x => x.DeptId == deptId && x.IsQuitJob == false)
            .Select(x => x.EmployeeId).ToListAsync();
        if (employeeIds.Count == 0)
            return false;
        int A2 = deptId == 1 ? 22 : 23;//deptId=1 Ban giám đốc luôn dc A1=22
        foreach (var employeeId in employeeIds)
        {
            var beforeQuarter = QuarterInYear.Before(quarter);
            var before = await context.QuarterEmployeeRanks.FirstOrDefaultAsync
                (x => x.EmployeeId == employeeId && x.Year == beforeQuarter.Year && x.Quarter == beforeQuarter.Quarter);

            var quarterEmployeeRank = new QuarterEmployeeRank
            {
                EmployeeId = employeeId,
                Quarter = quarter.Quarter,
                Year = quarter.Year,
                RewardId = A2,
                TotalWork = before?.TotalWork ?? 2,
                NumWorked = before?.NumWorked ?? 2,
            };
            context.QuarterEmployeeRanks.Add(quarterEmployeeRank);
        }
        return await context.SaveChangesAsync() > 0;
    }

    public static async Task<int> GetDeptIdByUserId(this IGenericDapper dapper, string userId)
    {
        return await dapper.GetOneAsync<int>(SqlQueryString.SelectDeptIdByUserId(userId));
    }
}