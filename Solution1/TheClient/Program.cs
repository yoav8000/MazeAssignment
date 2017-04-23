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
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(5000);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            Console.WriteLine("You are connected");
            while (true)
            {
                // Send data to server
                Console.WriteLine("Please enter a number: ");
                int num = int.Parse(Console.ReadLine());
                writer.WriteLine(num);
                writer.Flush();
                // Get result from server
                string result = reader.ReadLine();
                Console.WriteLine("Result = {0}", result);
            }
            client.Close();
        }
    }
}
