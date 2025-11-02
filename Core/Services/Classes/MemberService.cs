using Infrastructure.Entities;
using Core.Services.AttachmentService;

namespace Core.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;

        public MemberService(IUnitOfWork unitOfWork,IAttachmentService attachmentService)
        {
            _unitOfWork = unitOfWork;
            _attachmentService = attachmentService;
        }

        public bool CreateMember(CreateMemberViewModel memberViewModel)
        {
            try
            {
                if (IsEmailExist(memberViewModel.Email) || IsPhoneExist(memberViewModel.Phone))
                {
                    return false;
                }
                var PhotoName = _attachmentService.Upload("members", memberViewModel.FormFile);

                if(string.IsNullOrEmpty(PhotoName))  return false;

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
                    Photo = PhotoName
                };
                _unitOfWork.GetRepository<Member>().Add(member);
               var result = _unitOfWork.SaveChanges() > 0;
                if(!result)
                {
                    _attachmentService.Delete(member.Photo,"members");
                    return false;
                }
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll() ?? [];
            if (!members.Any() || members is null)
            {
               return [];
            }
            var memberViewModels = members.Select(m => new MemberViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                DateOfBirth = m.DateOfBirth.ToShortDateString(),
                Phone = m.Phone,
                Photo = m.Photo,
                Gender = m.Gender.ToString()
            });
            return memberViewModels;
        }

        public MemberViewModel? GetMemberDetails(int id)
        {
            var member = _unitOfWork.GetRepository<Member>().GetByID(id);
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

            var activeMembership = _unitOfWork.GetRepository<MemberShip>().GetAll(ms => ms.MemberId == id && ms.Status == "Active").FirstOrDefault();
            if (activeMembership is not null)
            {
                var plan = _unitOfWork.GetRepository<Plan>().GetByID(activeMembership.PlanId);
                memberViewModel.PlanName = plan.Name;
                memberViewModel.MembershipStartDate = activeMembership.CreatedAt.ToShortDateString();
                memberViewModel.MembershipEndDate = activeMembership.EndDate.ToShortDateString();
            }
            return memberViewModel;

        }
        public bool UpdateMember(int id, MemberToUpdateViewModel memberViewModel)
        {
            var member = _unitOfWork.GetRepository<Member>().GetByID(id);
            if (member is null)
                return false;

            // Check for duplicates
            var emailExists = _unitOfWork.GetRepository<Member>()
                .GetAll(m => m.Email.ToLower() == memberViewModel.Email.ToLower() && m.Id != id)
                .Any();

            var phoneExists = _unitOfWork.GetRepository<Member>()
                .GetAll(m => m.Phone == memberViewModel.Phone && m.Id != id)
                .Any();

            if (emailExists || phoneExists)
                return false;

            if (memberViewModel.PhotoFile != null && memberViewModel.PhotoFile.Length > 0)
            {
                // 1️⃣ Try uploading the new photo first
                var uploadedPhoto = _attachmentService.Upload("members", memberViewModel.PhotoFile);

                if (uploadedPhoto != null)
                {
                    if (!string.IsNullOrEmpty(member.Photo))
                        _attachmentService.Delete(member.Photo, "members");

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
            member.Address.Street = memberViewModel.Street;
            member.Address.City = memberViewModel.City;
            member.Address.BuildingNumber = memberViewModel.BuildingNumber;
            member.UpdatedAt = DateTime.Now;

            _unitOfWork.GetRepository<Member>().Update(member);
            _unitOfWork.SaveChanges();

            return true;
        }

        public HealthRecordViewModel? GetMemberHealthRecord(int id)
        {
            var healthRecord = _unitOfWork.GetRepository<HealthRecord>().GetByID(id);
            if (healthRecord is null)
            {
                return null;
            }
            var healthRecordViewModel = new HealthRecordViewModel
            {

                Height = healthRecord.Height,
                Weight = healthRecord.Weight,
                BloodType = healthRecord.BloodType,
                Note = healthRecord.Note
            };
            return healthRecordViewModel;
        }
        public MemberToUpdateViewModel GetMemberToUpdate(int id)
        {
            var member = _unitOfWork.GetRepository<Member>().GetByID(id);
            if (member is null)
            {
                return null;
            }

            var memberViewModel = new MemberToUpdateViewModel
            {
                Name = member.Name,
                Email = member.Email,
                Photo = member.Photo,
                Phone = member.Phone,
                BuildingNumber = member.Address.BuildingNumber,
                Street = member.Address.Street,
                City = member.Address.City
            };

            return memberViewModel;
        }

        public bool RemoveMember(int MemberId)
        {
            var Repo = _unitOfWork.GetRepository<Member>();
            var Member = Repo.GetByID(MemberId);
            if (Member is null) return false;
            var sessionIds = _unitOfWork.GetRepository<Booking>().GetAll(
               b => b.MemberId == MemberId).Select(S => S.SessionId); 

            var hasFutureSessions = _unitOfWork.GetRepository<Session>()
                .GetAll(S => sessionIds.Contains(S.Id) && S.StartDate > DateTime.Now).Any();

            if (hasFutureSessions) return false;

            var MemberShips = _unitOfWork.GetRepository<MemberShip>().GetAll(X => X.MemberId == MemberId);

            try
            {
                if (MemberShips.Any())
                {
                    foreach (var membership in MemberShips)
                        _unitOfWork.GetRepository<MemberShip>().Delete(membership);
                }
                _unitOfWork.GetRepository<Member>().Delete(Member);
                bool IsDeleted = _unitOfWork.SaveChanges() > 0;
                if (IsDeleted)
                    _attachmentService.Delete(Member.Photo, "members");
                return IsDeleted;
            }
            catch
            {
                return false;
            }

        }
        #region Helper Methods

        public string FormatAddress(Address address)
        {
            if (address is null)
            {
                return "N/A";
            }
            return $"{address.Street}, {address.BuildingNumber}, {address.City}";
        }
        public bool IsEmailExist(string email)
        {
            var member = _unitOfWork.GetRepository<Member>().GetAll(m => m.Email.ToLower() == email.ToLower()).FirstOrDefault();
            return member is not null;
        }

        public bool IsPhoneExist(string phone)
        {
            var member = _unitOfWork.GetRepository<Member>().GetAll(m => m.Phone == phone).FirstOrDefault();
            return member is not null;
        }
        #endregion

    }
}
