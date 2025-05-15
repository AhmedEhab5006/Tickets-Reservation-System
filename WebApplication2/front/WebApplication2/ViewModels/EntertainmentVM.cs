using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace WebApplication2.ViewModels
{
    public class EntertainmentVM
    {
        public int id { get; set; }

        [DefaultValue("Entertainment")]
        public string category { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Event Date and Time")]
        public DateTime Date { get; set; }

        [Required]
        public string location { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int aviilableSeats { get; set; }

        [Required]
        public string performerName { get; set; }

        public IFormFile? eventImage_upload { get; set; }
        public string? eventImage { get; set; }

        [Required]
        public string genre { get; set; }

        [Required]
        public string showCategory { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int ageRestriction { get; set; }

        [Required]
        [Range(0.5, double.MaxValue)]
        public double duration { get; set; }

        [DefaultValue("Pending")]
        public string status { get; set; }

        // Helper properties for date components
        [Required(ErrorMessage = "Day is required")]
        [Range(1, 31, ErrorMessage = "Day must be between 1 and 31")]
        public int Day { get; set; }

        [Required(ErrorMessage = "Month is required")]
        [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]
        public int Month { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(2024, 2100, ErrorMessage = "Year must be between 2024 and 2100")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Hour is required")]
        [Range(0, 23, ErrorMessage = "Hour must be between 0 and 23")]
        public int Hour { get; set; }

        [Required(ErrorMessage = "Minute is required")]
        [Range(0, 59, ErrorMessage = "Minute must be between 0 and 59")]
        public int Minute { get; set; }
    }
}
