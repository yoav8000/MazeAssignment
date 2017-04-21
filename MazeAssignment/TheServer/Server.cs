using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheView;
using TheServer.TheMazeGame;

namespace TheServer
{
    class Server
    {
        private int port;
        private TcpListener listener;
        private IClientHandler ch;


        public Server(int port, IClientHandler ch)
        {
            this.port = port;
            this.ch = ch;
        }


        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine("Waiting for connections...");
            Task task = new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ch.HandleClient(new Player(client));
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
            task.Wait();// so that the main thread won't keep on going and exit.    
        }

        public void Stop()
        {
            listener.Stop();
        }
    }



}