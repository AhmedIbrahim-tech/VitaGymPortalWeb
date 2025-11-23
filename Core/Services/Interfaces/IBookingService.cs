namespace Core.Services.Interfaces;

public interface IBookingService
{
	Task<IEnumerable<SessionViewModel>> GetAllSessionsAsync(CancellationToken cancellationToken = default);
	Task<IEnumerable<MemberForSessionViewModel>> GetMembersForUpcomingBySessionIdAsync(int sessionId, CancellationToken cancellationToken = default);
	Task<IEnumerable<MemberForSessionViewModel>> GetMembersForOngoingBySessionIdAsync(int sessionId, CancellationToken cancellationToken = default);
	Task<IEnumerable<MemberSelectListViewModel>> GetMembersForDropDownAsync(int sessionId, CancellationToken cancellationToken = default);
	Task<bool> CancelBookingAsync(int memberId, int sessionId, CancellationToken cancellationToken = default);
	Task<bool> CreateNewBookingAsync(CreateBookingViewModel createdBooking, CancellationToken cancellationToken = default);
	Task<bool> MemberAttendedAsync(int memberId, int sessionId, CancellationToken cancellationToken = default);
}
