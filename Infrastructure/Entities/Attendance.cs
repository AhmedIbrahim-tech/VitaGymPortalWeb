using Infrastructure.Entities.Users;

namespace Infrastructure.Entities;

public class Attendance : BaseEntity
{
    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;

    public DateTime CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
}
