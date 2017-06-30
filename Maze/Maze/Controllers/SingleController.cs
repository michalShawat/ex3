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

    /// <summary>
    /// contriller of the single player game
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class SingleController : ApiController
    {
        /// <summary>
        /// My model
        /// </summary>
        private static IModel myModel = new Model();

        // GET: /single/mazeName/0
        /// <summary>
        /// Gets the solve.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="algorithmType">Type of the algorithm.</param>
        /// <returns></returns>
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


        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        // GET: api/single/mazeName/4/5
        //[ActionName("GetMaze")]
        //[Route("api/Single")]
        [HttpGet()]
        public JObject GetMaze(string name, int rows, int cols)
        {
            Maze maze = myModel.GenerateMaze(name, rows, cols);
            JObject obj = JObject.Parse(maze.ToJSON());
            return obj;
        }


        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        ///    // POST: api/Single
        public void Post([FromBody]string value)
        {
        }


        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        // PUT: api/Single/5
        public void Put(int id, [FromBody]string value)
        {
        }


        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        // DELETE: api/Single/5
        public void Delete(int id)
        {
        }
    }
}