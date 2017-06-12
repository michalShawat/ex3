using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Maze.Controllers
{
    public class SingleController : ApiController
    {
        // GET: api/Single
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Single/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Single
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Single/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Single/5
        public void Delete(int id)
        {
        }
    }
}
