using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TheServer.TheMazeGame
{
    public class Player
    {
        bool communicate;
        private bool needToWait;
        private string mazeName;
        private TcpClient client;
        private string message;
        private bool needToBeNotified;
       

        public Player(string mazeName, TcpClient client = null)
        {
            this.needToWait = false;
            this.communicate = true;
            this.mazeName = mazeName;
            this.client = client;
  
        }

        public Player(TcpClient client = null)
        {
            this.needToWait = false;
            this.communicate = true;
            this.mazeName = null;
            this.client = client;
          
        }
        public Player()
        {
            this.needToWait = false;
            this.communicate = true;
            this.mazeName = null;
            this.client = null;
           

        }


        public bool NeedToWait
        {
            get
            {
                return this.needToWait;
            }
            set
            {
                this.needToWait = value;
            }
        }

        public bool Communicate
        {
            get
            {
                return this.communicate; ;
            }
            set
            {
                this.communicate = value;
            }
        }

        public string MazeName
        {
            get
            {
                return this.mazeName;
            }

            set
            {
                this.mazeName = value;  
            }
        }

        public TcpClient Client
        {
            get
            {
                return this.client;
            }

            set
            {
                this.client = value;
            }
        }


        public void WaitForEvent(string waitFor)
        {
            NeedToWait = true;


        }

        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
            }
        }

        public void NotifyClient(string message)
        {
            NetworkStream stream = Client.GetStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine(message);
            streamWriter.Flush();
        }



        public bool NeedToBeNotified
        {
            get
            {
                return this.needToBeNotified;
            }
            set
            {
                this.needToBeNotified = value;
            }
        }


        public bool Equals(Player other)
        {
            return this.Client.Equals(other.Client);
        }



    }
}
