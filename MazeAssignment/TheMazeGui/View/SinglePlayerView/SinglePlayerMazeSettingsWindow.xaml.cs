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
using TheMazeGui.ViewModel;
using TheMazeGui.Model.TheSinglePlayerModel;
using TheMazeGui.View.SinglePlayerView;
using TheMazeGui.Model.TheSettingsModel;

namespace TheMazeGui.View.SinglePlayerView
{
    /// <summary>
    /// Interaction logic for SinglePlayerMazeSettingsWindow.xaml
    /// </summary>
    public partial class SinglePlayerMazeSettingsWindow : Window
    {
        private ISettingsModel settingsModel;
      

        public SinglePlayerMazeSettingsWindow(ISettingsModel settingsModel)
        {
            InitializeComponent();
            this.settingsModel = settingsModel;
            MazeSettingsUC.DataContext = settingsModel;
           
            
        }

        protected override void OnClosed(EventArgs e)
        {
            MainWindow window = new MainWindow();
            window.ShowDialog();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            SinglePlayerGameWindow game = new SinglePlayerGameWindow(MazeSettingsUC.txtMazeName.Text, int.Parse(MazeSettingsUC.txtMazeRows.Text), int.Parse(MazeSettingsUC.txtMazeCols.Text));
            this.Hide();
            try { 
                game.ShowDialog();
            }
            catch
            {

            }
            Close();
        }

        private void MazeSettingsUC_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
