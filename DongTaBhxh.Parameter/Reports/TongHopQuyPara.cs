namespace DongTaBhxh.Parameter.Reports;

/// <summary>
/// Tham số cho báo cáo xếp loại và tiền thưởng quý
/// </summary>
public record TongHopQuyPara {
    /// <summary>
    /// Quý
    /// </summary>
    public int Quarter { get; set; }

    public int Year { get; set; }
    /// <summary>
    /// Văn bản kèm theo
    /// </summary>
    public string DocDescription { get; set; } = string.Empty;

    /// <summary>
    /// ngày ký
    /// </summary>
    public string SigningDate { get; set; } = string.Empty;

    /// <summary>
    /// Giám đốc/người đứng đầu đơn vị
    /// </summary>
    public string Director { get; set; } = string.Empty;

    /// <summary>
    /// Đơn vị tạo báo cáo
    /// </summary>
    public string DeptNameCreator { get; set; } = "Phòng Tổ chức cán bộ";
    /// <summary>
    /// Đối với báo cáo xếp loại thì Phòng Tổ chức cán bộ
    /// Đối với báo cáo tiền thưởng quý Thì dùng Kế toán trưởng
    /// </summary>
    public string Creator { get; set; } = string.Empty;

    /// <summary>
    /// Kiểu file xuất. 0=PDF, 1=Excel
    /// </summary>
    public int FileExtension { get; set; }

    /// <summary>
    /// Mã đơn vị hành chính = 0 tất cả, = 1 khối văn phòng, = 2 khối huyện
    /// </summary>
    public int LevelId { get; set; }

    /// <summary>
    /// Loại báo cáo. 0 = danh sách tổng hợp (TCCB), 1 = danh sách kèm theo quyết định (TCCB), 3 = Danh sách chi tiền thưởng xếp loại quý (KHTC)
    /// </summary>
    public int ReportType { get; set; }
}