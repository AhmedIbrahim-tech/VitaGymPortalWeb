namespace Core.ViewModels.PaymentViewModels;

public class CreatePaymentViewModel
{
    public int MemberId { get; set; }
    public decimal Amount { get; set; }
    public string Method { get; set; } = null!;
}

