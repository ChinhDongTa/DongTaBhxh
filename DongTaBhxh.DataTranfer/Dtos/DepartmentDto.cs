namespace DongTaBhxh.DataTranfer.Dtos;

public record DepartmentDto {
    public int DepartmentId { get; set; }

    public string ShortName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? Score { get; set; }

    public bool IsActivity { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int? SortOrder { get; set; }
    public int? LevelId { get; set; }
    public string? LevelName { get; set; }
}