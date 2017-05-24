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
    public class MessageReceiver
    {
        private NetworkStream stream;
        private StreamReader streamReader;


        public MessageReceiver(NetworkStream stream1)
        {
            Stream = stream1;
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
                if (value != null)
                {
                    StreamReader = new StreamReader(Stream);
                }
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


        public string Read()
        {
            try
            {
                string result = StreamReader.ReadLine();
                if (result != null)
                {

                    string[] arr;
                    arr = result.Split(' ');
                    if (arr[0].StartsWith("Error"))
                    {
                        Console.WriteLine("there was an error please type another command ");
                        return null;
                    }else
                        {
                            return result;
                        }
                }
                return null;
            }

            catch
            {
                return "ConnectionError";
            }
        }

        


    }
}
