using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubManagement.DAL.Entities;

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
    }
}

