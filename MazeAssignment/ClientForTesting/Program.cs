using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;



namespace ClientForTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(1000);// make sure that the server will react to the connection request.

            bool connect = false;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            TcpClient client1 = new TcpClient();
            client1.Connect(ep);//connect to the server



            Console.WriteLine("you are connected ");


            ClientForTesting client = new ClientForTesting(client1);
            Task taskListenToServer = new Task(() =>//create a reading thread from the server.
            {
                while (client.Communicate)
                {
                    client.ReadMessage();
                }
            });
            //    taskListenToServer.Start();

            Task taskWriteToServer = new Task(() =>//create a writing thread to the server.
            {

                while (client.Communicate)
                {
                    client.WriteMessage();
                }
            });
            //      taskWriteToServer.Start();



            while (true)
            {
                taskListenToServer.Start();
                taskWriteToServer.Start();
                taskListenToServer.Wait();
                taskWriteToServer.Wait();
            

            }
            client.CloseConnection();





        }
    }
}