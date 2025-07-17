using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubManagement.DAL.Entities;
using ClubManagement.DAL.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace ClubManagement.DLL.Services
{
    public class UserService
    {
        private UserRepository _repo = new();

        public User? GetAccount(string email, string plainPassword)
        {
            var user = _repo.GetAccountRepo(email);
            if (user == null)
                return null;

            // So sánh mật khẩu người dùng nhập với mật khẩu đã hash trong DB
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(plainPassword, user.Password);
            if (isPasswordCorrect)
                return user;

            return null;
        }

        public List<User> GetUserInClub(int clubId)
        {
            return _repo.GetMembersOfMyClub(clubId);
        }
        public List<User> GetAllUser()
        {
            return _repo.GetAll();
        }

        public void AddUser(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _repo.Add(user);
        }

        public void UpdateUser(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _repo.Update(user);
        }

        public void DeleteUser(User user)
        {
            _repo.Delete(user);
        }

        public List<User> SearchUserByNameOrEmail(string name, string email)
        {
            List<User> result = _repo.GetAll();
            if (name.IsNullOrEmpty() && email.IsNullOrEmpty())
            {
                return result;
            }
            if (!email.IsNullOrEmpty() && !name.IsNullOrEmpty())
            {
                result = result.Where(x => x.FullName.ToLower().Contains(name.ToLower()) || x.Email.ToLower().Contains(email.ToLower())).ToList();
            }
            else if (!email.IsNullOrEmpty()) //1 trong 2 th đang khác rỗng
            { 
                result = result.Where(x => x.Email.ToLower().Contains(email.ToLower())).ToList();
            }
            else 
            {
                result = result.Where(x => x.FullName.ToLower().Contains(name.ToLower())).ToList(); ;
            }

            return result;
        }
    }
}
