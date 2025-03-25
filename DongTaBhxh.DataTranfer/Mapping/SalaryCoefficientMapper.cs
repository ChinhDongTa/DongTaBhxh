using DongTaBhxh.DataTranfer.Dtos;
using DongTaBhxh.Domain.Models.EmployeeDb;

namespace DongTaBhxh.DataTranfer.Mapping;

public static class SalaryCoefficientMapper {

    public static SalaryCoefficientDto ToDto(this SalaryCoefficient salaryCoefficient)
    {
        return new()
        {
            SalaryCoefficientId = salaryCoefficient.SalaryCoefficientId,
            Name = salaryCoefficient.Name,
            Description = salaryCoefficient.Description
        };
    }

    public static SalaryCoefficient ToEntity(this SalaryCoefficientDto dto, SalaryCoefficient? salaryCoefficient = null)
    {
        if (salaryCoefficient is null)
        {
            return new()
            {
                SalaryCoefficientId = dto.SalaryCoefficientId,
                Name = dto.Name,
                Description = dto.Description
            };
        }
        salaryCoefficient.Name = dto.Name;
        salaryCoefficient.Description = dto.Description;
        return salaryCoefficient;
    }
}