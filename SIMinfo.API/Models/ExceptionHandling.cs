using System.ComponentModel.DataAnnotations;

namespace SIMinfo.API.Models
{
    public class ExceptionHandling
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}
