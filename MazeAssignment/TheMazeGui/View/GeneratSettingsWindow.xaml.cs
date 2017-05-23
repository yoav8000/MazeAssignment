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
using System.Threading;
namespace TheMazeGui.View
{
    /// <summary>
    /// Interaction logic for GeneratSettingsWindow.xaml
    /// </summary>
    public partial class GeneratSettingsWindow : Window
    {
        private SettingsViewModel vm;
        private static GeneratSettingsWindow instance;
        public static Mutex MuTexLock = new Mutex();

        private GeneratSettingsWindow()
        {
            InitializeComponent();
            vm = new SettingsViewModel(new SettingsModel());
            this.DataContext = vm;
        }


        public static GeneratSettingsWindow GetInstance()
        {
              if(MuTexLock == null)
            {
                MuTexLock = new Mutex();
            }

            MuTexLock.WaitOne();
                if (instance == null)
                {
                    instance = new GeneratSettingsWindow();
                }
                MuTexLock.ReleaseMutex();
                return instance;
            
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveSettings();
            Hide();
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            instance = null;
        }
    }
}
