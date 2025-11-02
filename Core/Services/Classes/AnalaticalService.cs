namespace Core.Services.Classes;

public class AnalaticalService : IAnalaticalService
{
    private readonly IUnitOfWork _unitOfWork;
    public AnalaticalService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public AnalaticalViewModel GetAnalaticalData()
    {
        var SessionRepo = _unitOfWork.GetRepository<Session>();
        return new AnalaticalViewModel
        {
            ActiveMembers = _unitOfWork.GetRepository<MemberShip>().GetAll(x=>x.Status=="Active").Count(),
            TotalMembers = _unitOfWork.GetRepository<Member>().GetAll().Count(),
            TotalTrainers = _unitOfWork.GetRepository<Trainer>().GetAll().Count(),
            UpcomingSessions = SessionRepo.GetAll(s => s.StartDate > DateTime.Now).Count(),
            OngoingSessions = SessionRepo.GetAll(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now).Count(),
            CompletedSessions = SessionRepo.GetAll(s => s.EndDate < DateTime.Now).Count()
        };
    }
}
