using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS_SYSTEM.DOMAIN.DTO;

namespace EMS_SYSTEM.APPLICATION.Repositories.Interfaces
{
    public interface IGlobalService
    {
        public Task<ResponseDTO> GetFacultiesWithCommitteeCount();

    }
}
