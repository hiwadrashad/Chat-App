using Chat_App_JWT_API.Attributes;
using Chat_App_Bussiness_Logic.Configuration;
using Chat_App_Bussiness_Logic.JWT;
using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using Chat_App_Library.Singletons;
using Chat_App_Library.ViewModels;
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
    public class CredentialsController : ControllerBase, ICredentialsController
    {
        private IDatabaseSingleton _databaseSingleton;
        private IRepository _repo;
        private readonly JwtConfig _jwtConfig;
        private JWTTokens _tokenGenerator;
        private readonly TokenValidationParameters _tokenValidationParams;
        private readonly JWTVerification _jwtVerification;
        private readonly ICredentialsService _credentialService;
        public CredentialsController(IDatabaseSingleton databaseSingleton, IOptionsMonitor<JwtConfig> optionsMonitor, 
            TokenValidationParameters tokenValidationParameters, ICredentialsService credentialsService)
        {
            _databaseSingleton = databaseSingleton;
            _repo = databaseSingleton.GetRepository();
            _jwtConfig = optionsMonitor.CurrentValue;
            _tokenGenerator = new JWTTokens(_jwtConfig, databaseSingleton);
            _tokenValidationParams = tokenValidationParameters;
            _jwtVerification = new JWTVerification(databaseSingleton, optionsMonitor, tokenValidationParameters);
            _credentialService = credentialsService;
        }



        [HttpPost("api/makeuseradmin")]
        public async Task<IActionResult> MakeUserAdmin([FromBody] AscendUserToAdminRequest input)
        {
            if (input.RequestingUser.Role != Chat_App_Library.Enums.Role.Admin)
            {
                return BadRequest(new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        "User doesn't have necessary privileges"
                        },
                    Success = false
                });
            }
            else
            {
                input.UserToAscend.Role = Chat_App_Library.Enums.Role.Admin;
                var Return = await _credentialService.MakeUserAdmin(input);
                var ReturnConverted = Return as RegistrationResponse;
                if (ReturnConverted.Success == true)
                {
                    return Ok(Return);
                }
                else
                {
                    return BadRequest(Return);
                }
            }
        }

        [HttpPost("api/register/{password}")]
        public async Task<IActionResult> Register(string password,[FromBody] User input)
        {
            if (ModelState.IsValid)
            {

                //var testtoken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                var existingUsers = _databaseSingleton.GetRepository().GetUsers();

                if (existingUsers.Any(a => a.Username == input.Username))
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
                input.HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash(
                password, Chat_App_Library.Constants.Salts.Saltvalue));
                var Return = await _credentialService.Register(input);
                var ReturnConverted = Return as RegistrationResponse;
                if (ReturnConverted.Success == true)
                {
                    return Ok(jwtToken);
                }
                else
                {
                    return BadRequest(Return);
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
       
        [HttpPost("api/login/{password}")]
       
        public async Task<IActionResult> Login(string password,[FromBody] User input)
        {
            if (ModelState.IsValid)
            {
                var existingUsers = _databaseSingleton.GetRepository().GetUsers();
                if (existingUsers.Any(a => a.Id == input.Id))
                {
                    var databaseuser = existingUsers.FirstOrDefault(a => a.Email == input.Email && a.Username == input.Username);
                    if (Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.CompareHash(password, databaseuser.HashBase64, input.Salt))
                    {
                        var jwtToken = await _tokenGenerator.GenerateJwtToken(input);
                        //HttpContext.Request.Headers["Authorization"] = jwtToken;
                        //var testtoken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                        var Return = await _credentialService.Login(input, jwtToken);
                        var ReturnConverted = Return as AuthResult;
                        if (ReturnConverted != null)
                        {
                            return Ok(jwtToken);
                        }
                        else
                        {
                            return BadRequest(Return);
                        }

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
                var Return = await _credentialService.RefreshToken(tokenRequest);

                var ReturnConverted = Return.GetType() == typeof(AuthResult);
                if (ReturnConverted == true)
                {
                    return Ok(Return);
                }
                else
                {
                    return BadRequest(Return);
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
        [HttpGet("api/getusersbyemail/{id}")]
        public async Task<IActionResult> GetUsersByEmail(string id)
        {
            var Return = await _credentialService.GetUsersByEmail(id);
            var ReturnConverted = Return as IEnumerable<User>;
            if (ReturnConverted != null)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
        }

        //[Authorize]
        [HttpGet("api/getusers/{requestingid}")]
        public async Task<IActionResult> GetUsers(int requestingid)
        {
            var Return = await _credentialService.GetUsers(requestingid);
            var ReturnConverted = Return as IEnumerable<User>;
            if (ReturnConverted != null)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
        }
        [HttpGet("api/getuserbyid/{id}/{requestingid}")]
#nullable enable
        public async Task<IActionResult> GetUserById(int id, int requestingid)
        {
            var Return = await _credentialService.GetUserById(id,requestingid);
            var ReturnConverted = Return as User;
            if (ReturnConverted != null)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
        }
#nullable disable
        [HttpGet("api/getusersbyname/{id}/{requestingid}")]
        public async Task<IActionResult> GetUsersByName(string id, int requestingid)
        {
            var Return = await _credentialService.GetUsersByEmail(id);
            var ReturnConverted = Return as IEnumerable<User>;
            if (ReturnConverted != null)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
        }

        [HttpPost("api/updateuserdata")]
        public async Task<IActionResult> UpdateUserData([FromBody] User input, string placeholder = "placeholder")
        {
            var Return = await _credentialService.UpdateUserData(input);
            var ReturnConverted = Return as RegistrationResponse;
            if (ReturnConverted.Success == true)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
        }

        [HttpPost("api/banuser/{id}/{requestingid}")]
        public async Task<IActionResult> BanUser(int id, int requestingid)
        {
            var Return = await _credentialService.BanUser(id, requestingid);
            var ReturnConverted = Return as RegistrationResponse;
            if (ReturnConverted.Success == true)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
        }
    }
}
