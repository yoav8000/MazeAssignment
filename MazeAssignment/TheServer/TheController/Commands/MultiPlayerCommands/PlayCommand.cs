using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheModel;
using TheServer.TheMazeGame;
using Newtonsoft.Json.Linq;
using MazeLib;

namespace TheServer.TheController.Commands.MultiPlayerCommands
{ 

    class PlayCommand: MultiPlayerCommand
    {
        public PlayCommand(IModel imodel):base(imodel){ }


        public override string Execute(string[] args, Player player)
        {
            if (args.Length != 1) 
            {
                return "Error: Incorrect number of arguments";
            }

                string result = IModel.Play(args, player);
            if (result.StartsWith("Error"))
            {
                return result;
            }
                return new JObject().ToString();
            
         
        }

    }
}
