using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using DepthCrawlerGame.Items;

namespace DepthCrawlerGame.Entities.NPCs
{
    public class DungeonEzekielNPC : NPC
    {
        bool hasTalkedTo = false;
        int count2 = 0;

        public DungeonEzekielNPC(string name, string description) : base(name, description)
        {
        }

        public override void Talk()
        {
            Console.Clear();
            Console.WriteLine(this.Description);
            Console.ReadLine();
            Console.Clear();
            if (hasTalkedTo == false)
            {
                InitialDialogue();
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("You Contemplate The Man's Request.");
                    Console.WriteLine();
                    Console.WriteLine("-------------------");
                    Console.WriteLine("ACCEPT");
                    Console.WriteLine();
                    Console.WriteLine("DECLINE");
                    Console.WriteLine();
                    Console.WriteLine("-------------------");
                    string choice = Console.ReadLine().ToLower();

                    if(choice == "accept")
                    {
                        Console.Clear();
                        Console.WriteLine("Grabbing his hand firmly, you confirm your intentions to help him");
                        Console.ReadLine();
                        break;
                    }
                    else if(choice == "decline")
                    {
                        RefusalDialogue();
                        Weapon excalibur = new Weapon("Excalibur", true, false, 10, 6, 6, Weapon.WeaponType.LONGSWORD, DamageTypes.damageType.SLASHING);
                        GameManager.PlayerInstance.HolsterInventory.Add(excalibur);
                        GameManager.currentRoom.Description = "The Room Is Small And Damp. A Large Crystal Formation Hangs From The Ceiling Overhead, Basking The Center Of The Chamber, The Walls And Corners Still Obscured. Moss Covers Supporting Pillars And Water Drips, Leaving The Floor Wet And Cold. The Man's Body, Lays, Lifeless At The Room’s Center. Blood Spreads Through The Puddle He Lies In, Slowly Turning The Water Black As It Spreads.";
                        GameManager.currentRoom.Npcs = null;
                        return;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("This Is An Invalid Response To The Situation.");
                        Console.ReadLine();
                        Console.Clear();
                    }

                }
                hasTalkedTo = true;

            }
            if(hasTalkedTo == true)
            {
                int count = 0;
                
                foreach(KeyValuePair<Item, int> item in GameManager.PlayerInstance.ConsumableInventory)
                {
                    if(item.Key is HealthPotion)
                    {
                        if(item.Value == 1)
                        {
                            GameManager.PlayerInstance.ConsumableInventory.Remove(item.Key);
                            
                            
                        }
                        else
                        {
                            int quantity = item.Value - 1;
                            GameManager.PlayerInstance.ConsumableInventory.Remove(item.Key);
                            GameManager.PlayerInstance.ConsumableInventory.Add(item.Key, quantity);


                        }
                        Console.Clear();
                        Console.WriteLine("You Take A Health Potion From Your Backpack.");
                        Console.ReadKey();
                        Console.Clear();
                        HasPotionDialogue();
                        GameManager.currentRoom.Description = "The Room Is Small And Damp. A Large Crystal Formation Hangs From The Ceiling Overhead, Basking The Center Of The Chamber, The Walls And Corners Still Obscured. Moss Covers Supporting Pillars And Water Drips, Leaving The Floor Wet And Cold.";
                        GameManager.currentRoom.Npcs = null;
                        
                        return;
                        
                    }
                    else
                    {
                        count++;
                    }
                }
                if(count == GameManager.PlayerInstance.ConsumableInventory.Count)
                {
                    if(count2 == 0)
                    {
                        ConfirmationDialogue();
                        count2++;

                    }
                    else
                    {
                        NoPotionDialogue();
                    }
                }
            }
        }

