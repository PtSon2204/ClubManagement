using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubManagement.DAL.Repositories
{
    public class EventParticipantRepository
    {
        private ClubManagementContext? _context; //khi dùng thì mới new()

        public List<EventParticipant> GetAll()
        {
            _context = new();
            return _context.EventParticipants.Include("Event").Include("User").ToList();
        }

        public List<EventParticipant> GetAllEventParByClubId(int clubId)
        {
            _context = new();
            return _context.EventParticipants.Include("Event").Include("User").Where(x => x.Event.ClubId == clubId).ToList();
        }
        public List<string> GetAllStatus()
        {
            _context = new();
            return _context.EventParticipants.Select(x => x.Status).Distinct().ToList();
        }
        public void Add(EventParticipant x)
        {
            _context = new();
            _context.EventParticipants.Add(x);
            _context.SaveChanges();
        }

        public void Update(EventParticipant x)
        {
            _context = new();
            _context.EventParticipants.Update(x);
            _context.SaveChanges();
        }

        public void Delete(EventParticipant x)
        {
            _context = new();
            _context.EventParticipants.Remove(x);
            _context.SaveChanges();
        }
    }
}
