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

      

        public async Task<ResponseDTO> GetCommitteesForFaculty(int FacultyID)
        {
            var Committees = await _context.Committees
                .Join(_context.SubjectCommittees, C => C.Id, su => su.CommitteeId, (C, su) => new { Committees = C, SubjectCommittes = su })
                .Join(_context.Subjects, SC => SC.SubjectCommittes.SubjectId, SUB => SUB.Id, (SC, SUB) => new { SubjectCommittes = SC, Subject = SUB })
                .Join(_context.FacultyNodes, FNS => FNS.Subject.FacultyNodeId, FN => FN.FacultyNodeId, (FNS, FN) => new { FacultyNodeSubject = FNS, FacultyNode = FN })
                .Join(_context.Faculties, FAN => FAN.FacultyNode.FacultyId, FA => FA.Id, (FAN, FA) => new { NodeInFaculty = FAN, Faculty = FA })
                .Where(C=>C.Faculty.Id==FacultyID)
                .GroupBy(g => g.NodeInFaculty.FacultyNodeSubject.SubjectCommittes.Committees)
                .Select(C=>new 
                {
                    Id= C.Key.Id,
                    Name =C.Key.Name,
                    Date= C.Key.Date,
                    Interval= C.Key.Interval,
                    From= C.Key.From,
                    To= C.Key.To,
                    SubjectName= C.Key.SubjectName,
                    Place= C.Key.Place,
                    Status= C.Key.Status,
                    Day= C.Key.Day,
                    StudyMethod= C.Key.StudyMethod,
                    ByLaw = C.Key.ByLaw,
                    FacultyNode= C.Key.FacultyNode,
                    FacultyPhase= C.Key.FacultyPhase                   
                })
                .AsNoTracking()
                .ToListAsync();

            if (Committees.Count > 0)
            {
                return new ResponseDTO
                {
                    IsDone = true,
                    Message = $"All Committees For Faculty {FacultyID}",
                    StatusCode = 200,
                    Model = Committees.ToList(),
                };
            }
            return new ResponseDTO
            {
                IsDone = false,
                Message = $"There is No Committees Until Now For This Faculty {FacultyID}",
                StatusCode = 404,
                Model = null,
            };
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

    }
}
