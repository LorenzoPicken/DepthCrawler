using DepthCrawlerGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Items
{
    public class ManaPotion : Consumable
    {
        public ManaPotion(string name, int replenishedAmount) : base(name, replenishedAmount)
        {
        }

        public override void Consume(Player user)
        {
            if (user.Mana + this.ReplenishedAmount > user.MaxMana)
            {
                user.Mana = user.MaxMana;
            }
            else
            {
                user.Mana += this.ReplenishedAmount;
            }
            
            Console.ReadKey();
        }
    }
}
