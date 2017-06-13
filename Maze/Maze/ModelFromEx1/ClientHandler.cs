using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;

namespace Server
{
    using System.Threading;

    using Maze.Server;

    /// <summary>
    /// class that handel the interaction between the server and the client 
    /// </summary>
    /// <seealso cref="Server.IClientHandler" />
    public class ClientHandler : IClientHandler
    {
        /// <summary>
        /// the controller
        /// </summary>
        private IController con;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientHandler"/> class.
        /// </summary>
        /// <param name="con">The controller.</param>
        public ClientHandler(IController con)
        {
            this.con = con;
        }

        /// <summary>
        /// Handles the client and the interaction with the server 
        /// </summary>
        /// <param name="client">The client.</param>
        public void HandleClient(TcpClient client)
        {
            new Task(
                () =>
                    {
                        using (NetworkStream stream = client.GetStream())
                        using (BinaryReader reader = new BinaryReader(stream))
                        using (BinaryWriter writer = new BinaryWriter(stream))
                        {
                            while (true)
                            {
                                try
                                {
                                    string commandLine = reader.ReadString();
                                    Console.WriteLine("Got command: {0}", commandLine);
                                    string result = this.con.ExecuteCommand(commandLine, client);
                                    writer.Write(result);
                                }
                                catch (SocketException)
                                {
                                    break;
                                }
                                catch (System.IO.IOException)
                                {
                                    Thread.Sleep(500);
                                    break;
                                }
                            }
                        }
                    }).Start();
        }
    }
}