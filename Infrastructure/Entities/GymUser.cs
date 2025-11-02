using Infrastructure.Entities.Enums;
namespace Infrastructure.Entities
{
    [Owned]
    public class Address
    {
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string BuildingNumber { get; set; }

    }
    public abstract class GymUser : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime DateOfBirth { get; set; } 
        public Gender Gender { get; set; }
        public Address Address { get; set; } = null!;

    }
}
