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
using TheServer.TheView;
using TheServer.TheController;
namespace TheServer
{
    class Program
    {

        static void Main(string[] args)
        {
            IModel model = new Model();
            IController controller = new Controller(model);
            model.IController = controller;
            IClientHandler clientHandler = new ClientHandler(controller);
            controller.IModel = model;
            controller.IClientHandler = clientHandler;

            Server server = new Server(8000, clientHandler);
            server.Start();

        }
    }
}
