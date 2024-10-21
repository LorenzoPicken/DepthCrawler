using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepthCrawlerGame.Entities;

namespace DepthCrawlerGame.Items
{
    public class Item
    {
        private string name;
        private ItemType itemType;

        public string Name { get => name; set => name = value; }
        public ItemType ItemType1 { get => itemType; set => itemType = value; }


        public Item( string name)
        {
            this.ItemType1 = itemType;
            this.name = name;
        }

        public virtual void Consume(Player user)
        {
            Console.WriteLine("Item Consumed");
            Console.ReadKey();
        }


        

        public enum ItemType { WEAPON, GRIMOIRE, CONSUMABLE }

    }
}
