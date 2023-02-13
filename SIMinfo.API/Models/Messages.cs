using System.ComponentModel.DataAnnotations;

namespace SIMinfo.API.Models
{
    public class Messages
    {
        public string? Message { get; set; }
        public string? ErrorCode { get; set; }
        public bool? Success { get; set; }
        public string? Token { get; set; }
    }
}
