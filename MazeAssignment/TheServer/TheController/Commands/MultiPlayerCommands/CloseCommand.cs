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
    class CloseCommand: MultiPlayerCommand
    {
        public CloseCommand(IModel imodel):base(imodel) { }

        public override string Execute(string[] args, Player player)
        {
            if (args.Length != 1)
            {
                return "Error: Incorrect number of arguments";
            }
           string result = IModel.Close(args[0]);
            if (result.StartsWith("Error"))
            {
                return result;
            }
            JObject jobject = new JObject();
            return jobject.ToString();
        }


    }
}
