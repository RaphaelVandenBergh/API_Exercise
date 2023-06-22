using API_Exercise.Domain.Models;
using API_Exercise.Domain.Repository;
using API_Exercise.Infrastructure.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Exercise.Infrastructure.Repository
{
    public class TimeRegistrationRepository : ITimeRegistrationRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly DataContext _dataContext;

        public TimeRegistrationRepository(DapperContext dapperContext, DataContext dataContext)
        {
            _dapperContext = dapperContext;
            _dataContext = dataContext;

        }

        public async Task<IEnumerable<TimeRegistration>> GetAllTimeRegistrations()
        {
            var sql = @"SELECT * FROM TimeRegistrations LEFT JOIN Users ON TimeRegistrations.UserId = Users.Id;";

            using (var connection = _dapperContext.CreateConnection())
            {
                var timeRegistrations = await connection.QueryAsync<TimeRegistration, User, TimeRegistration>(sql, (timeRegistration, user) =>
                {
                    timeRegistration.User = user;
                    return timeRegistration;
                }, splitOn: "Id");
                return timeRegistrations;
            }

            //var timeRegistrations = await _dataContext.TimeRegistrations.Include(t => t.User).ToListAsync();

            //return timeRegistrations;
        }

        public async Task<TimeRegistration> GetTimeRegistrationById(int id)
        {
            var timeRegistration = await _dataContext.TimeRegistrations.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);

            return timeRegistration;
        }

        public async Task<IEnumerable<TimeRegistration>> GetAllTimeRegistrationsForUser(int userid, int page=1, int pagesize=1)
        {
            var offset = (page - 1) * pagesize;
            var sql = @"SELECT * FROM TimeRegistrations LEFT JOIN Users ON TimeRegistrations.UserId = Users.Id WHERE UserId = @userid ORDER BY TimeRegistrations.Start OFFSET @offset ROWS FETCH NEXT @pagesize ROWS ONLY";
            
            using (var connection = _dapperContext.CreateConnection())
            {
                var timeRegistrations = await connection.QueryAsync<TimeRegistration, User, TimeRegistration>(sql, (timeRegistration, user) =>
                {
                    timeRegistration.User = user;
                    return timeRegistration;
                }, new { userid , offset, pagesize }, splitOn: "Id");

                return timeRegistrations;
            }
            

            //var timeRegistrations = await _dataContext.TimeRegistrations.Include(t => t.User).Where(t => t.UserId == userid).ToListAsync();

            //return timeRegistrations;
        }

        public async Task<TimeRegistration> CreateTimeRegistration(TimeRegistration timeRegistration)
        {
            _dataContext.TimeRegistrations.Add(timeRegistration);
            await _dataContext.SaveChangesAsync();

            return timeRegistration;
        }

        public async Task<TimeRegistration> DeleteTimeRegistration(int id)
        {
            var timeRegistrationToDelete = await _dataContext.TimeRegistrations.FirstOrDefaultAsync(t => t.Id == id);
            if (timeRegistrationToDelete == null)
            {
                return null;
            }
            _dataContext.TimeRegistrations.Remove(timeRegistrationToDelete);
            await _dataContext.SaveChangesAsync();
            return timeRegistrationToDelete;
        }

        public async Task<TimeRegistration> UpdateTimeRegistration(int id, TimeRegistration timeRegistration)
        {
            var timeRegistrationToUpdate = await _dataContext.TimeRegistrations.FirstOrDefaultAsync(t => t.Id == id);
            if (timeRegistrationToUpdate == null)
            {
                return null;
            }

            timeRegistrationToUpdate.Start = timeRegistration.Start;
            timeRegistrationToUpdate.End = timeRegistration.End;
            timeRegistrationToUpdate.Description = timeRegistration.Description;

            await _dataContext.SaveChangesAsync();

            return timeRegistrationToUpdate;
        }

        public async Task<IEnumerable<TimeRegistration>> GetAllTimeRegistrationsForCompany(int companyId)
        {
            string sql = @"SELECT * FROM TimeRegistrations LEFT JOIN Users ON TimeRegistrations.UserId = Users.Id WHERE UserId IN (SELECT Id FROM Users WHERE CompanyId = @CompanyId);";

            using (var connection = _dapperContext.CreateConnection())
            {
                var timeRegistrations = await connection.QueryAsync<TimeRegistration, User, TimeRegistration>(sql, (timeRegistration, user) =>
                {
                    timeRegistration.User = user;
                    return timeRegistration;
                }, new { companyId }, splitOn: "Id");
                return timeRegistrations;
            }

           //var timeRegistrations = await _dataContext.TimeRegistrations.Include(t => t.User).Where(t => t.User.CompanyId == companyId).ToListAsync();

           // return timeRegistrations;
        }
    }
}
