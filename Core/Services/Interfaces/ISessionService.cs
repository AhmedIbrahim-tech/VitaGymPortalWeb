using Core.ViewModels;

namespace Core.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel>GetAllSessions();

        SessionViewModel? GetSessionByID(int id);
        UpdateSessionViewModel? GetSessionToUpdate(int id); 
        bool CreateSession(CreateSessionViewModel ViewModel);
        bool UpdateSession(int id,UpdateSessionViewModel ViewModel);
        bool RemoveSession(int id);
        IEnumerable<TrainerSelectViewModel> LoadTrainersDropDown();

        IEnumerable<CategorySelectViewModel> LoadCategoriesDropDown();
    }
}
