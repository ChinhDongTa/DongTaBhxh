namespace DongTaBhxh.DataTranfer.Dtos;

public record SalaryCoefficientDto {
    public int SalaryCoefficientId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
    
}