namespace Core.ViewModels.TrainerViewModels;

public class TrainerToUpdateViewModel
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string BuildingNumber { get; set; } = null!;
    public Speicalites? Specialization { get; set; }
}
