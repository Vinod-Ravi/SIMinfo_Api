using Microsoft.AspNetCore.Mvc;
using SIMinfo.API.Models;

namespace SIMinfo.API.Services.Interface
{
    public interface IAuthenticationServices
    {
        public Task<Messages> CheckUserAuthentication([FromBody] User user);
        public Task<Messages> Refresh([FromBody] Messages message);
    }
}
