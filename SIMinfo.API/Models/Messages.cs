using System.ComponentModel.DataAnnotations;

namespace SIMinfo.API.Models
{
    public class Messages
    {
        public string? Message { get; set; }
        public string? ErrorCode { get; set; }
        public bool? Success { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
