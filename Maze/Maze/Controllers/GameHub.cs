using System.Collections.Concurrent;

using Microsoft.AspNet.SignalR;
using System.Collections.Generic;

using Maze.ModelFromEx1;

using Microsoft.AspNet.SignalR.Hubs;


namespace Maze.Controllers
{
    using MazeLib;

    using Newtonsoft.Json.Linq;

    [HubName("gameHub")]
    public class GameHub : Hub
    {
        private static IModel myModel = new Model();
        private static ConcurrentDictionary<string, string> connectedUsers =
           new ConcurrentDictionary<string, string>();

        private static Dictionary<string, List<string>> gamesToUsers =
            new Dictionary<string, List<string>>();

        private static Dictionary<string, string> gamesWaiting =
            new Dictionary<string, string>();

        public void Start(string name, int rows, int cols)
        {
            string clientId = Context.ConnectionId;
            myModel.StartMaze(name, rows, cols, clientId);

            // add to waiting dictionary
            gamesWaiting.Add(name, clientId);
        }

        public void List()
        {
            Clients.Client(Context.ConnectionId).parseList(myModel.ListMaze());
        }

        public void Join(string name)
        {
            string secondClientId = gamesWaiting[name];
            string clientId = Context.ConnectionId;
            Maze maze = myModel.JoinMaze(name, clientId);

            // add to playing dictionary?

            // remove the old game
            gamesWaiting.Remove(name);

            // draw for first player
            JObject m = JObject.Parse(maze.ToJSON());
            Clients.Client(clientId).drawTheMaze(m);
            Clients.Client(clientId).drawTheOtherMaze(m);

            // draw for second player
            Clients.Client(secondClientId).drawTheMaze(m);
            Clients.Client(secondClientId).drawTheOtherMaze(m);

            // add to new dictionary
            List<string> players = new List<string>();
            players.Add(clientId);
            players.Add(secondClientId);
            gamesToUsers.Add(name,players);
        }

        public void Connect(string name)
        {
            connectedUsers[name] = Context.ConnectionId;
        }

        //public void SendMessage(string senderName, string recipientName, string text)
        //{
        //    string recipientId = connectedUsers[recipientName];
        //    if (recipientId == null)
        //        return;
        //    Clients.Client(recipientId).gotMessage(senderName, text);
        //}
    }
}