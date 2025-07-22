using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubManagement.DAL.Repositories
{
    public class ClubRepository
    {
        private ClubManagementContext? _context; //khi dùng thì mới new()

        public List<Club> GetAll()
        {
            _context = new();
            return _context.Clubs.ToList();
        }
        public void Add(Club club)
        {
            _context = new();
            _context.Clubs.Add(club);
            _context.SaveChanges();
        }

        public void Update(Club club)
        {
            _context = new();
            _context.Clubs.Update(club);
            _context.SaveChanges();
        }

        public void Delete(Club club)
        {
            _context = new();
            _context.Clubs.Remove(club);
            _context.SaveChanges();
        }
        public List<ClubStatisticDTO> GetClubMemberStatistics()
        {
            _context = new();

            return _context.Clubs
                .Select(c => new ClubStatisticDTO
                {
                    ClubId = c.ClubId,
                    ClubName = c.ClubName,
                    Count = c.Users.Count() // tự join theo FK
                })
                .ToList();
        }
        public List<ClubStatisticDTO> GetClubEventStatistics()
        {
            _context = new();

            return _context.Clubs
                .Select(c => new ClubStatisticDTO
                {
                    ClubId = c.ClubId,
                    ClubName = c.ClubName,
                    Count = c.Events.Count() // tự join theo FK
                })
                .ToList();
        }

        //thống kê 
        public List<ClubStatisticDTO> GetClubTotalMemberByClubId(int clubId)
        {
            _context = new();
            return _context.Clubs
                .Where(c => c.ClubId == clubId)
                .Select(c => new ClubStatisticDTO
                {
                    ClubId = c.ClubId,
                    ClubName = c.ClubName,
                    Count = c.Users.Count()
                }).ToList();
        }

        public List<ClubStatisticDTO> GetClubTotalEventByClubId(int clubId)
        {
            _context = new();
            return _context.Clubs
                .Where(c => c.ClubId == clubId)
                .Select(c => new ClubStatisticDTO
                {
                    ClubId = c.ClubId,
                    ClubName = c.ClubName,
                    Count = c.Events.Count()
                }).ToList();
        }
    }
}

