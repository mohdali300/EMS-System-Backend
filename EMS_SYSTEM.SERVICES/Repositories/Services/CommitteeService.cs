using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.Committee;
using EMS_SYSTEM.DOMAIN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EMS_SYSTEM.APPLICATION.Repositories.Services
{
    public class CommitteeService:GenericRepository<Committee>, ICommitteeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommitteeService(UnvcenteralDataBaseContext Db, IUnitOfWork unitOfWork) : base(Db)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseDTO> AddCommitteeAsync(CommitteeDTO model)
        {
           if(model is not null)
            {
                var Committee = new Committee()
                {
    
                    Name = model.Name,
                    Date=model.Date.Date,
                    Interval=model.Interval,
                    From=model.From,
                    To=model.To,
                    Place=model.Place,
                    SubjectName=model.SubjectsName,
                    StudyMethod=model.StudyMethod,
                    Status = model.Status,
                    Day=model.Day,
                    ByLaw = model.ByLaw,
                    FacultyNode=model.FacultyNode,
                    FacultyPhase = model.FacultyPhase
                    
                };
                var isSubjectIdExist= await _unitOfWork.Subject.IsExistAsync(s => s.Id ==model.SubjectID);
                if (isSubjectIdExist != null)
                {
                    await _unitOfWork.Committees.AddAsync(Committee);
                    await _unitOfWork.SaveAsync();
                    await _unitOfWork.SubjectCommittees.AddAsync(new SubjectCommittee { SubjectId = model.SubjectID, CommitteeId = Committee.Id });
                    await _unitOfWork.SaveAsync();
                    return new ResponseDTO
                    {
                        Message = "Committee Created Successfully",
                        IsDone = true,
                        Model = Committee,
                        StatusCode = 201
                    };
                }
                return new ResponseDTO
                {
                    Message = "Invalid SubjectID", // For FrontEnd not User
                    IsDone = false,
                    Model = null,
                    StatusCode = 400
                };
            }
            return new ResponseDTO { 
                Message = "Invalid Data in Model",
                IsDone = false,
                Model = null,
                StatusCode = 400 
            };
        }

        public async Task<ResponseDTO> GetCommitteesSchedule(int Id)
        {
            var phase = _context.Faculties
                .Where(f => f.Id == Id)
                .Join(_context.FacultyPhases, f => f.Id, p => p.FacultyId, (f, p) => new { FacultyPhase = p })    
                .Select(f=>f.FacultyPhase.Id)
                .ToList();
  
            CommiteLevels levels = new CommiteLevels();

            for (var i=0; i<phase.Count; i++)
            {
                var sub = _context.FacultyPhases
                    .Where(p => p.Id == phase[i])
                    .Join(_context.FacultyHieryicals, p => p.Id, h => h.PhaseId, (p, h) => new { h.Id })
                    .Join(_context.Subjects, p => p.Id, s => s.FacultyHieryricalId, (h, s) => new { s.Id })
                    .Join(_context.SubjectCommittees, s => s.Id, sc => sc.SubjectId, (s, sc) => new { sc.CommitteeId })
                    .Join(_context.Committees, sc => sc.CommitteeId, c => c.Id, (sc, c) => new { c })
                    .OrderBy(p=>p.c.Date).ThenBy(p=>p.c.From)
                    .ToList();
                switch(i+1)
                {
                    case 1:
                        levels.level1 = sub; break;
                    case 2:
                        levels.level2 = sub; break;
                    case 3:
                        levels.level3 = sub; break;
                    case 4:
                        levels.level4 = sub; break;
                    case 5:
                        levels.level5 = sub; break;
                    case 6:
                        levels.level6 = sub; break;
                    case 7:
                        levels.level7 = sub; break;
                   
                }

            }


            levels.len = phase.Count;

            return new ResponseDTO
            {
                Model = levels,
                StatusCode = 200,
                IsDone = true
            };
            
        }

        public async Task<ResponseDTO> DeleteCommittee(int CommiteeID)
        {
            var Commitee = await _context.Committees.FirstOrDefaultAsync(c => c.Id == CommiteeID);
            if (Commitee == null)
            {
                return new ResponseDTO
                {
                    IsDone = false,
                    Message = $"There is No Committee was found with ID {CommiteeID}",
                    StatusCode = 404,
                    Model = null,
                };
            }

            _context.Committees.Remove(Commitee);
            await _unitOfWork.SaveAsync();

            return new ResponseDTO
            {
                IsDone = true,
                Message = $"Commitee with ID {CommiteeID} was Deleted",
                StatusCode = 200,
                Model = Commitee,
            };
        }

        public async Task<ResponseDTO> DeleteAllFacultyCommitee(int FacultyID)
        {
            var isfacultyexist = await _unitOfWork.Faculty.IsExistAsync(s => s.Id == FacultyID);
            // if facultyid is wrong
            if (isfacultyexist is null)
            {
                return new ResponseDTO
                {
                    Message = "There is No Faculty with this ID",
                    IsDone = false,
                    Model = null,
                    StatusCode = 404
                };
            }


            var Committees = await _context.Committees
               .Join(_context.SubjectCommittees, c => c.Id, sc => sc.CommitteeId, (c, sc) => new { Committee = c, SubjectCommittee = sc })
               .Join(_context.Subjects, cs => cs.SubjectCommittee.SubjectId, s => s.Id, (cs, s) => new { cs.Committee, Subject = s })
               .Join(_context.FacultyNodes, csc => csc.Subject.FacultyNodeId, fn => fn.FacultyNodeId, (csc, fn) => new { csc.Committee, FacultyNode = fn })
               .Join(_context.Faculties, cscfn => cscfn.FacultyNode.FacultyId, f => f.Id, (cscfn, f) => new { cscfn.Committee, Faculty = f })
               .Where(c => c.Faculty.Id == FacultyID)
               .Select(c => c.Committee)
               .ToListAsync();
            // no committees found
            if (Committees.Count == 0)
            {

                return new ResponseDTO
                {
                    IsDone = false,
                    Message = $"There is No Committees in the faculty with id {FacultyID} to Delete",
                    StatusCode = 404
                };
            }

            foreach (var committee in Committees)
            {
                _context.Committees.Remove(committee);
            }
            await _unitOfWork.SaveAsync();

            return new ResponseDTO
            {
                IsDone = true,
                Message = $"All Committees For Faculty {FacultyID} Was Deleted Successfully",
                StatusCode = 200,
                Model = Committees.ToList(),
            };
        }
        public async Task<ResponseDTO> FilterFacultyCommittees(int facultyID, int level, string committeeName , string subjectName)
        {
            var CommitteesDetails = _context.Committees
               .Join(_context.SubjectCommittees, c => c.Id, sc => sc.CommitteeId, (c, sc) => new { Committee = c, SubjectCommittee = sc })
               .Join(_context.Subjects, sc => sc.SubjectCommittee.SubjectId, s => s.Id, (sc, s) => new { sc, Subject = s })
               .Join(_context.FacultyHieryicals, s => s.Subject.FacultyHieryricalId, fh => fh.Id, (s, fh) => new { s, FacultyHieryical = fh })
               .Join(_context.FacultyPhases, fh => fh.FacultyHieryical.PhaseId, fp => fp.Id, (fh, fp) => new { fh, FacultyPhase = fp });
            if (facultyID == 0)
            {
                return new ResponseDTO()
                {
                    IsDone = false,
                    StatusCode = 400,
                    Model = null,
                    Message = "Faculty Id is Required!"
                };
            }
            else
            {
                if (facultyID != 0)
                {
                    CommitteesDetails = CommitteesDetails.Where(f => f.FacultyPhase.FacultyId == facultyID);
                }

                if (level != 0)
                {
                    CommitteesDetails = CommitteesDetails.Where(f => f.FacultyPhase.Id == level);
                }

                if (!string.IsNullOrEmpty(subjectName))
                {
                    CommitteesDetails = CommitteesDetails.Where(f => f.fh.s.sc.Committee.SubjectName.Contains(subjectName));
                }

                if (!string.IsNullOrEmpty(committeeName))
                {
                    CommitteesDetails = CommitteesDetails.Where(f => f.fh.s.sc.Committee.Name.Contains(committeeName));
                }

                return new ResponseDTO
                {
                    IsDone = true,
                    Model = CommitteesDetails.OrderBy(s => s.fh.s.sc.Committee.Date).ThenBy(s => s.fh.s.sc.Committee.From).Select(s => s.fh.s.sc.Committee)  ,
                    StatusCode = 200,
                    Message = "Success Response"
                };
            }
        }
    }
}
