namespace API_Exercise.API.DTOs
{
    public class TimeRegistrationPutDTO
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public long UserId { get; set; }
    }
}
