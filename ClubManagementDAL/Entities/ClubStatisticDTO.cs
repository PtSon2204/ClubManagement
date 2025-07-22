using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubManagement.DAL.Entities
{
    public class ClubStatisticDTO
    {
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public int Count { get; set; }
    }
}
