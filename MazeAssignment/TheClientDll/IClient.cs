using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TheClientDll
{
    public interface IClient
    {
        string CreateNewConnection(string ep, int port);
        string Write(string command);
        string Read(); 
        void Disconnect();
        void setNetworkStat(string ip, int port);
    }
}
