using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Entities.NPCs
{
    public class EllaNPC : NPC
    {
        public EllaNPC(string name, string description) : base(name, description)
        {
        }

        public override void Talk()
        {
            this.Dialogue = DialogueManager.ellaInitialDialogue[0];

            while (this.Dialogue.Count > 0)
            {
                Console.WriteLine(Dialogue.ToString());

                Console.ReadKey();
                Dialogue.Dequeue();
            }
            DialogueManager.ellaInitialDialogue[0] = DialogueManager.ellaRepeatingDialogue[0];
        }
    }
}
