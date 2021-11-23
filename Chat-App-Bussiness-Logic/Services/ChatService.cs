using Chat_App_Library.Models;
using Chat_App_Library.Singletons;
using Chat_App_Logic.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat_App_Library.Extension_Methods;
using Chat_App_Library.Interfaces;
using System.Linq.Expressions;
using Chat_App_Bussiness_Logic.Conversions;
using Chat_App_Bussiness_Logic.Logging;


namespace Chat_App_Bussiness_Logic.Services
{
    public class ChatService : IChatService
    {
        IDatabaseSingleton _databaseSingleton;
        IRepository _repo;
        public ChatService(IDatabaseSingleton databaseSingleton)
        {
            _databaseSingleton = databaseSingleton;
            _repo = databaseSingleton.GetRepository();
        }
        public async Task<object> GetMessages(Expression<Func<User,bool>> id)
        {
            try
            {
                var User = await Task.Run(() => _repo.GetUserById(a =>
                a.Id == ExpressionConversion.ReturnIntegerExpressionParameter<User>(id)));
                if (User.Role != Chat_App_Library.Enums.Role.Admin)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Only Admins have this right!"
                        },
                        Success = false
                    };
                }
                var Return = await Task.Run(() => _repo.GetMessages());

                    return Return;

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

        public async Task<object> GetMessagesByUserId(Expression<Func<Message, bool>> id)
        {
            try
            {

                var User = await Task.Run(() => _repo.GetUserById(a => 
                a.Id == ExpressionConversion.ReturnIntegerExpressionParameter<Message>(id)));
                if (User.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is banned"
                        },
                        Success = false
                    };
                }
                var Return = await Task.Run(() => _repo.GetMessagesByUserId(id));
                if (Return.Count > 0)
                {
                    foreach (var message in Return)
                    {
                        if (message.User == null)
                        {
                            return new RegistrationResponse()
                            {
                                Errors = new List<string>() {
                                "No user found"
                        },
                                Success = false
                            };
                        }
                    }
                }
                else
                {
                    return Return;
                }
                return Return;
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

