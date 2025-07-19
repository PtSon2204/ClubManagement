using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubManagement.DAL.Entities;
using ClubManagement.DAL.Repositories;
using Microsoft.Extensions.Logging;

namespace ClubManagement.DLL.Services
{
    public class EventService
    {
        private EventRepository _repo = new();

        public List<Event> GetAllEvent()
        {
            return _repo.GetAll();
        }
        
        public List<Event> GetAllEventByClubId(int clubId)
        {
            return _repo.GetEventInClubId(clubId);
        }

        public void AddEvent(Event events)
        {
            _repo.Add(events);
        }

        public void UpdateEvent(Event events)
        {
            _repo.Update(events);
        }

        public void DeleteEvent(Event events)
        {
            _repo.Delete(events);
        }

        public List<Event> SearchClubByNameOrEstabDate(string name, string dateString)
        {
            List<Event> result = _repo.GetAll();

            bool hasName = !string.IsNullOrWhiteSpace(name);
            bool hasDate = DateTime.TryParse(dateString, out DateTime eventDate);

            if (!hasName && !hasDate)
            {
                return result;
            }

            if (hasName && hasDate)
            {
                result = result.Where(x =>
                    x.EventName.ToLower().Contains(name.ToLower()) ||
                    x.EventDate == eventDate).ToList();
            }
            else if (hasDate)
            {
                result = result.Where(x => x.EventDate == eventDate).ToList();
            }
            else // chỉ có name
            {
                result = result.Where(x => x.EventName.ToLower().Contains(name.ToLower())).ToList();
            }

            return result;
        }
    }
}
