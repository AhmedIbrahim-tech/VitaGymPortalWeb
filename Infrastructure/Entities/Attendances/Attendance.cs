using Infrastructure.Entities.Shared;
using Infrastructure.Entities.Users.GymUsers;

namespace Infrastructure.Entities.Attendances;

public class Attendance : BaseEntity
{
    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;

    public DateTime CheckInTime { get; set; }
}
