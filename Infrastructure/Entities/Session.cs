using Infrastructure.Entities.Users;

namespace Infrastructure.Entities;

public class Session : BaseEntity
{
    public string Title { get; set; } = string.Empty; // Yoga, CrossFit, etc.
    public string Description { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<Booking> SessionMembers { get; set; } = [];
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public int TrainerId { get; set; }
    public Trainer Trainer { get; set; } = null!;

}
