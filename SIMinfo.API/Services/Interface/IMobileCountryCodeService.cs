using Microsoft.AspNetCore.Mvc;
using SIMinfo.API.Models;

namespace SIMinfo.API.Services.Interface
{
    public interface IMobileCountryCodeService
    {
        public Task<IEnumerable<MobileCountryCode>> GetMobileCountryCodes();
        public Task<Messages> SaveMobileCountryCode([FromBody] MobileCountryCode mobileCountryCode);
        public Task<Messages> UpdateMobileCountryCode([FromRoute] Guid id, [FromBody] MobileCountryCode mobileCountryCode);
        public Task<Messages> DeleteMobileCountryCode([FromRoute] Guid id);
    }
}
