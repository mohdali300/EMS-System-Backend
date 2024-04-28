using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.DOMAIN.DTO.Register
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "NID is required")]
        [StringLength(14, ErrorMessage = "NID must be exactly 14 digits")]
        [RegularExpression(@"^([1-9]{1})([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{2})[0-9]{3}([0-9]{1})[0-9]{1}$", ErrorMessage = " National ID format is invalid")]
        public string NID { get; set; }

        [Required(ErrorMessage = "Password is required")]
       [RegularExpression(@"^([1-9]{1})([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{2})[0-9]{3}([0-9]{1})[0-9]{1}$", ErrorMessage = "Password must be like National ID")]
        public string Password { get; set; }
    }
}
