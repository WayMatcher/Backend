using WayMatcherBL.Models;

namespace WayMatcherBL.Interfaces
{
    public interface IDatabaseService
    {
        public bool CreateUser(User user);
        public bool UpdateUser(User user);
        public User GetUserById(int id);
        public List<User> GetActiveUser();
    }
}
