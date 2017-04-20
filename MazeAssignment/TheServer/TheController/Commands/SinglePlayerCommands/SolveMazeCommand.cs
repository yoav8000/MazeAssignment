using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheModel;

namespace TheServer.TheController.Commands.SinglePlayerCommands
{
    public class SolveMazeCommand:SinglePlayerCommand
    {
        public SolveMazeCommand(IModel imodel) : base(imodel) { }

        public override string Execute(string[] args, TcpClient client = null)
        {
            string mazeName = args[0];
            int algorithmIndicator = -1;
            string theAlgorithm;
            try
            {
                algorithmIndicator = Convert.ToInt32(args[1]);
            }


            catch (FormatException exception)
            {
                Console.WriteLine("Input string is not a sequence of digits.");
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
                        throw new Exception($"there is no such algorithm in the search algorithm pool {algorithmIndicator}");
                    }
            }

            return IModel.SolveMaze(mazeName, theAlgorithm);
        }



    }
}
