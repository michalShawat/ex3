using System.Net.Sockets;
using MazeLib;

namespace Server
{
    using Maze = MazeLib.Maze;

    /// <summary>
    /// A class for the multi player games 
    /// contains the maze and two players 
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The first player
        /// </summary>
        private TcpClient firstPlayer;

        /// <summary>
        /// The second player
        /// </summary>
        private TcpClient secondPlayer;

        /// <summary>
        /// The maze
        /// </summary>
        private Maze maze;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="maze">The maze.</param>
        /// <param name="firstPlayer">The first player.</param>
        public Game(Maze maze, TcpClient firstPlayer)
        {
            this.maze = maze;
            this.firstPlayer = firstPlayer;
        }

        /// <summary>
        /// Gets or sets the second player.
        /// </summary>
        /// <value>
        /// The second player.
        /// </value>
        public TcpClient SecondPlayer
        {
            get
            {
                return this.secondPlayer;
            }

            set
            {
                this.secondPlayer = value;
            }
        }

        /// <summary>
        /// Gets the first player.
        /// </summary>
        /// <value>
        /// The first player.
        /// </value>
        public TcpClient FirstPlayer
        {
            get
            {
                return this.firstPlayer;
            }
        }

        /// <summary>
        /// Gets my maze.
        /// </summary>
        /// <value>
        /// My maze.
        /// </value>
        public Maze MyMaze
        {
            get
            {
                return this.maze;
            }
        }
    }
}