        private void InitialDialogue()
        {
            Console.WriteLine("Dying Man >  Oi…");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("The man speaks weakly, out of breath");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Dying Man >  What did I say about scaring me?");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Dying Man >  Don't you have anything better to do…");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Dying Man >  Let me die in peace, you stupid illusion.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("The Man weakly swats his hand at you, his hand lightly slapping your cheek.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("He pauses, looking a bit confused");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Backing his hand away, he slaps you again.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Dying Man >  Blimey… That usually works… Must be crossing over to the land of the dead now.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Closing his eyes, the man goes to lie down, crossing his arms over his chest.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("You catch his shoulder and pull him back up to meet your eyes. The man's glassy stare looks even more surprised.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Dying Man >  Oi Mate… If I didn't know any better, I'd say you're the real thing.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("He attempts to slap you one more time. You catch his arm before he can do so, confirming that you are, in fact, not an illusion.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("The man's expression suddenly gets much more serious.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Dying Man >  Bugger! And here I thought I'd be dying here alone.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("He reaches his arm out towards you.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Dying Man >  You mind giving this dying bloke a hand?");
            Console.ReadKey();
            Console.Clear();
        }

        private void ConfirmationDialogue()
        {
            
            Console.Clear();
            Console.WriteLine("Dying Man >  Ah… A real beauty you are lad. Listen, I've been lying in the room for some time now so I'll be right as rain on my own. What you need to do is find something to treat.. treat this…");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("The man, as if just realising the gravity of his wound, looks a bit mortified.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Dying Man >  In the meantime, I'll wait here. I got my hands full with this not dying thing.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Dying Man >  I'll be cheering for ya!");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Dying Man >  Oh and don't talk to the ghost blokes. They'll sneak up on ya, then vanish.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Dying Man >  Bunch a crazy fuckers.");
            
        }

        private void RefusalDialogue()
        {
            Console.Clear();
            Console.WriteLine("Reaching forward, you grab the hilt of the sword stuck in the man's gut and begin to pull.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Dread filling his eyes, he attempts to stop you, placing his hands on yours but he is too weak.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Pulling at the blade, blood haemorrhages out of the wound. The man yells in pain. As the weapon slides from his body, he goes limp, blood creating a puddle at your feet.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("The blade is black and gold, garnished with intricate designs. A black dragon engraving with a red jewel eye coils up the hilt. On the side, you can read the message:");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("EXCALIBUR. PROUD PROPERTY AND CRAFT OF EZEKIEL SMITH");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Still bloodied, you place the sword into your bottomless holster.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("EXCALIBUR was added to your HOLSTER");
            
            
            QuestManager.EzekielDead();
        }

        private void HasPotionDialogue()
        {
            Console.Clear();
            Console.WriteLine("Dying Man >  Ah… now that's a sight for sore eyes. You mind passing that here?");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Removing the cork off the top of the vial, he drinks the potion, then placing both hands on the weapon embedded in his gut, he pulls.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("The man grinds his teeth as the sword easily slides out of his wound, the bleeding stopping and the gash mostly closing up as the weapon leaves his body.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("One hand over his wound, he sits up and immediately, pulls a cloth out of his back pocket, cleaning his blood off of the blade. He speaks to you, eyes remaining on his sword as he cleans.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Dying Man >  Ah, I gotta tell ya, waking with a sword through my gut wasn't at the top of my list of things to do.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Dying Man >  Woke up falling from somewhere up there… Landed on my blade.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Dying Man >  She cuts well though.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("He points above his head, at a dark hole in the ceiling.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Ezekiel >  The name’s Ezekiel by the way. Good on ya for helping me out. I was starting to hallucinate from the blood loss. Thought you were another one of those illusions when you walked in.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("He slaps you on the shoulder, as if to insure once and for all that he wasn't talking to the void.");
            Console.ReadLine();
            Console.Clear();   
            Console.WriteLine("Finishing cleaning his blade, he reveals it to you. A black and gold blade, garnished with intricate designs. A black dragon engraving with a red jewel eye coils up the hilt.");
            Console.ReadLine();
            Console.Clear();   
            Console.WriteLine("Ezekiel >  This is my beauty, Excalibur. Crafted her myself.  My finest work yet and she cuts like it too.");
            Console.ReadLine();
            Console.Clear();   
            Console.WriteLine("He says this laughing, patting his wound. He winces from the laughter, or contact with the gash. Maybe both.");
            Console.ReadLine();
            Console.Clear();   
            Console.WriteLine("Ezekiel >  Say lad, would ya happen to have anywhere I could go to rest? Dying has me feeling mighty tired.");
            Console.ReadLine();
            Console.Clear();   
            Console.WriteLine("You decide to take Ezekiel with you. You should head back to the camp.");
            Console.ReadLine();
            Console.Clear();   
            Console.WriteLine("EZEKIEL JOINS YOUR PARTY");
            QuestManager.blackSmithInParty = true; ;

        }

        private void NoPotionDialogue()
        {
            Console.Clear();
            Console.WriteLine("Dying Man >  No luck yet?");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Dying Man >  No pressure mate but I can stay like this much longer if you get what i'm telling ya.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Dying Man >  Good luck lad. I’m counting on you.");
            
        }
    }
}
