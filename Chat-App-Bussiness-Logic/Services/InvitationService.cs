using Chat_App_Library.Enums;
using Chat_App_Library.Models;
using Chat_App_Library.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat_App_Library.Extension_Methods;
using System.Linq.Expressions;
using Chat_App_Bussiness_Logic.Conversions;
using Chat_App_Bussiness_Logic.Logging;
using Chat_App_Library.Interfaces;

namespace Chat_App_Bussiness_Logic.Services
{
    public class InvitationService : IInvitationService
    {
        IDatabaseSingleton _databaseSingleton;
        IRepository _repo;
        public InvitationService(IDatabaseSingleton databaseSingleton)
        {
            _databaseSingleton = databaseSingleton;
            _repo = databaseSingleton.GetRepository();
        }
        public async Task<object> SendInvitation(int RecieverId, GroupType Grouptype, int Chatid, Invitation Invitation)
        {
            try
            {
                if (Invitation == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No Invitation selected"
                        },
                        Success = false
                    };
                }
                if (Invitation.Accepted == true || Invitation.Seen)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Invitation already answered"
                        },
                        Success = false
                    };
                }
                if (InputChecking.ContainsSwearWords(Chat_App_Library.Constants.Swear_Word_Collection.GetAllSwearWords(), Invitation.Message))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Our profanity filter detected some banned words"
                        },
                        Success = false
                    };
                }
                var User =  await Task.Run(() => _repo
                .GetUsers().Where(a => a.Id == RecieverId).FirstOrDefault());
                if (User == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Recieving user not found"
                        },
                        Success = false
                    };
                }
                Invitation.DateSend = DateTime.Now;
                if (Grouptype == GroupType.groupchat)
                {
                    Invitation.Grouptype = GroupType.groupchat;
                    Invitation.GroupId = Chatid;

                }

                if (Grouptype == GroupType.generalchat)
                {
                    Invitation.Grouptype = GroupType.generalchat;
                    Invitation.GroupId = Chatid;

                }
                if (Grouptype == GroupType.singleuserchat)
                {
                    Invitation.Grouptype = GroupType.singleuserchat;
                    Invitation.GroupId = Chatid;

                }
                await Task.Run(() => User.Invitations.Add(Invitation));
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

        public async Task<object> AcceptInvitation(int RecieverId, Invitation invitation)
        {
            try
            {
                if (invitation == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No Invitation selected"
                        },
                        Success = false
                    };
                }
                if (invitation.Accepted == true && invitation.Seen)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Invitation already answered"
                        },
                        Success = false
                    };
                }
                if (InputChecking.ContainsSwearWords(Chat_App_Library.Constants.Swear_Word_Collection.GetAllSwearWords(), invitation.Message))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Our profanity filter detected some banned words"
                        },
                        Success = false
                    };
                }
                var User = await Task.Run(()=> _repo.GetUsers().Where(a => a.Id == RecieverId).FirstOrDefault());
                if (User == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Recieving user not found"
                        },
                        Success = false
                    };
                }
                if (invitation.Grouptype == GroupType.groupchat)
                {
                    var UserInvitation = User.Invitations.Where(a => a.Id == invitation.Id).FirstOrDefault();
                    UserInvitation.Seen = true;
                    UserInvitation.Accepted = true;

                    var Chat = await Task.Run(() => _repo.GetGroupChats().Where(a => a.Id == invitation.GroupId).FirstOrDefault());
                    if (Chat == null)
                    {
                        return new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                            "Chat not found"
                        },
                            Success = false
                        };
                    }
                    if (Chat.MaxAmountPersons <= Chat.Users.Count() && Chat.MaxAmountPersons != 0)
                    {
                        return new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                            "Chat is full"
                        },
                            Success = false
                        };
                    }
                    Chat.Users.Add(User);
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                            ""
                        },
                        Success = true
                    };

                }

                if (invitation.Grouptype == GroupType.singleuserchat)
                {
                    var UserInvitation = User.Invitations.Where(a => a.Id == invitation.Id).FirstOrDefault();
                    UserInvitation.Seen = true;
                    UserInvitation.Accepted = true;

                    var Chat = await Task.Run(() => _repo.GetSingleUserChat().Where(a => a.Id == invitation.GroupId).FirstOrDefault());
                    if (Chat == null)
                    {
                        return new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                            "Chat not found"
                        },
                            Success = false
                        };
                    }
                    Chat.RecipientUser = User;
                }
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                            "General chats do not have the invitations system"
                        },
                    Success = false
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

        public async Task<object> DeclineInvitation(int RecieverId, Invitation invitation)
        { 
            try
            {
                if (invitation == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No Invitation selected"
                        },
                        Success = false
                    };
                }
                if (invitation.Accepted == true && invitation.Seen)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Invitation already answered"
                        },
                        Success = false
                    };
                }
                if (InputChecking.ContainsSwearWords(Chat_App_Library.Constants.Swear_Word_Collection.GetAllSwearWords(), invitation.Message))
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Our profanity filter detected some banned words"
                        },
                        Success = false
                    };
                }
                var User = await Task.Run(() => _repo.GetUsers().Where(a => a.Id == RecieverId).FirstOrDefault());
                if (User == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "Recieving user not found"
                        },
                        Success = false
                    };
                }
                var UserInvitation = await Task.Run(() => User.Invitations.Where(a => a.Id == invitation.Id).FirstOrDefault());
                UserInvitation.Seen = true;
                UserInvitation.Accepted = false;
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

        public async Task<object> GetGroup(Expression<Func<User,bool>> requestinguserid,string password,GroupType grouptype,int groupid)
        {
            try
            {
                if (grouptype == GroupType.groupchat)
                {
                    var Group = await Task.Run(() => _repo.GetGroupChats().Where(a => a.Id == groupid).FirstOrDefault());
                    var User = await Task.Run(() => _repo.GetUserById(requestinguserid));
                    if (Group == null)
                    {
                        return new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                            "Group not found"
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
                    if (User.Banned == true && User.Role != Role.Admin)
                    {
                        return new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                            "User is banned"
                        },
                            Success = false
                        };
                    }
                    if (Group.BannedUsers.Where(a => a.Id ==
                    ExpressionConversion.ReturnIntegerExpressionParameter<User>(requestinguserid)).Any() && User.Role != Role.Admin)
                    {
                        return new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                            "User is banned"
                        },
                            Success = false
                        };

                    }

                    if (Group.ChatBanned == true && User.Role != Role.Admin)
                    {
                        return new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                            "Chat is removed"
                        },
                            Success = false
                        };
                    }

                    if (Group.GroupOwner.Id == User.Id || User.Role == Role.Admin)
                    {
                        return Group;
                    }

                    if (Group.Private == true)
                    {
                        if (Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.CompareHash(password, Group.HashBase64, Chat_App_Library.Constants.Salts.Saltvalue))
                        {
                            return Group;
                        }
                        else
                        {
                            return new RegistrationResponse()
                            {
                                Errors = new List<string>() {
                                "Password is incorrect"
                                },
                                Success = false
                            };
                        }
                    }
                   

                    return Group;
                }
                if (grouptype == GroupType.singleuserchat)
                {
                    var Group = await Task.Run(() => _repo.GetSingleUserChat().Where(a => a.Id == groupid).FirstOrDefault());
                    var User = await Task.Run(() => _repo.GetUserById(requestinguserid));
                    if (Group == null)
                    {
                        return new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                            "Group not found"
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
                    if (User.Banned == true && User.Role != Role.Admin)
                    {
                        return new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                            "User is banned"
                        },
                            Success = false
                        };
                    }
                    if (Group.BannedUsers.Where(a => a.Id ==
                    ExpressionConversion.ReturnIntegerExpressionParameter<User>(requestinguserid)).Any() && User.Role != Role.Admin)
                    {
                        return new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                            "User is banned"
                        },
                            Success = false
                        };

                    }

                    if (Group.ChatBanned == true && User.Role != Role.Admin)
                    {
                        return new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                            "Chat is removed"
                        },
                            Success = false
                        };
                    }

                    if (Group.OriginUser.Id == User.Id || User.Role == Role.Admin)
                    {
                        return Group;
                    }

                    if (Group.Private == true)
                    {
                        if (Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.CompareHash(password, Group.HashBase64, Chat_App_Library.Constants.Salts.Saltvalue))
                        {
                            return Group;
                        }
                        else
                        {
                            return new RegistrationResponse()
                            {
                                Errors = new List<string>() {
                                "Password is incorrect"
                                },
                                Success = false
                            };
                        }
                    }


                    return Group;
                }
                if (grouptype == GroupType.generalchat)
                {
                    var Group = await Task.Run(() => _repo.GetGeneralChat().Where(a => a.Id == groupid).FirstOrDefault());
                    var User = await Task.Run(() => _repo.GetUserById(requestinguserid));
                    if (Group == null)
                    {
                        return new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                            "Group not found"
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
                    if (User.Banned == true && User.Role != Role.Admin)
                    {
                        return new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                            "User is banned"
                        },
                            Success = false
                        };
                    }
                    if (Group.BannedUsers.Where(a => a.Id ==
                    ExpressionConversion.ReturnIntegerExpressionParameter<User>(requestinguserid)).Any() && User.Role != Role.Admin)
                    {
                        return new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                            "User is banned"
                        },
                            Success = false
                        };

                    }

                    if (Group.ChatBanned == true)
                    {
                        return new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                            "Chat is removed"
                        },
                            Success = false
                        };
                    }

                    return Group;
                }
                else
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "No appropriate grouptype chosen"
                        },
                        Success = false
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
