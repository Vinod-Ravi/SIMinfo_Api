using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIMinfo.API.DataAccessLayer;
using SIMinfo.API.Models;
using SIMinfo.API.Services.Interface;

namespace SIMinfo.API.Services.Class
{
    public class SimInfoService : ISimInfoService
    {
        private readonly SimInfoDbContext _simInfoDbContext;
        private readonly Messages _messages;
        public SimInfoService(SimInfoDbContext simInfoDbContext, Messages messages)
        {
            _simInfoDbContext = simInfoDbContext;
            _messages = messages;
        }
        public async Task<IEnumerable<SimInformation>> GetSimInformation()
        {
            var simInfo = await _simInfoDbContext.SimInformation.ToListAsync();
            if (simInfo.Count() != 0)
            {
                return simInfo;
            }
            return simInfo;
        }

        public async Task<Messages> SaveSimInformation([FromBody] SimInformation simInformation)
        {
            simInformation.Id = Guid.NewGuid();
            await _simInfoDbContext.SimInformation.AddAsync(simInformation);
            var simInfo = await _simInfoDbContext.SaveChangesAsync();
            if (simInfo.ToString() != null)
            {
                _messages.Success = true;
                _messages.Message = "Sim information submitted";
                return _messages;
            }
            _messages.Message = "Error in submitting sim information";
            return _messages;
        }

        public async Task<Messages> UpdateSimInformation([FromRoute] Guid id, [FromBody] SimInformation simInformation)
        {
            var existingSiminformation = await _simInfoDbContext.SimInformation.FirstOrDefaultAsync(x => x.Id == id);
            if (existingSiminformation != null)
            {
                existingSiminformation.AdviceOfCharge = simInformation.AdviceOfCharge;
                existingSiminformation.AuthenticationKey = simInformation.AuthenticationKey;
                existingSiminformation.MobileCountryCode = simInformation.MobileCountryCode;
                existingSiminformation.LocalAreaIdentity = simInformation.LocalAreaIdentity;
                existingSiminformation.ServiceProviderName = simInformation.ServiceProviderName;
                existingSiminformation.IntegratedCircuitCardId = simInformation.IntegratedCircuitCardId;
                existingSiminformation.ValueAddedServices = simInformation.ValueAddedServices;
                existingSiminformation.CreatedDate = simInformation.CreatedDate;
                existingSiminformation.CreatedUser = simInformation.CreatedUser;
                await _simInfoDbContext.SaveChangesAsync();
                _messages.Success = true;
                _messages.Message = "Sim information updated";
                return _messages;
            }
            _messages.Message = "Error in updating sim information";
            return _messages;
        }
        public async Task<Messages> DeleteSimInformation([FromRoute] Guid id)
        {
            var siminformation = await _simInfoDbContext.SimInformation.FirstOrDefaultAsync(x => x.Id == id);
            if (siminformation != null)
            {
                _simInfoDbContext.Remove(siminformation);
                await _simInfoDbContext.SaveChangesAsync();
                _messages.Success = true;
                _messages.Message = "Sim information deleted";
                return _messages;
            }
            _messages.Message = "Error in deleting sim information";
            return _messages;
        }
    }
}
