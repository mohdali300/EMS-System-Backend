using EMS_SYSTEM.DOMAIN.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.APPLICATION.Repositories.Interfaces
{
    public interface IEmailService
    {
        void SendOTPEmailAsync(string email, string subject , string name , string password);
        Task<ResponseDTO> HandelSendEmail(string NID);
    }
}
