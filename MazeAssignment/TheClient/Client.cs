using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TheClient
{
    class Client
    {
        private bool communicate;
        private NetworkStream stream;
        private StreamReader streamReader;
        private StreamWriter streamWriter;
        private TcpClient theClient;
        private bool waitForOtherPlayer;
        private IPEndPoint ep;
        private int portNumber;


        public Client(IPEndPoint ep, int portNumber)
        {
            this.ep = ep;
            this.portNumber = portNumber;
            CreateANewConnection();
            communicate = true;
            streamWriter.AutoFlush = true;
            waitForOtherPlayer = false;

        }


        public TcpClient TheClient
        {
            get
            {
                return this.theClient;
            }

            set
            {
                this.theClient = value;
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
            try
            {
                string result = StreamReader.ReadLine();

                if (result != null)
                {
                    if (result.Equals("wait"))
                    {
                        Console.WriteLine("wait");
                        this.waitForOtherPlayer = true;
                        
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
            catch
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
                if (!communicate)
                {
                    communicate = true;
                    CreateANewConnection();
                }
                if (this.waitForOtherPlayer)
                {
                    Console.WriteLine($"Ignored the command: {command} ");
                    command = " ";
                    WaitForOtherPlayerToJoin();
                    int s = 2;
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
            while (!joined.StartsWith("The Game Has Started"))
            {
                joined = streamReader.ReadLine();
                if (joined.Equals("The Game Has Started")) 
                {
                    Console.WriteLine("The Game Has Started");
                    this.waitForOtherPlayer = false;
                    break;
                }
                if (joined.Equals("a player joined the game"));
                {
                    Console.WriteLine("a player joined the game");
                }
            }
            
            this.waitForOtherPlayer = false;
        }


        public void CloseConnection()
        {
            TheClient.Close();
        }

        public void CreateANewConnection()
        {
            TheClient = new TcpClient();
            TheClient.Connect(this.ep);//connect to the server
            this.stream = TheClient.GetStream();
            StreamReader = new StreamReader(TheClient.GetStream());
            StreamWriter = new StreamWriter(TheClient.GetStream());
            Console.WriteLine("you are connected ");
            communicate = true;
        }



    }
}
