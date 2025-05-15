namespace WebApplication2.ViewModels
{
    public class ApplicationUserVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool isDeleted { get; set; }
        public string acceptanceStatus { get; set; } 
    }
} 