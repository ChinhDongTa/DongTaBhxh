using DongTaBhxh.DataTranfer.Dtos;
using DongTaBhxh.Domain.Models.EmployeeDb;

namespace DongTaBhxh.DataTranfer.Mapping;

public static class QuarterDepartmentRankMapper {

    public static QuarterDepartmentRankDto ToDto(this QuarterDepartmentRank quarterDepartmentRank)
    {
        return new()
        {
            QuarterDepartmentRankId = quarterDepartmentRank.QuarterDepartmentRankId,
            DeptId = quarterDepartmentRank.DeptId,
            DeptName = quarterDepartmentRank.Dept?.Name,
            RewardId = quarterDepartmentRank.RewardId,
            RewardName = quarterDepartmentRank.Reward?.Name,
            Quarter = quarterDepartmentRank.Quarter,
            Year = quarterDepartmentRank.Year,
            SelfScore = quarterDepartmentRank.SelfScore,
            ResultScore = quarterDepartmentRank.ResultScore,
            BaseCore = quarterDepartmentRank.Dept?.Score ?? 1,
            Note = quarterDepartmentRank.Note,
        };
    }

    public static QuarterDepartmentRank ToEntity(this QuarterDepartmentRankDto dto, QuarterDepartmentRank? quarterDepartmentRank = null)
    {
        if (quarterDepartmentRank is null)
        {
            return new()
            {
                QuarterDepartmentRankId = dto.QuarterDepartmentRankId,
                DeptId = dto.DeptId,
                RewardId = dto.RewardId,
                Quarter = dto.Quarter,
                Year = dto.Year,
                SelfScore = dto.SelfScore,
                ResultScore = dto.ResultScore,
                Note = dto.Note,
            };
        }
        quarterDepartmentRank.DeptId = dto.DeptId;
        quarterDepartmentRank.RewardId = dto.RewardId;
        quarterDepartmentRank.Quarter = dto.Quarter;
        quarterDepartmentRank.Year = dto.Year;
        quarterDepartmentRank.SelfScore = dto.SelfScore;
        quarterDepartmentRank.ResultScore = dto.ResultScore;
        quarterDepartmentRank.Note = dto.Note;
        return quarterDepartmentRank;
    }
}