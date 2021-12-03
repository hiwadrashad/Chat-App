using Chat_App_Bussiness_Logic.Configuration;
using Chat_App_Bussiness_Logic.JWT;
using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using Chat_App_Library.Singletons;
using Chat_App_Library.ViewModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat_App_Bussiness_Logic.Services;
using Chat_App_Library.Extension_Methods;
using Chat_App_Bussiness_Logic.Logging;


namespace Chat_App_Bussiness_Logic.Services
{
    public class CredentialsService : ICredentialsService
    {
        private IDatabaseSingleton _databaseSingleton;
        private IRepository _repo;
        private readonly JwtConfig _jwtConfig;
        private JWTTokens _tokenGenerator;
        private readonly TokenValidationParameters _tokenValidationParams;
        private readonly JWTVerification _jwtVerification;

        public CredentialsService(IDatabaseSingleton databaseSingleton, IOptionsMonitor<JwtConfig> optionsMonitor,
            TokenValidationParameters tokenValidationParameters)
        {
            _databaseSingleton = databaseSingleton;
            _repo = databaseSingleton.GetRepository();
            _jwtConfig = optionsMonitor.CurrentValue;
            _tokenGenerator = new JWTTokens(_jwtConfig, databaseSingleton);
            _tokenValidationParams = tokenValidationParameters;
            _jwtVerification = new JWTVerification(databaseSingleton, optionsMonitor, tokenValidationParameters);
        }
       
