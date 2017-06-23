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
        private static IModel myModel = new Model();

        // GET: /generate/mazeName/0
        public string GetSolve(string name, int algorithmType)
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
            string solution = myModel.SolveMaze(name, algorithmSearcher);
            return solution;
        }

        // GET: api/generate/mazeName/4/5

        public JObject GetMaze(string name, int rows, int cols)
        {
            Maze maze = myModel.GenerateMaze(name, rows, cols);
            JObject obj = JObject.Parse(maze.ToJSON());
            return obj;
        }

        public string GetList()
        {
            return myModel.ListMaze();
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