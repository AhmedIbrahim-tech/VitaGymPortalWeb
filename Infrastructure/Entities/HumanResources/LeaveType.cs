using Infrastructure.Entities.Shared;

namespace Infrastructure.Entities.HumanResources;

public class LeaveType : BaseEntity
{
    public string Name { get; set; } = null!;
    public int AnnualAllowanceDays { get; set; }
    public bool IsActive { get; set; }

    public ICollection<LeaveRequest> LeaveRequests { get; set; } = [];
}
