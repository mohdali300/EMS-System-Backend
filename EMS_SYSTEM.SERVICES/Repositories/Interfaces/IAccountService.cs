using EMS_SYSTEM.DOMAIN.DTO.LogIn;
using EMS_SYSTEM.DOMAIN.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.APPLICATION.Repositories.Interfaces
{
    public interface IAccountService
    {
      Task<AuthModel> LogIn(LogInDTO model);
    }
}
