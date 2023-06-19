using API_Exercise.API.DTOs;
using API_Exercise.Domain.Models;
using API_Exercise.Domain.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Exercise.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeRegistryController : ControllerBase
    {
        private readonly ITimeRegistrationRepository _timeRegistrationRepository;
        private readonly IMapper _mapper;
        public TimeRegistryController(ITimeRegistrationRepository timeRegistrationRepository, IMapper mapper)
        {
            _timeRegistrationRepository = timeRegistrationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TimeRegistration>>> GetAllTimeRegistrations()
        {
            try
            {
                var timeRegistrations = await _timeRegistrationRepository.GetAllTimeRegistrations();
                var TimeRegistrationsDTO = _mapper.Map<List<TimeRegistrationDTO>>(timeRegistrations);
                return Ok(TimeRegistrationsDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TimeRegistration>> CreateTimeRegistration(TimeRegistrationPostDTO timeRegistration)
        {
            var input = _mapper.Map<TimeRegistration>(timeRegistration);
            var returned = _timeRegistrationRepository.CreateTimeRegistration(input).Result;
            var timeRegistrationDTO = _mapper.Map<TimeRegistrationDTO>(returned);
            return Created($"id:{timeRegistrationDTO.Id}", timeRegistrationDTO);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<TimeRegistration>> UpdateTimeRegistration(int id, TimeRegistrationPutDTO timeRegistration)
        {
            if (id != timeRegistration.Id)
            {
                return BadRequest();
            }
            var input = _mapper.Map<TimeRegistration>(timeRegistration);
            var returned = _timeRegistrationRepository.UpdateTimeRegistration(id, input).Result;
            var timeRegistrationDTO = _mapper.Map<TimeRegistrationDTO>(returned);
            return Ok(timeRegistrationDTO);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<TimeRegistration>> DeleteTimeRegistration(int id)
        {
            var returned = _timeRegistrationRepository.DeleteTimeRegistration(id).Result;
            var timeRegistrationDTO = _mapper.Map<TimeRegistrationDTO>(returned);
            return Ok(timeRegistrationDTO);
        }

        [HttpGet]
        [Route("user/{id}")]
        public async Task<ActionResult<List<TimeRegistration>>> GetAllTimeRegistrationsForUser(int id)
        {
            try
            {
                var timeRegistrations = await _timeRegistrationRepository.GetAllTimeRegistrationsForUser(id);
                var TimeRegistrationsDTO = _mapper.Map<List<TimeRegistrationDTO>>(timeRegistrations);
                return Ok(TimeRegistrationsDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("company/{id}")]
        public async Task<ActionResult<List<TimeRegistration>>> GetAllTimeRegistrationsForCompany(int id)
        {
            try
            {
                var timeRegistrations = await _timeRegistrationRepository.GetAllTimeRegistrationsForCompany(id);
                var TimeRegistrationsDTO = _mapper.Map<List<TimeRegistrationDTO>>(timeRegistrations);
                return Ok(TimeRegistrationsDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

    }
}
