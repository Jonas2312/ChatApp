using WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        // GET: api/Users
        public IEnumerable<User> Get()
        {
            return MockDatabase.Users.ToArray();
        }

        // GET: api/Users/5
        /*
        public string Get(int id)
        {
            return "value";
        }*/

        // POST: api/Users
        [HttpPost]
        public void Post([FromBody]User user)
        {
            MockDatabase.Users.Add(user);
        }

        // PUT: api/Users/5
        /*
        public void Put(int id, [FromBody]string value)
        {
        }*/

        // DELETE: api/Users/5
        /*
        public void Delete(int id)
        {
        }*/
    }
}
