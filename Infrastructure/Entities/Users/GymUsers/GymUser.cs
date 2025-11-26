using Infrastructure.Entities.Shared;
using Infrastructure.Entities.Enums;

namespace Infrastructure.Entities.Users.GymUsers;

public abstract class GymUser : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public Address? Address { get; set; }
    public UserStatus Status { get; set; } = UserStatus.Active;
    public string? PhotoUrl { get; set; }
}
