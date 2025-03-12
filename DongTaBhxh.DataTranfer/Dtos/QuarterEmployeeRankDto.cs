namespace DongTaBhxh.DataTranfer.Dtos;

public record  QuarterEmployeeRankDto {
    public int QuarterEmployeeRankId { get; set; }
    public int EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public int RewardId { get; set; }
    public string? RewardName { get; set; }
    public byte Quarter { get; set; }

    public int Year { get; set; }

    public int? SelfScore { get; set; }

    public int? ResultScore { get; set; }

    public int? TotalWork { get; set; }

    public int? NumWorked { get; set; }

    public string? Note { get; set; }
}