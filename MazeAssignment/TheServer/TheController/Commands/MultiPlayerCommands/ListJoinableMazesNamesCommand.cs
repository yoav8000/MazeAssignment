﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TheServer.TheModel;
using TheServer.TheMazeGame;

namespace TheServer.TheController.Commands.MultiPlayerCommands
{
    public class ListJoinableMazesNamesCommand:MultiPlayerCommand
    {
        public ListJoinableMazesNamesCommand(IModel imodel) : base(imodel) { }
        public override string Execute(string[] args, Player player)
        {
            if(args.Length != 0)
            {
                return "Error the amount of arguments is not right";
            }
            return JsonConvert.SerializeObject(IModel.GetNamesOfJoinableMazes(),Formatting.Indented);
        }

    }
}
