using WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        [HttpGet]
        // GET: api/Users
        public List<User> Get()
        {
            return MockDatabase.Users;
        }

        // GET: api/Users/5
        /*
        public string Get(int id)
        {
            return "value";
        }*/

        // POST: api/Users
        [HttpPost]
        public async Task<string> Post([FromBody]User user)
        {
            if (user.Name == null || user.Name.Length < 3)
                return "Did not register user, name too short.";
            foreach (User u in MockDatabase.Users)
            {
                if (user.Name.Equals(u.Name))
                    return "Did not register user, user already exists.";
            }

            MockDatabase.Users.Add(user);
            return "Registered user " + user.Name;
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
