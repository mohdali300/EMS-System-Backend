using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EMS_SYSTEM.DOMAIN.DTO
{
    public class AuthModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }
        public string Message { get; set; }

        public bool IsAuthenticated { get; set; } = false;
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }

    }
}
