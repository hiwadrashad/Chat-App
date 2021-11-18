﻿using Chat_App_Library.Models;
using Chat_App_Library.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Chat_App_Library.Extension_Methods;

namespace Chat_App_Bussiness_Logic.Services
{
    public class InputChecking
    {
        public bool IsEmailValid(string email)
        {
            string expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (Regex.IsMatch(email, expression))
            {
                if (Regex.Replace(email, expression, string.Empty).Length == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ContainsSwearWords(List<string> swearwords, string input)
        {

                foreach (string badWord in swearwords)
                {
                    if (input.Contains(badWord))
                    {
                    return false;
                    }
                }
                return true; 
        }
        

        public bool ValidUserInput(User user)
        {
            if (DatabaseSingleton.GetSingleton().GetRepository().GetUsers()
                 .Any(a => a.Username == user.Username)
                 || user.Username.IsNullOrWhiteSpace() || IsEmailValid(user.Email) ||
                 ContainsSwearWords(Chat_App_Library.Constants.Swear_Word_Collection.GetAllSwearWords(),
                 user.Name) || user.AttemptedPassword.IsNullOrWhiteSpace())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
