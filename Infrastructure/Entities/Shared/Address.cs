using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities.Shared;

[Owned]
public class Address
{
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string BuildingNumber { get; set; } = string.Empty;
}

