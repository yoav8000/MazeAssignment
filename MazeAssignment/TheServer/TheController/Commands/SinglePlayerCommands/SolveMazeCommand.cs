using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheModel;
using TheServer.TheMazeGame;

namespace TheServer.TheController.Commands.SinglePlayerCommands
{
    public class SolveMazeCommand:SinglePlayerCommand
    {
        public SolveMazeCommand(IModel imodel) : base(imodel) { }

        public override string Execute(string[] args, Player player)
        {
            if (args.Length != 2)
            {
                return "Error: Incorrect number of arguments";
            }

            string mazeName = args[0];
            int algorithmIndicator = -1;
            string theAlgorithm;
            try
            {
                algorithmIndicator = Convert.ToInt32(args[1]);
            }


            catch (FormatException exception)
            {
                return "Error: The second argument isn't a number";
            }
            switch (algorithmIndicator)
            {
                case 0:
                    {
                        theAlgorithm = "bfs";
                        break;
                    }
                case 1:
                    {
                        theAlgorithm = "dfs";
                        break;
                    }
                default:
                    {
                        return "Error: There is no such Algorithm";
                    }
            }

            return IModel.SolveMaze(mazeName, theAlgorithm);
        }



    }
}
