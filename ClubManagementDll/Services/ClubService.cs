﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubManagement.DAL.Entities;
using ClubManagement.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ClubManagement.DLL.Services
{
    public class ClubService
    {
        private ClubRepository _repo = new();

        public List<Club> GetAllClub()
        {
            return _repo.GetAll();
        }
        public void AddClub(Club club)
        {
            _repo.Add(club);
        }

        public void UpdateClub(Club club)
        {
            _repo.Update(club);
        }

        public void DeleteClub(Club club)
        {
            _repo.Delete(club);
        }

        public List<Club> SearchClubByNameOrEstabDate(string name, string dateString)
        {
            List<Club> result = _repo.GetAll();

            bool hasName = !string.IsNullOrWhiteSpace(name);
            bool hasDate = DateOnly.TryParse(dateString, out DateOnly estabDate);

            if (!hasName && !hasDate)
            {
                return result;
            }

            if (hasName && hasDate)
            {
                result = result.Where(x =>
                    x.ClubName.ToLower().Contains(name.ToLower()) ||
                    x.EstablishedDate == estabDate).ToList();
            }
            else if (hasDate)
            {
                result = result.Where(x => x.EstablishedDate == estabDate).ToList();
            }
            else // chỉ có name
            {
                result = result.Where(x => x.ClubName.ToLower().Contains(name.ToLower())).ToList();
            }

            return result;
        }
        public List<ClubStatisticDTO> GetClubTotalMember()
        {
            return _repo.GetClubMemberStatistics();
        }

        public List<ClubStatisticDTO> GetClubTotalEvent()
        {
            return _repo.GetClubEventStatistics();
        }

        public List<ClubStatisticDTO> GetClubStatisticMemberByClubId(int clubId)
        {
            return _repo.GetClubTotalMemberByClubId(clubId);
        }

        public List<ClubStatisticDTO> GetClubStatisticEventByClubId(int clubId)
        {
            return _repo.GetClubTotalEventByClubId(clubId);
        }


        //Cách 2:
        //public List<Club> SearchClubByNameOrEstabDate(string name, DateOnly? estabDate)
        //{
        //    List<Club> result = _repo.GetAll();

        //    if (string.IsNullOrEmpty(name) && estabDate == null)
        //    {
        //        return result;
        //    }

        //    if (!string.IsNullOrEmpty(name) && estabDate != null)
        //    {
        //        result = result.Where(x =>
        //            x.ClubName.ToLower().Contains(name.ToLower()) ||
        //            x.EstablishedDate == estabDate.Value
        //        ).ToList();
        //    }
        //    else if (estabDate != null)
        //    {
        //        result = result.Where(x => x.EstablishedDate == estabDate.Value).ToList();
        //    }
        //    else // chỉ có name
        //    {
        //        result = result.Where(x => x.ClubName.ToLower().Contains(name.ToLower())).ToList();
        //    }

        //    return result;
        //}
    }
}
