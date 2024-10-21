using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepthCrawlerGame.Items;

namespace DepthCrawlerGame.Ressources
{
    public class ItemNode
    {
        public Consumable consumable;
        public int quantity;
        public ItemNode Next;

        public ItemNode(Consumable consumable, int quantity)
        {
            this.consumable = consumable;
            this.quantity = quantity;
            this.Next = null;
        }
    }
}
