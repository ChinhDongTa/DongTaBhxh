namespace DongTaBhxh.Parameter.Reports;

/// <summary>
/// Tổng hợp kết quả công tác quý của công chức, viên chức
/// </summary>
public record TongHopKetQuaCongTacQuyPara {
    public byte Quarter { get; set; }
    public int Year { get; set; }
    public string Creator { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public string SigningDate { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string DeptName { get; set; } = string.Empty;
    public int DeptId { get; set; }
    public string Position { get; set; } = string.Empty;
}