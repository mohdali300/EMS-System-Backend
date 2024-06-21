using EMS_SYSTEM.DOMAIN.DTO.Committee;
using EMS_SYSTEM.DOMAIN.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS_SYSTEM.DOMAIN.Models;

namespace EMS_SYSTEM.APPLICATION.Repositories.Interfaces
{
    public interface ICommitteeService
    {
        public Task<ResponseDTO> Distributions(int observerId, List<int> noticers, CommitteeDTO model);      
        public Task<ResponseDTO> GetCommitteesSchedule(int id);
        public Task<ResponseDTO> GetCommitteeDetails(int id);
        public Task<ResponseDTO> FilterFacultyCommittees(int FacultyID, int Level, string CommitteeName , string subjectName);
        public Task<ResponseDTO> DeleteCommittee(int CommiteeID);
        public Task<ResponseDTO> DeleteAllFacultyCommitee(int FacultyID);
        public Task<ResponseDTO> GetCommiteStudents(int comId);
        public Task<ResponseDTO> UpdateCommitee(int committeeID, CommitteeDTO model);
        //public  Task<List<ResponseDTO>> GetObserversInFaculty(int facultyId);
        public  Task<ResponseDTO> GetObserversInFaculty(int facultyId);
        public Task<ResponseDTO> GetInvigilatorsInFaculty(int facultyId);



    }
}
