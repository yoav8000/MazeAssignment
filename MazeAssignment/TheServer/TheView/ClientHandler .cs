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
         
            new Task(() =>
            {
                NetworkStream stream = player.Client.GetStream();
                StreamReader streamReader = new StreamReader(stream);
                StreamWriter streamWriter = new StreamWriter(stream);
                while (player.Communicate)
                {

                    try
                    {
                        string commandLine = streamReader.ReadLine();//recieve command from the client.


                        Console.WriteLine($"Got command: {commandLine}");
                        //got the command


                        string result = icontroller.ExecuteCommand(commandLine, player);//executed it.

                        Console.WriteLine($"the result is: {result}");

                        streamWriter.WriteLine(result);//writes the result back to the client 
                        streamWriter.Flush();
                        if (player.NeedToWait)
                        {
                            WaitForOtherPlayerToJoin(player, streamWriter);
                        }
                        if (player.NeedToBeNotified)
                        {
                            SendOponentPlayedMessage(player, streamWriter);
                        }



                        if (IController.GetCommand(commandLine) is SinglePlayerCommand)//check if the command has to be open.
                        {
                            System.Threading.Thread.Sleep(1000);
                            player.Communicate = false;
                            player.Client.Close();
                        }
                    }
                    catch
                    {

                        System.Threading.Thread.Sleep(1000);
                        player.Communicate = false;
                        player.Client.Close();
                        break;
                    }
                }
            }).Start();
        }

        private void WaitForOtherPlayerToJoin(Player player, StreamWriter streamWriter)
        {
            streamWriter.WriteLine("wait");//writes the result back to the client 
            streamWriter.Flush();

            while (player.NeedToWait)
            {
                System.Threading.Thread.Sleep(1000);
            }

        }

        private void SendOponentPlayedMessage(Player player, StreamWriter streamWriter)
        {
            string message = player.Message;
            streamWriter.WriteLine(message);
            streamWriter.Flush();
            player.Message = "";
            player.NeedToBeNotified = false;
        }



        public IController IController
        {
            get
            {
                return this.icontroller;
            }
        }
    }
}