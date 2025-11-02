namespace Core.Services.Classes
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TrainerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateTrainer(CreateTrainerViewModel trainerViewModel)
        {
            
            try
            {
                if (IsEmailExist(trainerViewModel.Email) || IsPhoneExist(trainerViewModel.Phone))
                {
                    return false;
                }
                var trainer = new Trainer
                {
                    Name = trainerViewModel.Name,
                    Email = trainerViewModel.Email,
                    DateOfBirth = trainerViewModel.DateOfBirth,
                    Phone = trainerViewModel.Phone,
                    Address = new Address
                    {
                        Street = trainerViewModel.Street,
                        City = trainerViewModel.City,
                        BuildingNumber = trainerViewModel.BuildingNumber
                    },
                    Gender = trainerViewModel.Gender,
                    Speciality = trainerViewModel.Specialization

                };
                _unitOfWork.GetRepository<Trainer>().Add(trainer);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll() ?? [];
            if (!trainers.Any() || trainers is null)
            {
                return [];
            }
            var trainerViewModels = trainers.Select(t => new TrainerViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email,
                DateOfBirth = t.DateOfBirth.ToShortDateString(),
                Phone = t.Phone,
                Gender = t.Gender.ToString(),
                specialization = t.Speciality.ToString()
            });
            return trainerViewModels;
        }

        public TrainerViewModel? GetTrainerDetails(int id)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetByID(id);
            if (trainer is null)
            {
                return null;
            }
            var TrainerViewModel = new TrainerViewModel
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Email = trainer.Email,
                DateOfBirth = trainer.DateOfBirth.ToShortDateString(),
                Phone = trainer.Phone,
                Gender = trainer.Gender.ToString(),
                Address = FormatAddress(trainer.Address),
                specialization = trainer.Speciality.ToString()

            };
           
            return TrainerViewModel;

        }

        public bool UpdateTrainer(int id, TrainerToUpdateViewModel trainerViewModel)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetByID(id);
            if (trainer is null)
            {
                return false;
            }
            var emailExists = _unitOfWork.GetRepository<Trainer>().GetAll(m => m.Email.ToLower() == trainerViewModel.Email.ToLower() && m.Id != id).Any();

            var phoneExists = _unitOfWork.GetRepository<Trainer>().GetAll(m => m.Phone == trainerViewModel.Phone && m.Id != id).Any();
            if (emailExists || phoneExists)
            {
                return false;
            }

            trainer.Name = trainerViewModel.Name;
            trainer.Email = trainerViewModel.Email;
            trainer.Phone = trainerViewModel.Phone;
            trainer.Address.Street = trainerViewModel.Street;
            trainer.Address.City = trainerViewModel.City;
            trainer.Address.BuildingNumber = trainerViewModel.BuildingNumber;
            trainer.UpdatedAt = DateTime.Now;
            trainer.Speciality = trainerViewModel.Specialization;

            _unitOfWork.GetRepository<Trainer>().Update(trainer);
            _unitOfWork.SaveChanges();
            return true;
        }

        public TrainerToUpdateViewModel? GetTrainerToUpdate(int id)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetByID(id);
            if (trainer is null)
            {
                return null;
            }

            var TrainerViewModel = new TrainerToUpdateViewModel
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Specialization = trainer.Speciality,
                BuildingNumber = trainer.Address.BuildingNumber,
                Street = trainer.Address.Street,
                City = trainer.Address.City,
                DateOfBirth = trainer.DateOfBirth
            };

            return TrainerViewModel;
        }

        public bool RemoveTrainer(int id)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetByID(id);
            if (trainer is null)
            {
                return false;
            }

            var activebooking = _unitOfWork.GetRepository<Session>().GetAll(b => b.TrainerId == id && b.StartDate > DateTime.Now);
            if (activebooking.Any())
            {
                return false;
            }

            try
            {
                
                _unitOfWork.GetRepository<Trainer>().Delete(trainer);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception)
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
            var member = _unitOfWork.GetRepository<Trainer>().GetAll(m => m.Email.ToLower() == email.ToLower()).FirstOrDefault();
            return member is not null;
        }

        public bool IsPhoneExist(string phone)
        {
            var member = _unitOfWork.GetRepository<Trainer>().GetAll(m => m.Phone == phone).FirstOrDefault();
            return member is not null;
        }

        #endregion
    }

}
