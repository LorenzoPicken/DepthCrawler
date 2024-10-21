using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepthCrawlerGame.Items;

namespace DepthCrawlerGame
{
    public class Searchable
    {
        private string name;
        private Item containedLoot;
        private bool isInfested;
        
        public Searchable(string name, Item loot, bool isInfested)
        {
            this.name = name;
            this.containedLoot = loot;
            this.isInfested = isInfested;
        }

        public string Name { get => name; set => name = value; }
        public Item ContainedLoot { get => containedLoot; set => containedLoot = value; }
        public bool IsInfested { get => isInfested; set => isInfested = value; }
    }
}
