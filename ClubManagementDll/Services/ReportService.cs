using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubManagement.DAL.Entities;
using ClubManagement.DAL.Repositories;

namespace ClubManagement.DLL.Services
{
    public class ReportService
    {
       private ReportRepository _repo = new();

        public List<Report> GetAllReport()
        {
            return _repo.GetAll();
        }

        public void AddReport(Report report)
        {
            _repo.Add(report);
        }

        public void UpdateReport(Report report)
        {
            _repo.Update(report);
        }

        public void DeleteReport(Report report)
        {
            _repo.Delete(report);
        }
    }
}
