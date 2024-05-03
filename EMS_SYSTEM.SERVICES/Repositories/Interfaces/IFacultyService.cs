using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.GenericRepository;
using EMS_SYSTEM.DOMAIN.DTO;

namespace EMS_SYSTEM.APPLICATION.Repositories.Interfaces
{
    public interface IFacultyService 
    {
       public Task<ResponseDTO> GetFacultyDataByID(int Id);
        //   public Task<ResponseDTO> GetSubjects(int bylawId, int facultyPhaseId, int facultyNodeId, int facultyId);

    }
}
