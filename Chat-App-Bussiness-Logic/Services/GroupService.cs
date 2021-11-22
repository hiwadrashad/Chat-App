using Chat_App_Library.Models;
using Chat_App_Library.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat_App_Library.Extension_Methods;

namespace Chat_App_Bussiness_Logic.Services
{
    public class GroupService
    {
        public async Task<object> AddGroupChat(string password,GroupChat input)
        {
            try
            {
                if (input.GroupOwner == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No group owner found"
                        },
                        Success = false
                    };
                }
                var User = await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().GetUserById(a =>
                a.Id == input.GroupOwner.Id));
                if (User.Role != Chat_App_Library.Enums.Role.Admin && User.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "You are banned!"
                        },
                        Success = false
                    };
                }
                if (InputChecking.ContainsSwearWords(Chat_App_Library.Constants.Swear_Word_Collection.GetAllSwearWords(), input.Title) ||
                    InputChecking.ContainsSwearWords(Chat_App_Library.Constants.Swear_Word_Collection.GetAllSwearWords(), password))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Profanity filter picked up censored expression"
                        },
                        Success = false
                    };
                }

                if (input.ChatBanned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat is banned"
                        },
                        Success = false
                    };
                }

                // add messages in inbox in  v1.2
                if (input.Private == true)
                {
                    if (password.IsNullOrWhiteSpace())
                    {
                        return new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                        "Please give a valid password"
                        },
                            Success = false
                        };
                    }
                    input.HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash(
                    password, Chat_App_Library.Constants.Salts.Saltvalue));
                }
                await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().AddGroupChat(input));
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
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> AddSingleUserChat(string password,SingleUserChat input)
        {
            try
            {
                if (input.OriginUser == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No group owner found"
                        },
                        Success = false
                    };
                }
                var User = await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().GetUserById(a =>
                a.Id == input.OriginUser.Id));
                if (User.Role != Chat_App_Library.Enums.Role.Admin && User.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "You are banned!"
                        },
                        Success = false
                    };
                }
                if (InputChecking.ContainsSwearWords(Chat_App_Library.Constants.Swear_Word_Collection.GetAllSwearWords(), input.Title) ||
                    InputChecking.ContainsSwearWords(Chat_App_Library.Constants.Swear_Word_Collection.GetAllSwearWords(), password))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Profanity filter picked up censored expression"
                        },
                        Success = false
                    };
                }

                if (input.ChatBanned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat is banned"
                        },
                        Success = false
                    };
                }

                // add messages in inbox in  v1.2
                if (input.Private == true)
                {
                    if (password.IsNullOrWhiteSpace())
                    {
                        return new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                        "Please give a valid password"
                        },
                            Success = false
                        };
                    }
                    input.HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash(
                    password, Chat_App_Library.Constants.Salts.Saltvalue));
                }
                await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().AddSingleUserChat(input));
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
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> AddGeneralChat(GeneralChat input)
        {
            try
            {
                if (input.Owner == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No group owner found"
                        },
                        Success = false
                    };
                }
                var User = await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().GetUserById(a =>
                a.Id == input.Owner.Id));
                if (User.Role != Chat_App_Library.Enums.Role.Admin && User.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "You are banned!"
                        },
                        Success = false
                    };
                }
                if (User.Role != Chat_App_Library.Enums.Role.Admin)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Only Admins have this privelege"
                        },
                        Success = false
                    };
                }
                if (InputChecking.ContainsSwearWords(Chat_App_Library.Constants.Swear_Word_Collection.GetAllSwearWords(), input.Title) ||
                    InputChecking.ContainsSwearWords(Chat_App_Library.Constants.Swear_Word_Collection.GetAllSwearWords(), input.Password))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Profanity filter picked up censored expression"
                        },
                        Success = false
                    };
                }

                if (input.ChatBanned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat is banned"
                        },
                        Success = false
                    };
                }

                // add messages in inbox in  v1.2

                await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().AddGeneralChat(input));
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
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> DeleteGroup(int requestuserid,GroupChat input)
        {
            try
            {
                var User = await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().GetUserById(a =>
                a.Id == requestuserid));
                if (User == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No requester found"
                        },
                        Success = false
                    };
                }

                if (User.Role != Chat_App_Library.Enums.Role.Admin && User.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "You are banned!"
                        },
                        Success = false
                    };
                }
                if (input.ChatBanned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat is banned"
                        },
                        Success = false
                    };
                }
                if (User != input.GroupOwner || User.Role != Chat_App_Library.Enums.Role.Admin)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "You don't have the credentials to do this"
                        },
                        Success = false
                    };
                }

                await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().DeleteGroup(input));
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
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> DeleteGeneralChat(int requestuserid, GeneralChat input)
        {
            try
            {
                var User = await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().GetUserById(a =>
                a.Id == requestuserid));
                if (User == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No requester found"
                        },
                        Success = false
                    };
                }

                if (User.Role != Chat_App_Library.Enums.Role.Admin && User.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "You are banned!"
                        },
                        Success = false
                    };
                }
                if (input.ChatBanned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat is banned"
                        },
                        Success = false
                    };
                }
                if (User != input.Owner || User.Role != Chat_App_Library.Enums.Role.Admin)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "You don't have the credentials to do this"
                        },
                        Success = false
                    };
                }

                await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().DeleteGeneralChat(input));
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
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> DeleteSinglePersonChat(int requestuserid, SingleUserChat input)
        {
            try
            {
                var User = await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().GetUserById(a =>
                a.Id == requestuserid));
                if (User == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No requester found"
                        },
                        Success = false
                    };
                }

                if (User.Role != Chat_App_Library.Enums.Role.Admin && User.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "You are banned!"
                        },
                        Success = false
                    };
                }
                if (input.ChatBanned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat is banned"
                        },
                        Success = false
                    };
                }
                if (User != input.OriginUser || User.Role != Chat_App_Library.Enums.Role.Admin || User != input.RecipientUser)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "You don't have the credentials to do this"
                        },
                        Success = false
                    };
                }

                await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().DeleteSiglePersonChat(input));
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
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> GetGroupChatsByUserId(int requestuserid)
        {
            try
            {
                var User = await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().GetUserById(a =>
                a.Id == requestuserid));
                if (User == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No requester found"
                        },
                        Success = false
                    };
                }

                if (User.Role != Chat_App_Library.Enums.Role.Admin && User.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "You are banned!"
                        },
                        Success = false
                    };
                }

                await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().
                GetGroupChatsByUserId(a => a.Id == requestuserid).Where(a => a.ChatBanned != true)
                .Where(a => a.BannedUsers.All(a => a.Id != requestuserid)));

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
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> GetSingleUserChatByUserId(int requestuserid)
        {
            try
            {
                var User = await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().GetUserById(a =>
                a.Id == requestuserid));
                if (User == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No requester found"
                        },
                        Success = false
                    };
                }

                if (User.Role != Chat_App_Library.Enums.Role.Admin && User.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "You are banned!"
                        },
                        Success = false
                    };
                }

                await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().
                GetSingleUserChatByUserId(a => a.Id == requestuserid).Where(a => a.ChatBanned != true)
                .Where(a => a.BannedUsers.All(a => a.Id != requestuserid)));

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
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> GetGroupChats(int requestuserid)
        {
            try
            {
                var User = await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().GetUserById(a =>
                a.Id == requestuserid));
                if (User == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No requester found"
                        },
                        Success = false
                    };
                }

                if (User.Role != Chat_App_Library.Enums.Role.Admin)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Only Admins have this privelege"
                        },
                        Success = false
                    };
                }

                var Return = await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().
                GetGroupChats());

                return Return;
            }
            catch (Exception ex)
            {
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> GetGeneralChat(int requestuserid)
        {
            try
            {
                var User = await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().GetUserById(a =>
                a.Id == requestuserid));
                if (User == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No requester found"
                        },
                        Success = false
                    };
                }

                if (User.Role != Chat_App_Library.Enums.Role.Admin)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Only Admins have this privelege"
                        },
                        Success = false
                    };
                }

                var Return = await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().
                GetGeneralChat());

                return Return;
            }
            catch (Exception ex)
            {
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> GetSingleUserChat(int requestuserid)
        {
            try
            {
                var User = await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().GetUserById(a =>
                a.Id == requestuserid));
                if (User == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No requester found"
                        },
                        Success = false
                    };
                }

                if (User.Role != Chat_App_Library.Enums.Role.Admin)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Only Admins have this privelege"
                        },
                        Success = false
                    };
                }

                var Return = await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().
                GetGeneralChat());

                return Return;
            }
            catch (Exception ex)
            {
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
