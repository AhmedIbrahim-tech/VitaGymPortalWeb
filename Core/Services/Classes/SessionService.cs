namespace Core.Services.Classes
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISessionRepository _sessionRepository;

        public SessionService(IUnitOfWork unitOfWork,IMapper mapper,ISessionRepository sessionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sessionRepository = sessionRepository;
        }

        public bool CreateSession(CreateSessionViewModel ViewModel)
        {
            if (!IsValidSessionTime(ViewModel.StartDate, ViewModel.EndDate))
            {
                return false;
            }
            if (!IsTrainerExist(ViewModel.TrainerId))
            {
                return false;
            }
            if (!IsCategoryExist(ViewModel.CategoryId))
            {
                return false;
            }

            var session = _mapper.Map<Session>(ViewModel);

            _unitOfWork.GetRepository<Session>().Add(session);

            return _unitOfWork.SaveChanges() > 0;
        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessions = _sessionRepository.GetAllSessionsWithTrainerAndCategory()
                                                        .OrderByDescending(s => s.StartDate); 

            if (sessions is null || !sessions.Any())
            {
                return [];
            }

            var mappedSessions = _mapper.Map<IEnumerable<SessionViewModel>>(sessions);

            foreach(var session in mappedSessions)
            {
                session.AvailableSlots = session.Capacity - _sessionRepository.GetCountOfBookedSlots(session.Id);
            }

            return mappedSessions;

        }

        public SessionViewModel? GetSessionByID(int id)
        {
            var session = _sessionRepository.GetSessionWithTrainerAndCategory(id);

            if (session is null )
            {
                return null;
            }

            var mappedSession = _mapper.Map<SessionViewModel>(session);

            mappedSession.AvailableSlots = mappedSession.Capacity - _sessionRepository.GetCountOfBookedSlots(session.Id);

            return mappedSession;
        }

        public bool UpdateSession(int id,UpdateSessionViewModel ViewModel)
        {
            var session = _unitOfWork.GetRepository<Session>().GetByID(id);
           
            if (!IsSessionAvailableForUpdate(session))
            {
                return false;
            }
            if (!IsTrainerExist(ViewModel.TrainerId))
            {
                return false;
            }
            
            session.TrainerId = ViewModel.TrainerId;
            session.StartDate = ViewModel.StartDate;
            session.EndDate = ViewModel.EndDate;
            session.Description = ViewModel.Description;
            session.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.GetRepository<Session>().Update(session);
            return _unitOfWork.SaveChanges() > 0;

        }

        public UpdateSessionViewModel? GetSessionToUpdate(int id)
        {
            var session = _unitOfWork.GetRepository<Session>().GetByID(id);

            if (session is null)
            {
                return null;
            }
            var mappedSession = _mapper.Map<UpdateSessionViewModel>(session);
            return mappedSession;
        }

        public bool RemoveSession(int id)
        {
            var session = _unitOfWork.GetRepository<Session>().GetByID(id);
            if (!IsSessionAvailableForDelete(session))
            {
                return false;
            }
            _unitOfWork.GetRepository<Session>().Delete(session);
            return _unitOfWork.SaveChanges() > 0;
        }
        public IEnumerable<TrainerSelectViewModel> LoadTrainersDropDown()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            return _mapper.Map<IEnumerable<TrainerSelectViewModel>>(trainers);
        }

        public IEnumerable<CategorySelectViewModel> LoadCategoriesDropDown()
        {
            var categories = _unitOfWork.GetRepository<Category>().GetAll();
            return _mapper.Map<IEnumerable<CategorySelectViewModel>>(categories);
        }
        #region Helper Methods
        private bool IsValidSessionTime(DateTime startTime, DateTime endTime)
        {
            DateTime now = DateTime.UtcNow;

            return startTime < endTime &&
                   startTime > now &&  
                   endTime > now;      
        }

        private bool IsTrainerExist(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetByID(trainerId);
            return trainer != null;
        }

        private bool IsCategoryExist(int categoryId)
        {
            var category = _unitOfWork.GetRepository<Category>().GetByID(categoryId);
            return category != null;
        }

       private bool IsSessionAvailableForUpdate(Session session )
        {
            if (session is null)
            {
                return false;
            }

            if (!IsValidSessionTime(session.StartDate, session.EndDate))
            {
                return false;
            }
            if (session.StartDate <= DateTime.UtcNow && session.EndDate >= DateTime.UtcNow)
            {
                return false;
            }
            // session has booked slots

            var bookedSlots = _sessionRepository.GetCountOfBookedSlots(session.Id) > 0;

            if(bookedSlots)
            {
                return false;
            }
            return true;
        }

        private bool IsSessionAvailableForDelete(Session session)
        {
            if (session is null)
            {
                return false;
            }
            // session is running
            if(session.StartDate <= DateTime.Now && session.EndDate >= DateTime.Now)
            {
                return false;
            }
            // session already ended
            if (session.EndDate < DateTime.UtcNow)
            {
                return true;
            }
            // session has booked slots
            var bookedSlots = _sessionRepository.GetCountOfBookedSlots(session.Id) > 0;
            if (bookedSlots)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
