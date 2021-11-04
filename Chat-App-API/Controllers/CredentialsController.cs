using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using Chat_App_Library.Singletons;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredentialsController : ControllerBase
    {
        IRepository Repo = DatabaseSingleton.GetSingleton().GetRepository();
        // GET: api/<CredentialsController>

        [HttpPost("api/adduser")]
        public void Post([FromBody] User input)
        {
            Repo.AddUser(input);
        }
        [HttpPost("api/addgroupchat")]
        public void Post([FromBody] GroupChat input)
        {
            Repo.AddGroupChat(input);
        }
        [HttpPost("api/addsingleuserchat")]
        public void Post([FromBody] SingleUserChat input)
        {
            Repo.AddSingleUserChat(input);
        }
        [HttpPost("api/addgeneralchat")]
        public void Post([FromBody] GeneralChat input)
        {
            Repo.AddGeneralChat(input);
        }
        [HttpGet("api/getusers")]
        public IEnumerable<User> GetUsers()
        {
            return Repo.GetUsers();
        }
        [HttpGet("api/getgroupschat")]
        public IEnumerable<GroupChat> GetGroupsChat()
        {
            return Repo.GetGroupChats();
        }
        [HttpGet("api/getgeneralchat")]
        public IEnumerable<GeneralChat> GetGeneralChat()
        {
            return Repo.GetGeneralChat();
        }
        [HttpGet("api/getsingleuserchat")]
        public IEnumerable<SingleUserChat> GetSingleUserChat()
        {
            return Repo.GetSingleUserChat();
        }
        [HttpGet("api/getuserbyid/{id}")]
#nullable enable
        public User? GetUserById(int id)
        {
            return Repo.GetUserById(id);
        }
#nullable disable
        [HttpGet("api/getusersbyname/{id}")]
        public IEnumerable<User> GetUsersByName(string id)
        {
            return Repo.GetUserByName(id);
        }
        [HttpGet("api/getgroupchatsbyuserid/{id}")]
        public IEnumerable<GroupChat> GetGroupChatsByUserId(int id)
        {
            return Repo.GetGroupChatsByUserId(id);
        }

        [HttpGet("api/getsingleuserchatbyuserid/{id}")]
        public IEnumerable<SingleUserChat> GetSingleUserChatByUserId(int id)
        {
            return Repo.GetSingleUserChatByUserId(id);
        }

        [HttpPost("api/updateuserdata")]
        public void Post([FromBody] User input, string placeholder = "placeholder")
        {
            Repo.UpdateUserData(input);
        }

        // PUT api/<CredentialsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CredentialsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
