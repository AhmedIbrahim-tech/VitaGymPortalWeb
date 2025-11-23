using Infrastructure.Entities.Users;

namespace Infrastructure.Entities;

public class Payment : BaseEntity
{
    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;

    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Method { get; set; } = null!; // Cash, Card, etc.
}
