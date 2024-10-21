using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Entities.NPCs
{
    public class ZorinNPC : NPC
    {
        
        public ZorinNPC(string name, string description) : base(name, description)
        {

        }

        
        public override void Talk()
        {
            Console.WriteLine(this.Description);
            Console.ReadKey();
            Console.Clear();
            this.Name = "Zorin";
            this.Dialogue = DialogueManager.zorinInitialDialogue[0];

            while(this.Dialogue.Count > 0)
            {
                Console.WriteLine(Dialogue.First());

                Console.ReadKey();
                Console.Clear();
                Dialogue.Dequeue();
            }
            foreach(string dialogue in DialogueManager.zorinRepeatingDialogue[0])
            {
                DialogueManager.zorinInitialDialogue[0].Enqueue(dialogue);
            }
            
        }
    }
}
