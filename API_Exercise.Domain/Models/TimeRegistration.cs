namespace API_Exercise.Domain.Models
{
    public class TimeRegistration
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }


        public long UserId { get; set; }
        public User? User { get; set; }
    }
}