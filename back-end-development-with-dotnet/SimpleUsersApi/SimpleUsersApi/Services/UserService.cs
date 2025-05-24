using SimpleUsersApi.Models;

namespace SimpleUsersApi.Services
{
    public class UserService : IUserService
    {
        private static readonly List<User> _users = new();
        private static int _nextId = 1;

        public List<User> GetAll()
        {
            return _users.ToList();
        }

        public User? GetById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public User Create(User user)
        {
            var newUser = new User
            {
                Id = _nextId++,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };
            _users.Add(newUser);
            return newUser;
        }

        public bool Update(int id, User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
                return false;

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            return true;
        }

        public bool Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return false;

            _users.Remove(user);
            return true;
        }
    }
}
