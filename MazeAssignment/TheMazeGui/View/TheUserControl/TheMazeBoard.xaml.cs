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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TheMazeGui.View.TheUserControl
{
    /// <summary>
    /// Interaction logic for TheMazeBoard.xaml
    /// </summary>
    public partial class TheMazeBoard : UserControl
    {

        private int rows;
        private int cols;
        private Rectangle[,] mazeGrid;
        private Brush barrierBrush;
        private Brush freeRecBrush;
        private Brush playerPosBrush;
        private Brush goalPosBrush;


      
        

        protected override void OnInitialized(EventArgs e)
        {

            InitializeComponent();

            rows = int.Parse(Rows);
            cols = int.Parse(Cols);
            mazeGrid = new Rectangle[rows, cols];
            double rectHeight = MazeCanvas.Height / rows;
            double rectWidth = MazeCanvas.Width / cols;
            

            barrierBrush = new SolidColorBrush(Colors.Black);
            freeRecBrush = new SolidColorBrush(Colors.White);
            playerPosBrush = new ImageBrush(new BitmapImage(new Uri(@"C:\Users\yoav gomberg\Desktop\minionphoto.png", UriKind.Relative)));
            goalPosBrush = new ImageBrush(new BitmapImage(new Uri(@"C:\Users\yoav gomberg\Desktop\destinationimage.png", UriKind.Relative)));
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Height = rectHeight;
                    rectangle.Width = rectWidth;
                    if(Maze[(i*rows)+j] == '1')
                    {
                        rectangle.Fill = barrierBrush;
                    }
                    else
                    {
                        rectangle.Fill = freeRecBrush;
                    }
                    
                    MazeCanvas.Children.Add(rectangle);
                    Canvas.SetTop(rectangle, i * rectHeight);
                    Canvas.SetLeft(rectangle, j * rectWidth);
                    mazeGrid[i, j] = rectangle;
                }
            }

            Rectangle playerPosRect = new Rectangle();
            playerPosRect.Height = rectHeight;
            playerPosRect.Width = rectWidth;
            playerPosRect.Fill = playerPosBrush;
            MazeCanvas.Children.Add(playerPosRect);


            Rectangle goalPosRect = new Rectangle();
            goalPosRect.Height = rectHeight;
            goalPosRect.Width = rectWidth;
            goalPosRect.Fill = goalPosBrush;
            MazeCanvas.Children.Add(goalPosRect);



            string[] arr = InitialPosition.Split(',');
            string xPostion = arr[0];
            string yPosition = arr[1];
            arr = xPostion.Split('(');
            int x = int.Parse(arr[1]);
            arr = yPosition.Split(')');
            int y = int.Parse(arr[0]);

            Canvas.SetTop(playerPosRect, x * rectHeight);
            Canvas.SetLeft(playerPosRect, y * rectWidth);
           
            
            arr = GoalPosition.Split(',');
            xPostion = arr[0];
            yPosition = arr[1];
            arr = xPostion.Split('(');
            x = int.Parse(arr[1]);
            arr = yPosition.Split(')');
            y = int.Parse(arr[0]);

            Canvas.SetTop(goalPosRect, x * rectHeight);
            Canvas.SetLeft(goalPosRect, y * rectWidth);
            mazeGrid[x, y] = goalPosRect;
            
            base.OnInitialized(e);
        }


        public static readonly DependencyProperty MazeProperty = DependencyProperty.Register(
                  "Maze",
                  typeof(string),
                  typeof(TheMazeBoard),
                  null);

        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(
         "Rows",
         typeof(string),
         typeof(TheMazeBoard),
         null);

        public static readonly DependencyProperty ColsProperty = DependencyProperty.Register(
       "Cols",
       typeof(string),
       typeof(TheMazeBoard),
       null);


        public static readonly DependencyProperty InitialPositionProperty = DependencyProperty.Register(
            "InitialPosition",
            typeof(string),
            typeof(TheMazeBoard),
            null);

        public static readonly DependencyProperty GoalPositionProperty = DependencyProperty.Register(
        "GoalPosition",
        typeof(string),
        typeof(TheMazeBoard),
        null);


       

        public static readonly DependencyProperty PlayerPositionProperty = DependencyProperty.Register(
       "PlayerPosition",
       typeof(string),
       typeof(TheMazeBoard),
       new PropertyMetadata(MovePlayer));



        private static void MovePlayer(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TheMazeBoard mazeBoard = d as TheMazeBoard;
            string newPos = e.NewValue as string;


        }

       

        public string Maze
        {
            get
            {
                return (string)GetValue(MazeProperty);
            }
            set
            {
                SetValue(MazeProperty, value);
            }
        }


         
        public string PlayerPosition
        {
            get
            {
                return (string)GetValue(PlayerPositionProperty);
            }
            set
            {
                SetValue(PlayerPositionProperty, value);
            }
        }

        public string InitialPosition
        {
            get
            {
                return (string)GetValue(InitialPositionProperty);
            }
            set
            {
                SetValue(InitialPositionProperty, value);
            }
        }

        public string GoalPosition
        {
            get
            {
                return (string)GetValue(GoalPositionProperty);
            }
            set
            {
                SetValue(GoalPositionProperty, value);
            }
        }
        
        public string Rows
        {
            get
            {
                return (string)GetValue(RowsProperty);
            }
            set
            {
                SetValue(RowsProperty, value);
            }
        }

        public string Cols
        {
            get
            {
                return (string)GetValue(ColsProperty);
            }
            set
            {
                SetValue(ColsProperty, value);
            }
        }

        
        
    }
}
