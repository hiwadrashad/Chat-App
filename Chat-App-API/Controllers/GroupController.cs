using Microsoft.AspNetCore.Mvc;
using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using Chat_App_Library.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        IDatabaseSingleton _databaseSingleton;
        IRepository _repo;
        public GroupController(IDatabaseSingleton databaseSingleton)
        {
            _databaseSingleton = databaseSingleton;
            _repo = databaseSingleton.GetRepository();
        }

        [HttpPost("api/addgroupchat")]
        public void AddGroupChat([FromBody] GroupChat input)
        {
            _repo.AddGroupChat(input);
        }
        [HttpPost("api/addsingleuserchat")]
        public void AddSingleUserChat([FromBody] SingleUserChat input)
        {
            _repo.AddSingleUserChat(input);
        }
        [HttpPost("api/addgeneralchat")]
        public void AddGeneralChat([FromBody] GeneralChat input)
        {
            _repo.AddGeneralChat(input);
        }
        [HttpPost("api/deletegroup")]
        public void DeleteGroup(GroupChat input)
        {
            _repo.DeleteGroup(input);
        }
        [HttpPost("api/deletegeneralchat")]
        public void DeleteGeneralChat(GeneralChat input)
        {
            _repo.DeleteGeneralChat(input);
        }
        [HttpPost("api/deletesinglepersonchat")]
        public void DeleteSinglePersonChat(SingleUserChat input)
        {
            _repo.DeleteSiglePersonChat(input);
        }
        [HttpGet("api/getgroupchatsbyuserid/{id}")]
        public IEnumerable<GroupChat> GetGroupChatsByUserId(int id)
        {
            return _repo.GetGroupChatsByUserId(id);
        }

        [HttpGet("api/getsingleuserchatbyuserid/{id}")]
        public IEnumerable<SingleUserChat> GetSingleUserChatByUserId(int id)
        {
            return _repo.GetSingleUserChatByUserId(id);
        }
        [HttpGet("api/getgroupschat")]
        public IEnumerable<GroupChat> GetGroupsChat()
        {
            return _repo.GetGroupChats();
        }
        [HttpGet("api/getgeneralchat")]
        public IEnumerable<GeneralChat> GetGeneralChat()
        {
            return _repo.GetGeneralChat();
        }
        [HttpGet("api/getsingleuserchat")]
        public IEnumerable<SingleUserChat> GetSingleUserChat()
        {
            return _repo.GetSingleUserChat();
        }
    }
}
