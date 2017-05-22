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
           

            //DataContext = vm;
            vm.StartNewGame(name, rows, cols);
            this.DataContext = this.vm;//check if it should be before initialization.
            InitializeComponent();
           

        }

        private void RestartGameButton_Click(object sender, RoutedEventArgs e)
        {
            int x = 2;
        }

        private void SolveMazeButton_Click(object sender, RoutedEventArgs e)
        {
            int x = 2;
        }

        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            int x = 2;
        }

        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
