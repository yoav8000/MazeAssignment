using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheModel;

namespace TheServer.TheController.Commands.MultiPlayerCommands
{
    public abstract class MultiPlayerCommand:AbstractCommand
    {
       public MultiPlayerCommand(IModel imodel):base(imodel) { }
       public override abstract string Execute(string[] args, TcpClient client = null);

    }
}
