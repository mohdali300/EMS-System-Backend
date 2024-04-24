using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.LogIn;
using EMS_SYSTEM.DOMAIN.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.APPLICATION.Repositories.Services
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountService(UserManager<ApplicationUser> _userManager)
        {
            this._userManager= _userManager;
        }
        public async Task<AuthModel> LogIn(LogInDTO model)
        {

            return new AuthModel();
            
        }
        
    }
}
