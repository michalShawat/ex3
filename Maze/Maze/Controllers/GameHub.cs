using System.Collections.Concurrent;

using Microsoft.AspNet.SignalR;
using System.Collections.Generic;

using Maze.ModelFromEx1;

using Microsoft.AspNet.SignalR.Hubs;


namespace Maze.Controllers
{
    using MazeLib;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// controller of the multiplayer game
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.SignalR.Hub" />
    [HubName("gameHub")]
    public class GameHub : Hub
    {
        /// <summary>
        /// My model
        /// </summary>
        private static IModel myModel = new Model();
        /// <summary>
        /// The connected users
        /// </summary>
        private static ConcurrentDictionary<string, string> connectedUsers =
           new ConcurrentDictionary<string, string>();

        /// <summary>
        /// The games to users
        /// </summary>
        private static Dictionary<string, List<string>> gamesToUsers =
            new Dictionary<string, List<string>>();

        /// <summary>
        /// The games waiting
        /// </summary>
        private static Dictionary<string, string> gamesWaiting =
            new Dictionary<string, string>();

        /// <summary>
        /// The users to games
        /// </summary>
        private static Dictionary<string, string> usersToGames =
            new Dictionary<string, string>();

        /// <summary>
        /// start a game
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        public void Start(string name, int rows, int cols)
        {
            string clientId = Context.ConnectionId;
            myModel.StartMaze(name, rows, cols, clientId);

            // add to 'waiting' dictionary
            gamesWaiting.Add(name, clientId);
        }

        /// <summary>
        /// Lists this instance.
        /// </summary>
        public void List()
        {
            Clients.Client(Context.ConnectionId).parseList(myModel.ListMaze());
        }

        /// <summary>
        /// Plays the specified direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void Play(string direction)
        {
            // find second player's id
            string secondClientId = myModel.PlayMaze(Context.ConnectionId);

            // update second player
            Clients.Client(secondClientId).updateSecondMaze(direction);
        }

        /// <summary>
        /// Joins the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public void Join(string name)
        {
            string secondClientId = gamesWaiting[name];
            string clientId = Context.ConnectionId;
            Maze maze = myModel.JoinMaze(name, clientId);
            
            // remove the old game
            gamesWaiting.Remove(name);

            // draw for first player
            JObject m = JObject.Parse(maze.ToJSON());
            Clients.Client(clientId).drawTheMaze(m);
            Clients.Client(clientId).drawTheOtherMaze(m);

            // draw for second player
            Clients.Client(secondClientId).drawTheMaze(m);
            Clients.Client(secondClientId).drawTheOtherMaze(m);
        }

        /// <summary>
        /// Connects the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public void Connect(string name)
        {
            connectedUsers[name] = Context.ConnectionId;
        }

    }
}