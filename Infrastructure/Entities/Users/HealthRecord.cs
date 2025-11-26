using Infrastructure.Entities.Shared;

namespace Infrastructure.Entities.Users;

public class HealthRecord : BaseEntity
{
    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;

    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public string BloodType { get; set; } = null!;
    public string? Note { get; set; }
    public string? PhotoUrl { get; set; }
}

