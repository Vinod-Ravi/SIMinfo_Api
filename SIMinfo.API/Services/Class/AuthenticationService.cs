using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SIMinfo.API.DataAccessLayer;
using SIMinfo.API.Helper;
using SIMinfo.API.Models;
using SIMinfo.API.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SIMinfo.API.Services.Class
{
    public class AuthenticationService : IAuthenticationServices
    {
        private readonly SimInfoDbContext _simInfoDbContext;
        private readonly Messages _messages;
        public AuthenticationService(SimInfoDbContext simInfoDbContext, Messages messages)
        {
            _simInfoDbContext = simInfoDbContext;
            _messages = messages;
        }
        public async Task<Messages> CheckUserAuthentication([FromBody] User user)
        {
            if (user.UserName != null && user.Password != null)
            {
                var userObj = await _simInfoDbContext.Users.FirstOrDefaultAsync(x => x.Email == user.UserName);
                if (userObj != null)
                {
                    if (!PasswordHasher.VerifyPassword(user.Password, userObj.Password))
                    {
                        _messages.Message = "Password is incorrect";
                        return _messages;
                    }
                }
                else
                {
                    _messages.Message = "Please enter valid User name";
                    return _messages;
                }
                userObj.Token = CreateJwt(userObj);

                _messages.Success = true;
                _messages.Message = "Login Success";
                _messages.Token = userObj.Token;
                return _messages;
            }
            _messages.Message = "Please enter username and password";
            return _messages;
        }
        private string CreateJwt(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("SimInfoSecretKey");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}")
            });
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);

        }
    }
}
