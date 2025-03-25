using DongTaBhxh.DataTranfer.Dtos;
using DongTaBhxh.Domain.Models.EmployeeDb;

namespace DongTaBhxh.DataTranfer.Mapping;

public static class DepartmentMapper {

    public static DepartmentDto ToDto(this Department department)
    {
        return new()
        {
            DepartmentId = department.DepartmentId,
            Name = department.Name,
            ShortName = department.ShortName,
            Email = department.Email,
            Phone = department.Phone,
            Score = department.Score,
            SortOrder = department.SortOrder,
            IsActivity = department.IsActivity,
            LevelId = department.LevelId,
            LevelName = department.Level?.Name
        };
    }

    public static Department ToEntity(this DepartmentDto dto, Department? department = null)
    {
        if (department is null)
        {
            return new()
            {
                DepartmentId = dto.DepartmentId,
                Name = dto.Name,
                ShortName = dto.ShortName,
                Email = dto.Email,
                Phone = dto.Phone,
                Score = dto.Score,
                SortOrder = dto.SortOrder,
                IsActivity = dto.IsActivity,
                LevelId = dto.LevelId ?? 2 // mặc định là văn phòng
            };
        }
        department.Name = dto.Name;
        department.ShortName = dto.ShortName;
        department.Email = dto.Email;
        department.Phone = dto.Phone;
        department.Score = dto.Score;
        department.SortOrder = dto.SortOrder;
        department.IsActivity = dto.IsActivity;
        department.LevelId = dto.LevelId ?? 2; // Default value or handle appropriately
        return department;
    }
}