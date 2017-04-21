using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using TheServer.TheController;

namespace TheServer.TheModel
{
    public interface IModel
    {

        Maze GenerateMaze(string name, int rows, int cols);
        Maze GenerateMultiPlayerMaze(string name, int rows, int cols);
        Maze GenerateteSinglePlayerMaze(string name, int rows, int cols);
        string SolveMaze(string mazeName, string algorithm);
        ISearcher<Position> GetAlgorithmAccordingToIndicator(string algorithmIndicator);
        List<string> GetNamesOfJoinableMazes();
        Maze JoinMaze(string maze);



        SearchAlgorithmFactory<Position> AlgorithmFactory
        {
            get;
        }

        Dictionary<string, Solution<Position>> MazeSolutions
        {
            get;
        }


        Dictionary<string, Maze> SinglePlayerMazes
        {
            get;
        }

        Dictionary<string, Maze> MultiPlayerMazes
        {
            get;
        }

        Dictionary<string, Maze> JoinableMazes
        {
            get;
        }

        Dictionary<string, Maze> ActiveMultiPlayerMazes
        {
            get;
        }

        DFSMazeGenerator MazeGenerator
        {
            get;
        }

        IController IController
        {
            get;
        }
    }
}