using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using TheMazeGui.Model;
using TheMazeGui.Model.TheSinglePlayerModel;
using TheMazeGui.Model.AnAbstractPlayerModel;
using TheMazeGui.ViewModel.AnAbstractPlayerVM;
namespace TheMazeGui.ViewModel
{
    public class SinglePlayerViewModel : PlayerViewModel
    {


        public SinglePlayerViewModel(SinglePlayerModel spm):base(spm){}


        public void StartNewGame(string name, int rows, int cols)
        {
            if (PlayerModel.Is_Enabled)
            {
                (PlayerModel as SinglePlayerModel).GenerateSinglePlayerMaze(name, rows, cols);
            }
        }

        public void MovePlayer(string direction)
        {
            PlayerModel.MovePlayer(direction);
        }

        public void RestartMaze()
        {
            (PlayerModel as SinglePlayerModel).RestartMaze();
        }

        public void SolveMaze()
        {
            (PlayerModel as SinglePlayerModel).SolveMaze();
        }

    }


}


