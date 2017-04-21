using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TheClient
{
    class Client
    {
        private bool communicate;
        private StreamReader streamReader;
        private StreamWriter streamWriter;
        private TcpClient theClient;

        public Client()
        {
            this.theClient = new TcpClient();
            StreamReader = new StreamReader(TheClient.GetStream());
            StreamWriter = new StreamWriter(TheClient.GetStream());
            communicate = true;
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


        public void ReadMessage()
        {
            char[] buffer = new char[20000];
            string result = StreamReader.ReadLine();
            if (result.Equals("close"))
            {
                communicate = false;
            }
            if (result != null)
            {
                string[] arr;
                arr = result.Split(' ');
                if (arr[0].StartsWith("Error"))
                {
                    Console.WriteLine("there was an error please type another command ");
                    WriteMessage();
                }
            }

        }

        public void WriteMessage()
        {
            Console.Write("Please enter a command: ");
            string command = Console.ReadLine();
            if(command != null)
            {
                StreamWriter.Write(command);
            }

        }

        public void CloseConnection()
        {
            TheClient.Close();
        }

    }
}
