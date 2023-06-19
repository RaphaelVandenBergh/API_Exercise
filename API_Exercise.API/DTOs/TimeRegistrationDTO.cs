namespace API_Exercise.API.DTOs
{
    public class TimeRegistrationDTO
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string UserName { get; set; }

    }
}
