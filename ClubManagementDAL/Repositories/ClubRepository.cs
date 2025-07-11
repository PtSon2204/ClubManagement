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
    }
}
