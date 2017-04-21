﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheController.Commands;
using TheServer.TheController.Commands.SinglePlayerCommands;
using TheServer.TheController.Commands.MultiPlayerCommands;
using TheServer.TheModel;
using TheServer.TheView;



namespace TheServer.TheController
{
   public class Controller:IController
    {
        private Dictionary<string, ICommand> commandsDictionary;
        private IModel imodel;
        private IClientHandler clientHandler;

        public Controller()
        {

            commandsDictionary = new Dictionary<string, ICommand>();
            commandsDictionary.Add("generate", new GenerateSinglePlayerMazeCommand(imodel));
            commandsDictionary.Add("solve", new SolveMazeCommand(imodel));
            commandsDictionary.Add("start", new StartCommand(imodel));
            commandsDictionary.Add("join", new JoinMazeCommand(imodel));
            commandsDictionary.Add("list", new ListJoinableMazesNamesCommand(imodel));
        }

        public string ExecuteCommand(string commandLine, TcpClient client)
        {
            ICommand command = GetCommand(commandLine);
            if (command == null)
            {
                return "Command not found";
            }
            else
            {

            }
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            string[] args = arr.Skip(1).ToArray();
            return command.Execute(args, client);
        }




        public Dictionary<string, ICommand> CommandDictionary
        {
            get
            {
                return this.commandsDictionary;
            }
        }

        public IModel IModel
        {
            get
            {
                return this.imodel;
            }
            set
            {
                this.imodel = value;
            }
        }

        public IClientHandler IClientHandler
        {
            get
            {
                return this.clientHandler;
            }
            set
            {
                this.clientHandler = value;
            }
        }

        public ICommand GetCommand(string commandLine)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commandsDictionary.ContainsKey(commandKey))
                return null;
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commandsDictionary[commandKey];
            return command;

        }
    }
}
