using DepthCrawlerGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Items
{
    public class StaminaPotion : Consumable
    {
        public StaminaPotion(string name, int replenishedAmount) : base(name, replenishedAmount)
        {
        }


        public override void Consume(Player user)
        {
            if (user.Fatigue - this.ReplenishedAmount < 0)
            {
                user.Fatigue = 0;
            }
            else
            {
                user.Fatigue -= this.ReplenishedAmount;
            }
        }
    }
}
