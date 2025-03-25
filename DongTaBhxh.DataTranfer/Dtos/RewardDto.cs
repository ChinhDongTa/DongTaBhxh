namespace DongTaBhxh.DataTranfer.Dtos;

public record RewardDto {
    public int RewardId { get; set; }

    public string Name { get; set; } = null!;

    public string? ShortName { get; set; }
    public string? Classify { get; set; }
    public string? Type { get; set; }
}