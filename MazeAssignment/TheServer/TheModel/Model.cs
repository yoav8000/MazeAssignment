using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using Newtonsoft.Json.Linq;
using TheServer.TheController;
using TheServer.TheMazeGame;
using System.Net.Sockets;

namespace TheServer.TheModel
{
    public class Model : IModel
    {
        private DFSMazeGenerator mazeGenerator;
        private Dictionary<string, Solution<Position>> mazeSolutions;
        private SearchAlgorithmFactory<Position> algorithmFactory;
        private Dictionary<string, Maze> singlePlayerMazes;
        private Dictionary<string, Maze> multiPlayerMazes;
        private Dictionary<string, MazeGame> joinableMazes;
        private IController icontroller;
        private Dictionary<string, MazeGame> activeMultiPlayerMazes;
        private Dictionary<Player, MazeGame> playersAndGames;


        public Model(IController icontroller)
        {
            this.icontroller = icontroller;
            this.mazeGenerator = new DFSMazeGenerator();
            this.algorithmFactory = new SearchAlgorithmFactory<Position>();
            this.singlePlayerMazes = new Dictionary<string, Maze>();
            this.mazeSolutions = new Dictionary<string, Solution<Position>>();
            this.multiPlayerMazes = new Dictionary<string, Maze>();
            this.joinableMazes = new Dictionary<string, MazeGame>();
            this.activeMultiPlayerMazes = new Dictionary<string, MazeGame>();
            this.playersAndGames = new Dictionary<Player, MazeGame>();
        }

        public Model()///////////////////////for testing deleteeeeee later on!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        {
            this.mazeGenerator = new DFSMazeGenerator();
            this.algorithmFactory = new SearchAlgorithmFactory<Position>();
            this.singlePlayerMazes = new Dictionary<string, Maze>();
            this.mazeSolutions = new Dictionary<string, Solution<Position>>();
            this.multiPlayerMazes = new Dictionary<string, Maze>();
            this.joinableMazes = new Dictionary<string, MazeGame>();
            this.activeMultiPlayerMazes = new Dictionary<string, MazeGame>();
            this.playersAndGames = new Dictionary<Player, MazeGame>();
        }

        public IController IController
        {
            get
            {
                return this.icontroller;
            }
            set
            {
                this.icontroller = value;
            }
        }



        public Dictionary<string, Maze> MultiPlayerMazes
        {
            get
            {
                return this.multiPlayerMazes;
            }
        }

        public Dictionary<string, MazeGame> JoinableMazes
        {
            get
            {
                return this.joinableMazes;
            }
        }

        public Dictionary<string, MazeGame> ActiveMultiPlayerMazes
        {
            get
            {
                return this.activeMultiPlayerMazes;
            }
        }

        

        public Dictionary<string, Maze> SinglePlayerMazes
        {
            get
            {
                return this.singlePlayerMazes;
            }
        }

        public Dictionary<string, Solution<Position>> MazeSolutions
        {
            get
            {
                return this.mazeSolutions;
            }
        }

        public Dictionary<Player, MazeGame> PlayersAndGames
        {
            get
            {
                return this.playersAndGames;
            }
        }

        public DFSMazeGenerator MazeGenerator
        {
            get
            {
                return this.mazeGenerator;
            }
        }


        public SearchAlgorithmFactory<Position> AlgorithmFactory
        {
            get
            {
                return this.algorithmFactory;
            }
        }


        

        private Maze GenerateMaze(string name, int rows, int cols)
        {
            Maze maze = this.mazeGenerator.Generate(rows, cols);
            return maze;
        }


        public Maze GenerateteSinglePlayerMaze(string name, int rows, int cols)
        {
            if (NameExistsInDictionary(singlePlayerMazes, name))
            {
                return null;
            }
            Maze maze = this.GenerateMaze(name, rows, cols);
            maze.Name = name;
            singlePlayerMazes[name] = maze;
            return maze;
        }

        public Maze GenerateMultiPlayerMaze(string name, int rows, int cols,Player player)
        {
            if ((joinableMazes.ContainsKey(name)) || (activeMultiPlayerMazes.ContainsKey(name)))
            {
                return null;
            }
            int playersCapacity = 2;
            Maze maze = this.GenerateMaze(name, rows, cols);
            maze.Name = name;
            MultiPlayerMazes[name] = maze;
            MazeGame game = new MazeGame(name, maze, playersCapacity);
            JoinableMazes[name] = game;
            player.NeedToWait = true;
            game.AddPlayer(player);//the player needs to wait for another player to join the game.
            player.MazeName = game.MazeName;
            return maze;
        }


