using System.Collections.Concurrent;
using System.Linq;
using Microsoft.AspNet.SignalR;

namespace Maze.Controllers
{
    using System.Collections.Generic;

    using Maze.ModelFromEx1;

    using Microsoft.AspNet.SignalR.Hubs;

    using Newtonsoft.Json.Linq;
    [HubName("gameHub")]
    public class GameHub : Hub
    {
        private static IModel myModel = new Model();
        private static ConcurrentDictionary<string, string> connectedUsers =
           new ConcurrentDictionary<string, string>();

        public void Start(string name, int rows, int cols)
        {
            string clientId = Context.ConnectionId;
            myModel.StartMaze(name, rows, cols, clientId);
            //connectedUsers.AddOrUpdate()
        }

        public void List()
        {
            Clients.Client(Context.ConnectionId).parseList(myModel.ListMaze());
        }

        public void Connect(string name)
        {
            connectedUsers[name] = Context.ConnectionId;
        }

        public void SendMessage(string senderName, string recipientName, string text)
        {
            string recipientId = connectedUsers[recipientName];
            if (recipientId == null)
                return;
            Clients.Client(recipientId).gotMessage(senderName, text);
        }
    }
}