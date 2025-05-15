using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels
{
    public class EventVM
    {
        [Required(ErrorMessage = "Category is required")]
        public string category { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime date { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string location { get; set; }

        [Required(ErrorMessage = "Number of seats is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of seats must be greater than 0")]
        public int numberOfSeats { get; set; }

        // Entertainment Event specific fields
        public string? performerName { get; set; }
        public string? showCategory { get; set; }
        public int? ageRestriction { get; set; }
        public double? duration { get; set; }
        public string? genre { get; set; }
        public string? eventImage { get; set; }

        // Sport Event specific fields
        public string? team1 { get; set; }
        public string? team2 { get; set; }
        public string? team1Image { get; set; }
        public string? team2Image { get; set; }
        public string? tournament { get; set; }
        public string? sport { get; set; }
        public string? tournamentStage { get; set; }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
} 