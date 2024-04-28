using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.DOMAIN.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string NID { get; set; }
        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
