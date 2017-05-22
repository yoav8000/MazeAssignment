using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TheClientDll
{
    public class SinglePlayerClient : IClient
    {
        private bool communicate;
        private NetworkStream stream;
        private TcpClient theClient;
        private string ep;
        private int portNumber;
        private MessageReceiver receiver;
        private MessageSender sender;



        public SinglePlayerClient()
        {  
            this.receiver = new MessageReceiver(null);
            this.sender = new MessageSender(null);
        }

        public void CreateNewConnection(string ep, int port)
        {

            if (ep!= null)
            {
                this.portNumber = port;
                this.ep = ep;
            }
            communicate = true;
            TheClient = new TcpClient();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ep), port);
            TheClient.Connect(endPoint);    //connect to the server
            this.stream = TheClient.GetStream();
            Sender.Stream = Stream;
            Receiver.Stream = Stream;
        }

        public void Disconnect()
        {
            TheClient.Close();
        }

        public string Read()
        {
            string result = Receiver.Read();
            communicate = false;
            return result; // check what about null.
        }

        public void Write(string command)
        {
            if (!communicate)
            {
                CreateNewConnection(this.ep,this.portNumber);
            }
            sender.Write(command);
            communicate = false;
        }

        public void setNetworkStat(string ip, int port)
        {
            IpAddres = ip;
            Port = port;    
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

        public MessageSender Sender
        {
            get
            {
                return this.sender;
            }
            set
            {
                this.sender = value;
            }
        }

        public MessageReceiver Receiver
        {
            get
            {
                return this.receiver;
            }
            set
            {
                this.receiver = value;
            }
        }

        public NetworkStream Stream
        {
            get
            {
                return this.stream;
            }
            set
            {
                this.stream = value;
            }
        }
       
        public string IpAddres
        {
            get
            {
                return this.ep;
            }
            set
            {
                this.ep = value;
            }
        }

        public int Port
        {
            get
            {
                return this.portNumber;
            }
            set
            {
                this.portNumber = value;
            }
        }

    }
}
