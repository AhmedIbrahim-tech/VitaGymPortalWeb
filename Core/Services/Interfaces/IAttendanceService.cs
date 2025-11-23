namespace Core.Services.Interfaces;

public interface IAttendanceService
{
    Task<bool> CheckInAsync(CreateAttendanceViewModel viewModel, CancellationToken cancellationToken = default);
    Task<bool> CheckOutAsync(int attendanceId, CancellationToken cancellationToken = default);
    Task<IEnumerable<AttendanceViewModel>> GetAllAttendancesAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<AttendanceViewModel>> GetMemberAttendancesAsync(int memberId, CancellationToken cancellationToken = default);
    Task<AttendanceViewModel?> GetAttendanceByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> HasActiveCheckInAsync(int memberId, CancellationToken cancellationToken = default);
}

