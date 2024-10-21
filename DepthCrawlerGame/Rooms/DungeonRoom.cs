using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepthCrawlerGame.Entities;

namespace DepthCrawlerGame.Rooms
{
    public class DungeonRoom: Room
    {
        private Enemy target;
        public DungeonRoom(Room south, Room north, Room east, Room west,List<Searchable> lootables, Enemy target, List<NPC> npcs): base(south, north, east, west, lootables, target, npcs)
        {
            this.Target = target;
        }

       
        public Enemy Target { get => target; set => target = value; }
    }
}
