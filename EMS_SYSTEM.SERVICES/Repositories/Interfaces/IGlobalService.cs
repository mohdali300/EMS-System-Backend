using EMS_SYSTEM.DOMAIN.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.APPLICATION.Repositories.Interfaces
{
    public interface IGlobalService
    {
        public Task<ResponseDTO> GetAllFaculties();
        public Task<ResponseDTO> GetFacultyByName(string FacultyName);

    }
}
