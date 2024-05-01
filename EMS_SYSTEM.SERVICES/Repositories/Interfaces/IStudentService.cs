using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.GenericRepository;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.APPLICATION.Repositories.Interfaces
{
    public interface IStudentService:IGenericRepository<Student>
    {
        Task<ResponseDTO> GetStudentDataByNID(string Id);
    }
}
