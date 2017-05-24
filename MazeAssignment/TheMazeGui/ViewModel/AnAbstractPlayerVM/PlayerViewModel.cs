using MazeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMazeGui.Model.AnAbstractPlayerModel;

namespace TheMazeGui.ViewModel.AnAbstractPlayerVM
{
    public abstract class PlayerViewModel:ViewModel
    {
        private PlayerModel playerModel;

        
        public PlayerViewModel(PlayerModel model)
        {
            this.playerModel = model;

            playerModel.ConnectionErrorOccurred += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyConnectionError("VM_" + "IsEnabled");
            };

            playerModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

       

        public bool Is_Enabled
        {
            get
            {
                return playerModel.Is_Enabled;
            }
        }

      public string VM_ConnectionError
        {
            get
            {
                return playerModel.ConnectionError;
            }
        }

        public PlayerModel PlayerModel
        {
            get
            {
                return this.playerModel;
            }
        }

        public string VM_MazeRows
        {
            get
            {
                return playerModel.Rows;
            }
        }

        public string VM_MazeCols
        {
            get
            {
                return playerModel.Cols;
            }

        }

        public string VM_MazeName
        {
            get
            {
                return playerModel.MazeName;
            }

        }


        public string VM_Maze
        {
            get
            {
                return playerModel.Maze;
            }
        }



        public Position VM_PlayerPosition
        {
            get
            {
                return playerModel.PlayerPosition;
            }

        }

        public Position VM_InitialPosition
        {
            get
            {

                return playerModel.InitialPosition;
            }

        }

        public Position VM_GoalPosition
        {
            get
            {
                return playerModel.GoalPosition;
            }

        }

        public string ServerIP
        {
            get { return playerModel.IpAddress; }
            set
            {
                playerModel.IpAddress = value;
            }
        }

        public int PortNumber
        {
            get
            {
                return playerModel.PortNumber;
            }
            set
            {
                this.playerModel.PortNumber = value;
            }
        }

    }
}
