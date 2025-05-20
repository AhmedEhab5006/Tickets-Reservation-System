namespace WebApplication2.ViewModels
{
    public class FullDetailSportEventReadDto
    {
        public int Id { get; set; }
        public string team1 { get; set; }
        public string team2 { get; set; }
        public string tournament { get; set; }
        public string sport { get; set; }
        public string tournamentStage { get; set; }
        public string day { get; set; }
        public string mounth { get; set; }
        public string year { get; set; }
        public string location { get; set; }
        public int numberOfSeats { get; set; }
        public string team1Image { get; set; }
        public string team2Image { get; set; }
    }
} 