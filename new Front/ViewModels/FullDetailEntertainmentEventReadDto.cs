namespace WebApplication2.ViewModels
{
    public class FullDetailEntertainmentEventReadDto
    {
        public int Id { get; set; }
        public string performerName { get; set; }
        public string showCategory { get; set; }
        public int ageRestriction { get; set; }
        public double duration { get; set; }
        public string genre { get; set; }
        public string day { get; set; }
        public string mounth { get; set; }  // Consider renaming to "Month" if it was a typo
        public string year { get; set; }
        public string location { get; set; }
        public int aviilableSeats { get; set; } // Consider fixing spelling: "AvailableSeats"
        public string eventImage { get; set; }
        public DateTime date { get; set; }
    }
}
