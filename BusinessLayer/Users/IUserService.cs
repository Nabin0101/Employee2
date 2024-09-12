using Data_Access_Layer.Model;
using Infrastructure.Common.ViewModel.LoginSignup;
using Infrastructure.Common.ViewModel.ResponseModel;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Business_Layer.Users
{
    public interface IUserService
    {
        Task<APIResponseModel>SaveUser(SignUpDTO signupDto);
        Task<string> GenerateToken(String username);
        Task<String> SaveTokenToDatabase(string userId, string tokenString, DateTime expiresAt);
        Task<APIResponseModel> UserLogin(LoginViewModel loginViewModel);
        bool IsTokenValid(string tokenString);

       

   
    }
}
