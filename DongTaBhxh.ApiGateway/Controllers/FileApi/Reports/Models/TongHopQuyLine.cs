namespace DongTaBhxh.ApiGateway.Controllers.FileApi.Reports.Models;

public record TongHopQuyLine {
    /// <summary>
    /// Kí tự La Mã
    /// </summary>
    public string? Roman { get; set; }
    public int SortOrder { get; set; }
    /// <summary>
    /// Phòng ban
    /// </summary>
    public string? DeptName { get; set; }
    /// <summary>
    /// Xếp loại phòng tự chấm
    /// </summary>
    public string? SelfClassificationDept { get; set; }
    /// <summary>
    /// Kết quả xếp loại bởi hội đồng
    /// </summary>
    public string? ResultClassificationDept { get; set; }
    /// <summary>
    /// Điểm phòng tự chấm
    /// </summary>
    public string? SelfScoreDept
    {
        get; set;
    }

    /// <summary>
    /// Điểm của phòng do hội đồng chấm
    /// </summary>
    public string? ResultScoreDept
    {
        get; set;
    }

    /// </summary>
    /// <summary>
    /// Hệ số thu nhập tập thể
    /// </summary>
    public string? Group
    {
        get; set;
    }
    /// <summary>
    /// Tóm tắt phòng : số lượng A, B,..
    /// </summary>
    public string? SummaryDept { get; set; }

    /// <summary>
    /// Số thứ tự
    /// </summary>
    public int STT { get; set; }//Số thứ tự

    /// <summary>
    /// Họ tên
    /// </summary>
    public string? FullName
    {
        get; set;
    }//Họ tên

    /// <summary>
    /// Điểm cá nhân tự chấm
    /// </summary>
    public string? SelfScore
    {
        get; set;
    }

    /// <summary>
    /// /Điểm cá nhân do hội đồng chấm
    /// </summary>
    public string? ResultScore
    {
        get; set;
    }
    /// <summary>
    /// Xếp loại cá nhân A
    /// </summary>
    public char? A { get; set; }
    /// <summary>
    /// Xếp loại cá nhân B
    /// </summary>
    public char? B { get; set; }//Xếp loại cá nhân
    /// <summary>
    /// Xếp loại cá nhân C
    /// </summary>
    public char? C { get; set; }//Xếp loại cá nhân
    /// <summary>
    /// Xếp loại cá nhân D
    /// </summary>
    public char? D { get; set; }//Xếp loại cá nhân

    /// <summary>
    /// Hệ số thu nhập cá nhân
    /// </summary>
    public string? Personal
    {
        get; set;
    }
    /// <summary>
    /// Ghi chú
    /// </summary>
    public string? Note { get; set; }// Ghi chú
}