using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.DOMAIN.DTO
{
    public class ResponseDTO
    {
        public int StatusCode { get; set; }
        public bool IsDone { get; set; }
        public object? Model { get; set; }
        public ICollection<object>? Models { get; set; }
        public string? Message { get; set; }
        public ICollection<string>? Errors { get; set; }
        public string? Status { get; set; }
    }
}
