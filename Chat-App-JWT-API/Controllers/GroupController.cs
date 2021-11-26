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
        IGroupService _groupService;
        public GroupController(IDatabaseSingleton databaseSingleton, IGroupService groupService)
        {
            _databaseSingleton = databaseSingleton;
            _repo = databaseSingleton.GetRepository();
            _groupService = groupService;
        }

        [HttpPost("api/addgroupchat/{password}")]
        public async Task<IActionResult> AddGroupChat(string password,[FromBody] GroupChat input)
        {
            var Return = await _groupService.AddGroupChat(password,input);
            var ReturnConverted = Return as RegistrationResponse;
            if (ReturnConverted.Success == true)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
        }
        [HttpPost("api/addsingleuserchat/{password}")]
        public async Task<IActionResult> AddSingleUserChat(string password,[FromBody] SingleUserChat input)
        {
            var Return = await _groupService.AddSingleUserChat(password, input);
            var ReturnConverted = Return as RegistrationResponse;
            if (ReturnConverted.Success == true)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
        }
        [HttpPost("api/addgeneralchat")]
        public async Task<IActionResult> AddGeneralChat([FromBody] GeneralChat input)
        {
            var Return = await _groupService.AddGeneralChat(input);
            var ReturnConverted = Return as RegistrationResponse;
            if (ReturnConverted.Success == true)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
        }
        [HttpPost("api/deletegroup/{requestuserid}")]
        public async Task<IActionResult> DeleteGroup(int requestuserid,[FromBody]GroupChat input)
        {
            var Return = await _groupService.DeleteGroup(requestuserid, input);
            var ReturnConverted = Return as RegistrationResponse;
            if (ReturnConverted.Success == true)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
        }
        [HttpPost("api/deletegeneralchat/{requestuserid}")]
        public async Task<IActionResult> DeleteGeneralChat(int requestuserid,[FromBody] GeneralChat input)
        {
            var Return = await _groupService.DeleteGeneralChat(requestuserid, input);
            var ReturnConverted = Return as RegistrationResponse;
            if (ReturnConverted.Success == true)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
        }
        [HttpPost("api/deletesinglepersonchat{requestuserid}")]
        public async Task<IActionResult> DeleteSinglePersonChat(int requestuserid,[FromBody] SingleUserChat input)
        {
            var Return = await _groupService.DeleteSinglePersonChat(requestuserid, input);
            var ReturnConverted = Return as RegistrationResponse;
            if (ReturnConverted.Success == true)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
        }
        [HttpGet("api/getgroupchatsbyuserid/{id}")]
        public async Task<IActionResult> GetGroupChatsByUserId(int id)
        {
            var Return = await _groupService.GetGroupChats(id);
            var ReturnConverted = Return as IEnumerable<GroupChat>;
            if (ReturnConverted != null)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
        }

        [HttpGet("api/getsingleuserchatbyuserid/{id}")]
        public async Task<IActionResult> GetSingleUserChatByUserId(int id)
        {
            var Return = await _groupService.GetSingleUserChatByUserId(id);
            var ReturnConverted = Return as IEnumerable<SingleUserChat>;
            if (ReturnConverted != null)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
        }
        [HttpGet("api/getgroupschat/{requestuserid}")]
        public async Task<IActionResult> GetGroupsChat(int requestuserid)
        {
            var Return = await _groupService.GetGroupChats(requestuserid);
            var ReturnConverted = Return as List<GroupChat>;
            if (ReturnConverted != null)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
        }
        [HttpGet("api/getgeneralchat/{requestuserid}")]
        public async Task<IActionResult> GetGeneralChat(int requestuserid)
        {
            var Return = await _groupService.GetGeneralChat(requestuserid);
            var ReturnConverted = Return as List<GeneralChat>;
            if (ReturnConverted != null)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
        }
        [HttpGet("api/getsingleuserchat/{requestuserid}")]
        public async Task<IActionResult> GetSingleUserChat(int requestuserid)
        {
            var Return = await _groupService.GetSingleUserChat(requestuserid);
            var ReturnConverted = Return as List<GeneralChat>;
            if (ReturnConverted != null)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
        }
    }
}
