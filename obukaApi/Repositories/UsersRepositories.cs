using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UsersRepositories
    {
        private readonly BazaContext _context;
        public UsersRepositories(BazaContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public void CreateUser(User user) 
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int id) 
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
