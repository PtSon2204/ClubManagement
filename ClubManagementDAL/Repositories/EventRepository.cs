using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubManagement.DAL.Repositories
{
    public class EventRepository
    {
        private ClubManagementContext? _context;

        public List<Event> GetAll()
        {
            _context = new();
            return _context.Events.Include("Club").ToList();
        }

        public List<Event> GetEventInClubId(int clubId)
        {
            _context = new();
            return _context.Events.Where(x => x.ClubId == clubId).ToList();
        }

        public void Add(Event eventA)
        {
            _context = new();
            _context.Events.Add(eventA);
            _context.SaveChanges();
        }

        public void Update(Event eventU)
        {
            _context = new();
            _context.Events.Update(eventU);
            _context.SaveChanges();
        }

        public void Delete(Event eventD)
        {
            _context = new();
            _context.Events.Remove(eventD);
            _context.SaveChanges();
        }
    }
}
