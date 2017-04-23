using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace ClientForTesting
{
    class ClientForTesting
    {
        private bool communicate;
        private StreamReader streamReader;
        private StreamWriter streamWriter;
        private TcpClient theClient;
        private bool waitForOtherPlayer;
        private bool startCommandWasSent;
        public ClientForTesting()
        {
            this.theClient = new TcpClient();
            StreamReader = new StreamReader(TheClient.GetStream());
            StreamWriter = new StreamWriter(TheClient.GetStream());
            communicate = true;
            streamWriter.AutoFlush = true;
            waitForOtherPlayer = false;
            startCommandWasSent = false;
        }

        public ClientForTesting(TcpClient client)
        {
            this.theClient = client;
            StreamReader = new StreamReader(TheClient.GetStream());
            StreamWriter = new StreamWriter(TheClient.GetStream());
            communicate = true;
            streamWriter.AutoFlush = true;
            waitForOtherPlayer = false;
            startCommandWasSent = false;
        }


        public TcpClient TheClient
        {
            get
            {
                return this.theClient;
            }
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

        public bool Communicate
        {
            get
            {
                return this.communicate;
            }
            set
            {
                this.communicate = value;
            }
        }
        public void ReadMessage()
        {
            string result = StreamReader.ReadLine();

            if (result != null)
            {
                if (result.Equals("wait"))
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("wait");
                    waitForOtherPlayer = true;
                    WaitForOtherPlayerToJoin();
                    result = "";
                }
                Console.WriteLine(result);
                string[] arr;
                arr = result.Split(' ');
                if (arr[0].StartsWith("Error"))
                {
                    Console.WriteLine("there was an error please type another command ");
                }
            }
            else
            {
                communicate = false;
            }
        }

        public void WriteMessage()
        {
            if (!waitForOtherPlayer)
            {
                Console.WriteLine("Please enter a command: ");
                string command = Console.ReadLine();
                if (waitForOtherPlayer)
                {
                    WaitForOtherPlayerToJoin();
                }

                if (command != null && command != " ")
                {
                    Console.WriteLine($"the command is: {command} ");
                    StreamWriter.WriteLine(command);
                    StreamWriter.Flush();
                }
            }

        }

        public void WaitForOtherPlayerToJoin()
        {
            waitForOtherPlayer = true;
            string joined = "";
            joined = streamReader.ReadLine();
            while (!joined.StartsWith("other players joined "))
            {
                joined = streamReader.ReadLine();

            }
            Console.WriteLine("a player joined the game");
            waitForOtherPlayer = false;
        }


        public void CloseConnection()
        {
            TheClient.Close();
        }

    }
}
