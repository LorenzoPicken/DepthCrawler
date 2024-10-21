using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Ressources
{
    public class DecisionNode
    {
        public string Action { get; set; }
        public DecisionNode Left { get; set; }
        public DecisionNode Right { get; set; }

        public DecisionNode(string choice)
        {
            Action = choice;
            Left = null;
            Right = null;
        }
    }
}
