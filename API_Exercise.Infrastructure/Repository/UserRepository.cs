using API_Exercise.Domain.Models;
using API_Exercise.Domain.Repository;
using API_Exercise.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API_Exercise.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly DapperContext _dapperContext;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, DapperContext dapperContext, IMapper mapper)
        {
            _context = context;
            _dapperContext = dapperContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            //DAPPER QUERY
            //using (var connection = _dapperContext.CreateConnection())
            //{
            //    string query = "SELECT * FROM Users u LEFT JOIN TimeRegistrations t ON u.Id = t.UserId";
            //    connection.Open();
            //    var queryResult = await connection.QueryAsync<User, TimeRegistration, User>(query, (user, timeRegistration) =>
            //    {
            //        user.TimeRegistration.Add(timeRegistration);

            //        return user;
            //    }, splitOn: "Id");

            //    return queryResult;
            //}

            var users = await _context.Users.Include(u => u.Company).Include(u => u.TimeRegistration).ToListAsync();

            return users;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.Include(u => u.Company).Include(u => u.TimeRegistration).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<User> AddUser(User user)
        {
            var returneduser = _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateUser(int id, User user)
        {
            var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            userToUpdate.Name = user.Name;
            userToUpdate.CompanyId = user.CompanyId;
            await _context.SaveChangesAsync();

            return userToUpdate;
        }
    }
}