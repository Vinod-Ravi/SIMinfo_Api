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
using System.Security.Cryptography;
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
            try
            {
                if (user.UserName != null && user.Password != null)
                {
                    var userObj = await _simInfoDbContext.Users.FirstOrDefaultAsync(x => x.Email == user.UserName);
                    if (userObj != null)
                    {
                        if (!PasswordHasher.VerifyPassword(user.Password, userObj.Password!))
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
                    var newAccessToken = userObj.Token;
                    var newRefreshToken = CreateRefreshToken();
                    userObj.RefreshToken = newRefreshToken;
                    userObj.RefeshTokenExpiryTime = DateTime.UtcNow.AddDays(5);
                    await _simInfoDbContext.SaveChangesAsync();

                    _messages.Success = true;
                    _messages.Message = "Login Success";
                    _messages.AccessToken = newAccessToken;
                    _messages.RefreshToken = newRefreshToken;

                    return _messages;
                }
                _messages.Message = "Please enter username and password";
                return _messages;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public async Task<Messages> Refresh([FromBody] Messages message)
        {
            try
            {
                if (message is null)
                {
                    _messages.Message = "Invalid client request";
                    return _messages;
                }
                if (message.AccessToken != null && message.RefreshToken != null)
                {
                    string accessToken = message.AccessToken;
                    string refreshToken = message.RefreshToken;
                    var principle = GetPrincipleFromExpiredToken(accessToken);
                    if (principle.Identity != null)
                    {
                        var userName = principle.Identity.Name;
                        var user = await _simInfoDbContext.Users.FirstOrDefaultAsync(u => u.Email == userName);
                        if (user is null || user.RefreshToken != refreshToken || user.RefeshTokenExpiryTime <= DateTime.UtcNow)
                        {
                            _messages.Message = "Invalid Request";
                            return _messages;
                        }
                        var newAccessToken = CreateJwt(user);
                        var newRefreshToken = CreateRefreshToken();
                        user.RefreshToken = newRefreshToken;
                        await _simInfoDbContext.SaveChangesAsync();
                        _messages.Success = true;
                        _messages.AccessToken = newAccessToken;
                        _messages.RefreshToken = newRefreshToken;
                    }
                }
                return _messages;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        private string CreateJwt(User user)
        {
            try
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("SimInfoSecretKey");
                var identity = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Role,user.Role!),
                new Claim(ClaimTypes.Name,$"{user.Email}")

                });

                var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = identity,
                    Expires = DateTime.UtcNow.AddSeconds(10),
                    SigningCredentials = credentials
                };
                var token = jwtTokenHandler.CreateToken(tokenDescriptor);
                return jwtTokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

        }
        private string CreateRefreshToken()
        {
            try
            {
                var tokenBytes = RandomNumberGenerator.GetBytes(64);
                var refreshToken = Convert.ToBase64String(tokenBytes);
                var tokenInUser = _simInfoDbContext.Users.Any(a => a.RefreshToken == refreshToken);
                if (tokenInUser)
                {
                    return CreateRefreshToken();
                }
                return refreshToken;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        private ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes("SimInfoSecretKey");
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = false
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("This is invalid token");
                }
                return principal;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
