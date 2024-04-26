using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.DOMAIN.DTO.LogIn
{
    public class LogInDTO
    {

        [Required(ErrorMessage = "User Name is required")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }

    }
}
