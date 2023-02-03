using Microsoft.AspNetCore.Mvc;
using SIMinfo.API.Models;

namespace SIMinfo.API.Services.Interface
{
    public interface IMobileCountryCodeService
    {
        public Task<IEnumerable<MobileCountryCode>> GetMobileCountryCodes();
        public Task<bool> SaveMobileCountryCode([FromBody] MobileCountryCode mobileCountryCode);
        public Task<bool> UpdateMobileCountryCode([FromRoute] Guid id, [FromBody] MobileCountryCode mobileCountryCode);
        public Task<bool> DeleteMobileCountryCode([FromRoute] Guid id);
    }
}
