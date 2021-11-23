using Chat_App_Bussiness_Logic.Services;
using Chat_App_Library.Enums;
using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using Chat_App_Library.Singletons;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat_App_JWT_API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class InvitationController : ControllerBase
    {
        private readonly IInvitationService _invitationService;
        public InvitationController(IInvitationService invitationService)
        {
            _invitationService = invitationService;
        }
        // GET: api/<InvitationController

        [HttpPost("api/sendinvitation/{recieverid}/{grouptype}/{chatid}")]
        public async  Task<IActionResult> SendInvitation(int recieverid,GroupType grouptype,int chatid, [FromBody] Invitation invitation)
        {
            var Return = await _invitationService.SendInvitation(recieverid,grouptype,chatid,invitation);
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

        [HttpPost("api/acceptinvitation/{recieverid}")]
        public async Task<IActionResult> AcceptInvitation(int recieverid,[FromBody] Invitation invitation)
        {
            var Return = await _invitationService.AcceptInvitation(recieverid,invitation);
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

        [HttpPost("api/declineinvitation/{recieverid}")]
        public async Task<IActionResult> DeclineInvitation(int recieverid, [FromBody] Invitation invitation)
        {
            var Return = await _invitationService.AcceptInvitation(recieverid, invitation);
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

        [HttpPost("api/getgroup/{requestinguserid}/{password/{grouptype}/{groupid}}")]
        public async Task<IActionResult> GetGroup(int requestinguserid,string password,GroupType grouptype,int groupid)
        {
            var Return = await _invitationService.GetGroup(a => a.Id == requestinguserid,password,grouptype,groupid);
            var ReturnConverted1 = Return as SingleUserChat;
            var ReturnConverted2 = Return as GroupChat;
            var ReturnConverted3 = Return as GeneralChat;

            if (ReturnConverted1 != null || ReturnConverted2 != null || ReturnConverted3 != null)
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
