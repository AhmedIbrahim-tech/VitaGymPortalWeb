using Infrastructure.Entities.Shared;

namespace Infrastructure.Entities.Membership;

public class Plan : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }
    public int DurationDays { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<MemberShip> MemberShips { get; set; } = [];
}
