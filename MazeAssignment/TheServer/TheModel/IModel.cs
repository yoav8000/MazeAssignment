using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using TheServer.TheController;
using TheServer.TheMazeGame;
namespace TheServer.TheModel
{
    public interface IModel
    {

        Maze GenerateMultiPlayerMaze(string name, int rows, int cols, Player player);
        Maze GenerateteSinglePlayerMaze(string name, int rows, int cols);
        string SolveMaze(string mazeName, string algorithm);
        ISearcher<Position> GetAlgorithmAccordingToIndicator(string algorithmIndicator);
        List<string> GetNamesOfJoinableMazes();
        Maze JoinMaze(string maze, Player player);
        string Play(string [] args, Player player);
        string Close(string mazeName);
               

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

        Dictionary<string, MazeGame> JoinableMazes
        {
            get;
        }

        Dictionary<string, MazeGame> ActiveMultiPlayerMazes
        {
            get;
        }

        DFSMazeGenerator MazeGenerator
        {
            get;
        }

        Dictionary<Player,MazeGame> PlayersAndGames
        {
            get;
        }


        IController IController
        {
            get;
            set;
        }
    }
}