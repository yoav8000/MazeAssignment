using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace TheServer.TheMazeGame
{
   public class MazeGame
    {
        private int gameCapacity;
        private List<Player> players;
        private string mazeName;
        private Maze maze;
        bool reachedGoalCapacity;
       

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



        public void AddPlayer(Player player)
        {
            players.Add(player);
            if(players.Count == gameCapacity)
            {
                reachedGoalCapacity = true;
            }
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


        public void NotifyPlayers(string message)
        {
         foreach(Player p in players)
            {
                p.GetNotified(message);
            }  
        }


    }
}
