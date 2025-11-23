namespace Infrastructure.Entities.Users;

public class Trainer : GymUser
{
    public Speicalites? Speciality { get; set; }
    public ICollection<Session> Sessions { get; set; } = [];
}
