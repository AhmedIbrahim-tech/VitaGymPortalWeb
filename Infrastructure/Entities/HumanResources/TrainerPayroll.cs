using Infrastructure.Entities.Shared;
using Infrastructure.Entities.Users.GymUsers;

namespace Infrastructure.Entities.HumanResources;

public class TrainerPayroll : BaseEntity
{
    public int TrainerId { get; set; }
    public Trainer Trainer { get; set; } = null!;

    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }

    public decimal GrossAmount { get; set; }
    public decimal NetAmount { get; set; }
    public bool IsPaid { get; set; }
    public DateTime? PaidAt { get; set; }
    public string? Note { get; set; }
}
