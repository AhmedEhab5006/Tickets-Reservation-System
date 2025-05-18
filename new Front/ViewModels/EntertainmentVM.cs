using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace WebApplication2.ViewModels
{
    public class EntertainmentVM
    {
        public int EventId { get; set; }

        [DefaultValue("Entertainment")]
        public string category { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Event Date and Time")]
        public DateTime date { get; set; }

        [Required]
        public string location { get; set; }

        [Required]
        [Range(1, int.MaxValue)]

        public int numberOfSeats { get; set; }

        [Required]
        public string PerformerName { get; set; }

        public IFormFile? EventImage_upload { get; set; } 
        public string? EventImage { get; set; }

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
        public string status { get; set; } = "Pending";

        // Date helper fields
        [Required(ErrorMessage = "Day is required")]
        [Range(1, 31)]
        public int Day { get; set; }

        [Required(ErrorMessage = "Month is required")]
        [Range(1, 12)]
        public int Month { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(2024, 2100)]
        public int Year { get; set; }

        [Required(ErrorMessage = "Hour is required")]
        [Range(0, 23)]
        public int Hour { get; set; }

        [Required(ErrorMessage = "Minute is required")]
        [Range(0, 59)]
        public int Minute { get; set; }
    }
}
