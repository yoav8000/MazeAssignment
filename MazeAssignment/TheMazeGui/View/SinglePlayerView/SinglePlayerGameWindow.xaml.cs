using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TheMazeGui.Model.TheSettingsModel;
using TheMazeGui.Model.TheSinglePlayerModel;
using TheMazeGui.ViewModel;

namespace TheMazeGui.View.SinglePlayerView
{
    /// <summary>
    /// Interaction logic for SinglePlayerGameWindow.xaml
    /// </summary>
    public partial class SinglePlayerGameWindow : Window
    {
        private SinglePlayerViewModel vm;


        public SinglePlayerGameWindow(string name, int rows, int cols)
        {
            vm = new SinglePlayerViewModel(new SinglePlayerModel(new SettingsModel()));

            vm.StartNewGame(name, rows, cols);
            this.DataContext = this.vm;//check if it should be before initialization.
            InitializeComponent();


        }

        private void RestartGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to restart the game?", "Restart the game",MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {

                vm.RestartMaze();
              
            }
            
        }

        private void SolveMazeButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to solve the maze?", "Solve the maze", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {

                vm.SolveMaze();

            }
           
        }

        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to go back to the main menu?", "Go back to main menu", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {

                Close();
                MainWindow mainWin = new MainWindow();
                mainWin.ShowDialog();

            }
        }

        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.KeyDown += HandleKeyPress;
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            string direction = "";
            switch (e.Key)
            {
                case Key.Down:
                    {
                        direction = "Down";
                        break;
                    }
                case Key.Up:
                    {
                        direction = "Up";
                        break;
                    }
                case Key.Right:
                    {
                        direction = "Right";
                        break;
                    }
                case Key.Left:
                    {
                        direction = "Left";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            if (direction != "" && MazeBoard.PlayerPosition != MazeBoard.GoalPosition)
            {
                vm.MovePlayer(direction);
            }
        }
    }
}

