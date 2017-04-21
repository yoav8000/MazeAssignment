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


        public IController IController
        {
            get
            {
                return this.icontroller;
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
            game.AddPlayer(player);
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
                ActiveMultiPlayerMazes[mazeName] = game;
                joinableMazes.Remove(mazeName);
                return game.Maze;

            }
            catch (Exception exception)
            {
                return null;
            }

        }


        public void Close(string mazeName)
        {
            MazeGame game = ActiveMultiPlayerMazes[mazeName];//getting the game.
            //update that the game was closed.
            game.NotifyPlayers("the game was closed by one of the players");
            //!!!!!!
            activeMultiPlayerMazes.Remove(mazeName);
        }
    }
}