using Chat_App_Library.Models;
using Chat_App_Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.Interfaces
{
    public interface ICredentialsService
    {
        public Task<object> Login(User input, AuthResult jwtToken);
        public Task<object> MakeUserAdmin(AscendUserToAdminRequest input);
        public Task<object> Register(User input);
        public Task<object> RefreshToken(TokenRequest input);
        public Task<object> GetUsersByEmail(string id, int requestingid);
        public Task<object> GetUsers(int requestingid);
        public Task<object> GetUserById(int id, int requestingid);
        public Task<object> GetUsersByName(string id, int requestingid);
        public Task<object> UpdateUserData(User User);
        public Task<object> BanUser(int id, int requestingid);
    }
}
