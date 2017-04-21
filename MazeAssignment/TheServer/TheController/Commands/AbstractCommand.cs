using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheModel;
using TheServer.TheMazeGame;

namespace TheServer.TheController.Commands
{
    public abstract class AbstractCommand : ICommand
    {
        private IModel imodel;

        public AbstractCommand(IModel imodel)
        {
            this.imodel = imodel;
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

        public abstract string Execute(string[] args, Player player);
    }
}
