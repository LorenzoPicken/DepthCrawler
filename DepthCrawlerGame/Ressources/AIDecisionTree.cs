using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Ressources
{
    public class AIDecisionTree
    {
        private DecisionNode root;


        public AIDecisionTree()
        {
            root = new DecisionNode("");
            root.Left = new DecisionNode("High Health");
            root.Right = new DecisionNode("Low Health");
        }


        public string MakeDecision(int currentEnemyHealth, int lowHealthTreshhold)
        {
            return Traverse(root, currentEnemyHealth, lowHealthTreshhold);
        }

        private string Traverse(DecisionNode current, int enemyHealth, int lowHalthTreshhold)
        {
            if(enemyHealth >= Convert.ToInt32(lowHalthTreshhold))
            {
                current.Action = "High Health";
                if(current.Action == "High Health")
                {
                    return current.Action;
                }
                else
                {
                    Traverse(current.Left, enemyHealth, lowHalthTreshhold);
                    

                }
            }
            else
            {
                current.Action = "Low Health";
                if (current.Action == "Low Health")
                {
                    return current.Action;
                }
                else
                {
                    Traverse(current.Right, enemyHealth, lowHalthTreshhold);

                }
            }
            return "Low Health";
        }
    }
}
