using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// an interface of the client handler
    /// </summary>
    public interface IClientHandler
    {
        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="client">The client.</param>
        void HandleClient(TcpClient client);
    }
}