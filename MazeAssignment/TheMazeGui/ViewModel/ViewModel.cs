using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMazeGui.ConnectionErrorInterface;

namespace TheMazeGui.ViewModel
{
   public abstract class ViewModel : IClientViewModel 
    {
        public event PropertyChangedEventHandler PropertyChanged;
     
        public event CriticalErrorHandler ConnectionErrorOccurred;

        public void NotifyConnectionError(string message)
        {
            this.ConnectionErrorOccurred?.Invoke(this, new PropertyChangedEventArgs(message));
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}