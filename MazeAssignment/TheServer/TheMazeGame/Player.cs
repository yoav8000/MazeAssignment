using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TheServer.TheMazeGame
{
    public class Player
    {
        private bool needToWait;
        private string mazeName;
        private TcpClient client;


        public Player(string mazeName, TcpClient client = null)
        {
            this.needToWait = true;
            this.mazeName = mazeName;
            this.client = client;
        }

        public Player(TcpClient client = null)
        {
            this.needToWait = true;
            this.mazeName = null;
            this.client = client;
        }
        public Player()
        {
            this.needToWait = true;
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


            while (NeedToWait)
            {
                switch (waitFor.ToLower())
                {
                    case "other player to play":
                        {
                            System.Threading.Thread.Sleep(50);
                            break;
                        }
                    case "maze to reach capacity":
                        {
                            System.Threading.Thread.Sleep(1000);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }  
            }
        }

        public void GetNotified(string message)//check if neccessery
        {
            return;
        }

        public bool Equals(Player other)
        {
            return this.Client.Equals(other.Client);
        }



    }
}
