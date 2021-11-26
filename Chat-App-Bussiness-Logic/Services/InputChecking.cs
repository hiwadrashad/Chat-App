using Chat_App_Library.Models;
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
        private static bool IsEmailValid(string email)
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

        public static bool ContainsSwearWords(List<string> swearwords, string input)
        {
            List<string> splitinput = input.Split(new Char[] {',','\n',' ','.',':'
                ,'/',@"\".ToCharArray()[0],'~','!','@','#','$','%','^','&','*'
                ,'(',')','-','_','+','=','{','}','[',']','|',';'
                ,'"',"'".ToCharArray()[0],'<','>','?' }).ToList();

            foreach(var splitinputword in splitinput)
            {
                foreach (string badWord in swearwords)
                {
                    if (badWord == splitinputword)
                    {
                        return true;
                    }
                }
            }
                return false; 
        }
        

        public static bool ValidUserInput(User user)
        {
            if (DatabaseSingleton.GetSingleton().GetRepository().GetUsers()
                 .Any(a => a.Username == user.Username)
                 || user.Username.IsNullOrWhiteSpace() || IsEmailValid(user.Email) ||
                 ContainsSwearWords(Chat_App_Library.Constants.Swear_Word_Collection.GetAllSwearWords(),
                 user.Name) )
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
