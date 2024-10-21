using DepthCrawlerGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Items
{
    public class RageDust : Consumable
    {
        public RageDust(string name, int replenishedAmount) : base(name, replenishedAmount)
        {
        }

        public override void Consume(Player user)
        {
            if(user.Fatigue >= user.MaxFatigue)
            {
                user.CurrentStrength = user.Strength;
            }
            else
            {
                user.CurrentStrength = (user.Strength * (6 / 4));
            }
        }
    }
}
