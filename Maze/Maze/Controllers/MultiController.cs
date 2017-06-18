using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using SearchAlgorithmsLib;
using Maze.ModelFromEx1;
using MazeLib;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;


namespace Maze.Controllers
{
    using Maze.ModelFromEx1;
    using MazeLib;

    public class MultiController : ApiController
    {
        private static IModel myModel = new Model();

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        //public JObject GetMaze(string name, int rows, int cols)
        //{
        //    Maze maze = myModel.StartMaze(name, rows, cols,);
        //    JObject obj = JObject.Parse(maze.ToJSON());
        //    return obj;
        //}

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}