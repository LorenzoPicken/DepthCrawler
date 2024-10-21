using DepthCrawlerGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Items
{
    public class HealthPotion : Consumable
    {
        public HealthPotion(string name, int replenishedAmount) : base(name, replenishedAmount)
        {

        }


        public override void Consume(Player user)
        {
            if(user.HP+ this.ReplenishedAmount > user.Maxhp)
            {
                user.HP = user.Maxhp;
            }
            else
            {
                user.HP += this.ReplenishedAmount;
            }
        }
    }
}
