using Infrastructure.Entities.Enums;

namespace Infrastructure.Entities
{
    public class Trainer : GymUser
    {
        public Speicalites? Speciality { get; set; }
        public ICollection<Session> Sessions { get; set; } = null!;
    }
}
