using System.ComponentModel.DataAnnotations;

namespace SIMinfo.API.Models
{
    public class MobileCountryCode
    {
        [Key]
        public Guid Id { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
        public string? CodeName { get; set; }
    }
}
