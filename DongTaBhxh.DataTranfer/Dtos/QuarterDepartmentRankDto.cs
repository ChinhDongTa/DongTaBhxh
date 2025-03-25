namespace DongTaBhxh.DataTranfer.Dtos;

public record QuarterDepartmentRankDto {
    public int QuarterDepartmentRankId { get; set; }

    public int DeptId { get; set; }
    public string? DeptName { get; set; }
    public int RewardId { get; set; }
    public string? RewardName { get; set; }
    public byte Quarter { get; set; }

    public int Year { get; set; }

    public int? SelfScore { get; set; }

    public int? ResultScore { get; set; }
    /// <summary>
    /// Điểm cơ sở năm ở bảng Department
    /// </summary>
    public int BaseCore { get; set; }
    public string? Note { get; set; }
}