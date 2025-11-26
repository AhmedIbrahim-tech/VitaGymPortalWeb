using Infrastructure.Entities.Shared;
using Infrastructure.Entities.Enums;
using Infrastructure.Entities.Users.GymUsers;

namespace Infrastructure.Entities.HumanResources;

public class LeaveRequest : BaseEntity
{
    public int TrainerId { get; set; }
    public Trainer Trainer { get; set; } = null!;

    public int LeaveTypeId { get; set; }
    public LeaveType LeaveType { get; set; } = null!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalDays { get; set; }

    public LeaveStatus Status { get; set; } = LeaveStatus.Pending;

    public string? Reason { get; set; }
    public string? AdminComment { get; set; }
}
