﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubManagement.DAL.Repositories
{
    public class UserRepository
    {
        private ClubManagementContext? _context; //khi dùng thì mới new()

        public User? GetAccountRepo(string email)
        {
            _context = new();
            return _context.Users.FirstOrDefault(x => x.Email.Equals(email));
        }

        public List<User> GetMembersOfMyClub(int currentUserId)
        {
            _context = new();
            {
                // Lấy ClubID của user hiện tại nếu là Chairman
                var myClubId = _context.Users
                                .Where(u => u.UserId == currentUserId && u.Role == "Chairman")
                                .Select(u => u.ClubId)
                                .FirstOrDefault();

                if (myClubId == 0)
                    return new List<User>();

                // Lấy danh sách các thành viên thuộc CLB đó
                return _context.Users
                              .Where(u => u.ClubId == myClubId)
                              .ToList();
            }
        }

        public List<User> GetAll()
        {
            _context = new();
            return _context.Users.Include("Club").ToList();
        }

        public void Add(User user)
        {
            _context = new();
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context = new();
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context = new();
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
