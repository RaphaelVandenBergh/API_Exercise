using API_Exercise.Domain.Models;
using API_Exercise.Domain.Repository;
using API_Exercise.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API_Exercise.Infrastructure.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CompanyRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<Company> AddCompany(Company company)
        {
            var returned = _dataContext.Companies.Add(company).Entity;
            await _dataContext.SaveChangesAsync();

            return returned;
        }

        public async Task<Company> DeleteCompany(int id)
        {
            var company = await _dataContext.Companies.FirstOrDefaultAsync(c => c.Id == id);
            if (company == null)
            {
                return null;
            }
            _dataContext.Companies.Remove(company);
            await _dataContext.SaveChangesAsync();

            return company;
        }

        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            var companies = await _dataContext.Companies.Include(u => u.Users).ToListAsync();

            return companies;
        }

        public async Task<Company> GetCompanyById(int id)
        {
            var company = await _dataContext.Companies.FirstOrDefaultAsync(c => c.Id == id);

            return company;
        }

        public async Task<Company> UpdateCompany(int id, Company company)
        {
            var companyToUpdate = await _dataContext.Companies.FirstOrDefaultAsync(c => c.Id == id);
            if (companyToUpdate == null)
            {
                return null;
            }
            companyToUpdate.Name = company.Name;
            await _dataContext.SaveChangesAsync();

            return company;
        }
    }
}