        public async Task<object> Login(User input, AuthResult jwtToken)
        {
            try
            {
                if (!(input.Role == Chat_App_Library.Enums.Role.Admin) && input.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "You are banned!"
                        },
                        Success = false
                    };
                }
                if (input == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User not found!"
                        },
                        Success = false
                    };
                }
                else
                {
                    await Task.Run(() => AuthResultSingleton.GetSingleton().SetAuth(jwtToken));
                    await Task.Run(() =>UserSingleton.GetSingleton().SetUser(input));
                    return jwtToken;
                }
            }
            catch(Exception ex)
            {
                ApiLogging.WriteErrorLog($"Error : {ex.Message}");

                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }
        public async Task<object> MakeUserAdmin(AscendUserToAdminRequest input)
        {
            try
            {
                if (input.RequestingUser == null || input.UserToAscend == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Requesting or User to Ascend missing"
                        },
                        Success = false
                    };
                }

                if (input.UserToAscend.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is banned"
                        },
                        Success = false
                    };
                }
                if (input.RequestingUser.Role != Chat_App_Library.Enums.Role.Admin)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User doesn't have necessary privileges"
                        },
                        Success = false
                    };
                }
                await Task.Run(() => _repo.UpdateUserData(input.UserToAscend));

                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        ""
                        },
                    Success = true
                };
            }
            catch (Exception ex)
            {
                ApiLogging.WriteErrorLog($"Error : {ex.Message}");

                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }


        public async Task<object> Register(User input)
        {
            try
            {
                if (input == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Requesting User missing"
                        },
                        Success = false
                    };
                }

                if (input.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is banned"
                        },
                        Success = false
                    };

                }

                if (input.Email.IsNullOrWhiteSpace() || input.Name.IsNullOrWhiteSpace() || input.Username.IsNullOrWhiteSpace())
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Missing credentials"
                        },
                        Success = false
                    };
                }

                if (!InputChecking.ValidUserInput(input))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Credentials not valid"
                        },
                        Success = false
                    };
                }

                await Task.Run(() => _repo.AddUser(input));
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        ""
                        },
                    Success = true
                };
            }
            catch (Exception ex)
            {
                ApiLogging.WriteErrorLog($"Error : {ex.Message}");

                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> RefreshToken(TokenRequest input)
        {
            try
            {
                var result = await _jwtVerification.VerifyAndGenerateToken(input);

                if (result == null)
                {

                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Invalid tokens"
                    },
                        Success = false
                    };
                }
                else
                {
                    return result;
                }

            }
            catch (Exception ex)
            {
                ApiLogging.WriteErrorLog($"Error : {ex.Message}");

                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> GetUsersByEmail(string id)
        {
            try
            {
               
                var  result = await Task.Run(() => _repo.GetUsers().Where(a => a.Email == id));
  
                
                return result;                

            }
            catch (Exception ex)
            {
                ApiLogging.WriteErrorLog($"Error : {ex.Message}");

                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> GetUsers(int requestingid)
        {
            try
            {
                var User = await Task.Run(() => _repo.GetUserById(a =>
                a.Id == requestingid));


                if (User == null)
                {

                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Requester not fond"
                    },
                        Success = false
                    };
                }
                if (User.Banned == true && User.Role != Chat_App_Library.Enums.Role.Admin)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "You are banned"
                    },
                        Success = false
                    };
                }
                IEnumerable<User> result;
                if (User.Role == Chat_App_Library.Enums.Role.Admin)
                {
                    result = await Task.Run(() => _repo.GetUsers());
                }
                else
                {
                    result = await Task.Run(() => _repo.GetUsers().Where(a => a.Banned != true));
                }
                return result;

            }
            catch (Exception ex)
            {
                ApiLogging.WriteErrorLog($"Error : {ex.Message}");

                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> GetUserById(int id, int requestingid)
        {
            try
            {
                var User = await Task.Run(() => _repo.GetUserById(a =>
                a.Id == requestingid));


                if (User == null)
                {

                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Requester not fond"
                    },
                        Success = false
                    };
                }
                if (User.Banned == true && User.Role != Chat_App_Library.Enums.Role.Admin)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "You are banned"
                    },
                        Success = false
                    };
                }
                 
                var result = await Task.Run(() => _repo.GetUserById(a => a.Id == id));

                if (result == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User not found"
                    },
                        Success = false
                    };
                }

                if (User.Role != Chat_App_Library.Enums.Role.Admin && result.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User you tried to find is banned"
                    },
                        Success = false
                    };
                }

                return result;

            }
            catch (Exception ex)
            {
                ApiLogging.WriteErrorLog($"Error : {ex.Message}");

                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> GetUsersByName(string id, int requestingid)
        {
            try
            {
                var User = await Task.Run(() => _repo.GetUserById(a =>
                a.Id == requestingid));


                if (User == null)
                {

                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Requester not fond"
                    },
                        Success = false
                    };
                }
                if (User.Banned == true && User.Role != Chat_App_Library.Enums.Role.Admin)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "You are banned"
                    },
                        Success = false
                    };
                }

                IEnumerable<User> result;
                if (User.Role == Chat_App_Library.Enums.Role.Admin)
                {
                    result = await Task.Run(() => _repo.GetUserByName(a => a.Name == id));
                }
                else
                {
                    result = await Task.Run(() => _repo.GetUserByName(a => a.Name == id).Where(a => a.Banned != true));
                }
                return result;

            }
            catch (Exception ex)
            {
                ApiLogging.WriteErrorLog($"Error : {ex.Message}");

                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> UpdateUserData(User User)
        {
            try
            {

                if (User == null)
                {

                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Requester not fond"
                    },
                        Success = false
                    };
                }
                if (User.Banned == true && User.Role != Chat_App_Library.Enums.Role.Admin)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "You are banned"
                    },
                        Success = false
                    };
                }

                if (!InputChecking.ValidUserInput(User))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Invalid credentials"
                    },
                        Success = false
                    };
                }

                await Task.Run(() => _repo.UpdateUserData(User));

                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        ""
                    },
                    Success = true
                };
            }
            catch (Exception ex)
            {
                ApiLogging.WriteErrorLog($"Error : {ex.Message}");

                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> BanUser(int id, int requestingid)
        {
            try
            {
                var RequestingUser = await Task.Run(() => _repo.GetUserById(a =>
                 a.Id == requestingid));
                var User = await Task.Run(() => _repo.GetUserById(a =>
                 a.Id == id));
                if (RequestingUser == null)
                {

                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Requester not fond"
                    },
                        Success = false
                    };
                }
                if (RequestingUser.Banned == true && User.Role != Chat_App_Library.Enums.Role.Admin)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "You are banned"
                    },
                        Success = false
                    };
                }

                if (User == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User not found"
                    },
                        Success = false
                    };
                }

                await Task.Run(() => _repo.DeleteUser(id));

                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        ""
                    },
                    Success = true
                };
            }
            catch (Exception ex)
            {
                ApiLogging.WriteErrorLog($"Error : {ex.Message}");

                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }

    }
}