        public async Task<object> DeleteMessageGroup(int id, GroupChat input)
        {
            try
            {

                var User = await Task.Run(() => _repo.GetUserById(a =>
                a.Id == id));
                if (User.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is banned"
                        },
                        Success = false
                    };
                }
                if (input.ChatBanned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat is removed"
                        },
                        Success = false
                    };
                }

                if (input.BannedUsers.Contains(User))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is Banned"
                        },
                        Success = false
                    };
                }

                if (!input.Users.Contains(User) && !(User.Role == Chat_App_Library.Enums.Role.Admin))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is restricted"
                        },
                        Success = false
                    };
                }

                await Task.Run(() => _repo.DeleteMessageGroup(input, id));
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

        public async Task<object> DeleteMessageGeneral(int id, GeneralChat input)
        {
            try
            {
                var User = await Task.Run(() => _repo.GetUserById(a =>
                a.Id == id));
                if (User.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is banned"
                        },
                        Success = false
                    };
                }
                if (input.ChatBanned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat is removed"
                        },
                        Success = false
                    };
                }

                if (input.BannedUsers.Contains(User))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is Banned"
                        },
                        Success = false
                    };
                }

                await Task.Run(() => _repo.DeleteMessageGeneral(input, id));
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

        public async Task<object> DeleteMessageSingleUser(int id, SingleUserChat input)
        {
            try
            {
                var User = await Task.Run(() => _repo.GetUserById(a =>
           a.Id == id));
                if (User.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is banned"
                        },
                        Success = false
                    };
                }
                if (input.ChatBanned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat is removed"
                        },
                        Success = false
                    };
                }


                if (input.BannedUsers.Contains(User))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is Banned"
                        },
                        Success = false
                    };
                }
                await Task.Run(() => _repo.DeleteMessageSingleUser(input, id));
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


        public async Task<object> UpdateMessageToGroupChat(int id, Message input)
        {
            try
            {
                var Chat = await Task.Run(() => _repo.GetGroupChats().Where(a => a.Id == id).FirstOrDefault());
                if (InputChecking.ContainsSwearWords(Chat_App_Library.Constants.Swear_Word_Collection.GetAllSwearWords(), input.Text))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Profanity filter picked up censored expression"
                        },
                        Success = false
                    };
                }
                if (Chat == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat doesn't exist anymore"
                        },
                        Success = false
                    };
                }

                if (Chat.ChatBanned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat is removed"
                        },
                        Success = false
                    };
                }
                var User = await Task.Run(() => _repo.GetUserById(a =>
                a.Id == input.User.Id));

                if (User.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is banned"
                        },
                        Success = false
                    };
                }

                if (Chat.BannedUsers.Contains(User))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is Banned"
                        },
                        Success = false
                    };
                }

                if (!Chat.Users.Contains(User) && !(User.Role == Chat_App_Library.Enums.Role.Admin))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is restricted"
                        },
                        Success = false
                    };
                }
                if (input.User == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No user found"
                        },
                        Success = false
                    };
                }
                else
                {
                    await Task.Run(() => _repo.UpdateMessageToGroupChat(input, id));
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        ""
                        },
                        Success = true
                    };
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

        public async Task<object> UpdateMessageToSingleUserChat(int id, Message input)
        {
            try
            {

                var Chat = await Task.Run(() => _repo.GetSingleUserChat().Where(a => a.Id == id).FirstOrDefault());
                if (InputChecking.ContainsSwearWords(Chat_App_Library.Constants.Swear_Word_Collection.GetAllSwearWords(), input.Text))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Profanity filter picked up censored expression"
                        },
                        Success = false
                    };
                }
                if (Chat == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat doesn't exist anymore"
                        },
                        Success = false
                    };
                }
                if (Chat.ChatBanned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat is removed"
                        },
                        Success = false
                    };
                }
                var User = await Task.Run(() => _repo.GetUserById(a =>
                a.Id == input.User.Id));

                if (User.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is banned"
                        },
                        Success = false
                    };
                }

                if (Chat.BannedUsers.Contains(User))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is Banned"
                        },
                        Success = false
                    };
                }
                if (input.User == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No user found"
                        },
                        Success = false
                    };
                }
                else
                {
                    await Task.Run(() => _repo.UpdateMessageToSingleUserChat(input, id));
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        ""
                        },
                        Success = true
                    };
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

        public async Task<object> UpdateMessageToGeneralChat(int id, Message input)
        {
            try
            {

                var Chat = await Task.Run(() => _repo.GetGeneralChat().Where(a => a.Id == id).FirstOrDefault());
                if (InputChecking.ContainsSwearWords(Chat_App_Library.Constants.Swear_Word_Collection.GetAllSwearWords(), input.Text))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Profanity filter picked up censored expression"
                        },
                        Success = false
                    };
                }
                if (Chat == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat doesn't exist anymore"
                        },
                        Success = false
                    };
                }
                if (Chat.ChatBanned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat is removed"
                        },
                        Success = false
                    };
                }
                var User = await Task.Run(() => _repo.GetUserById(a =>
                a.Id == input.User.Id));

                if (User.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is banned"
                        },
                        Success = false
                    };
                }

                if (Chat.BannedUsers.Contains(User))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is Banned"
                        },
                        Success = false
                    };
                }
                if (input.User == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No user found"
                        },
                        Success = false
                    };
                }
                else
                {
                    await Task.Run(() => _repo.UpdateMessageToGeneralChat(input, id));
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        ""
                        },
                        Success = true
                    };
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

        public async Task<object> AddMessageToGroupChat(Expression<Func<GroupChat, bool>> id , Message input)
        {
            try
            {

                var Chat = await Task.Run(() => _repo.GetGroupChats()
                .Where(a => a.Id == ExpressionConversion.ReturnIntegerExpressionParameter<GroupChat>(id)).FirstOrDefault());
                if (InputChecking.ContainsSwearWords(Chat_App_Library.Constants.Swear_Word_Collection.GetAllSwearWords(), input.Text))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Profanity filter picked up censored expression"
                        },
                        Success = false
                    };
                }
                if (Chat == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat doesn't exist anymore"
                        },
                        Success = false
                    };
                }
                if (Chat.ChatBanned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat is removed"
                        },
                        Success = false
                    };
                }
                var User = await Task.Run(() => _repo.GetUserById(a =>
                a.Id == input.User.Id));

                if (User.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is banned"
                        },
                        Success = false
                    };
                }

                if (Chat.BannedUsers.Contains(User))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is Banned"
                        },
                        Success = false
                    };
                }
                if (input.User == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No user found"
                        },
                        Success = false
                    };
                }
                else
                {
                    await Task.Run(() => _repo.AddMessageToGroupChat(input, id));
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        ""
                        },
                        Success = true
                    };
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

        public async Task<object> AddMessageToSingleUserChat(Expression<Func<SingleUserChat, bool>> id, Message input)
        {
            try
            {

                var Chat = await Task.Run(() => _repo.GetSingleUserChat()
                        .Where(a => a.Id == ExpressionConversion.ReturnIntegerExpressionParameter<SingleUserChat>(id)).FirstOrDefault());
                if (InputChecking.ContainsSwearWords(Chat_App_Library.Constants.Swear_Word_Collection.GetAllSwearWords(), input.Text))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Profanity filter picked up censored expression"
                        },
                        Success = false
                    };
                }
                if (Chat == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat doesn't exist anymore"
                        },
                        Success = false
                    };
                }
                if (Chat.ChatBanned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat is removed"
                        },
                        Success = false
                    };
                }
                var User = await Task.Run(() => _repo.GetUserById(a =>
                a.Id == input.User.Id));

                if (User.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is banned"
                        },
                        Success = false
                    };
                }

                if (Chat.BannedUsers.Contains(User))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is Banned"
                        },
                        Success = false
                    };
                }
                if (input.User == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No user found"
                        },
                        Success = false
                    };
                }
                else
                {
                    await Task.Run(() => _repo.AddMessageToSingleUserChat(input,id));
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        ""
                        },
                        Success = true
                    };
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

        public async Task<object> AddMessageToGeneralChat(Expression<Func<GeneralChat, bool>> id, Message input)
        {
            try
            {
                var Chat = await Task.Run(() => _repo.GetGeneralChat()
                         .Where(a => a.Id == ExpressionConversion.ReturnIntegerExpressionParameter<GeneralChat>(id)).FirstOrDefault());
                if (InputChecking.ContainsSwearWords(Chat_App_Library.Constants.Swear_Word_Collection.GetAllSwearWords(), input.Text))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Profanity filter picked up censored expression"
                        },
                        Success = false
                    };
                }
                if (Chat == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat doesn't exist anymore"
                        },
                        Success = false
                    };
                }
                if (Chat.ChatBanned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Chat is removed"
                        },
                        Success = false
                    };
                }
                var User = await Task.Run(() => _repo.GetUserById(a =>
                a.Id == input.User.Id));

                if (User.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is banned"
                        },
                        Success = false
                    };
                }

                if (Chat.BannedUsers.Contains(User))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User is Banned"
                        },
                        Success = false
                    };
                }
                if (input.User == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No user found"
                        },
                        Success = false
                    };
                }
                else
                {
                    await Task.Run(() => _repo.AddMessageToGeneralChat(input, id));
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        ""
                        },
                        Success = true
                    };
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

    }
}
