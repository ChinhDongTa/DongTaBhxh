namespace DongTaBhxh.ApiGateway.Controllers.FileApi.Reports.Models;
/// <summary>
/// Xếp loại các nhân quý dùng cho biên bản họp quý
/// </summary>
public record XepLoaiCaNhanLine {
    public byte STT { get; set; }
    public required string FullName { get; set; }
    public string? A { get; set; }
    public string? B { get; set; }
    public string? C { get; set; }
    public string? D { get; set; }
    public string? Note { get; set; }
}