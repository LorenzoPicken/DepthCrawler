using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepthCrawlerGame.Entities;

namespace DepthCrawlerGame.Rooms
{
    public class Room
    {
        string description;
        private Room southConnectingRoom;
        private Room northConnecingRoom;
        private Room eastConnectingRoom;
        private Room westConnectingRoom;

        private bool isCleared;
        private bool isScanned = false;
        private bool isCamp = false;
        private bool isScoutRoom = false;
        private bool isAltarRoom = false;


        private List<Searchable> lootables = new List<Searchable>();
        private List<NPC> npcs = new List<NPC>();

        private Enemy enemy;

        private int id;


        public Room(Room south, Room north, Room east, Room west, List<Searchable> lootables, Enemy target, List<NPC> npcs)
        {
            
            this.southConnectingRoom = south;
            this.northConnecingRoom = north;
            this.eastConnectingRoom = east;
            this.westConnectingRoom = west;
            this.lootables = lootables;
            this.enemy = target;
            this.npcs = npcs;
        }



        public bool IsCleared { get => isCleared; set => isCleared = value; }
        public string Description { get => description; set => description = value; }
        public List<Searchable> Lootables { get => lootables; set => lootables = value; }
        public Room SouthConnectingRoom { get => southConnectingRoom; set => southConnectingRoom = value; }
        public Room NorthConnectingRoom { get => northConnecingRoom; set => northConnecingRoom = value; }
        public Room EastConnectingRoom { get => eastConnectingRoom; set => eastConnectingRoom = value; }
        public Room WestConnectingRoom { get => westConnectingRoom; set => westConnectingRoom = value; }
        public bool IsScanned { get => isScanned; set => isScanned = value; }
        public int Id { get => id; set => id = value; }
        public Enemy Enemy { get => enemy; set => enemy = value; }
        public List<NPC> Npcs { get => npcs; set => npcs = value; }
        public bool IsCamp { get => isCamp; set => isCamp = value; }
        public bool IsScoutRoom { get => isScoutRoom; set => isScoutRoom = value; }
        public bool IsAltarRoom { get => isAltarRoom; set => isAltarRoom = value; }

        public enum ConnectingDirections { NORTH, SOUTH, EAST, WEST};
    }
}
