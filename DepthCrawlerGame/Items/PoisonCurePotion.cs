using DepthCrawlerGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Items
{
    public class PoisonCurePotion : Consumable
    {
        public PoisonCurePotion(string name, int replenishedAmount) : base(name, replenishedAmount)
        {
        }

        public override void Consume(Player user)
        {
            user.IsPoisoned = false;
        }
    }
}
