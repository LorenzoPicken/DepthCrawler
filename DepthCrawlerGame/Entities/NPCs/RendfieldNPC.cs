using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Entities.NPCs
{
    public class RendfieldNPC : NPC
    {
        readonly private Random random = new Random();
        bool hasSpoken = false;
        int spokenCount = 0;
        List<string> dishes = new List<string> { "Minotaur Steak", "Gargoyle Wing Soup", "Bonedrike Marrow And Red Sauce", "Grilled Spider Legs", "Dungeon Mushroom and Cave Kail Salad", "Cave N Turf Of The Week" };
        public RendfieldNPC(string name, string description) : base(name, description)
        {
        }

        public override void Talk()
        {
            Console.WriteLine(this.Description);
            Console.ReadKey();
            Console.Clear();
            if (QuestManager.hasReportedScouts == true)
            {
                this.Dialogue = DialogueManager.rendfieldInitialDialogue[0];
                while (this.Dialogue.Count > 0)
                {
                    Console.WriteLine(Dialogue.First());

                    Console.ReadKey();
                    Console.Clear();
                    Dialogue.Dequeue();
                }
                FeedPlayer(GameManager.PlayerInstance);
                GameManager.currentRoom.Npcs.Remove(this);
            }
            else
            {

                this.Name = "Rendfield";
                string dish = dishes[random.Next(0, dishes.Count)];
                if(GameManager.EnemiesFought >= 3)
                {
                    if(spokenCount < 2)
                    {
                        this.Dialogue = DialogueManager.rendfieldInitialDialogue[0];

                        while (this.Dialogue.Count > 0)
                        {
                            Console.WriteLine(Dialogue.First());

                            Console.ReadKey();
                            Console.Clear();
                            Dialogue.Dequeue();
                        }
                        spokenCount++;
                        if (hasSpoken == true)
                        {
                            Console.WriteLine("Rendfield >  Speaking Of, Today's Special Is " + dish + ".");
                            Console.WriteLine("Eat Up!");
                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine("As You Eat, You Feel Your Energy Returning.");
                            Console.ReadKey();
                            Console.Clear();
                            
                        }
                        DialogueManager.rendfieldInitialDialogue[0] = DialogueManager.rendfieldRepeatingDialogue[0];

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Rendfield >  You Look Hungry. Here's Today's Special, " + dish);
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine("You Feel You Strength Return With Every Bite.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    hasSpoken = true;
                    GameManager.EnemiesFought = 0;
                    FeedPlayer(GameManager.PlayerInstance);

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Rendfield >  Sorry, I Gotta Save Some Food For The Others. Come See Me Later, When Ive Got My Hands On Some More Ingredients.");
                    Console.ReadKey();
                }
            }
            
        }

        private void FeedPlayer(Player player)
        {
            if (player.HP < player.Maxhp * 3 / 4 )
            {
                player.HP = player.Maxhp * 3 / 4;
            }
            player.Fatigue = 0;
        }
    }
}
