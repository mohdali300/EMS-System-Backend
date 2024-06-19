using EMS_SYSTEM.DOMAIN.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS_SYSTEM.DOMAIN.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EMS_SYSTEM.APPLICATION.Repositories.Interfaces
{
    public interface IGlobalService
    {

        public Task<ResponseDTO> GetAllFaculties();
        public Task<ResponseDTO> GetFacultyByName(string FacultyName);
        public Task<ResponseDTO> GetFacultiesWithCommitteeCount();

        public Task<ResponseDTO> GetFacultiesWithCommitteeToday();

        public Task<ResponseDTO> GetFacultiesByDate(DateTime date);
        public Task<ResponseDTO> GetStudentCountInActiveCommitteesToday();
        public  Task<ResponseDTO> GetFacultyWithMostActiveCommitteesToday();


    }
}
