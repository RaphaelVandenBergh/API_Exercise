using API_Exercise.Domain.Models;

namespace API_Exercise.Domain.Repository
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompanies();

        Task<Company> GetCompanyById(int id);

        Task<Company> AddCompany(Company company);

        Task<Company> DeleteCompany(int id);

        Task<Company> UpdateCompany(int id, Company company);
    }
}