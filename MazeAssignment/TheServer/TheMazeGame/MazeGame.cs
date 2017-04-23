using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.IO;
using System.Net.Sockets;

namespace TheServer.TheMazeGame
{
   public class MazeGame
    {
        private int gameCapacity;
        private List<Player> players;
        private string mazeName;
        private Maze maze;
  
       

        public MazeGame(string mazeName,Maze maze, int gameCapacity)
        {
            this.players = new List<Player>();
            this.maze = maze;
            this.gameCapacity = gameCapacity;
            this.mazeName = mazeName;
        }

        public int GameCapacity//the minimum is 2 players
        {
            get
            {
                return this.gameCapacity;
            }
            set
            {
                if (value < 2)
                {
                    this.gameCapacity = 2;
                }
                else
                {
                    this.gameCapacity = value;
                }
            }
        }
        public Maze Maze
        {
            get
            {
                return this.maze;
            }
            
        }
        public string MazeName
        {
            get
            {
                return this.mazeName;
            }
        }

        public List<Player> Players
        {
            get
            {
                return this.players;
            }
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
            
        }

        public void RemovePlayer(Player player)//need to override the equal method.
        {
            players.Remove(player);
        }
        
        public void WaitForPlayerToJoin()
        {
            foreach(Player p in players)
            {
                p.WaitForEvent("maze to reach capacity");
            }
        }

        public void WaitForAPlayerToPlay(Player currentPlayer)//fix so that were stuck until player plays.
        {
            foreach(Player p in players)
            {
                if (!p.Equals(currentPlayer))
                {
                    p.WaitForEvent("other player to play");
                }
            }
        }


        public void NotifyAllPlayers(string message)
        {
         foreach(Player p in players)
            {

                p.NotifyClient(message);
            }  
        }

        public void CloseAllClients()
        {
            foreach (Player p in players)
            {
                p.Communicate = false;
                p.Client.Close();
            }
        }



        public void NotifyOtherPlayers(string message, Player player)
        {
            foreach (Player p in players)
            {
                if (!p.Equals(player))
                {
                    p.NotifyClient(message);
                }
                
            }
        }
    }
}
