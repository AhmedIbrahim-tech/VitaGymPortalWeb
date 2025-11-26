using Core.ViewModels.UserManagementViewModels;

namespace Core.Services.Interfaces;

public interface IUserManagementService
{
    Task<IEnumerable<UserListViewModel>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<UserListViewModel?> GetUserByIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<bool> CreateUserAsync(CreateUserViewModel viewModel, CancellationToken cancellationToken = default);
    Task<bool> UpdateUserAsync(UpdateUserViewModel viewModel, CancellationToken cancellationToken = default);
    Task<bool> ChangeUserPasswordAsync(string userId, string newPassword, CancellationToken cancellationToken = default);
    Task<bool> ToggleUserStatusAsync(string userId, CancellationToken cancellationToken = default);
    Task<bool> DeleteUserAsync(string userId, CancellationToken cancellationToken = default);
    Task<List<string>> GetAvailableRolesAsync();
}

