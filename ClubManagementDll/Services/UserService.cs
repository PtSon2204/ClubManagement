using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubManagement.DAL.Entities;
using ClubManagement.DAL.Repositories;

namespace ClubManagement.DLL.Services
{
    public class UserService
    {
        private UserRepository _repo = new();

        public User? GetAccount(string email, string password)
        {
            return _repo.GetAccountRepo(email, password);
        }

        public List<User> GetAllUser()
        {
            return _repo.GetAll();
        }

        public void AddUser(User user)
        {
            _repo.Add(user);
        }

        public void UpdateUser(User user)
        {
            _repo.Update(user);
        }

        public void DeleteUser(User user)
        {
            _repo.Delete(user);
        }
    }
}
