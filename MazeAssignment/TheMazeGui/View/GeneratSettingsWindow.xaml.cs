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
using TheMazeGui.Model.TheSettingsModel;
namespace TheMazeGui.View
{
    /// <summary>
    /// Interaction logic for GeneratSettingsWindow.xaml
    /// </summary>
    public partial class GeneratSettingsWindow : Window
    {
        private SettingsViewModel vm;

        public GeneratSettingsWindow()
        {
            InitializeComponent();
            vm = new SettingsViewModel(new SettingsModel());
            this.DataContext = vm;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveSettings();
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
    }
}
