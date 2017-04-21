using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheController;
using TheServer.TheMazeGame;


namespace TheServer.TheView
{
    public interface IClientHandler
    {
        void HandleClient(Player player);

        IController IController
        {
            get;
        }


    }

    




}