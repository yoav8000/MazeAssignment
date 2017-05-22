using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using TheClientDll;
using TheMazeGui.Model.TheSettingsModel;
using Newtonsoft.Json.Linq;

namespace TheMazeGui.Model.TheSinglePlayerModel
{
   public class SinglePlayerModel : IClientModel
    {
        private Position playerPosition;
        private string mazeName;
        private string rows;
        private string cols;
        private string maze;
        private Position initialPosition;
        private Position goalPosition;
        private IClient singlePlayerClient;
        private string stringMaze;
        private string solution;
        private int searchAlgo;
        private string ipAddress;
        private int portNumber;
        private volatile bool stop;
        Maze resultMaze;


        public event PropertyChangedEventHandler PropertyChanged;

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

        public SinglePlayerModel(IClient client)
        {
         //   this.singlePlayerClient = client;
         //   this.searchAlgo = Properties.Settings.Default.SearchAlgorithm;
        }

        public SinglePlayerModel(ISettingsModel settingsModel)
        {
            SinglePlayerClient = new SinglePlayerClient();
            IpAddress = settingsModel.ServerIp;
            PortNumber = settingsModel.ServerPort;
            SinglePlayerClient.setNetworkStat(IpAddress, PortNumber);
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


        public Position PlayerPosition
        {
            get
            {
                return ResultMaze.InitialPos;
            }

            set
            {
                playerPosition = value;
                NotifyPropertyChanged("PlayerPosition");
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
        
        public IClient SinglePlayerClient
        {
            get
            {
                return singlePlayerClient;
            }

            set
            {
                this.singlePlayerClient = value;
            }

        }
        
    
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        
        public void GenerateSinglePlayerMaze(string mazeName,int rows, int cols)
        {
            SinglePlayerClient.Write ("generate" +" "+ mazeName + " " + rows + " " + cols);
            string result = SinglePlayerClient.Read();
            if (result != null)
            {
               
                ResultMaze = MazeLib.Maze.FromJSON(result);
                string temp = ResultMaze.ToString();
                Maze = temp.Replace(Environment.NewLine, "");
                GoalPosition = ResultMaze.GoalPos;//invokes the setter and activates the event.
                InitialPosition = ResultMaze.InitialPos;//invokes the setter and activates the event.
                
            }
        }

        /*
        public void PlayUp()
        {
            Position p = PlayerPosition;
            int row = p.Row;
            int col = p.Col;
            if ((row > 0) && (row < Maze.Rows) && (Maze[row - 1, col] == CellType.Free))
            {
                PlayerPosition = new Position(p.Row - 1, p.Col); //check the goal position
            }
        }

        public void PlayDown()
        {
            Position p = PlayerPosition;
            int row = p.Row;
            int col = p.Col;
            if ((row > 0) && (row < Maze.Rows) && (Maze[row + 1, col] == CellType.Free))
            {
                PlayerPosition = new Position(p.Row + 1, p.Col); //check the goal position
            }
        }

        public void PlayRight()
        {
            Position p = PlayerPosition;
            int row = p.Row;
            int col = p.Col;
            if ((col >= 0) && (col < Maze.Cols - 1) && (Maze[row, col + 1] == CellType.Free))
            {
                PlayerPosition = new Position(row, col + 1); //check the goal position
            }
        }

        public void PlayLeft()
        {
            Position p = PlayerPosition;
            int row = p.Row;
            int col = p.Col;
            if ((col > 0) && (col < Maze.Cols) && (Maze[row, col - 1] == CellType.Free))
            {
                PlayerPosition = new Position(row, col - 1); //check the goal position
            }
        }
        */

        public void SendMessageToServer(string message)
        {
            singlePlayerClient.Write(message);
        }


        public string RecieveMessageFromServer()
        {
            string resultFromServer = singlePlayerClient.Read();
              if (resultFromServer != null)
            {
                if (resultFromServer.Contains("Maze"))
                {

                    ResultMaze = MazeLib.Maze.FromJSON(resultFromServer);
                    InitialPosition = ResultMaze.InitialPos;
                    GoalPosition = ResultMaze.GoalPos;
                    PlayerPosition = ResultMaze.InitialPos;
                    return resultFromServer;

                }
                else if (resultFromServer.Contains("Solution"))
                {
                    return resultFromServer;
                }
                return null;
            }
            return null;


        }

        public void Connect(string ip, int port)
        {
            this.SinglePlayerClient.CreateNewConnection(ip,port);
            Stop = false;
        }

        public void Disconnect()
        {
            singlePlayerClient.Disconnect();
        }

        public void Start()
        {

        }

      
    }
}
