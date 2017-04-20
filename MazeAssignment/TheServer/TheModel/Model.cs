using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using Newtonsoft.Json.Linq;

namespace TheServer.TheModel
{
    public class Model : IModel
    {
        private DFSMazeGenerator mazeGenerator;
        private Dictionary<string, Solution<Position>> mazeSolutions;
        private SearchAlgorithmFactory<Position> algorithmFactory;
        private Dictionary<string, Maze> singlePlayerMazes;
        private Dictionary<string, Maze> multiPlayerMazes;
        private Dictionary<string, Maze> joinableMazes;
        private Dictionary<string, Maze> activeMultiPlayerMazes;



        public Model()
        {
            this.mazeGenerator = new DFSMazeGenerator();
            this.algorithmFactory = new SearchAlgorithmFactory<Position>();
            this.singlePlayerMazes = new Dictionary<string, Maze>();
            this.multiPlayerMazes = new Dictionary<string, Maze>();
            this.joinableMazes = new Dictionary<string, Maze>();
            this.activeMultiPlayerMazes = new Dictionary<string, Maze>();
        }



        public Dictionary<string, Maze> MultiPlayerMazes
        {
            get
            {
                return this.multiPlayerMazes;
            }
        }

        public Dictionary<string, Maze> JoinableMazes
        {
            get
            {
                return this.joinableMazes;
            }
        }

        public Dictionary<string, Maze> ActiveMultiPlayerMazes
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

        public Maze GenerateMaze(string name, int rows, int cols)
        {
            Maze maze = this.mazeGenerator.Generate(rows, cols);
            return maze;
        }


        public Maze GenerateteSinglePlayerMaze(string name, int rows, int cols)
        {
            Maze maze = this.GenerateMaze(name, rows, cols);
            singlePlayerMazes[name] = maze;
            return maze;
        }

        public Maze GenerateMultiPlayerMaze(string name, int rows, int cols)
        {
            Maze maze = this.GenerateMaze(name, rows, cols);
            MultiPlayerMazes[name] = maze;
            JoinableMazes[name] = maze;
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
            JObject jobject = new JObject();
            jobject["Name"] = mazeName;
            jobject["Path"] = solutionAdapter.ToJson();
            jobject["NodesEvaluated"] = solutionAdapter.Solution.NodesEvaluated;//check if it is connected to the solutiion property


            return jobject.ToString();
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



        public Maze JoinMaze(string mazeName)
        {
            if (!NameExistsInDictionary(joinableMazes, mazeName))
            {
                throw new Exception($"there is no such maze with the name {mazeName}");
            }

            try
            {
                ActiveMultiPlayerMazes[mazeName] = JoinableMazes[mazeName];
                RemoveMazeFromJoinableMazes(mazeName);
                return ActiveMultiPlayerMazes[mazeName];

            }
            catch (Exception exception)
            {
                return null;
            }

        }


        private void RemoveMazeFromJoinableMazes(string name)
        {
            if (!JoinableMazes.Remove(name))
            {
                throw new Exception("something went wrong with removing the maze from the joinable mazes list");
            }

        }






    }
}