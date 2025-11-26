using Infrastructure.Entities.Enums;
using Infrastructure.Entities.Sessions;
using Infrastructure.Entities.HumanResources;

namespace Infrastructure.Entities.Users.GymUsers;

public class Trainer : GymUser
{
    public Specialities? Speciality { get; set; }

    // HR
    public decimal BasicSalary { get; set; }
    public DateTime HireDate { get; set; }
    public int AnnualLeaveBalanceDays { get; set; }

    // Collections
    public ICollection<Session> Sessions { get; set; } = [];
    public ICollection<TrainerPayroll> Payrolls { get; set; } = [];
    public ICollection<LeaveRequest> LeaveRequests { get; set; } = [];
}
