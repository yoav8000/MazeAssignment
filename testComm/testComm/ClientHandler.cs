using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace testComm
{
    public class ClientHandler : IClientHandler
    {
        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    while (true)
                    {
                        string commandLine = reader.ReadLine();
                        Console.WriteLine("Got command: {0}", commandLine);
                        string result = ExecuteCommand(commandLine, client);
                        writer.Write(result);
                        string t = result;
                    }
                }
                client.Close();
            }).Start();
        }

        public string ExecuteCommand(string command,TcpClient client)
        {
            return "0";
        }


    }

}