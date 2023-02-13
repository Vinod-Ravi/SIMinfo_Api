using Microsoft.AspNetCore.Mvc;
using SIMinfo.API.Models;

namespace SIMinfo.API.Services.Interface
{
    public interface ILoginService
    {
        public Task<Messages> SubmitUserRegistration([FromBody] User user);
    }
}
