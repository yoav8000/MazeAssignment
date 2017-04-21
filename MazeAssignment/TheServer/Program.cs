using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheModel;
using TheServer.TheController.Commands.SinglePlayerCommands;
using TheServer.TheController.Commands.MultiPlayerCommands;
using TheServer.TheMazeGame;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TheServer
{
    class Program
    {
         
        static void Main(string[] args)
        {



            

            Player p1 = new Player();
            Player p2 = new Player();
            IModel model = new Model();
            string[] args1 = new string[3];
            args1[0] = "BLA";
            args1[1] = "10";
            args1[2] = "10";
            StartCommand start = new StartCommand(model);
           string result1 =  start.Execute(args1,p1);

            

            string[] arg = new string[0];
            ListJoinableMazesNamesCommand list = new ListJoinableMazesNamesCommand(model);
            string res = list.Execute(arg, p1);
            Console.WriteLine(res);


            string[] args4 = new string[1];
            args4[0] = "BLA";//test different amount and wrong args-maze name and different algo
            JoinMazeCommand join = new JoinMazeCommand(model);
            string solution = join.Execute(args4, p2);
            Console.WriteLine(solution);



            CloseCommand close = new CloseCommand(model);
            string result2 = close.Execute(args4, p2);
            Console.WriteLine(result2);



            int x = 2;
            
        }
    }
}
