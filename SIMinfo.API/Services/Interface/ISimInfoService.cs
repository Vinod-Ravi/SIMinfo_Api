using Microsoft.AspNetCore.Mvc;
using SIMinfo.API.Models;

namespace SIMinfo.API.Services.Interface
{
    public interface ISimInfoService
    {
        public Task<IEnumerable<SimInformation>> GetSimInformation();
        public Task<bool> SaveSimInformation([FromBody] SimInformation simInformation);
        public Task<bool> UpdateSimInformation([FromRoute] Guid id, [FromBody] SimInformation simInformation);
        public Task<bool> DeleteSimInformation([FromRoute] Guid id);
    }
}
