using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using CompareMazeSolvers;
using MazeGeneratorLib;
using MazeLib;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;

namespace Server
{
    using Maze = MazeLib.Maze;

    /// <summary>
    /// controls the logic of the program
    /// </summary>
    /// <seealso cref="Server.IModel" />
    public class Model : IModel
    {
        /// <summary>
        /// The controller
        /// </summary>
        private IController con;

        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        /// <param name="con">The controller.</param>
        public Model(IController con)
        {
            this.con = con;
        }

        /// <summary>
        /// The dictionary of the mazes' names to the mazes.
        /// </summary>
        private Dictionary<string, Maze> mazes = new Dictionary<string, Maze>();

        /// <summary>
        /// The dictionary of the available games' names to the games.
        /// </summary>
        private Dictionary<string, Game> games = new Dictionary<string, Game>();

        /// <summary>
        /// The dictionary of the playing games' names to the games
        /// </summary>
        private Dictionary<string, Game> gamesPlaying = new Dictionary<string, Game>();

        /// <summary>
        /// The dictionary of the players' names to the name of the game they are playing.
        /// </summary>
        private Dictionary<TcpClient, string> playing = new Dictionary<TcpClient, string>();

        /// <summary>
        /// The dictionary of the mazes to their solutions
        /// </summary>
        private Dictionary<Maze, Solution<Position>> solutions = new Dictionary<Maze, Solution<Position>>();

        /// <summary>
        /// the property of mazes.
        /// </summary>
        /// <value>
        /// The mazes dictionary.
        /// </value>
        public Dictionary<string, Maze> Mazes
        {
            get
            {
                return this.mazes;
            }
        }

        /// <summary>
        /// the property of games.
        /// </summary>
        /// <value>
        /// The games dictionary.
        /// </value>
        public Dictionary<string, Game> Games
        {
            get
            {
                return this.games;
            }
        }

        /// <summary>
        /// the property of gamesPlaying.
        /// </summary>
        /// <value>
        /// The gamesPlaying dictionary.
        /// </value>
        public Dictionary<string, Game> GamesPlaying
        {
            get
            {
                return this.gamesPlaying;
            }
        }

        /// <summary>
        /// the property of solutions.
        /// </summary>
        /// <value>
        /// The solutions dictionary.
        /// </value>
        public Dictionary<Maze, Solution<Position>> Solutions
        {
            get
            {
                return this.solutions;
            }
        }

        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The maze's name.</param>
        /// <param name="rows">The number of rows.</param>
        /// <param name="cols">The number of cols.</param>
        /// <returns></returns>
        public Maze GenerateMaze(string name, int rows, int cols)
        {
            // create maze
            DFSMazeGenerator myMazeGen = new DFSMazeGenerator();
            Maze myMaze = myMazeGen.Generate(rows, cols);
            myMaze.Name = name;
            mazes.Add(name, myMaze);
            return this.mazes[name];
        }

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="name">The maze's name.</param>
        /// <param name="algorithm">The algorithm type to solve the maze.</param>
        /// <returns></returns>
        public string SolveMaze(string name, ISearcher<Position> algorithm)
        {
            Solution<Position> sol;
            if (!this.solutions.ContainsKey(mazes[name]))
            {
                ObjectAdapter mazeAdapter = new ObjectAdapter(mazes[name]);
                sol = algorithm.Search(mazeAdapter);
                sol.EvaluatedNodes = algorithm.GetNumberOfNodesEvaluated();
                this.solutions.Add(this.mazes[name], sol);
            }

            sol = this.solutions[this.mazes[name]];
            StringBuilder way = new StringBuilder(string.Empty);
            State<Position> first, second;
            for (int i = 0; i < sol.Trace.Count() - 1; ++i)
            {
                first = sol.Trace.ElementAt(i);
                second = sol.Trace.ElementAt(i + 1);

                // left
                if (first.myState.Col < second.myState.Col)
                {
                    way.Append(0);
                }

                // right
                else if (first.myState.Col > second.myState.Col)
                {
                    way.Append(1);
                }

                // up
                else if (first.myState.Row < second.myState.Row)
                {
                    way.Append(2);
                }

                // down
                else if (first.myState.Row > second.myState.Row)
                {
                    way.Append(3);
                }
            }

            return way.ToString();
        }

        /// <summary>
        /// Starts the maze.
        /// </summary>
        /// <param name="name">The maze's name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="client">The client's connection.</param>
        public void StartMaze(string name, int rows, int cols, TcpClient client)
        {
            // create maze
            DFSMazeGenerator myMazeGen = new DFSMazeGenerator();
            Maze myMaze = myMazeGen.Generate(rows, cols);
            myMaze.Name = name;

            // create game
            Game myGame = new Game(myMaze, client);
            this.games.Add(name, myGame);
        }

        /// <summary>
        /// Lists the games waiting for second player.
        /// </summary>
        /// <returns>the list in JSON format</returns>
        public string ListMaze()
        {
            JArray jsonList = new JArray(games.Keys);
            return jsonList.ToString();
        }

        /// <summary>
        /// Joins the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="client">The client.</param>
        /// <returns>the game's maze</returns>
        public Maze JoinMaze(string name, TcpClient client)
        {
            Game game = this.games[name];
            game.SecondPlayer = client;
            this.gamesPlaying.Add(name, game);
            this.playing.Add(client, name);
            this.playing.Add(game.FirstPlayer, name);
            this.Games.Remove(name);

            // print to the first player
            NetworkStream stream = game.FirstPlayer.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write(this.gamesPlaying[name].MyMaze.ToJSON());
            return this.gamesPlaying[name].MyMaze;
        }

        /// <summary>
        /// Plays a move in the game.
        /// </summary>
        /// <param name="move">The move.</param>
        /// <param name="client">The client.</param>
        public void PlayMaze(string move, TcpClient client)
        {
            // find the game
            Game game = this.GamesPlaying[this.playing[client]];

            // find the second client
            TcpClient secondClient;
            if (game.SecondPlayer.Equals(client))
            {
                secondClient = game.FirstPlayer;
            }
            else
            {
                secondClient = game.SecondPlayer;
            }

            // print to the second client
            NetworkStream stream = secondClient.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);

            JObject mazeObj = new JObject();
            mazeObj["Name"] = this.playing[client];
            mazeObj["Direction"] = move;
            writer.Write(mazeObj.ToString());
        }

        /// <summary>
        /// Closes the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="client">The client.</param>
        public void CloseMaze(string name, TcpClient client)
        {
            // find the game
            Game game = this.GamesPlaying[this.playing[client]];

            // find the second client
            TcpClient secondClient;
            if (game.SecondPlayer.Equals(client))
            {
                secondClient = game.FirstPlayer;
            }
            else
            {
                secondClient = game.SecondPlayer;
            }

            // remove the game from all the dictionaries
            this.gamesPlaying.Remove(name);
            this.playing.Remove(client);
            this.playing.Remove(secondClient);

            // print massage to the second client
            NetworkStream stream = secondClient.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            JObject empty = new JObject();
            writer.Write(empty.ToString());
        }
    }
}