using Infrastructure.Entities.Enums;

namespace Core.ViewModels
{
    public class CreateTrainerViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Name must contain only letters and spaces.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [RegularExpression(@"^(010|011|012|015)[0-9]{8}$", ErrorMessage = "Phone must start with 010, 011, 012, or 015 and be 11 digits long.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Street name is required.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Street must be between 2 and 150 characters.")]
        [RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only letters, numbers, and spaces are allowed.")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "City name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "City must be between 2 and 100 characters.")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Only letters and spaces are allowed.")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Building number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Building number must be > 1 character.")]
        public string BuildingNumber { get; set; } = null!;

        [Required(ErrorMessage = "Specialization is required.")]
        public Speicalites? Specialization { get; set; } 
    }
}
