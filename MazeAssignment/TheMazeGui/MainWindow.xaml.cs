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
using TheMazeGui.View.SinglePlayerView;
using TheMazeGui;
using TheMazeGui.View;
using TheMazeGui.Model.TheSettingsModel;

namespace TheMazeGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void SinglePlayerButton_Click(object sender, RoutedEventArgs e)
        {
           
            ISettingsModel settingsModel = new SettingsModel();
            SinglePlayerMazeSettingsWindow theSettingsModelWindow = new SinglePlayerMazeSettingsWindow(settingsModel);
            this.Hide();
            theSettingsModelWindow.ShowDialog();  
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            GeneratSettingsWindow g = new GeneratSettingsWindow();
            g.ShowDialog();
            
        }
    }
}