        private bool NameExistsInDictionary(Dictionary<string, Maze> dictionary, string mazeName)
        {
            return dictionary.ContainsKey(mazeName);
        }

        public string SolveMaze(string mazeName, string algorithm)
        {

            if (!NameExistsInDictionary(SinglePlayerMazes, mazeName))
            {
                throw new Exception($"there is no  maze with this name {mazeName}");
            }

            if (MazeExistsInSolutions(mazeName))
            {
                SolutionAdapter solutionAdapter1 = new SolutionAdapter(MazeSolutions[mazeName], mazeName);
                return solutionAdapter1.ToJson();
            }

            MazeAdapter mazeAdapter = new MazeAdapter(SinglePlayerMazes[mazeName]);
            ISearcher<Position> searchAlgorithm = GetAlgorithmAccordingToIndicator(algorithm);
            Solution<Position> solution = searchAlgorithm.Search(mazeAdapter);
            SolutionAdapter solutionAdapter = new SolutionAdapter(solution, mazeName);
            MazeSolutions[mazeName] = solution;
            return solutionAdapter.ToJson();
/*
            JObject jobject = new JObject();
            jobject["Name"] = mazeName;
            jobject["Solution"] = solutionAdapter.ToJson();
            jobject["NodesEvaluated"] = solutionAdapter.Solution.NodesEvaluated;//check if it is connected to the solutiion property


            return jobject.ToString();
            */
        }


        private bool MazeExistsInSolutions(string mazeName)
        {
            return MazeSolutions.ContainsKey(mazeName);
        }


        public ISearcher<Position> GetAlgorithmAccordingToIndicator(string algorithmIndicator)
        {
            return AlgorithmFactory.GetSearchAlgorithm(algorithmIndicator);
        }


        public List<string> GetNamesOfJoinableMazes()
        {
            List<string> mazesList = new List<string>(JoinableMazes.Keys.ToList());
            return mazesList;
        }



        public Maze JoinMaze(string mazeName, Player player)
        {
            if (!joinableMazes.ContainsKey(mazeName))
            {
                throw new Exception($"there is no such maze with the name {mazeName}");
            }

            try
            {
                MazeGame game = JoinableMazes[mazeName];
                game.AddPlayer(player);
                player.MazeName = game.MazeName;
                if (game.GameCapacity == game.Players.Count)
                {
                    ActiveMultiPlayerMazes[mazeName] = game;
                    joinableMazes.Remove(mazeName);
                    ReleasePlayerFromWaitingMode(game);
                }
                return game.Maze;

            }
            catch (Exception exception)
            {
                return null;
            }

        }

        private void ReleasePlayerFromWaitingMode(MazeGame game)
        {
            foreach(Player p in game.Players)
            {
                PlayersAndGames[p] = game;//adds the players as a key to the dictionary and the mazegame as the value.
                p.NeedToWait = false;
                p.Message = "The Game Has Started";
                p.NeedToBeNotified = true;   
            }

        }


       public string Play(string []args, Player player)
        {
            string direction = args[0];
            if(player.MazeName == null)
            {
                return "Error: you are not a part of an active game";

            }
            string mazeName = player.MazeName;
            if (!ActiveMultiPlayerMazes.ContainsKey(mazeName))
            {
                return $"Error: there is no such maze with the name {mazeName}";
            }
            if(PlayersAndGames[player] == null)
            {
                return ($"Error: you are not a part of a game at this point ");
            }
            MazeGame game = ActiveMultiPlayerMazes[mazeName];
            game.NotifyOtherPlayers($"the other player moved  {direction}", player);
            return "";
        }

        public void Close(string mazeName)
        {
            MazeGame game = ActiveMultiPlayerMazes[mazeName];//getting the game.
            RemovePlayersFromPlayersAndGames(mazeName); //getting the players of the dictionary of the players and games
            game.CloseAllClients();
            activeMultiPlayerMazes.Remove(mazeName);

        }

        private void RemovePlayersFromPlayersAndGames(string mazeName)
        {
            MazeGame game = ActiveMultiPlayerMazes[mazeName];
            foreach(Player p in game.Players)
            {
                PlayersAndGames.Remove(p);
            }
        }

    }
}