namespace DongTaBhxh.StringDefault;

public static class SqlQueryString {

    public static FormattableString SelectEmployeeDto { get; } = $"""
        SELECT
          EmployeeId,
          FirstName,
          LastName,
          e.Email,
          IdentityCard,
          e.Phone,
          e.Address,
          DeptId,
          Department.Name AS DeptName,
          e.PostId,
          POSITION.Name AS PositionName,
          e.SalaryCoefficientId,
          SalaryCoefficient.Description AS Salary,
          AccountBank,
          Birthdate,
          IsQuitJob,
          e.SortOrder,
          TelegramId,
          Gender
        FROM
          Department
          INNER JOIN Employee e ON Department.DepartmentId = e.DeptId
          INNER JOIN POSITION ON e.PostId = POSITION.PositionId
          INNER JOIN SalaryCoefficient ON e.SalaryCoefficientId = SalaryCoefficient.SalaryCoefficientId
        """;
}