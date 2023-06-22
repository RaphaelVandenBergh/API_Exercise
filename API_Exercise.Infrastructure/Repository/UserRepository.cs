using API_Exercise.Domain.Models;
using API_Exercise.Domain.Repository;
using API_Exercise.Infrastructure.Data;
using AutoMapper;
using Dapper;
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
            /////////////////////////////////////////////////////////////////////////////////////
            //THIS SOLUTION USES THE MULTIPLE QUERY METHOD TO GET ALL USERS, COMPANIES AND TIME REGISTRATIONS
            var sql = @"SELECT * FROM Users;
                        SELECT * FROM Companies
                        SELECT * FROM TimeRegistrations";

            using (var connection = _dapperContext.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(sql))
                {
                    var users = multi.Read<User>().ToList();
                    var companies = multi.Read<Company>().ToList();
                    var timeRegistrations = multi.Read<TimeRegistration>().ToList();

                    foreach (var user in users)
                    {
                        user.Company = companies.FirstOrDefault(c => c.Id == user.CompanyId);
                        user.TimeRegistration = timeRegistrations.Where(t => t.UserId == user.Id).ToList();
                    }
                    return users;
                }
            }
            //////////////////////////////////////////////////////////////////////////////////////


            /////////////////////////////////////////////////////////////////////////////////////
            //THIS SOLUTION USES THE GROUPBY METHOD TO GROUP THE TIME REGISTRATIONS FOR EACH USER
            //var sql = @"SELECT * FROM Users LEFT JOIN Companies ON Users.CompanyId = Companies.Id LEFT JOIN TimeRegistrations ON Users.Id = TimeRegistrations.UserId;";

            //using (var connection = _dapperContext.CreateConnection())
            //{
            //    var users = await connection.QueryAsync<User, Company, TimeRegistration, User>(sql, (user, company, timeRegistration) =>
            //    {
            //        user.Company = company;
            //        user.TimeRegistration.Add( timeRegistration);
            //        return user;
            //    }, splitOn: "Id, Id");
            //    var result = users.GroupBy(u => u.Id).Select(g =>
            //    {
            //        var groupedUser = g.First();
            //        groupedUser.TimeRegistration = g.Select(u => u.TimeRegistration.Single()).ToList();
            //        return groupedUser;
            //    });
            //    return result;
            //}
            //////////////////////////////////////////////////////////////////////////////////////

            //var users = await _context.Users.Include(u => u.Company).Include(u => u.TimeRegistration).ToListAsync();

            //return users;
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