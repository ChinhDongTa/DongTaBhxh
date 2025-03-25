using DongTaBhxh.DataTranfer.Dtos;
using DongTaBhxh.Domain.Models.EmployeeDb;

namespace DongTaBhxh.DataTranfer.Mapping;

public static class QuarterEmployeeRankMapper {

    public static QuarterEmployeeRankDto ToDto(this QuarterEmployeeRank quarterEmployeeRank)
    {
        return new()
        {
            QuarterEmployeeRankId = quarterEmployeeRank.QuarterEmployeeRankId,
            EmployeeId = quarterEmployeeRank.EmployeeId,
            EmployeeName = $"{quarterEmployeeRank.Employee?.FirstName} {quarterEmployeeRank.Employee?.LastName}",
            RewardId = quarterEmployeeRank.RewardId,
            RewardName = quarterEmployeeRank.Reward?.Name,
            Quarter = quarterEmployeeRank.Quarter,
            Year = quarterEmployeeRank.Year,
            SelfScore = quarterEmployeeRank.SelfScore,
            ResultScore = quarterEmployeeRank.ResultScore,
            TotalWork = quarterEmployeeRank.TotalWork,
            NumWorked = quarterEmployeeRank.NumWorked,
            Note = quarterEmployeeRank.Note,
        };
    }

    public static QuarterEmployeeRank ToEntity(this QuarterEmployeeRankDto dto, QuarterEmployeeRank? quarterEmployeeRank = null)
    {
        if (quarterEmployeeRank is null)
        {
            return new()
            {
                QuarterEmployeeRankId = dto.QuarterEmployeeRankId,
                EmployeeId = dto.EmployeeId,
                RewardId = dto.RewardId,
                Quarter = dto.Quarter,
                Year = dto.Year,
                SelfScore = dto.SelfScore,
                ResultScore = dto.ResultScore,
                NumWorked = dto.NumWorked,
                TotalWork = dto.TotalWork,
                Note = dto.Note,
            };
        }
        quarterEmployeeRank.EmployeeId = dto.EmployeeId;
        quarterEmployeeRank.RewardId = dto.RewardId;
        quarterEmployeeRank.Quarter = dto.Quarter;
        quarterEmployeeRank.Year = dto.Year;
        quarterEmployeeRank.SelfScore = dto.SelfScore;
        quarterEmployeeRank.ResultScore = dto.ResultScore;
        quarterEmployeeRank.NumWorked = dto.NumWorked;
        quarterEmployeeRank.TotalWork = dto.TotalWork;
        quarterEmployeeRank.Note = dto.Note;
        return quarterEmployeeRank;
    }
}