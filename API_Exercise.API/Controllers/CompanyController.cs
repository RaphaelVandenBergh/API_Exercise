using API_Exercise.API.DTOs;
using API_Exercise.Domain.Models;
using API_Exercise.Domain.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Exercise.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        // GET: api/<CompanyController>
        [HttpGet]
        public async Task<ActionResult<List<CompanyDTO>>> Get()
        {
            try
            {
                var companies = await _companyRepository.GetAllCompanies();
                var companiesDTO = _mapper.Map<List<CompanyDTO>>(companies);
                return Ok(companiesDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        // GET api/<CompanyController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDTO>> Get(int id)
        {
            var company = await _companyRepository.GetCompanyById(id);
            if (company == null)
            {
                return NotFound();
            }
            var companyDTO = _mapper.Map<CompanyDTO>(company);
            return Ok(companyDTO);
        }

        // POST api/<CompanyController>
        [HttpPost]
        public async Task<ActionResult<CompanyDTO>> Post(Company company)
        {
            var returned = _companyRepository.AddCompany(company).Result;
            var companyDTO = _mapper.Map<CompanyDTO>(returned);
            return Created($"id:{company.Id}", companyDTO);
        }

        // PUT api/<CompanyController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CompanyDTO>> Put(int id, Company company)
        {
            var updatedCompany = _companyRepository.UpdateCompany(id, company).Result;
            if (updatedCompany == null)
            {
                return NotFound();
            }
            var companyDTO = _mapper.Map<CompanyDTO>(updatedCompany);
            return Ok(companyDTO);
        }

        // DELETE api/<CompanyController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CompanyDTO>> Delete(int id)
        {
            var company = _companyRepository.DeleteCompany(id).Result;
            if (company == null)
            {
                return NotFound();
            }
            var companyDTO = _mapper.Map<CompanyDTO>(company);
            return Ok(companyDTO);
        }
    }
}