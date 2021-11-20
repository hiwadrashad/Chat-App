using Microsoft.AspNetCore.Mvc;
using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using Chat_App_Library.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat_App__JWT_API.Controllers
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
        [HttpPost("api/deletegroup/{requestuserid}")]
        public void DeleteGroup(int requestuserid,[FromBody]GroupChat input)
        {
            _repo.DeleteGroup(input);
        }
        [HttpPost("api/deletegeneralchat/{requestuserid}")]
        public void DeleteGeneralChat(int requestuserid,[FromBody] GeneralChat input)
        {
            _repo.DeleteGeneralChat(input);
        }
        [HttpPost("api/deletesinglepersonchat{requestuserid}")]
        public void DeleteSinglePersonChat(int requestuserid,[FromBody] SingleUserChat input)
        {
            _repo.DeleteSiglePersonChat(input);
        }
        [HttpGet("api/getgroupchatsbyuserid/{id}")]
        public IEnumerable<GroupChat> GetGroupChatsByUserId(int id)
        {
            return _repo.GetGroupChatsByUserId(a => a.Users.All(a => a.Id == id) || a.GroupOwner.Id == id);
        }

        [HttpGet("api/getsingleuserchatbyuserid/{id}")]
        public IEnumerable<SingleUserChat> GetSingleUserChatByUserId(int id)
        {
            return _repo.GetSingleUserChatByUserId(a => a.OriginUser.Id == id || a.RecipientUser.Id == id);
        }
        [HttpGet("api/getgroupschat/{requestuserid}")]
        public IEnumerable<GroupChat> GetGroupsChat(int requestuserid)
        {
            return _repo.GetGroupChats();
        }
        [HttpGet("api/getgeneralchat/{requestuserid}")]
        public IEnumerable<GeneralChat> GetGeneralChat(int requestuserid)
        {
            return _repo.GetGeneralChat();
        }
        [HttpGet("api/getsingleuserchat/{requestuserid}")]
        public IEnumerable<SingleUserChat> GetSingleUserChat(int requestuserid)
        {
            return _repo.GetSingleUserChat();
        }
    }
}
