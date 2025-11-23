using Infrastructure.Entities.Users;

namespace Core.Services.Classes;

public class MemberService(IUnitOfWork _unitOfWork, IAttachmentService _attachmentService) : IMemberService
{
    #region Create Member

    public async Task<bool> CreateMemberAsync(CreateMemberViewModel memberViewModel, CancellationToken cancellationToken = default)
    {
        try
        {
            if (memberViewModel.FormFile == null || memberViewModel.FormFile.Length == 0)
            {
                return false;
            }

            if (await IsEmailExistAsync(memberViewModel.Email, cancellationToken) ||
                await IsPhoneExistAsync(memberViewModel.Phone, cancellationToken))
            {
                return false;
            }

            var photoName = _attachmentService.Upload("members", memberViewModel.FormFile);
            if (string.IsNullOrEmpty(photoName))
            {
                return false;
            }

            var member = new Member
            {
                Name = memberViewModel.Name,
                Email = memberViewModel.Email,
                DateOfBirth = memberViewModel.DateOfBirth,
                Phone = memberViewModel.Phone,
                Address = new Address
                {
                    Street = memberViewModel.Street,
                    City = memberViewModel.City,
                    BuildingNumber = memberViewModel.BuildingNumber
                },
                Gender = memberViewModel.Gender,
                HealthRecord = new HealthRecord
                {
                    Height = memberViewModel.HealthRecordViewModel.Height,
                    Weight = memberViewModel.HealthRecordViewModel.Weight,
                    BloodType = memberViewModel.HealthRecordViewModel.BloodType,
                    Note = memberViewModel.HealthRecordViewModel.Note
                },
                Photo = photoName
            };

            await _unitOfWork.GetRepository<Member>().AddAsync(member, cancellationToken);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (!result)
            {
                _attachmentService.Delete(member.Photo, "members");
                return false;
            }

            return result;
        }
        catch (Exception)
        {
            return false;
        }
    }

    #endregion

    #region Get All Members

