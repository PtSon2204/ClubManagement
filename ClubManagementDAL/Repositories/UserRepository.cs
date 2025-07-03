using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubManagement.DAL.Entities;

namespace ClubManagement.DAL.Repositories
{
    public class UserRepository
    {
        private ClubManagementContext? _context; //khi dùng thì mới new()

        public User? GetAccountRepo(string email, string password)
        {
            _context = new();
            return _context.Users.FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
        }
    }
}
