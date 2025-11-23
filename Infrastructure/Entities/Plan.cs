namespace Infrastructure.Entities;

public class Plan : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } 
    public int DurationDays { get; set; }
    public bool IsActive { get; set; }

    public ICollection<MemberShip> PlanMembers { get; set; } = [];

}