    public async Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken cancellationToken = default)
    {
        var members = await _unitOfWork.GetRepository<Member>().GetAllAsync(null, cancellationToken);
        var membersList = members.ToList();

        if (!membersList.Any())
        {
            return [];
        }

        return membersList.Select(m => new MemberViewModel
        {
            Id = m.Id,
            Name = m.Name,
            Email = m.Email,
            DateOfBirth = m.DateOfBirth.ToShortDateString(),
            Phone = m.Phone,
            Photo = m.Photo,
            Gender = m.Gender.ToString()
        });
    }

    #endregion

    #region Get Member Details

    public async Task<MemberViewModel?> GetMemberDetailsAsync(int id, CancellationToken cancellationToken = default)
    {
        var member = await _unitOfWork.GetRepository<Member>().GetByIDAsync(id, cancellationToken);
        if (member is null)
        {
            return null;
        }

        var memberViewModel = new MemberViewModel
        {
            Id = member.Id,
            Name = member.Name,
            Email = member.Email,
            DateOfBirth = member.DateOfBirth.ToShortDateString(),
            Phone = member.Phone,
            Photo = member.Photo,
            Gender = member.Gender.ToString(),
            Address = FormatAddress(member.Address)
        };

        var memberships = await _unitOfWork.GetRepository<MemberShip>().GetAllAsync(
            ms => ms.MemberId == id && ms.EndDate >= DateTime.Now, cancellationToken);
        var activeMembership = memberships.FirstOrDefault();

        if (activeMembership is not null)
        {
            var plan = await _unitOfWork.GetRepository<Plan>().GetByIDAsync(activeMembership.PlanId, cancellationToken);
            if (plan is not null)
            {
                memberViewModel.PlanName = plan.Name;
                memberViewModel.MembershipStartDate = activeMembership.CreatedAt.ToShortDateString();
                memberViewModel.MembershipEndDate = activeMembership.EndDate.ToShortDateString();
            }
        }

        return memberViewModel;
    }

    #endregion

    #region Update Member

    public async Task<bool> UpdateMemberAsync(int id, MemberToUpdateViewModel memberViewModel, CancellationToken cancellationToken = default)
    {
        var member = await _unitOfWork.GetRepository<Member>().GetByIDAsync(id, cancellationToken);
        if (member is null)
        {
            return false;
        }

        var membersByEmail = await _unitOfWork.GetRepository<Member>().GetAllAsync(
            m => m.Email.ToLower() == memberViewModel.Email.ToLower() && m.Id != id, cancellationToken);
        var emailExists = membersByEmail.Any();

        var membersByPhone = await _unitOfWork.GetRepository<Member>().GetAllAsync(
            m => m.Phone == memberViewModel.Phone && m.Id != id, cancellationToken);
        var phoneExists = membersByPhone.Any();

        if (emailExists || phoneExists)
        {
            return false;
        }

        if (memberViewModel.PhotoFile != null && memberViewModel.PhotoFile.Length > 0)
        {
            var uploadedPhoto = _attachmentService.Upload("members", memberViewModel.PhotoFile);

            if (uploadedPhoto != null)
            {
                if (!string.IsNullOrEmpty(member.Photo))
                {
                    _attachmentService.Delete(member.Photo, "members");
                }

                member.Photo = uploadedPhoto;
            }
            else
            {
                Console.WriteLine("Photo upload failed, skipping deletion.");
                return false;
            }
        }

        member.Email = memberViewModel.Email;
        member.Phone = memberViewModel.Phone;

        if (member.Address is null)
        {
            member.Address = new Address();
        }
        member.Address.Street = memberViewModel.Street;
        member.Address.City = memberViewModel.City;
        member.Address.BuildingNumber = memberViewModel.BuildingNumber;
        member.UpdatedAt = DateTime.Now;

        _unitOfWork.GetRepository<Member>().Update(member);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }

    #endregion

    #region Get Member Health Record

    public async Task<HealthRecordViewModel?> GetMemberHealthRecordAsync(int id, CancellationToken cancellationToken = default)
    {
        var healthRecord = await _unitOfWork.GetRepository<HealthRecord>().GetByIDAsync(id, cancellationToken);
        if (healthRecord is null)
        {
            return null;
        }

        return new HealthRecordViewModel
        {
            Height = healthRecord.Height,
            Weight = healthRecord.Weight,
            BloodType = healthRecord.BloodType,
            Note = healthRecord.Note
        };
    }

    #endregion

    #region Get Member For Update

    public async Task<MemberToUpdateViewModel?> GetMemberToUpdateAsync(int id, CancellationToken cancellationToken = default)
    {
        var member = await _unitOfWork.GetRepository<Member>().GetByIDAsync(id, cancellationToken);
        if (member is null)
        {
            return null;
        }

        return new MemberToUpdateViewModel
        {
            Name = member.Name,
            Email = member.Email,
            Photo = member.Photo,
            Phone = member.Phone,
            BuildingNumber = member.Address?.BuildingNumber ?? string.Empty,
            Street = member.Address?.Street ?? string.Empty,
            City = member.Address?.City ?? string.Empty
        };
    }

    #endregion

    #region Remove Member

    public async Task<bool> RemoveMemberAsync(int memberId, CancellationToken cancellationToken = default)
    {
        var member = await _unitOfWork.GetRepository<Member>().GetByIDAsync(memberId, cancellationToken);
        if (member is null)
        {
            return false;
        }

        var bookings = await _unitOfWork.GetRepository<Booking>().GetAllAsync(
            b => b.MemberId == memberId, cancellationToken);
        var sessionIds = bookings.Select(b => b.SessionId).ToList();

        var sessions = await _unitOfWork.GetRepository<Session>().GetAllAsync(
            s => sessionIds.Contains(s.Id) && s.StartDate > DateTime.Now, cancellationToken);
        var hasFutureSessions = sessions.Any();

        if (hasFutureSessions)
        {
            return false;
        }

        var memberships = await _unitOfWork.GetRepository<MemberShip>().GetAllAsync(
            x => x.MemberId == memberId, cancellationToken);
        var membershipsList = memberships.ToList();

        try
        {
            if (membershipsList.Any())
            {
                foreach (var membership in membershipsList)
                {
                    _unitOfWork.GetRepository<MemberShip>().Delete(membership);
                }
            }

            _unitOfWork.GetRepository<Member>().Delete(member);
            var isDeleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isDeleted)
            {
                _attachmentService.Delete(member.Photo, "members");
            }

            return isDeleted;
        }
        catch
        {
            return false;
        }
    }

    #endregion

    #region Helper Methods

    private string FormatAddress(Address? address)
    {
        if (address is null)
        {
            return "N/A";
        }
        return $"{address.Street}, {address.BuildingNumber}, {address.City}";
    }

    private async Task<bool> IsEmailExistAsync(string email, CancellationToken cancellationToken = default)
    {
        var members = await _unitOfWork.GetRepository<Member>().GetAllAsync(
            m => m.Email.ToLower() == email.ToLower(), cancellationToken);
        return members.Any();
    }

    private async Task<bool> IsPhoneExistAsync(string phone, CancellationToken cancellationToken = default)
    {
        var members = await _unitOfWork.GetRepository<Member>().GetAllAsync(
            m => m.Phone == phone, cancellationToken);
        return members.Any();
    }

    #endregion
}
