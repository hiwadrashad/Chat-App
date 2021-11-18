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

namespace Chat_App_Bussiness_Logic.Services
{
    public class ChatService : IChatService
    {
        public async Task<object> GetMessages()
        {
            try
            {
                var Return = await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().GetMessages());
                if (Return.Count > 0)
                {
                    foreach (var item in Return)
                    {
                        if (item.User == null)
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
            catch
            {
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        "Something went wrong"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> GetMessagesByUserId(int id)
        {
            try
            {
                var Return = await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().GetMessagesByUserId(id));
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
            catch
            {
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        "Something went wrong"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> DeleteMessageGroup(int id, GroupChat input)
        {
            try
            {
                var User = await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().GetUserById(id));

                await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().DeleteMessageGroup(input, id));
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        ""
                        },
                    Success = true
                };
            }
            catch
            {
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        "Something went wrong"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> DeleteMessageGeneral(int id, GroupChat input)
        {
            try
            {
                await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().DeleteMessageGroup(input, id));
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        ""
                        },
                    Success = true
                };
            }
            catch
            {
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        "Something went wrong"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> DeleteMessageSingleUser(int id, SingleUserChat input)
        {
            try
            {
                await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().DeleteMessageSingleUser(input, id));
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        ""
                        },
                    Success = true
                };
            }
            catch
            {
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        "Something went wrong"
                        },
                    Success = false
                };
            }
        }


        public async Task<object> UpdateMessageToGroupChat(int id, Message input)
        {
            try
            {
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
                    await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().UpdateMessageToGroupChat(input, id));
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        ""
                        },
                        Success = true
                    };
                }
            }
            catch
            {
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        "Something went wrong"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> UpdateMessageToSingleUserChat(int id, Message input)
        {
            try
            {
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
                    await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().UpdateMessageToSingleUserChat(input, id));
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        ""
                        },
                        Success = true
                    };
                }
            }
            catch
            {
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        "Something went wrong"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> UpdateMessageToGeneralChat(int id, Message input)
        {
            try
            {
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
                    await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().UpdateMessageToGeneralChat(input, id));
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        ""
                        },
                        Success = true
                    };
                }
            }
            catch
            {
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        "Something went wrong"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> AddMessageToGroupChat(int id, Message input)
        {
            try
            {
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
                    await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().AddMessageToGroupChat(input, id));
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        ""
                        },
                        Success = true
                    };
                }
            }
            catch
            {
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        "Something went wrong"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> AddMessageToSingleUserChat(int id, Message input)
        {
            try
            {
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
                    await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().AddMessageToSingleUserChat(input, id));
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        ""
                        },
                        Success = true
                    };
                }
            }
            catch
            {
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        "Something went wrong"
                        },
                    Success = false
                };
            }
        }

        public async Task<object> AddMessageToGeneralChat(int id, Message input)
        {
            try
            {
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
                    await Task.Run(() => DatabaseSingleton.GetSingleton().GetRepository().AddMessageToGeneralChat(input, id));
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        ""
                        },
                        Success = true
                    };
                }
            }
            catch
            {
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        "Something went wrong"
                        },
                    Success = false
                };
            }
        }
    }
}
