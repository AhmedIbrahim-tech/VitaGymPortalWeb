using Infrastructure.Entities.Enums;

namespace Infrastructure.Entities.Users.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
    public bool IsActive { get; set; } = true;

    public int? MemberId { get; set; }
    public Member? Member { get; set; }

    public int? TrainerId { get; set; }
    public Trainer? Trainer { get; set; }
}
