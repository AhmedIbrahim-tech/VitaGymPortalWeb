using System.ComponentModel.DataAnnotations;

namespace Core.Modules.Plans.ViewModels;

public class CreatePlanViewModel
{
	[Required(ErrorMessage = "Plan Name is required")]
	[StringLength(50, ErrorMessage = "Plan Name must not exceed 50 characters")]
	[Display(Name = "Plan Name")]
	public string PlanName { get; set; } = null!;

	[Required(ErrorMessage = "Description is required")]
	[StringLength(500, ErrorMessage = "Description must not exceed 500 characters")]
	[Display(Name = "Description")]
	public string Description { get; set; } = null!;

	[Required(ErrorMessage = "Duration is required")]
	[Range(1, 365, ErrorMessage = "Duration must be between 1 and 365 days")]
	[Display(Name = "Duration (Days)")]
	public int DurationDays { get; set; }

	[Required(ErrorMessage = "Price is required")]
	[Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
	[Display(Name = "Price (EGP)")]
	public decimal Price { get; set; }
}

