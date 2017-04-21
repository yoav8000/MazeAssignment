using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheController;
using TheServer.TheController.Commands.SinglePlayerCommands;
using TheServer.TheMazeGame;

namespace TheServer.TheView
{
    class ClientHandler:IClientHandler
    {
        private IController icontroller;

        public ClientHandler(IController icontroller)
        {
            this.icontroller = icontroller;
        }

        public void HandleClient(Player player)
        {
            string currentCommand;
            new Task(() =>
            {
                using (NetworkStream stream = player.Client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string commandLine = reader.ReadLine();
                    currentCommand = commandLine;
                    Console.WriteLine("Got command: {0}", commandLine);
                    string result = icontroller.ExecuteCommand(commandLine, player);
                    writer.Write(result);
                }
                if (IController.GetCommand(currentCommand) is SinglePlayerCommand)
                {
                    player.Client.Close();
                }
                currentCommand = null;
            }).Start();
        }




        public IController IController
        {
            get
            {
                return this.IController;
            }
        }
    }
}