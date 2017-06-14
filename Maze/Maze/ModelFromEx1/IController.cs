using System.Net.Sockets;

namespace Maze.ModelFromEx1
{

    /// <summary>
    /// an interface of the controller 
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        IModel Model { get; set; }

        /// <summary>
        /// Gets or sets the ClientHandler.
        /// </summary>
        /// <value>
        /// The ClientHandler.
        /// </value>
        IClientHandler Ch { get; set; }

        /// <summary>
        /// Sets the dictionary.
        /// </summary>
        void SetDic();

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        /// <param name="client">The client.</param>
        /// <returns>the string returning from the command</returns>
        string ExecuteCommand(string commandLine, TcpClient client);
    }
}