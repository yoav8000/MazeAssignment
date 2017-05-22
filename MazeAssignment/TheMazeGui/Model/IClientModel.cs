using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMazeGui.Model
{
  public interface IClientModel:INotifyPropertyChanged
    {
        void SendMessageToServer(string message);
        string RecieveMessageFromServer();
        void Connect(string ip, int port);
        void Disconnect();
        void Start();
       
    }
}
