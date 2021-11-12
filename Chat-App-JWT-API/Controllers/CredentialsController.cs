using Chat_App_JWT_API.Attributes;
using Chat_App_JWT_API.Configuration;
using Chat_App_JWT_API.JWT;
using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using Chat_App_Library.Singletons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat_App__JWT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredentialsController : ControllerBase
    {
        private IDatabaseSingleton _databaseSingleton;
        private IRepository _repo;
        private readonly JwtConfig _jwtConfig;
        private JWTTokens _tokenGenerator;
        private readonly TokenValidationParameters _tokenValidationParams;
        private readonly JWTVerification _jwtVerification;
        public CredentialsController(IDatabaseSingleton databaseSingleton, IOptionsMonitor<JwtConfig> optionsMonitor, 
            TokenValidationParameters tokenValidationParameters)
        {
            _databaseSingleton = databaseSingleton;
            _repo = databaseSingleton.GetRepository();
            _jwtConfig = optionsMonitor.CurrentValue;
            _tokenGenerator = new JWTTokens(_jwtConfig, databaseSingleton);
            _tokenValidationParams = tokenValidationParameters;
            _jwtVerification = new JWTVerification(databaseSingleton, optionsMonitor, tokenValidationParameters);
        }

        [HttpPost("api/register")]
        public async Task<IActionResult> Register([FromBody] User input)
        {
            if (ModelState.IsValid)
            {

                var testtoken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                var existingUsers = _databaseSingleton.GetRepository().GetUsers();

                if (existingUsers.Any(a => a == input))
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Userdata already in use"
                        },
                        Success = false
                    });
                }
                var jwtToken = await _tokenGenerator.GenerateJwtToken(input);
                input.Salt = Chat_App_Library.Constants.Salts.Saltvalue;
                input.HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash(input.
                AttemptedPassword, Chat_App_Library.Constants.Salts.Saltvalue));
                input.AttemptedPassword = "";
                _repo.AddUser(input);
                return Ok(jwtToken);
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>() { 
                "Invalid payload"
                },
                Success = false
            });
        }
       
        [HttpPost("api/login")]
       
        public async Task<IActionResult> Login([FromBody] User input)
        {
            if (ModelState.IsValid)
            {
                var existingUsers = _databaseSingleton.GetRepository().GetUsers();
                if (existingUsers.Any(a => a.Id == input.Id))
                {
                    input.HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash(input.AttemptedPassword, input.Salt));
                    var databaseuser = existingUsers.FirstOrDefault(a => a.Email == input.Email && a.Username == input.Username);
                    if (Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.CompareHash(input.AttemptedPassword, databaseuser.HashBase64, input.Salt))
                    {
                        var jwtToken = await _tokenGenerator.GenerateJwtToken(input);
                        //HttpContext.Request.Headers["Authorization"] = jwtToken;
                        var testtoken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                        AuthResultSingleton.GetSingleton().SetAuth(jwtToken);
                        return Ok(jwtToken);
                    }
                    else
                    {
                        return BadRequest(new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                            "Incorrect user info"
                            },
                            Success = false
                        });
                    }
                }
                else
                {

                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                "Invalid payload"
                },
                        Success = false
                    });
                }
  
            }
            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>() {
                "Invalid payload"
                },
                Success = false
            });


        }

        [HttpPost("api/refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _jwtVerification.VerifyAndGenerateToken(tokenRequest);

                if (result == null)
                {

                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Invalid tokens"
                    },
                        Success = false
                    });
                }

                return Ok(result);
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>() { 
                    "Invalid payload"
                },
                Success = false
            });
        
        }
        [HttpGet("api/getusersbyemail/{id}")]
        public IEnumerable<User> GetUsersByEmail(string id)
        {
            return _repo.GetUsers().Where(a => a.Email == id);
        }

        [HttpPost("api/adduser")]
        public void AddUser([FromBody] User input)
        {
            _repo.AddUser(input);
        }
        //[Authorize]
        [HttpGet("api/getusers")]
        public IEnumerable<User> GetUsers()
        {
            return _repo.GetUsers();
        }
        [HttpGet("api/getuserbyid/{id}")]
#nullable enable
        public User? GetUserById(int id)
        {
            return _repo.GetUserById(id);
        }
#nullable disable
        [HttpGet("api/getusersbyname/{id}")]
        public IEnumerable<User> GetUsersByName(string id)
        {
            return _repo.GetUserByName(id);
        }

        [HttpPost("api/updateuserdata")]
        public void UpdateUserData([FromBody] User input, string placeholder = "placeholder")
        {
            _repo.UpdateUserData(input);
        }

        [HttpPost("api/deleteuser")]
        public void DeleteUser(int id)
        {
            _repo.DeleteUser(id);
        }
    }
}
