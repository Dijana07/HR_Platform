using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public ResultDTO() { }
        public ResultDTO(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
