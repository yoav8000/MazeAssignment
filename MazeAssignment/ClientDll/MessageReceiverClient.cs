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
    public class MessageReceiverClient
    {
        private bool communicate;
        private NetworkStream stream;
        private StreamReader streamReader;
        private TcpClient theClient;
        private IPEndPoint ep;
        private int portNumber;
        private string recievedMessage;



        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="ep">The ep.</param>
        /// <param name="portNumber">The port number.</param>
        public MessageReceiverClient(IPEndPoint ep, int portNumber)
        {
            this.ep = ep;
            this.portNumber = portNumber;
            CreateANewConnection();
            communicate = true;
           
        }

        public void CreateANewConnection()
        {
            TheClient = new TcpClient();
            TheClient.Connect(this.ep);//connect to the server
            this.stream = TheClient.GetStream();
            StreamReader = new StreamReader(TheClient.GetStream());
            Console.WriteLine("you are connected ");
            communicate = true;
        }

        public string RecievedMessage
        {
            get
            {
                return this.recievedMessage;
            }
            set
            {
                this.recievedMessage = value;
            }
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
        /// Gets or sets the stream reader.
        /// </summary>
        /// <value>
        /// The stream reader.
        /// </value>
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


        public string ReadMessage()
        {
            try
            {
                string result = StreamReader.ReadLine();
                if (result != null)
                {
                    
                 //   Console.WriteLine(result);
                    string[] arr;
                    arr = result.Split(' ');
                    if (arr[0].StartsWith("Error"))
                    {
                        Console.WriteLine("there was an error please type another command ");
                    }
                    else
                    {
                        recievedMessage = result;
                        return RecievedMessage;
                    }
                }
                return null;
            }

            catch
            {
                communicate = false; //connection was closed at ther server side.
                return null;

            }
        }

    }
}
