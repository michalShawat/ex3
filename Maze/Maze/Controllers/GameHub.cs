using System.Collections.Concurrent;
using System.Linq;
using Microsoft.AspNet.SignalR;

namespace Maze.Controllers
{
    public class GameHub : Hub
    {
        private static ConcurrentDictionary<string, string> connectedUsers =
           new ConcurrentDictionary<string, string>();

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