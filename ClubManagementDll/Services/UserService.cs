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

        public List<User> GetUserInClub(int userId)
        {
            return _repo.GetMembersOfMyClub(userId);
        }
        public List<User> GetAllUser()
        {
            return _repo.GetAll();
        }

        /*
         $2a$	Phiên bản thuật toán Bcrypt
         $2b$	Phiên bản cải tiến, sửa lỗi tràn bộ nhớ nhỏ
         $2y$	Dành cho hệ thống Unix (BSD), tương đương $2a$ nhưng khác tên
          11	Cost factor: chạy 2^11 = 2048 vòng lặp
         X4Tfuz9l3BC2vN5pe0hM5u	Salt (chuỗi ngẫu nhiên, 128-bit)
         K4/2zEF1D8U8k.X9a3EY6sszTxH9eVS	Mã hash thực sự (kết quả của thuật toán Blowfish)
         */
        public void AddUser(User user)
        {
            if (!user.Password.StartsWith("$2a$") && !user.Password.StartsWith("$2b$"))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }
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
