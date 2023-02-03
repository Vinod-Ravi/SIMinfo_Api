using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIMinfo.API.DataAccessLayer;
using SIMinfo.API.Models;
using SIMinfo.API.Services.Interface;

namespace SIMinfo.API.Services.Class
{
    public class MobileCountryCodeService : IMobileCountryCodeService
    {
        private readonly SimInfoDbContext _simInfoDbContext;
        public MobileCountryCodeService(SimInfoDbContext simInfoDbContext)
        {
            _simInfoDbContext = simInfoDbContext;
        }

        public async Task<IEnumerable<MobileCountryCode>> GetMobileCountryCodes()
        {
            var countryCodes = await _simInfoDbContext.MobileCountryCode.ToListAsync();
            if (countryCodes.Count != 0)
            {
                return countryCodes;
            }
            return countryCodes;
        }

        public async Task<bool> SaveMobileCountryCode([FromBody] MobileCountryCode mobileCountryCode)
        {
            mobileCountryCode.Id = Guid.NewGuid();
            mobileCountryCode.CodeName = mobileCountryCode.CountryCode + ' ' + mobileCountryCode.CountryName;
            await _simInfoDbContext.MobileCountryCode.AddAsync(mobileCountryCode);
            var countryCodes = await _simInfoDbContext.SaveChangesAsync();
            if (countryCodes.ToString() != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateMobileCountryCode([FromRoute] Guid id, [FromBody] MobileCountryCode mobileCountryCode)
        {
            var existingCountryCode = await _simInfoDbContext.MobileCountryCode.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCountryCode != null)
            {
                existingCountryCode.CountryCode = mobileCountryCode.CountryCode;
                existingCountryCode.CountryName = mobileCountryCode.CountryName;
                existingCountryCode.CodeName = mobileCountryCode.CountryCode + ' ' + mobileCountryCode.CountryName;

                await _simInfoDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteMobileCountryCode([FromRoute] Guid id)
        {
            var countryCode = await _simInfoDbContext.MobileCountryCode.FirstOrDefaultAsync(x => x.Id == id);
            if (countryCode != null)
            {
                _simInfoDbContext.Remove(countryCode);
                await _simInfoDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
