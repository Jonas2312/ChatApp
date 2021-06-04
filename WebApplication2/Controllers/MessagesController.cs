using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {


        // GET: api/Message
        [HttpGet]
        public List<ChatMessage> Get()
        {
            return MockDatabase.Messages;
        }

        // GET: api/Message/5
        /*
        public Message Get(int id)
        {
            return "value";
        }*/

        // POST: api/Message
        [HttpPost]
        public async Task<string> Post([FromBody]ChatMessage message)
        {
            MockDatabase.Messages.Add(message);
            return "Message recieved.";
        }

        // PUT: api/Message/5
        /*
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Message/5
        public void Delete(int id)
        {
        }*/
    }
}
