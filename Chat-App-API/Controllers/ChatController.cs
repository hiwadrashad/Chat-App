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
    public class ChatController : ControllerBase
    {
        IRepository Repo = DatabaseSingleton.GetSingleton().GetRepository();

        [HttpGet("api/getmessages")]
        public IEnumerable<Message> GetMessages()
        {
            return Repo.GetMessages();
        }
        [HttpGet("api/getmessagesbyuserid/{id}")]
        public IEnumerable<Message> GetMessagesByuserId(int id)
        {
            return Repo.GetMessagesByUserId(id);
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ChatController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ChatController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ChatController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ChatController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
