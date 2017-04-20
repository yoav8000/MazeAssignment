using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Net.Sockets;
using TheServer.TheModel;


namespace TheServer.TheController.Commands.SinglePlayerCommands
{
   public class GenerateSinglePlayerMazeCommand:SinglePlayerCommand
    {
        public GenerateSinglePlayerMazeCommand(IModel model):base(model){}


        public override string Execute(string[] args, TcpClient client=null)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            Maze maze = IModel.GenerateteSinglePlayerMaze(name, rows, cols);
            if(maze == null)
            {
                return "There is a maze with that name";//the maze already exists.
            }
            return maze.ToJSON();
        }
    }
}

