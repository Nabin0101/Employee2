using Data_Access_Layer.ApplicationContext;
using Data_Access_Layer.Model;
using Entities.User;
using Infrastructure.Common.ViewModel.LoginSignup;
using Infrastructure.Common.ViewModel.ResponseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly APIResponseModel _apiResponse;

        public UserService(ApplicationDBContext dBContext, IConfiguration configuration, APIResponseModel responseModel)
        {
            _dbContext = dBContext;
            _configuration = configuration;
            _apiResponse = responseModel;
        }

        public async Task<APIResponseModel> SaveUser(SignUpDTO signupDto)
        {
            try
            {
                var newUser = new User()
                {
                    FirstName = signupDto.FirstName,
                    LastName = signupDto.LastName,
                    Email = signupDto.Email,
                    UserName = signupDto.UserName,
                    Address = signupDto.Address,
                    Password = signupDto.Password,
                    ConfirmPassword = signupDto.ConfirmPassword,
                };
                await _dbContext.Users.AddAsync(newUser);
                _dbContext.SaveChanges();
                _apiResponse.Data = newUser;
                return _apiResponse;
            }
            catch (Exception ex)
            {
                _apiResponse.Message = ex.ToString();
                _apiResponse.IsSuccess = false;
                return _apiResponse;
            }
        }
        public async Task<string> GenerateToken(string userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                null,
                expires: DateTime.UtcNow.AddMinutes(100),
                signingCredentials: signIn);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            await SaveTokenToDatabase(userId, tokenString, DateTime.UtcNow.AddMinutes(100));
            return tokenString;

        }

        public async Task<string> SaveTokenToDatabase(string userId, string tokenString, DateTime expiresAt)
        {
            try
            {
                var existingToken = await _dbContext.Tokens.FirstOrDefaultAsync(t => t.UserId == userId);
                if (existingToken != null)
                {

                    existingToken.TokenName = tokenString;
                    existingToken.ExpiresAt = expiresAt;
                    existingToken.IsRevoked = false;
                    _dbContext.Tokens.Update(existingToken);
                }
                else
                {
                    var tokenEntity = new Token
                    {
                        TokenName = tokenString,
                        UserId = userId,
                        ExpiresAt = expiresAt,
                        IsRevoked = false
                    };
                    await _dbContext.Tokens.AddAsync(tokenEntity);
                }
                _dbContext.SaveChanges();
                return "Token saved successfully";
            }
            catch (Exception e)
            {
                return "Error: " + e.ToString();
            }
        }

        public async Task<APIResponseModel> UserLogin(LoginViewModel loginViewModel)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == loginViewModel.UserName && u.Password == loginViewModel.Password);
                if (user == null)
                {
                    _apiResponse.Message = "User is not found";
                    _apiResponse.IsSuccess = false;
                    return _apiResponse;
                }
                else
                {
                    var token = await GenerateToken(user.Id);
                    _apiResponse.Data = token;
                    return _apiResponse;
                }
            }
            catch (Exception ex)
            {
                _apiResponse.Message = ex.ToString();
                _apiResponse.IsSuccess = false;
                return _apiResponse;
            }
        }
        public bool IsTokenValid(string tokenString)
        {

            var token = _dbContext.Tokens.FirstOrDefault(t => t.TokenName == tokenString);
            if (token == null || token.ExpiresAt <= DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }
    }
}



