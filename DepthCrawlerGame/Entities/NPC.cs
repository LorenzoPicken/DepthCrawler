using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Entities
{
    public class NPC
    {
        private string name;
        private string description;
        private Queue<string> dialogue;

        public NPC(string name, string description)
        {
            this.name = name;
            this.description = description;
            
        }

        public Queue<string> Dialogue { get => dialogue; set => dialogue = value; }
        public string Description { get => description; set => description = value; }
        public string Name { get => name; set => name = value; }

        public virtual void Talk()
        {
            

        }
    }
}
