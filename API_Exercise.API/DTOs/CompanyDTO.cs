namespace API_Exercise.API.DTOs
{
    public class CompanyDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<UserDTO>? Users { get; set; }
    }
}
