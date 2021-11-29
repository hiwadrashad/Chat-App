using Chat_App_Library.Models;
using Chat_App_Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.Interfaces
{
    public interface ICredentialsController
    {
        [HttpPost("api/makeuseradmin")]
        public Task<IActionResult> MakeUserAdmin([FromBody] AscendUserToAdminRequest input);
        [HttpPost("api/register/{password}")]
        public Task<IActionResult> Register(string password, [FromBody] User input);
        [HttpPost("api/login/{password}")]
        public Task<IActionResult> Login(string password, [FromBody] User input);
        [HttpPost("api/refreshtoken")]
        public Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest);
        [HttpGet("api/getusersbyemail/{id}/{requestingid}")]
        public Task<IActionResult> GetUsersByEmail(string id, int requestingid);
        [HttpGet("api/getusers/{requestingid}")]
        public Task<IActionResult> GetUsers(int requestingid);
        [HttpGet("api/getuserbyid/{id}/{requestingid}")]
#nullable enable
        public Task<IActionResult> GetUserById(int id, int requestingid);
#nullable disable
        [HttpGet("api/getusersbyname/{id}/{requestingid}")]
        public Task<IActionResult> GetUsersByName(string id, int requestingid);

        [HttpPost("api/updateuserdata")]
        public Task<IActionResult> UpdateUserData([FromBody] User input, string placeholder = "placeholder");

        [HttpPost("api/banuser/{id}/{requestingid}")]
        public Task<IActionResult> BanUser(int id, int requestingid);
    }
}
