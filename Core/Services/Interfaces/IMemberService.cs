namespace Core.Services.Interfaces;

public interface IMemberService
{
    Task<bool> CreateMemberAsync(CreateMemberViewModel memberViewModel, CancellationToken cancellationToken = default);
    Task<bool> UpdateMemberAsync(int id, MemberToUpdateViewModel memberViewModel, CancellationToken cancellationToken = default);
    Task<bool> RemoveMemberAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken cancellationToken = default);
    Task<MemberViewModel?> GetMemberDetailsAsync(int id, CancellationToken cancellationToken = default);
    Task<HealthRecordViewModel?> GetMemberHealthRecordAsync(int id, CancellationToken cancellationToken = default);
    Task<MemberToUpdateViewModel?> GetMemberToUpdateAsync(int id, CancellationToken cancellationToken = default);
}
