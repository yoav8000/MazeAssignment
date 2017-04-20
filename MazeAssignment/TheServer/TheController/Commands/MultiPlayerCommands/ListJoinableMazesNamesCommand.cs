using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TheServer.TheModel;

namespace TheServer.TheController.Commands.MultiPlayerCommands
{
    public class ListJoinableMazesNamesCommand:MultiPlayerCommand
    {
        public ListJoinableMazesNamesCommand(IModel imodel) : base(imodel) { }
        public override string Execute(string[] args, TcpClient client = null)
        {
            return JsonConvert.SerializeObject(IModel.GetNamesOfJoinableMazes());
        }

    }
}
