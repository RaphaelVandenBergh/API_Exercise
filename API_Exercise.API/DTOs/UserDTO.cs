namespace API_Exercise.API.DTOs
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long CompanyId { get; set; }
        
        public String? CompanyName { get; set; }

        public ICollection<TimeRegistrationDTO>? TimeRegistration { get; set; } = new List<TimeRegistrationDTO>();
    }
}
