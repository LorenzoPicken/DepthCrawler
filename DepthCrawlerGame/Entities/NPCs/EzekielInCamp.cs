using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Entities.NPCs
{
    public class EzekielInCamp:NPC
    {
        int count = 0;
        
        public EzekielInCamp(string name, string description) : base(name, description)
        {
        }

        public override void Talk()
            {
                Console.WriteLine(this.Description);
                Console.ReadKey();
                Console.Clear();
                this.Dialogue = DialogueManager.ezekielInitialDialogue[0];
                

                while (this.Dialogue.Count > 0)
                {
                    Console.WriteLine(Dialogue.First());

                    Console.ReadKey();
                    Console.Clear();
                    Dialogue.Dequeue();
                }
                UpgradeMana(GameManager.PlayerInstance);
                foreach (string dialogue in DialogueManager.ezekielRepeatingDialogue[0])
                {
                    DialogueManager.ezekielInitialDialogue[0].Enqueue(dialogue);
                }
            }

        private void UpgradeMana(Player player)
        {
            if(QuestManager.hasReportedScouts == true && this.count == 0)
            {
                player.MaxMana = player.MaxMana*3;
                player.Mana = player.MaxMana;
                count++;
            }
        }
        
    }
}
