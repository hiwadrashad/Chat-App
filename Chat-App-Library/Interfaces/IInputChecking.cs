using Chat_App_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.Interfaces
{
    public interface IInputChecking
    {
        public bool IsEmailValid(string email);
        public bool ContainsSwearWords(List<string> swearwords, string input);
        public bool ValidUserInput(User user);
    }
}
