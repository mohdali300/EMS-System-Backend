using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.GenericRepository;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.Faculty;

namespace EMS_SYSTEM.APPLICATION.Repositories.Interfaces
{
    public interface IFacultyService 
    {
       public Task<ResponseDTO> GetFacultyDataByID(int Id);
        public Task<ResponseDTO> GetSubjects(FacultyHieryicalDTO hieryicalDTO);
        public Task<ResponseDTO> GetFacultyCommitteesDetails(int id);
        public Task<ResponseDTO> GetFacultyCommitteesForCurrentDay(int id);
        public Task<ResponseDTO> GetPlaces();
        public Task<ResponseDTO> GetStudentCountInActiveCommitteesForFacultyToday(int facultyId);
        public Task<ResponseDTO> GetAllStaffInFaculty(int facultyId);
        public Task<ResponseDTO> GetStaffInCommitteesForFaculty(int facultyId);
    }
}
