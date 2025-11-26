using Infrastructure.Entities.Membership;
using Infrastructure.Entities.Sessions;
using Infrastructure.Entities.Attendances;
using Infrastructure.Entities.Users;

namespace Infrastructure.Entities.Users.GymUsers;

public class Member : GymUser
{
    // Navigation Properties
    public HealthRecord? HealthRecord { get; set; }
    
    // Collections
    public ICollection<MemberShip> MemberShips { get; set; } = [];
    public ICollection<Booking> Bookings { get; set; } = [];
    public ICollection<Attendance> Attendances { get; set; } = [];
    public ICollection<Payment> Payments { get; set; } = [];
}
