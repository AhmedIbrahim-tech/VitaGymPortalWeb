namespace Core.Services.Interfaces
{
    public interface IAccountService
    {
        ApplicationUser? ValidiateUser(LoginViewModel input); 
    }
}
