using System.ComponentModel.DataAnnotations;

namespace SIMinfo.API.Models
{
    public class SimInformation
    {
        [Key]
        public Guid Id { get; set; }
        public string? AdviceOfCharge { get; set; }
        public string? AuthenticationKey { get; set; }
        public string? MobileCountryCode { get; set; }
        public string? LocalAreaIdentity { get; set; }
        public string? ServiceProviderName { get; set; }
        public string? IntegratedCircuitCardId { get; set; }
        public string? ValueAddedServices { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
    }
}
