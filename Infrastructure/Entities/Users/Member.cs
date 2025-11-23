namespace Infrastructure.Entities.Users;


public class Member : GymUser
{
    public string Photo { get; set; } = null!;
    public HealthRecord? HealthRecord { get; set; }
    public ICollection<Booking> MemberSessions { get; set; } = [];
    public ICollection<MemberShip> MemberPlans { get; set; } = [];
    public ICollection<Attendance> Attendances { get; set; } = [];
    public ICollection<Payment> Payments { get; set; } = [];
}
