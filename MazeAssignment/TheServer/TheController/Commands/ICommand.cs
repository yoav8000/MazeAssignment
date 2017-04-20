using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TheServer.TheController.Commands
{
    interface ICommand
    {
        string Execute(string[] args, TcpClient client = null);

    }
}
