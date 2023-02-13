using Microsoft.AspNetCore.Mvc;
using SIMinfo.API.Models;

namespace SIMinfo.API.Services.Interface
{
    public interface ISimInfoService
    {
        public Task<IEnumerable<SimInformation>> GetSimInformation();
        public Task<Messages> SaveSimInformation([FromBody] SimInformation simInformation);
        public Task<Messages> UpdateSimInformation([FromRoute] Guid id, [FromBody] SimInformation simInformation);
        public Task<Messages> DeleteSimInformation([FromRoute] Guid id);
    }
}
