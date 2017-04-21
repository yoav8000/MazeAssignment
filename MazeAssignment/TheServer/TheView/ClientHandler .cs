using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheController;
using TheServer.TheController.Commands.SinglePlayerCommands;

namespace TheServer.TheView
{
    class ClientHandler:IClientHandler
    {
        private IController icontroller;

        public ClientHandler(IController icontroller)
        {
            this.icontroller = icontroller;
        }

        public void HandleClient(TcpClient client)
        {
            string currentCommand;
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string commandLine = reader.ReadLine();
                    currentCommand = commandLine;
                    Console.WriteLine("Got command: {0}", commandLine);
                    string result = icontroller.ExecuteCommand(commandLine, client);
                    writer.Write(result);
                }
                if (IController.GetCommand(currentCommand) is SinglePlayerCommand)
                {
                    client.Close();
                }
                currentCommand = null;
            }).Start();
        }

        private void SendMessageToClient(string Message, TcpClient client)
        {


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