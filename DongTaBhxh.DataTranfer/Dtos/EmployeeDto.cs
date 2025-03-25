namespace DongTaBhxh.DataTranfer.Dtos;

public record EmployeeDto {
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
    public string FullName { get => $"{FirstName} {LastName}"; }
    public string? Email { get; set; }

    public string? IdentityCard { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int? DeptId { get; set; }
    public string? DeptName { get; set; }
    public int? PostId { get; set; }
    public string? PositionName { get; set; }

    public int? SalaryCoefficientId { get; set; }
    public string? Salary { get; set; }

    public string? AccountBank { get; set; }

    public DateTime? Birthdate { get; set; }

    public bool IsQuitJob { get; set; }

    public int? SortOrder { get; set; }

    public long? TelegramId { get; set; }
    public bool Gender { get; set; }
}

/// <summary>
/// Dùng cho ListBox
/// </summary>
/// <param name="Id"></param>
/// <param name="FullName"></param>
public record EmployeeDtoForListBox(
     /// <summary>
     /// Mã nhân viên staffId
     /// </summary>
     int Id = default,

     /// <summary>
     /// Họ tên nhân viên + đơn vị làm việc
     /// </summary>
     string FullName = ""
);