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
using TheMazeGui.Model.AnAbstractPlayerModel;
namespace TheMazeGui.Model.TheSinglePlayerModel
{
    public class SinglePlayerModel : PlayerModel
    {

        private int searchAlgorithm;
        private string mazeSolution;



        public string MazeSolution
        {
            get
            {
                return mazeSolution;
            }
            set
            {
                mazeSolution = value;
            }
        }


        public int SearchAlgorithm
        {
            get
            {
                return this.searchAlgorithm;
            }
            set
            {
                searchAlgorithm = value;

            }
        }

        public SinglePlayerModel(ISettingsModel settingsModel) : base(settingsModel)
        {

            MyClient = new SinglePlayerClient();
            MyClient.setNetworkStat(IpAddress, PortNumber);
            SearchAlgorithm = settingsModel.SearchAlgorithm;
        }

        public void GenerateSinglePlayerMaze(string mazeName, int rows, int cols)
        {
            SendMessageToServer("generate" + " " + mazeName + " " + rows + " " + cols);
            string result = RecieveMessage();
            if (result != null && (!result.Contains("Error")))
            {

                ResultMaze = MazeLib.Maze.FromJSON(result);
                string temp = ResultMaze.ToString();
                Maze = temp.Replace(Environment.NewLine, "");
                GoalPosition = ResultMaze.GoalPos;//invokes the setter and activates the event.
                InitialPosition = ResultMaze.InitialPos;//invokes the setter and activates the event.
                PlayerPosition = InitialPosition;

            }
        }


        public string RecieveMessage()
        {
            string resultFromServer = RecieveMessageFromServer();
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

        public void RestartMaze()
        {
            PlayerPosition = InitialPosition;
        }

        public void SolveMaze()
        {


            string result;
            string solution;
            StringBuilder sb = new StringBuilder();

            if (MazeSolution == null)
            {
                SendMessageToServer("solve" + " " + MazeName + " " + SearchAlgorithm);
                result = RecieveMessageFromServer();
                result = result.Replace(@"\", "");
                string[] arr = result.Split(':');
                arr = arr[2].Split(',');
                solution = arr[0];

            }
            else
            {
                solution = MazeSolution;
            }
            if (PlayerPosition.Row != InitialPosition.Row || PlayerPosition.Col != InitialPosition.Col)
            {
                RestartMaze();
            }

            Task task = new Task(() =>//creating a listening thread that keeps running.
            {
                for (int i = 0; i < solution.Length; i++)
                {
                    string direction = "";
                    switch (solution[i])
                    {
                        case '0':
                            {
                                direction = "Left";
                                break;
                            }

                        case '1':
                            {
                                direction = "Right";
                                break;
                            }
                        case '2':
                            {
                                direction = "Up";
                                break;
                            }
                        case '3':
                            {
                                direction = "Down";
                                break;
                            }
                        default:
                            {
                                break;
                            }



                    }
                    if (direction != "")
                    {
                        sb.Append(solution[i]);
                        MovePlayer(direction);
                        System.Threading.Thread.Sleep(200);
                    }
                }
                MazeSolution = sb.ToString();
            });
            task.Start();

        }

    }
}
