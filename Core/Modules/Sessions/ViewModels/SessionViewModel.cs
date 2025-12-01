namespace Core.Modules.Sessions.ViewModels;

public class SessionViewModel
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string TrainerName { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Capacity { get; set; }
    public int AvailableSlots { get; set; }

    #region  Computed properties
    public string DateDisplay => $"{StartDate:MMM dd , yyyy}";
    public string TimeRangeDisplay => $"{StartDate:hh:mm tt} - {EndDate:hh:mm tt}";
    public TimeSpan Duration => EndDate - StartDate;
    public string DurationDisplay
    {
        get
        {
            var duration = Duration;
            var hours = (int)duration.TotalHours;
            var minutes = duration.Minutes;
            
            if (hours > 0 && minutes > 0)
                return $"{hours}h {minutes}m";
            else if (hours > 0)
                return $"{hours}h";
            else if (minutes > 0)
                return $"{minutes}m";
            else
                return "Less than a minute";
        }
    }
    public string CapacityDisplay => $"{AvailableSlots} / {Capacity}";
    public string Status
    {
        get
        {
            if (StartDate > DateTime.Now)
                return "Upcoming";
            else if (StartDate <= DateTime.Now && EndDate >= DateTime.Now)
                return "Ongoing";
            else
                return "Completed";
        }
    }
    #endregion
}
