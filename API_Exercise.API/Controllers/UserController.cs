using API_Exercise.API.DTOs;
using API_Exercise.Domain.Models;
using API_Exercise.Domain.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API_Exercise.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllUsers();
                var usersDTO = _mapper.Map<List<UserDTO>>(users);
                return Ok(usersDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddUser(User user)
        {
            var returned = _userRepository.AddUser(user).Result;
            var userDTO = _mapper.Map<UserDTO>(returned);
            return Created($"id:{user.Id}", userDTO);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<UserDTO>> DeleteUser(int id)
        {
            var users = _userRepository.DeleteUser(id).Result;
            if (users == null)
            {
                return NotFound();
            }
            var userDTO = _mapper.Map<UserDTO>(users);
            return Ok(userDTO);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<UserDTO>> UpdateUser(int id, User user)
        {
            var users = _userRepository.UpdateUser(id, user).Result;
            if (users == null)
            {
                return NotFound();
            }
            var userDTO = _mapper.Map<UserDTO>(users);
            return Ok(userDTO);
        }
    }
}