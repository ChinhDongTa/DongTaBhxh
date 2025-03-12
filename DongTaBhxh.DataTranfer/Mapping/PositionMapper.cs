using DongTaBhxh.DataTranfer.Dtos;
using DongTaBhxh.Domain.Models.EmployeeDb;

namespace DongTaBhxh.DataTranfer.Mapping;

public static class PositionMapper {

    public static PositionDto ToDto(this Position position)
    {
        return new()
        {
            PositionId = position.PositionId,
            Name = position.Name,
            ShortName = position.ShortName
        };
    }

    public static Position ToEntity(this PositionDto dto, Position? position = null)
    {
        if (position is null)
        {
            return new()
            {
                PositionId = dto.PositionId,
                Name = dto.Name,
                ShortName = dto.ShortName
            };
        }
        position.Name = dto.Name;
        position.ShortName = dto.ShortName;
        return position;
    }
}