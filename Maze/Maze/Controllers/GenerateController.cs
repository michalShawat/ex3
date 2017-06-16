using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SearchAlgorithmsLib;

namespace Maze.Controllers
{
    using Maze.ModelFromEx1;

    using MazeLib;

    using Newtonsoft.Json.Linq;

    public class GenerateController : ApiController
    {
        private IModel myModel;

        public GenerateController()
        {
            this.myModel = new Model();
        }


        // GET: /generate/mazeName/0
        public JObject GetSolve(string name, int algorithmType)
        {
            ISearcher<Position> algorithmSearcher;
            if (algorithmType == 0)
            {
                algorithmSearcher = new BFS<Position>();
            }
            else
            {
                algorithmSearcher = new DFS<Position>();
            }
            string solution = this.myModel.SolveMaze(name, algorithmSearcher);
            JObject obj = JObject.Parse(solution);
            return obj;
        }

        // GET: api/generate/mazeName/4/5
        
        public JObject GetMaze(string name, int rows, int cols)
        {
            Maze maze = this.myModel.GenerateMaze(name, rows, cols);
            JObject obj = JObject.Parse(maze.ToJSON());
            return obj;
        }


        //// GET: api/Single
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

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
