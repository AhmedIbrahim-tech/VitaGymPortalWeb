using Infrastructure.Entities.Shared;
using Infrastructure.Entities.Users.GymUsers;

namespace Infrastructure.Entities.Sessions;

public class Booking : BaseEntity
{
    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;

    public int SessionId { get; set; }
    public Session Session { get; set; } = null!;

    public bool IsAttended { get; set; }
}
