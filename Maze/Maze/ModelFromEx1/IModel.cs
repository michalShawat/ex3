
namespace Maze.Server
{
    using System.Collections.Generic;
    using System.Net.Sockets;
    using MazeLib;
    using SearchAlgorithmsLib;
    using Maze = MazeLib.Maze;

    /// <summary>
    /// an interface of the model 
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Gets the mazes dictionary.
        /// </summary>
        /// <value>
        /// The mazes.
        /// </value>
        Dictionary<string, Maze> Mazes { get; }

        /// <summary>
        /// Gets the games dictionary.
        /// </summary>
        /// <value>
        /// The games.
        /// </value>
        Dictionary<string, Game> Games { get; }

        /// <summary>
        /// Gets the games playing dictionary.
        /// </summary>
        /// <value>
        /// The games playing.
        /// </value>
        Dictionary<string, Game> GamesPlaying { get; }

        /// <summary>
        /// Gets the solutions dictionary.
        /// </summary>
        /// <value>
        /// The solutions.
        /// </value>
        Dictionary<Maze, Solution<Position>> Solutions { get; }

        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name of the maze .</param>
        /// <param name="rows">The rows of the maze.</param>
        /// <param name="cols">The cols of the maze.</param>
        /// <returns>the maze</returns>
        Maze GenerateMaze(string name, int rows, int cols);

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="name">The name of the maze.</param>
        /// <param name="algorithm">The specified algorithm( bfs or dfs).</param>
        /// <returns>the string of the solution</returns>
        string SolveMaze(string name, ISearcher<Position> algorithm);

        /// <summary>
        /// Starts the maze.
        /// </summary>
        /// <param name="name">The name of the maze .</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="client">The client.</param>
        void StartMaze(string name, int rows, int cols, TcpClient client);

        /// <summary>
        /// return a lists of the mazes.
        /// </summary>
        /// <returns>the list of the games</returns>
        string ListMaze();

        /// <summary>
        /// Joins the maze.
        /// </summary>
        /// <param name="name">The name of the maze we want to join.</param>
        /// <param name="client">The client.</param>
        /// <returns>the joined maze</returns>
        Maze JoinMaze(string name, TcpClient client);

        /// <summary>
        /// Play- move one step in the maze .
        /// </summary>
        /// <param name="move">The move.</param>
        /// <param name="client">The client.</param>
        void PlayMaze(string move, TcpClient client);

        /// <summary>
        /// Closes the maze.
        /// </summary>
        /// <param name="name">The name of the maze we 
        /// want to close .</param>
        /// <param name="client">The client.</param>
        void CloseMaze(string name, TcpClient client);
    }
}