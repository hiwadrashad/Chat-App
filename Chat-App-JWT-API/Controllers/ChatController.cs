using Chat_App_JWT_API.Attributes;
using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using Chat_App_Library.Singletons;
using Chat_App_Logic.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat_App__JWT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        IDatabaseSingleton _databaseSingleton;
        IRepository _repo;
        IChatService _chatService;
        public ChatController(IDatabaseSingleton databaseSingleton, IChatService chatservice)
        {
            _chatService = chatservice;
            _databaseSingleton = databaseSingleton;
            _repo = databaseSingleton.GetRepository();
        }
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            ChatDbContextRepository DBContext = new ChatDbContextRepository();
            DBContext.SeedMoqData();
            return Ok();
        }

        [HttpGet("api/getmessages/{id}")]
        public async Task<IActionResult> GetMessages(int id)
        {
            var Return = await _chatService.GetMessages(a => a.Id == id);
            var ReturnConverted = Return as List<Message>;
            if (ReturnConverted != null)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
            
        }
        [HttpGet("api/getmessagesbyuserid/{id}")]
        public async Task<IActionResult> GetMessagesByuserId(int id)
        {
            var Return = await _chatService.GetMessagesByUserId(a => a.Id == id);
            var ReturnConverted = Return as List<Message>;
            if (ReturnConverted != null)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
        }
        [HttpPut("api/deletemessagegroup/{id}")]
        public async Task<IActionResult> DeleteMessageGroup(int id, [FromBody] GroupChat input)
        {
            var Return = await _chatService.GetMessagesByUserId(a => a.Id == id);
            var ReturnConverted = Return as List<Message>;
            if (ReturnConverted != null)
            {
                return Ok(Return);
            }
            else
            {
                return BadRequest(Return);
            }
        }
        [HttpPut("api/deletemessagegeneral/{id}")]
        public async Task<IActionResult> DeleteMessageGeneral(int id, [FromBody] GeneralChat input)
        {
            var Return = await _chatService.DeleteMessageGeneral(id,input);
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
        [HttpPut("api/deletemessagesingleuser/{id}")]
        public async Task<IActionResult> DeleteMessageSingleUser(int id, [FromBody] SingleUserChat input)
        {
            var Return = await _chatService.DeleteMessageSingleUser(id, input);
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
        [HttpPut("api/updatemessagetogroupchat/{id}")]
        public async Task<IActionResult> UpdateMessageToGroupChat(int id, [FromBody] Message input)
        {
            var Return = await _chatService.UpdateMessageToGroupChat(id, input);
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
        [HttpPut("api/updatemessagetosingleuserchat/{id}")]
        public async Task<IActionResult> UpdateMessageToSingleUserChat(int id, [FromBody] Message input)
        {
            var Return = await _chatService.UpdateMessageToSingleUserChat(id, input);
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
        [HttpPut("api/updatemessagetogeneralchat/{id}")]
        public async Task<IActionResult> UpdateMessageToGeneralChat(int id, [FromBody] Message input)
        {
            var Return = await _chatService.UpdateMessageToGeneralChat(id, input);
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

        [HttpPut("api/addmessagetogroupchat/{id}")]
        public async Task<IActionResult> AddMessageToGroupChat(int id, [FromBody] Message input)
        {
            var Return = await _chatService.AddMessageToGroupChat(a => a.Id == id, input);
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

        [HttpPut("api/addmessagetosingleuserchat/{id}")]
        public async Task<IActionResult> AddMessageToSingleUserChat(int id, [FromBody] Message input)
        {
            var Return = await _chatService.AddMessageToSingleUserChat(a => a.Id == id, input);
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
        [HttpPut("api/addmessagetogeneralchat/{id}")]
        public async Task<IActionResult> AddMessageToGeneralChat(int id, [FromBody] Message input)
        {
            var Return = await _chatService.AddMessageToGeneralChat(a => a.Id == id, input);
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
    }
}
