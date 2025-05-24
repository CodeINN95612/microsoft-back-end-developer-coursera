using SimpleUsersApi.Models;

namespace SimpleUsersApi.Services
{
    public interface IUserService
    {
        List<User> GetAll();
        User? GetById(int id);
        User Create(User user);
        bool Update(int id, User user);
        bool Delete(int id);
    }
}
