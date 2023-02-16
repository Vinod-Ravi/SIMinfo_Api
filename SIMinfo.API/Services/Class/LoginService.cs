using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIMinfo.API.DataAccessLayer;
using SIMinfo.API.Helper;
using SIMinfo.API.Models;
using SIMinfo.API.Services.Interface;
using System.Text;
using System.Text.RegularExpressions;

namespace SIMinfo.API.Services.Class
{
    public class LoginService : ILoginService
    {
        private readonly SimInfoDbContext _simInfoDbContext;
        private readonly Messages _messages;
        public LoginService(SimInfoDbContext simInfoDbContext, Messages messages)
        {
            _simInfoDbContext = simInfoDbContext;
            _messages = messages;
        }

        public async Task<Messages> SubmitUserRegistration([FromBody] User user)
        {
            try
            {
                if (user.Email != null)
                {
                    if (await CheckEmailIdExistAsync(user.Email))
                    {
                        _messages.Message = "Email id already exists!!";
                        return _messages;
                    }
                    else if (!string.IsNullOrEmpty(CheckValidEmail(user.Email)))
                    {
                        _messages.Message = "Please enter valid email!!";
                        return _messages;
                    }
                }
                if (user.Password != null)
                {
                    var pass = CheckPasswordStrength(user.Password);
                    if (!string.IsNullOrEmpty(pass))
                    {
                        _messages.Message = pass;
                        return _messages;
                    }
                }
                user.Id = Guid.NewGuid();
                user.UserName = user.FirstName + '.' + user.LastName;
                if (user.Password != null)
                    user.Password = PasswordHasher.HashPassword(user.Password);
                user.Role = "Admin";
                user.Token = "";
                await _simInfoDbContext.Users.AddAsync(user);
                var userObj = await _simInfoDbContext.SaveChangesAsync();
                if (userObj.ToString() != null)
                {
                    _messages.Message = "User registration completed";
                    _messages.Success = true;
                    return _messages;
                }
                _messages.Message = "Error in user registration";
                return _messages;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        private async Task<bool> CheckEmailIdExistAsync(string emailId)
        {
            try
            {
                return await _simInfoDbContext.Users.AnyAsync(x => x.Email == emailId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        private string CheckPasswordStrength(string password)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                if (password.Length < 8)
                    sb.Append("Minimum password length should be 8" + Environment.NewLine);
                if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]")
                    && Regex.IsMatch(password, "[0-9]")))
                    sb.Append("Password should be Alphanumeric" + Environment.NewLine);
                if (!Regex.IsMatch(password, "[!,@,#,$,%,^,&,*,(,),_,+,=,|,/,;,:,',.,<,>,?]"))
                    sb.Append("Password should contain special chars" + Environment.NewLine);
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        private string CheckValidEmail(string emailId)
        {
            try
            {
                var message = string.Empty;
                if (!Regex.IsMatch(emailId, "[@,.]"))
                    message = "Password should contain special chars";
                return message;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
