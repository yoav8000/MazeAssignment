using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TheServer.TheModel;
using MazeLib;

namespace TheServer.TheController.Commands.MultiPlayerCommands
{
    public class JoinMazeCommand:MultiPlayerCommand
    {
        public JoinMazeCommand(IModel imodel):base(imodel) { }

        public override string Execute(string[] args, TcpClient client=null)
        {
            try
            {
                Maze maze = IModel.JoinMaze(args[0]);
                maze.Name = args[0];
                return maze.ToJSON();
            }
            catch(Exception e)
            {
                return $"there is no such maze with the name - {args[0]}";
            }
        }


    }
}

