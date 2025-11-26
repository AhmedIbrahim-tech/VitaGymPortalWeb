using Infrastructure.Entities.Shared;

namespace Infrastructure.Entities.Sessions;

public class Category : BaseEntity
{
    public string CategoryName { get; set; } = null!;
    public string? IconUrl { get; set; }

    public ICollection<Session> Sessions { get; set; } = [];
}
