using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.Committee;
using EMS_SYSTEM.DOMAIN.DTO.ObserversAndInvigilators;
using EMS_SYSTEM.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.APPLICATION.Repositories.Services
{
    public class ObserversAndInvigilatorsService:GenericRepository<Staff>,IObserversAndInvigilatorsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ObserversAndInvigilatorsService(UnvcenteralDataBaseContext Db, IUnitOfWork unitOfWork) : base(Db)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> GetByNID(string id)
        {
            var staff = _context.Staff.Where(s => s.NID == id).Select(s=> new ObserversAndInvigilatorsDTO
            {
                Name = s.Name,
                FacultyName = s.Faculty.FacultyName,
                FacultyId = s.Faculty.Id,
                Degree = s.Degree
            });

            if (staff != null)
            {
                ObserversAndInvigilatorsDTO data = new ObserversAndInvigilatorsDTO();
                return new ResponseDTO
                {
                    Model = staff,
                    StatusCode = 200,
                    IsDone = true,
                };
            }
            return new ResponseDTO
            {
                StatusCode = 400,
                IsDone = false,
                Message="Sorry, This staff is not exist!"
            };
        }

        public async Task<ResponseDTO> GetStaffCommittees(string nid)
        {
            var committee = await _context.Staff
                .Where(s => s.NID == nid)
                .SelectMany(s => s.StaffCommittees
                .Select(c => new
                {
                    Id = c.Committee.Id,
                    Name = c.Committee.Name,
                    SubjectsName = c.Committee.SubjectName,
                    FacultyNode = c.Committee.FacultyNode,
                    FacultyPhase = c.Committee.FacultyPhase,
                    Day = c.Committee.Day,
                    Date = c.Committee.Date,
                    Interval = c.Committee.Interval,
                    From = c.Committee.From,
                    To = c.Committee.To,
                    Status = c.Committee.Status,
                    Place = c.Committee.Place,
                    ByLaw=c.Committee.ByLaw,
                    StudyMethod=c.Committee.StudyMethod,
                    StudentNumber=c.Committee.StudentsCommittees.Count
                })).ToListAsync();

            if (committee != null)
            {
                return new ResponseDTO
                {
                    Model = committee,
                    StatusCode = 200,
                    IsDone = true
                };
            }          

            else
            {
                return new ResponseDTO
                {
                    StatusCode = 400,
                    Message = $"There is no Committees for this Staff {nid}"
                };
            }
        }

    }
}



/*
  var committee = await _context.Staff
                .Where(s => s.NID == nid)
                .SelectMany(s => s.StaffCommittees
                .Join(_context.Committees, sc => sc.CommitteeID, c => c.Id, (sc, c) => new { com = c })
                .Select(c => c.com.Id)).ToListAsync();

            if (committee != null)
            {
                List<object> committs = new List<object>();
                for (var i = 0; i < committee.Count; i++)
                {
                    var query = await _context.Committees
                        .FirstOrDefaultAsync(c => c.Id == committee[i]);
                    if(query != null)
                    {
                        committs.Add(query);
                    }
                }
                return new ResponseDTO
                {
                    Model = committs,
                    StatusCode = 200,
                    IsDone = true
                };
            }          

            else
            {
                return new ResponseDTO
                {
                    StatusCode = 400,
                    Message = $"There is no Committees for this Staff {nid}"
                };
            }
*/
