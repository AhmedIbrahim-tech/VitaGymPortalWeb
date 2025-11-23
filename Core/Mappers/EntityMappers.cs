using Infrastructure.Entities.Users;

namespace Core.Mappers;

public static class EntityMappers
{
    #region Session Mappings

    public static SessionViewModel ToSessionViewModel(this Session session)
    {
        return new SessionViewModel
        {
            Id = session.Id,
            CategoryName = session.Category?.CategoryName ?? string.Empty,
            Description = session.Description,
            TrainerName = session.Trainer?.Name ?? string.Empty,
            StartDate = session.StartDate,
            EndDate = session.EndDate,
            Capacity = session.Capacity,
            AvailableSlots = 0 // This is set separately in services
        };
    }

    public static Session ToSession(this SessionViewModel viewModel)
    {
        return new Session
        {
            Id = viewModel.Id,
            Description = viewModel.Description,
            StartDate = viewModel.StartDate,
            EndDate = viewModel.EndDate,
            Capacity = viewModel.Capacity
        };
    }

    public static Session ToSession(this CreateSessionViewModel viewModel)
    {
        return new Session
        {
            Description = viewModel.Description,
            Capacity = viewModel.Capacity,
            StartDate = viewModel.StartDate,
            EndDate = viewModel.EndDate,
            TrainerId = viewModel.TrainerId,
            CategoryId = viewModel.CategoryId
        };
    }

    public static Session ToSession(this UpdateSessionViewModel viewModel, Session existingSession)
    {
        existingSession.Description = viewModel.Description;
        existingSession.StartDate = viewModel.StartDate;
        existingSession.EndDate = viewModel.EndDate;
        existingSession.TrainerId = viewModel.TrainerId;
        existingSession.UpdatedAt = DateTime.UtcNow;
        return existingSession;
    }

    public static UpdateSessionViewModel ToUpdateSessionViewModel(this Session session)
    {
        return new UpdateSessionViewModel
        {
            Description = session.Description,
            StartDate = session.StartDate,
            EndDate = session.EndDate,
            TrainerId = session.TrainerId
        };
    }

    #endregion

    #region Trainer Mappings

    public static TrainerSelectViewModel ToTrainerSelectViewModel(this Trainer trainer)
    {
        return new TrainerSelectViewModel
        {
            Id = trainer.Id,
            Name = trainer.Name
        };
    }

    #endregion

    #region Category Mappings

    public static CategorySelectViewModel ToCategorySelectViewModel(this Category category)
    {
        return new CategorySelectViewModel
        {
            Id = category.Id,
            Name = category.CategoryName
        };
    }

    #endregion

    #region Plan Mappings

    public static PlanSelectListViewModel ToPlanSelectListViewModel(this Plan plan)
    {
        return new PlanSelectListViewModel
        {
            Id = plan.Id,
            Name = plan.Name
        };
    }

    #endregion

    #region Member Mappings

    public static MemberSelectListViewModel ToMemberSelectListViewModel(this Member member)
    {
        return new MemberSelectListViewModel
        {
            Id = member.Id,
            Name = member.Name
        };
    }

    #endregion

    #region Membership Mappings

    public static MemberShipViewModel ToMemberShipViewModel(this MemberShip memberShip)
    {
        return new MemberShipViewModel
        {
            MemberId = memberShip.MemberId,
            PlanId = memberShip.PlanId,
            MemberName = memberShip.Member?.Name ?? string.Empty,
            PlanName = memberShip.Plan?.Name ?? string.Empty,
            StartDate = memberShip.CreatedAt,
            EndDate = memberShip.EndDate
        };
    }

    public static MemberShip ToMemberShip(this CreateMemberShipViewModel viewModel)
    {
        var memberShip = new MemberShip
        {
            MemberId = viewModel.MemberId,
            PlanId = viewModel.PlanId
        };
        
        // If StartDate is provided, set CreatedAt (which maps to StartDate in database)
        if (viewModel.StartDate.HasValue)
        {
            memberShip.CreatedAt = viewModel.StartDate.Value;
        }
        
        return memberShip;
    }

    #endregion

    #region Attendance Mappings

    public static AttendanceViewModel ToAttendanceViewModel(this Attendance attendance)
    {
        var duration = string.Empty;
        if (attendance.CheckOutTime.HasValue)
        {
            var timeSpan = attendance.CheckOutTime.Value - attendance.CheckInTime;
            duration = $"{timeSpan.Hours}h {timeSpan.Minutes}m";
        }
        else
        {
            duration = "In Progress";
        }

        return new AttendanceViewModel
        {
            Id = attendance.Id,
            MemberId = attendance.MemberId,
            MemberName = attendance.Member?.Name ?? string.Empty,
            CheckInTime = attendance.CheckInTime,
            CheckOutTime = attendance.CheckOutTime,
            Duration = duration
        };
    }

    public static Attendance ToAttendance(this CreateAttendanceViewModel viewModel)
    {
        return new Attendance
        {
            MemberId = viewModel.MemberId,
            CheckInTime = DateTime.Now
        };
    }

    #endregion

    #region Payment Mappings

    public static PaymentViewModel ToPaymentViewModel(this Payment payment)
    {
        return new PaymentViewModel
        {
            Id = payment.Id,
            MemberId = payment.MemberId,
            MemberName = payment.Member?.Name ?? string.Empty,
            Amount = payment.Amount,
            PaymentDate = payment.PaymentDate,
            Method = payment.Method
        };
    }

    public static Payment ToPayment(this CreatePaymentViewModel viewModel)
    {
        return new Payment
        {
            MemberId = viewModel.MemberId,
            Amount = viewModel.Amount,
            Method = viewModel.Method,
            PaymentDate = DateTime.Now
        };
    }

    #endregion
}

