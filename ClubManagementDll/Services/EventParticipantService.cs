using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubManagement.DAL.Entities;
using ClubManagement.DAL.Repositories;

namespace ClubManagement.DLL.Services
{
    public class EventParticipantService
    {
        private EventParticipantRepository _repo = new();

        public List<EventParticipant> GetAllEventPar()
        {
            return _repo.GetAll();
        }

        public void AddEventPar(EventParticipant x)
        {
            _repo.Add(x);
        }

        public void UpdateEventPar(EventParticipant x)
        {
            _repo.Update(x);
        }

        public void DeleteEventPar(EventParticipant x)
        {
            _repo.Delete(x);
        }

        public List<EventParticipant> SearchByNameAndStatus(string name, string status)
        {
            List<EventParticipant> result = _repo.GetAll(); 

            if (!string.IsNullOrWhiteSpace(name))
            {
                result = result.Where(p => p.User != null && p.User.FullName.ToLower().Contains(name.ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                result = result.Where(p => p.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return result;
        }
    }
}
