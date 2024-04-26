using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.DOMAIN.DTO.PasswordSettings
{
    public class ChangePasswordDTO
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string? CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage =
            "The new password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
