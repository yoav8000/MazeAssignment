﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TheServer.TheModel;
using MazeLib;
using TheServer.TheMazeGame;

namespace TheServer.TheController.Commands.MultiPlayerCommands
{
    public class JoinMazeCommand:MultiPlayerCommand
    {
        public JoinMazeCommand(IModel imodel):base(imodel) { }

        public override string Execute(string[] args, Player player)
        {
            if (args.Length != 1)
            {
                return "Error: Incorrect number of arguments";
            }

            try
            {
                Maze maze = IModel.JoinMaze(args[0],player);
                return maze.ToJSON();
            }
            catch(Exception e)
            {
                return $"Error: There is no such maze with the name - {args[0]}";
            }
        }


    }
}

