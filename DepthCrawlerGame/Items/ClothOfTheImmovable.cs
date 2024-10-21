using DepthCrawlerGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Items
{
    public class ClothOfTheImmovable : Consumable
    {
        public ClothOfTheImmovable(string name, int replenishedAmount) : base(name, replenishedAmount)
        {
        }

        public override void Consume(Player user)
        {
            user.CannotTakeDamage = true;
        }
    }
}
