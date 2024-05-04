using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.Committee;
using EMS_SYSTEM.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
