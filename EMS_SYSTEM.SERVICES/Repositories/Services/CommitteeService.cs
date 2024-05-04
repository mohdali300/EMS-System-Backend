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
                    Date=model.Date,
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
            var schedule = await _context.FacultyNodes
                .Where(n => n.FacultyId == Id)
                .SelectMany(n => n.Subjects
                .SelectMany(s => s.SubjectCommittees
                .Select(sc => new CommitteeDTO
                {
                    Name = sc.Committee.Name,
                    SubjectsName = s.Name,
                    SubjectID = s.Id,
                    StudyMethod = sc.Committee.StudyMethod,
                    Status = sc.Committee.Status,
                    ByLaw = sc.Committee.ByLaw,
                    Interval = sc.Committee.Interval,
                    From = sc.Committee.From,
                    To = sc.Committee.To,
                    Place = sc.Committee.Place,
                    FacultyNode = sc.Committee.FacultyNode,
                    FacultyPhase = sc.Committee.FacultyPhase,
                }))).ToListAsync();

            if (schedule != null)
            {
                return new ResponseDTO
                {
                    Model = schedule,
                    StatusCode = 200,
                    IsDone = true
                };
            }
            else
            {
                return new ResponseDTO
                {
                    StatusCode = 400,
                    IsDone = false,
                    Message = "There is no Schedule Yet!"
                };
            }
        }

        public async Task<ResponseDTO> DeleteCommitee(int CommiteeID)
        {
            var Commitee = await _context.Committees.SingleOrDefaultAsync(c => c.Id == CommiteeID);
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
                Message = $"All Committees For Faculty {FacultyID} Was Deleted Succesfully",
                StatusCode = 200,
                Model = Committees.ToList(),
            };
        }
    }
}
