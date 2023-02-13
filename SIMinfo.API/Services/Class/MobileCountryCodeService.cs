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
        private readonly Messages _messages;
        public MobileCountryCodeService(SimInfoDbContext simInfoDbContext, Messages messages)
        {
            _simInfoDbContext = simInfoDbContext;
            _messages = messages;
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

        public async Task<Messages> SaveMobileCountryCode([FromBody] MobileCountryCode mobileCountryCode)
        {
            mobileCountryCode.Id = Guid.NewGuid();
            mobileCountryCode.CodeName = mobileCountryCode.CountryCode + ' ' + mobileCountryCode.CountryName;
            await _simInfoDbContext.MobileCountryCode.AddAsync(mobileCountryCode);
            var countryCodes = await _simInfoDbContext.SaveChangesAsync();
            if (countryCodes.ToString() != null)
            {
                _messages.Success = true;
                _messages.Message = "Mobile country code submitted";
                return _messages;

            }
            _messages.Message = "Error in saving data";
            return _messages;
        }
        public async Task<Messages> UpdateMobileCountryCode([FromRoute] Guid id, [FromBody] MobileCountryCode mobileCountryCode)
        {
            var existingCountryCode = await _simInfoDbContext.MobileCountryCode.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCountryCode != null)
            {
                existingCountryCode.CountryCode = mobileCountryCode.CountryCode;
                existingCountryCode.CountryName = mobileCountryCode.CountryName;
                existingCountryCode.CodeName = mobileCountryCode.CountryCode + ' ' + mobileCountryCode.CountryName;

                await _simInfoDbContext.SaveChangesAsync();
                _messages.Success = true;
                _messages.Message = "Mobile country code updated";
                return _messages;
            }
            _messages.Message = "Error in updating data";
            return _messages;
        }
        public async Task<Messages> DeleteMobileCountryCode([FromRoute] Guid id)
        {
            var countryCode = await _simInfoDbContext.MobileCountryCode.FirstOrDefaultAsync(x => x.Id == id);
            if (countryCode != null)
            {
                _simInfoDbContext.Remove(countryCode);
                await _simInfoDbContext.SaveChangesAsync();
                _messages.Success = true;
                _messages.Message = "Mobile country code deleted";
                return _messages;
            }
            _messages.Message = "Error in country deleting data";
            return _messages;
        }
    }
}
