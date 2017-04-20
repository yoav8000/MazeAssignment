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

        public override string Execute(string[] args, TcpClient client)
        {
            Maze maze = IModel.JoinMaze(args[0]);
            JObject mazeObj = new JObject();
            mazeObj["Name"] = maze.Name;
            mazeObj["Rows"] = maze.Rows;
            mazeObj["Cols"] = maze.Cols;
            return JsonConvert.SerializeObject(IModel.JoinMaze(args[0]));
        }


    }
}

