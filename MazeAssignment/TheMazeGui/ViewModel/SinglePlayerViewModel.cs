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
       // private SinglePlayerModel singlePModel;


        public SinglePlayerViewModel(SinglePlayerModel spm):base(spm)
        {
            //this.singlePModel = spm;
            //SinglePModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            //{
            //    NotifyPropertyChanged("VM_" + e.PropertyName);
            //};
        }

    //    public event PropertyChangedEventHandler PropertyChanged;

       // public SinglePlayerModel SinglePModel
       // {
       //     get
        //    {
         //       return this.singlePModel;
          //  }
       // }


        //public void NotifyPropertyChanged(string propName)
        //{
        //    if (this.PropertyChanged != null)
        //    {
        //        this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        //    }
        //}


        //public string VM_MazeRows
        //{
        //    get
        //    {
        //        return singlePModel.Rows;
        //    }
        //}

        //public string VM_MazeCols
        //{
        //    get
        //    {
        //        return singlePModel.Cols;
        //    }

        //}

        //public string VM_MazeName
        //{
        //    get
        //    {
        //        return singlePModel.MazeName;
        //    }

        //}


        //public string VM_Maze
        //{
        //    get
        //    {
        //        return SinglePModel.Maze;
        //    }
        //}



        //public Position VM_PlayerPosition
        //{
        //    get
        //    {
        //        return SinglePModel.PlayerPosition;
        //    }

        //}

        //public Position VM_InitialPosition
        //{
        //    get
        //    {

        //        return SinglePModel.InitialPosition;
        //    }

        //}

        //public Position VM_GoalPosition
        //{
        //    get
        //    {
        //        return SinglePModel.GoalPosition;
        //    }

        //}

        //public string ServerIP
        //{
        //    get { return SinglePModel.IpAddress; }
        //    set
        //    {
        //        SinglePModel.IpAddress = value;
        //    }
        //}

        //public int PortNumber
        //{
        //    get
        //    {
        //        return SinglePModel.PortNumber;
        //    }
        //    set
        //    {
        //        this.singlePModel.PortNumber = value;
        //    }
        //}

        public void StartNewGame(string name, int rows, int cols)
        {
             (PlayerModel as SinglePlayerModel).GenerateSinglePlayerMaze(name, rows, cols);
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


