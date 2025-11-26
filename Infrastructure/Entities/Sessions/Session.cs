using Infrastructure.Entities.Shared;
using Infrastructure.Entities.Users.GymUsers;

namespace Infrastructure.Entities.Sessions;

public class Session : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Capacity { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string? ImageUrl { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public int TrainerId { get; set; }
    public Trainer Trainer { get; set; } = null!;

    public ICollection<Booking> Bookings { get; set; } = [];
}
