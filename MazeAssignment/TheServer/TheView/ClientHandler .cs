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
        private StreamReader streamReader;
        private StreamWriter streamWriter;


        public ClientHandler(IController icontroller)
        {
            this.icontroller = icontroller;
        }

        public StreamReader StreamReader
        {
            get
            {
                return this.streamReader;
            }
            set
            {
                this.streamReader = value;
            }
        }

        public StreamWriter StreamWriter
        {
            get
            {
                return this.streamWriter;
            }
            set
            {
                this.streamWriter = value;
            }
        }

        public void HandleClient(Player player)
        {
            NetworkStream stream = player.Client.GetStream();
            StreamReader = new StreamReader(stream);
            StreamWriter = new StreamWriter(stream);
            string currentCommand;
            new Task(() =>
            {
                while (player.Communicate)
                {
                    string commandLine = StreamReader.ReadLine();//recieve command from the client.
                    currentCommand = commandLine;
                    Console.WriteLine("Got command: {0}", commandLine);
                    //got the command


                    string result = icontroller.ExecuteCommand(commandLine, player);//executed it.


                    StreamWriter.Write(result);//writes the result back to the client 
                    if (IController.GetCommand(currentCommand) is SinglePlayerCommand)//check if the command has to be open.
                    {
                        StreamWriter.Write("close connection");
                        player.Communicate = false;
                    }
                    currentCommand = null;
                }
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