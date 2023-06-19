using API_Exercise.Domain.Models;

namespace API_Exercise.Domain.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUserById(int id);

        Task<User> AddUser(User user);

        Task<User> DeleteUser(int id);

        Task<User> UpdateUser(int id, User user);
    }
}