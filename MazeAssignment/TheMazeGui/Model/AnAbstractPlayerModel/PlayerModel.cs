using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheClientDll;
using MazeLib;
using TheMazeGui.Model.TheSettingsModel;

namespace TheMazeGui.Model.AnAbstractPlayerModel
{
    public abstract class PlayerModel : IClientModel
    {

        private string mazeName;
        private string rows;
        private string cols;
        private string maze;
        private Position initialPosition;
        private Position goalPosition;
        private IClient myClient;
        private string ipAddress;
        private int portNumber;
        private volatile bool stop;
        Maze resultMaze;
        private Position playerPosition;

        public event PropertyChangedEventHandler PropertyChanged;



        public PlayerModel(ISettingsModel settingsModel)
        {
            //myClient = new SinglePlayerClient();
            IpAddress = settingsModel.ServerIp;
            PortNumber = settingsModel.ServerPort;
            //SinglePlayerClient.setNetworkStat(IpAddress, PortNumber);
           // SearchAlgorithm = settingsModel.SearchAlgorithm;
        }


        public void Connect(string ip, int port)
        {
            this.myClient.CreateNewConnection(ip, port);
            Stop = false;
        }


        public void Disconnect()
        {
            myClient.Disconnect();
        }


        public string RecieveMessageFromServer()
        {
            return  myClient.Read();
        }

        public void SendMessageToServer(string message)
        {
            myClient.Write(message);
        }

        public void Start()
        {
            throw new NotImplementedException();
        }


        public bool Stop
        {
            get
            {
                return this.stop;
            }
            set
            {
                this.stop = value;
            }
        }

        public string MazeName
        {
            get
            {
                return ResultMaze.Name;
            }
            set
            {
                mazeName = value;
                NotifyPropertyChanged("MazeName");
            }
        }

        public string Rows
        {
            get
            {
                return ResultMaze.Rows.ToString();
            }
            set
            {
                rows = value;
            }
        }

        public string Cols
        {
            get
            {
                return ResultMaze.Cols.ToString();
            }
            set
            {
                cols = value;
            }
        }


        public string IpAddress
        {
            get
            {
                return this.ipAddress;
            }
            set
            {
                ipAddress = value;

            }
        }


        public int PortNumber
        {
            get
            {
                return this.portNumber;
            }
            set
            {
                this.portNumber = value;
            }
        }


        public string Maze
        {
            get
            {
                return maze;
            }

            set
            {
                maze = value;
                NotifyPropertyChanged("Maze");
            }
        }

        public Maze ResultMaze
        {
            get
            {
                return this.resultMaze;
            }
            set
            {
                this.resultMaze = value;
            }
        }

        public Position InitialPosition
        {
            get
            {
                return initialPosition;
            }

            set
            {
                initialPosition = value;
                NotifyPropertyChanged("InitialPosition");
            }
        }

        public Position GoalPosition
        {
            get
            {
                return goalPosition;
            }

            set
            {
                goalPosition = value;
                NotifyPropertyChanged("GoalPosition");
            }
        }

        public IClient MyClient
        {
            get
            {
                return myClient;
            }

            set
            {
                this.myClient = value;
            }

        }

        public Position PlayerPosition
        {
            get
            {
                return this.playerPosition;
            }

            set
            {
                this.playerPosition = value;
                NotifyPropertyChanged("PlayerPosition");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void MovePlayer(string keyDirection)
        {

            switch (keyDirection)
            {
                case "Down":
                    {
                        if (PlayerCanMove(Direction.Down))
                        {
                            PlayerPosition = new Position(PlayerPosition.Row + 1, PlayerPosition.Col);
                        }
                        break;
                    }
                case "Up":
                    {
                        if (PlayerCanMove(Direction.Up))
                        {
                            PlayerPosition = new Position(PlayerPosition.Row - 1, PlayerPosition.Col);
                        }
                        break;
                    }
                case "Right":
                    {
                        if (PlayerCanMove(Direction.Right))
                        {
                            PlayerPosition = new Position(PlayerPosition.Row, PlayerPosition.Col + 1);
                        }
                        break;
                    }
                case "Left":
                    {
                        if (PlayerCanMove(Direction.Left))
                        {
                            PlayerPosition = new Position(PlayerPosition.Row, PlayerPosition.Col - 1);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

        }

        private bool PlayerCanMove(Direction direction)
        {
            int currentColPosition = PlayerPosition.Col;
            int currentRowPosition = PlayerPosition.Row;

            switch (direction)
            {
                case Direction.Left:
                    {
                        return ((currentColPosition - 1 >= 0) && ('0' == Maze[(currentRowPosition * int.Parse(Cols)) + currentColPosition - 1]
                           || '*' == Maze[(currentRowPosition * int.Parse(Cols)) + currentColPosition - 1]
                           || '#' == Maze[(currentRowPosition * int.Parse(Cols)) + currentColPosition - 1])
                     );
                    }

                case Direction.Up:
                    {
                        return ((currentRowPosition - 1 >= 0) && ('0' == Maze[((currentRowPosition - 1) * int.Parse(Cols)) + currentColPosition]
                            || '*' == Maze[((currentRowPosition - 1) * int.Parse(Cols)) + currentColPosition]
                            || '#' == Maze[((currentRowPosition - 1) * int.Parse(Cols)) + currentColPosition])
                  );
                    }
                case Direction.Down:
                    {
                        return ((currentRowPosition + 1 < int.Parse(Rows)) && ('0' == Maze[((currentRowPosition + 1) * int.Parse(Cols)) + currentColPosition]
                             || '*' == Maze[((currentRowPosition + 1) * int.Parse(Cols)) + currentColPosition]
                             || '#' == Maze[((currentRowPosition + 1) * int.Parse(Cols)) + currentColPosition])
              );
                    }
                case Direction.Right:
                    {
                        return ((currentColPosition + 1 < int.Parse(Cols)) && ('0' == Maze[(currentRowPosition * int.Parse(Cols)) + currentColPosition + 1]
                              || '*' == Maze[(currentRowPosition * int.Parse(Cols)) + currentColPosition + 1]
                              || '#' == Maze[(currentRowPosition * int.Parse(Cols)) + currentColPosition + 1])
                        );
                    }
            }
            return false;
        }


    }
}
