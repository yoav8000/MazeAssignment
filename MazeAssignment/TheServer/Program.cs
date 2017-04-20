using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheModel;
using TheServer.TheController.Commands.SinglePlayerCommands;
using TheServer.TheController.Commands.MultiPlayerCommands;

namespace TheServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IModel model = new Model();
            string[] args1 = new string[3];
            args1[0] = "BLA";
            args1[1] = "10";
            args1[2] = "10";
            StartCommand start = new StartCommand(model);
            start.Execute(args1);

            string[] args4 = new string[1];
            args4[0] = "bla";
            JoinMazeCommand join = new JoinMazeCommand(model);
            join.Execute(args4);




            GenerateSinglePlayerMazeCommand generate = new GenerateSinglePlayerMazeCommand(model);
            string maze =  generate.Execute(args1);

            string[] args3 = new string[3];
            args3[0] = "BLA";
            args3[1] = "10";
            args3[2] = "10";
            string maze1 = generate.Execute(args3);


            SolveMazeCommand solve = new SolveMazeCommand(model);
            string[] args2 = new string[2];
            args2[0] = "BLA";
            args2[1] = "0";
            string sol =  solve.Execute(args2);
            Console.WriteLine(sol);
            int x = 2;

            
        }
    }
}
