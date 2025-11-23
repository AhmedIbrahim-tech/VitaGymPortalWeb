using Core.Mappers;
using Infrastructure.Entities.Users;

namespace Core.Services.Classes;

public class AttendanceService(IUnitOfWork _unitOfWork) : IAttendanceService
{
    #region Check In

    public async Task<bool> CheckInAsync(CreateAttendanceViewModel viewModel, CancellationToken cancellationToken = default)
    {
        try
        {
            // Check if member exists
            var member = await _unitOfWork.GetRepository<Member>().GetByIDAsync(viewModel.MemberId, cancellationToken);
            if (member is null)
            {
                return false;
            }

            // Check if member has active membership
            var activeMembership = await _unitOfWork.GetRepository<MemberShip>().GetAllAsync(
                ms => ms.MemberId == viewModel.MemberId && ms.EndDate >= DateTime.Now, cancellationToken);
            if (!activeMembership.Any())
            {
                return false;
            }

            // Check if member already has an active check-in (no check-out)
            if (await HasActiveCheckInAsync(viewModel.MemberId, cancellationToken))
            {
                return false;
            }

            var attendance = viewModel.ToAttendance();
            await _unitOfWork.GetRepository<Attendance>().AddAsync(attendance, cancellationToken);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
        catch
        {
            return false;
        }
    }

    #endregion

    #region Check Out

    public async Task<bool> CheckOutAsync(int attendanceId, CancellationToken cancellationToken = default)
    {
        try
        {
            var attendance = await _unitOfWork.GetRepository<Attendance>().GetByIDAsync(attendanceId, cancellationToken);
            if (attendance is null || attendance.CheckOutTime.HasValue)
            {
                return false;
            }

            attendance.CheckOutTime = DateTime.Now;
            attendance.UpdatedAt = DateTime.Now;
            _unitOfWork.GetRepository<Attendance>().Update(attendance);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
        catch
        {
            return false;
        }
    }

    #endregion

    #region Get All Attendances

    public async Task<IEnumerable<AttendanceViewModel>> GetAllAttendancesAsync(CancellationToken cancellationToken = default)
    {
        var attendances = await _unitOfWork.GetRepository<Attendance>().GetAllAsync(
            null, cancellationToken, a => a.Member);
        var attendancesList = attendances.OrderByDescending(a => a.CheckInTime).ToList();

        if (!attendancesList.Any())
        {
            return [];
        }

        return attendancesList.Select(a => a.ToAttendanceViewModel());
    }

    #endregion

    #region Get Member Attendances

    public async Task<IEnumerable<AttendanceViewModel>> GetMemberAttendancesAsync(int memberId, CancellationToken cancellationToken = default)
    {
        var attendances = await _unitOfWork.GetRepository<Attendance>().GetAllAsync(
            a => a.MemberId == memberId, cancellationToken, a => a.Member);
        var attendancesList = attendances.OrderByDescending(a => a.CheckInTime).ToList();

        if (!attendancesList.Any())
        {
            return [];
        }

        return attendancesList.Select(a => a.ToAttendanceViewModel());
    }

    #endregion

    #region Get Attendance By Id

    public async Task<AttendanceViewModel?> GetAttendanceByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var attendance = await _unitOfWork.GetRepository<Attendance>().GetByIDAsync(id, cancellationToken);
        if (attendance is null)
        {
            return null;
        }

        return attendance.ToAttendanceViewModel();
    }

    #endregion

    #region Has Active Check In

    public async Task<bool> HasActiveCheckInAsync(int memberId, CancellationToken cancellationToken = default)
    {
        var activeAttendances = await _unitOfWork.GetRepository<Attendance>().GetAllAsync(
            a => a.MemberId == memberId && a.CheckOutTime == null, cancellationToken);
        return activeAttendances.Any();
    }

    #endregion
}

