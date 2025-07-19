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

        public List<Report> SearchByDate(DateTime fromDate, DateTime toDate) 
        {
            var result = _repo.GetAll();

            return result.Where(r => r.CreatedDate.Value >= fromDate.Date && r.CreatedDate.Value <= toDate.Date).ToList(); ;
        }

        public List<Report> GetReportsByClubId(int clubId)
        {
            return _repo.GetAllReportByClubId(clubId);
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
