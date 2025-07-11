using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubManagement.DAL.Entities;
using ClubManagement.DAL.Repositories;

namespace ClubManagement.DLL.Services
{
    public class ClubService
    {
        private ClubRepository _repo = new();

        public List<Club> GetAllClub()
        {
            return _repo.GetAll();
        }
    }
}
