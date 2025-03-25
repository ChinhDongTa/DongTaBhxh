namespace DongTaBhxh.DataTranfer.Dtos;

public record QuarterEmployeeRankDto {
    public int QuarterEmployeeRankId { get; set; }
    public int EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public int RewardId { get; set; }
    public string? RewardName { get; set; }
    public byte Quarter { get; set; }

    public int Year { get; set; }

    public int? SelfScore { get; set; }

    public int? ResultScore { get; set; }

    public byte? TotalWork { get; set; }

    public byte? NumWorked { get; set; }

    public string? Note { get; set; }
}
/// <summary>
/// Kết quả xếp loại của 3 quý trước
/// </summary>
public record Result3QuarterEmployeeRankDto {
    public int EmployeeId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string TotalReward { get; set; } = string.Empty;
}