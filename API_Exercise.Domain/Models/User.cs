namespace API_Exercise.Domain.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long CompanyId { get; set; }
        
        public Company? Company { get; set; }

        public ICollection<TimeRegistration>? TimeRegistration { get; set; } = new List<TimeRegistration>();

    }
}