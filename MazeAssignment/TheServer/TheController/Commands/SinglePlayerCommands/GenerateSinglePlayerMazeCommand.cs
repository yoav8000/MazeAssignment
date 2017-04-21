using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Net.Sockets;
using TheServer.TheModel;
using TheServer.TheMazeGame;

namespace TheServer.TheController.Commands.SinglePlayerCommands
{
   public class GenerateSinglePlayerMazeCommand:SinglePlayerCommand
    {
        public GenerateSinglePlayerMazeCommand(IModel model):base(model){}


        public override string Execute(string[] args,Player player)
        {
            if (args.Length != 3)
            {
                return "Error: Incorrect number of arguments";
            }
            string name = args[0];
            try
            {
                int rows = Convert.ToInt32(args[1]);
                int cols = Convert.ToInt32(args[2]);
                  Maze maze = IModel.GenerateteSinglePlayerMaze(name, rows, cols);
            if(maze == null)
            {
                return "Error: there is a maze with the same name";
            }
            return maze.ToJSON();
            }
            catch
            {
                return "Error: One of the second or third argument isn't a valid number";
            }
           
          
        }
    }
}

