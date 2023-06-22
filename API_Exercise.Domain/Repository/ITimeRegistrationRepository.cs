using API_Exercise.Domain.Models;

namespace API_Exercise.Domain.Repository
{
    public interface ITimeRegistrationRepository
    {
        Task<TimeRegistration> CreateTimeRegistration(TimeRegistration timeRegistration);
        Task<TimeRegistration> DeleteTimeRegistration(int id);
        Task<IEnumerable<TimeRegistration>> GetAllTimeRegistrations();
        Task<IEnumerable<TimeRegistration>> GetAllTimeRegistrationsForUser(int userid, int page, int pagesize);
        Task<IEnumerable<TimeRegistration>> GetAllTimeRegistrationsForCompany(int companyId);
        Task<TimeRegistration> UpdateTimeRegistration(int id, TimeRegistration timeRegistration);
        Task<TimeRegistration> GetTimeRegistrationById(int id);
    }
}