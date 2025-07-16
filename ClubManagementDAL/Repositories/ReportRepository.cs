using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubManagement.DAL.Repositories
{
    public class ReportRepository
    {
        private ClubManagementContext? _context; //khi dùng thì mới new()

        public List<Report> GetAll()
        {
            _context = new();
            return _context.Reports.Include("Club").ToList();
        }

        public void Add(Report report)
        {
            _context = new();
            _context.Reports.Add(report);
            _context.SaveChanges();
        }

        public void Update(Report report)
        {
            _context = new();
            _context.Reports.Update(report);
            _context.SaveChanges();
        }

        public void Delete(Report report)
        {
            _context = new();
            _context.Reports.Remove(report);
            _context.SaveChanges();
        }


    }
}
