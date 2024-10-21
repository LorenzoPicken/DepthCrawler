using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Entities.NPCs
{
    public class AmirNPC : NPC
    {
        public AmirNPC(string name, string description) : base(name, description)
        {
        }

        public override void Talk()
        {
            Console.WriteLine(this.Description);
            Console.ReadKey();
            Console.Clear();
            this.Dialogue = DialogueManager.amirInitialDialogue[0];
            this.Name = "Amir";

            while (this.Dialogue.Count > 0)
            {
                Console.WriteLine(Dialogue.First());

                Console.ReadKey();
                Console.Clear();
                Dialogue.Dequeue();
            }
            foreach (string dialogue in DialogueManager.amirRepeatingDialogue[0])
            {
                DialogueManager.amirInitialDialogue[0].Enqueue(dialogue);
            }
        }
    }
}
