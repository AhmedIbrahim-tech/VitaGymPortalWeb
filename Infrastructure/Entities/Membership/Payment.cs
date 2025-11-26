using Infrastructure.Entities.Shared;
using Infrastructure.Entities.Enums;
using Infrastructure.Entities.Users.GymUsers;

namespace Infrastructure.Entities.Membership;

public class Payment : BaseEntity
{
    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;

    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentMethod Method { get; set; }
    public string? ReferenceNumber { get; set; }

    public int? MemberShipId { get; set; }
    public MemberShip? MemberShip { get; set; }
}
