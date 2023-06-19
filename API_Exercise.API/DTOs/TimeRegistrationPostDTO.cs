namespace API_Exercise.API.DTOs
{
    public class TimeRegistrationPostDTO
    {
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public long UserId { get; set; }
    }
}
