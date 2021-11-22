using Chat_App_Library.Enums;
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
        // GET: api/<InvitationController>

        [HttpPost("api/addgroupchat/{recieverid}/{grouptype}/{chatid}")]
        public IActionResult SendInvitation(int recieverid,GroupType grouptype,int chatid, [FromBody] Invitation invitation)
        {
                //var User = DatabaseSingleton.GetSingleton().GetRepository().GetUsers().Where(a => a.Id == recieverid).FirstOrDefault();
                return Ok(grouptype);
                //invitation.DateSend = DateTime.Now;
                //if (grouptype == GroupType.groupchat)
                //{
                //invitation.Grouptype = GroupType.groupchat;
                //invitation.GroupId = chatid;

                //}

                //if (grouptype == GroupType.generalchat)
                //{
                //invitation.Grouptype = GroupType.generalchat;
                //invitation.GroupId = chatid;

                //}
                //if (grouptype == GroupType.singleuserchat)
                //{
                //invitation.Grouptype = GroupType.singleuserchat;
                //invitation.GroupId = chatid;

                //}
                //User.Invitations.Add(invitation);
            
        }
    }
}
