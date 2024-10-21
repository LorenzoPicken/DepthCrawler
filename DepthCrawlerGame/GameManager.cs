using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepthCrawlerGame.Entities;
using DepthCrawlerGame.Rooms;
using DepthCrawlerGame.Ressources;

namespace DepthCrawlerGame
{
    public static class GameManager
    {
        public static Player PlayerInstance = new Player("player", 80, 5,10,20,10,10,0,20);

        public static int EnemiesFought = 3;

        public static int timeSinceLastQuestRoom = 0;

        public static ActionState currentState = ActionState.PLAYERSETUP;

        public static Room currentRoom;
        public static int roomID = 1;

        


        



    }
        public enum ActionState { PLAYERSETUP, EXPLORING, BATTLING, DYING, GAMEWIN }
        public enum EnemySpecification { SPIDER, EYE, GARGOYLE, BONEDRIKE, KNIGHT, MINOTAUR, DRAGON}
}
