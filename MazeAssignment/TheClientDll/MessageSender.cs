using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TheClientDll
{
    public class MessageSender
    {

        private NetworkStream stream;
        private StreamWriter streamWriter;

        public MessageSender(NetworkStream stream1)
        {
            Stream = stream1;

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
                    StreamWriter = new StreamWriter(Stream);
                }
            }
        }


        public void Write(string command)
        {
            if (command != null && command != " ")
            {
                StreamWriter.WriteLine(command);
                StreamWriter.Flush();
            }

        }

    }
}
