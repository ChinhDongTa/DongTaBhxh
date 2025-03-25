namespace DongTaBhxh.DataTranfer.Dtos;
public record PositionDto {
    public int PositionId { get; set; }

    public string Name { get; set; } = null!;
    public string? ShortName { get; set; }
}