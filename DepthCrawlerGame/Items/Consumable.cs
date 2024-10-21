using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepthCrawlerGame.Entities;

namespace DepthCrawlerGame.Items
{
    public class Consumable:Item
    {
        int replenishedAmount;
        bool isStackable = true;
        
        public Consumable(string name, int replenishedAmount): base(name)
        {
            this.replenishedAmount = replenishedAmount;
            
        }

        public int ReplenishedAmount { get => replenishedAmount; }
        public bool IsStackable { get => isStackable;}

        public override void Consume(Player user)
        {
            Console.WriteLine("Consumed Consumable");
            Console.ReadKey();
        }
    }
}
