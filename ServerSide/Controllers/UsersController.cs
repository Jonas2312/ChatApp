
using Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServerSide.Controllers
{
    public class UsersController : ApiController
    {
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
        public async Task<string> Post([FromBody]User user)
        {
            if(user.Name == null || user.Name.Length < 3)
                return "Did not register user, name too short.";
            foreach(User u in MockDatabase.Users)
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
