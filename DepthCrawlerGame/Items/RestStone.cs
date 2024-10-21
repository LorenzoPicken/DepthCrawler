using DepthCrawlerGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Items
{
    public class RestStone : Consumable
    {
        public RestStone(string name, int replenishedAmount) : base(name, replenishedAmount)
        {
        }

        public override void Consume(Player user)
        {
            if(user.HP < user.Maxhp * (3 / 4))
            {
                user.HP = user.Maxhp * (3 / 4);
            }

            user.Fatigue = 0;
            user.Rested();
        }
    }
}
