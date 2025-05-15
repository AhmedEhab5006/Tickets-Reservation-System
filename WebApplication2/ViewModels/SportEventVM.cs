using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace WebApplication2.ViewModels
{
    public class SportEventVM
    {
        public int id { get; set; }

        [DefaultValue("Sport")]
        public string category { get; set; } = "Sport";

        [DefaultValue("Pending")]
        public string status { get; set; } = "Pending";

        [Required(ErrorMessage = "Date is required")]
        public DateTime date { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters")]
        public string location { get; set; }

        [Required(ErrorMessage = "Number of seats is required")]
        [Range(5000, 200000, ErrorMessage = "Number of seats must be between 5000 and 200000")]
        [Display(Name = "Number of Seats")]
        public int numberOfSeats { get; set; }

        [Required(ErrorMessage = "Team 1 name is required")]
        [StringLength(50, ErrorMessage = "Team name cannot exceed 50 characters")]
        [Display(Name = "Team 1 Name")]
        public string team1 { get; set; }

        [Required(ErrorMessage = "Team 2 name is required")]
        [StringLength(50, ErrorMessage = "Team name cannot exceed 50 characters")]
        [Display(Name = "Team 2 Name")]
        public string team2 { get; set; }

        [Required(ErrorMessage = "Team 1 image is required")]
        [Display(Name = "Team 1 Image")]
        public IFormFile team1Image_upload { get; set; }

        [Required(ErrorMessage = "Team 2 image is required")]
        [Display(Name = "Team 2 Image")]
        public IFormFile team2Image_upload { get; set; }

        public string? team1Image { get; set; }
        public string? team2Image { get; set; }

        [Required(ErrorMessage = "Tournament name is required")]
        [StringLength(100, ErrorMessage = "Tournament name cannot exceed 100 characters")]
        [Display(Name = "Tournament Name")]
        public string tournament { get; set; }

        [Required(ErrorMessage = "Sport type is required")]
        [StringLength(50, ErrorMessage = "Sport type cannot exceed 50 characters")]
        [Display(Name = "Sport Type")]
        public string sport { get; set; }

        [Required(ErrorMessage = "Tournament stage is required")]
        [StringLength(50, ErrorMessage = "Tournament stage cannot exceed 50 characters")]
        [Display(Name = "Tournament Stage")]
        public string tournamentStage { get; set; }
    }
}

