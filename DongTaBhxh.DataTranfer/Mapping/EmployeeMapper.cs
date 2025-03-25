using DongTaBhxh.DataTranfer.Dtos;
using DongTaBhxh.Domain.Models.EmployeeDb;

namespace DongTaBhxh.DataTranfer.Mapping;

public static class EmployeeMapper {

    public static EmployeeDto ToDto(this Employee employee)
    {
        return new()
        {
            EmployeeId = employee.EmployeeId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Birthdate = employee.Birthdate,
            DeptId = employee.DeptId,
            DeptName = employee.Dept?.Name,
            Email = employee.Email,
            Phone = employee.Phone,
            PostId = employee.PostId,
            PositionName = employee.Post?.Name,
            Address = employee.Address,
            SalaryCoefficientId = employee.SalaryCoefficientId,
            Salary = employee.SalaryCoefficient?.Description,
            AccountBank = employee.AccountBank,
            IdentityCard = employee.IdentityCard,
            SortOrder = employee.SortOrder,
            IsQuitJob = employee.IsQuitJob,
            TelegramId = employee.TelegramId,
            Gender = employee.Gender,
        };
    }

    public static Employee ToEntity(this EmployeeDto dto, Employee? employee = null)
    {
        if (employee is null)
        {
            return new()
            {
                EmployeeId = dto.EmployeeId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Birthdate = dto.Birthdate,
                DeptId = dto.DeptId,
                Email = dto.Email,
                Phone = dto.Phone,
                PostId = dto.PostId,
                Address = dto.Address,
                SalaryCoefficientId = dto.SalaryCoefficientId,
                AccountBank = dto.AccountBank,
                IdentityCard = dto.IdentityCard,
                SortOrder = dto.SortOrder,
                IsQuitJob = dto.IsQuitJob,
                TelegramId = dto.TelegramId,
                Gender = dto.Gender,
            };
        }
        employee.FirstName = dto.FirstName;
        employee.LastName = dto.LastName;
        employee.Birthdate = dto.Birthdate;
        employee.DeptId = dto.DeptId;
        employee.Email = dto.Email;
        employee.Phone = dto.Phone;
        employee.PostId = dto.PostId;
        employee.Address = dto.Address;
        employee.SalaryCoefficientId = dto.SalaryCoefficientId;
        employee.AccountBank = dto.AccountBank;
        employee.IdentityCard = dto.IdentityCard;
        employee.SortOrder = dto.SortOrder;
        employee.IsQuitJob = dto.IsQuitJob;
        employee.TelegramId = dto.TelegramId;
        employee.Gender = dto.Gender;
        return employee;
    }
}