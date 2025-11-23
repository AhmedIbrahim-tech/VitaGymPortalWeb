namespace Core.Services.Interfaces;

public interface IPaymentService
{
    Task<bool> CreatePaymentAsync(CreatePaymentViewModel viewModel, CancellationToken cancellationToken = default);
    Task<IEnumerable<PaymentViewModel>> GetAllPaymentsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<PaymentViewModel>> GetMemberPaymentsAsync(int memberId, CancellationToken cancellationToken = default);
    Task<PaymentViewModel?> GetPaymentByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<decimal> GetTotalPaymentsByMemberAsync(int memberId, CancellationToken cancellationToken = default);
}

