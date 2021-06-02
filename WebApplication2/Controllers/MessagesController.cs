using Newtonsoft.Json.Linq;
using WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    public class MessagesController : ApiController
    {


        // GET: api/Message
        public IEnumerable<ChatMessage> Get()
        {
            //ChatMessage message = new ChatMessage(new Models.User("server", "password"), "Test message");
            //MockDatabase.Messages.Add(message);

            return MockDatabase.Messages.ToArray();
        }

        // GET: api/Message/5
        /*
        public Message Get(int id)
        {
            return "value";
        }*/

        // POST: api/Message
        public void Post([FromBody]ChatMessage message)
        {
            MockDatabase.Messages.Add(message);
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
