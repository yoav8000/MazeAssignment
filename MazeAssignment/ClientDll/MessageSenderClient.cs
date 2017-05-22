using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientDll
{
    public class MessageSenderClient
    {
        private bool communicate;
        private NetworkStream stream;
        private StreamWriter streamWriter;
        private TcpClient theClient;
        private IPEndPoint ep;
        private int portNumber;


        public MessageSenderClient(IPEndPoint ep, int portNumber)
        {
            this.ep = ep;
            this.portNumber = portNumber;
            CreateANewConnection();
            communicate = true;
            streamWriter.AutoFlush = true;
        }


        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
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

      

        /// <summary>
        /// Gets or sets the stream writer.
        /// </summary>
        /// <value>
        /// The stream writer.
        /// </value>
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

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Client"/> is communicate.
        /// </summary>
        /// <value>
        ///   <c>true</c> if communicate; otherwise, <c>false</c>.
        /// </value>
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

        public void CreateANewConnection()
        {
            TheClient = new TcpClient();
     //       TheClient.Connect(this.ep);//connect to the server
     //       this.stream = TheClient.GetStream();
      //      StreamWriter = new StreamWriter(TheClient.GetStream());
       //     Console.WriteLine("you are connected ");
            communicate = true;
        }

        public void SendMessageToServer(string message)
        {
            string command = null;
            if (message != null)
            {
                command = message;
            }

            //string command = Console.ReadLine();
            if (command != null && command != " ")
            {
                Console.WriteLine($"the command is: {command} ");
                if (!communicate)
                {
                    CreateANewConnection();
                    communicate = true;
                }
                StreamWriter.WriteLine(command);
                StreamWriter.Flush();
            }
        }




    }
}
