using Infrastructure.Entities.Users;

namespace Infrastructure.Entities;

public class MemberShip : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public string Status 
    {
        get
        {
            if (EndDate < DateTime.Now) return "Expired";
            return "Active";
        }
    }


    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;

    public int PlanId { get; set; }
    public Plan Plan { get; set; } = null!;
}

