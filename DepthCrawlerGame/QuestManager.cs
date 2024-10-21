using DepthCrawlerGame.Entities;
using DepthCrawlerGame.Entities.NPCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame
{
    public static class QuestManager
    {
        public static bool hasFoundCamp = false;
        public static bool hasFoundBlackSmith = false;
        public static bool hasFoundScouts = false;
        public static bool hasFoundAltarRoom = false;

        public static bool blackSmithInParty = false;

        public static bool ezekielInCamp = false;
        public static bool ezekielDead = false;

        public static bool hasReportedScouts = false;
        public static bool returnToCamp = false;

        

        public static void EzekielInCamp()
        {
            if(GameManager.currentRoom.IsCamp == true && blackSmithInParty == true)
            {
                blackSmithInParty = false;
                ezekielInCamp = true;
                GameManager.EnemiesFought = 5;

                Queue<string> rend1_2 = new Queue<string>();

                rend1_2.Enqueue("Rendfield >  Looks like you found yourself a friend while you were out there. Quite a handy one at that.");
                rend1_2.Enqueue("Rendfield >  First time I see this many newbies turn up so quick.");
                rend1_2.Enqueue("Rendfield >   Anyways, your buddy is somethin’ else. He was over here not too long ago and noticed that my knife was in need of some love. Took it and brought it back an hour later.");
                rend1_2.Enqueue("As he speaks, he twirls the blade between his fingers.");
                rend1_2.Enqueue("Rendfield >  Hard to believe it's even the same blade. Seriously sharp too. Cuts through anything like air. I've already sliced my finger tips a few times preparing this meal.");

                DialogueManager.rendfieldInitialDialogue.Add(rend1_2);


                Queue<string> amir2_2 = new Queue<string>();
                amir2_2.Enqueue("Amir >  So you actually found another person out there? And alive at that. Good work.");
                amir2_2.Enqueue("Amir >  He says he’s a blacksmith, pretty proud of that title too.");
                amir2_2.Enqueue("Amir >  I hope his skills are as good as he says, although if that sword of his is anything to go off of, then count me impressed.");
                amir2_2.Enqueue("Amir >  Supposedly by the time I recover, he’ll have a new weapon for me to use. That way I can be of some actual use to the guard team.");
                amir2_2.Enqueue("Amir >  He is a bit odd though. He’s been insisting on being some kind of ghost exterminator. Says that if ever you're faced with one of them you just wave your hand at them and they disappear.");
                amir2_2.Enqueue("Amir >  Sounds like a bunch of bull if you ask me, but the headmaster said that he lost quite a bit of blood before you found him so maybe that's where the crazy talk comes from.");
                amir2_2.Enqueue("Amir >  Well that and this place in general. When you spend enough time down here, you become a little crazy, no? I for one feel it.");
                DialogueManager.amirRepeatingDialogue.Add(amir2_2);

                DialogueManager.amirInitialDialogue.RemoveAt(0);
                DialogueManager.amirRepeatingDialogue.RemoveAt(0);

                DialogueManager.rendfieldInitialDialogue.RemoveAt(0);

                DialogueManager.zorinInitialDialogue.RemoveAt(0);
                DialogueManager.zorinRepeatingDialogue.RemoveAt(0);

                DialogueManager.zorinInitialDialogue[0].Enqueue("Zorin >  Oh, And Before You Go. Thank You For Bringing Ezekiel Back With You.");
                DialogueManager.zorinInitialDialogue[0].Enqueue("Zorin >  He Is Very Grateful For What You Have Done for Him And I Believe He Would Like A Word With You To Express This.");
                DialogueManager.zorinInitialDialogue[0].Enqueue("Zorin >  I Bandaged Him Up And Left Him A Tent To The North Of The Camp.");

                EzekielInCamp ezekiel = new EzekielInCamp("Ezekiel", "Sitting In His Tent, He Inspects His Freshly Bandaged Wound.");
                GameManager.currentRoom.Npcs.Add(ezekiel);
                StatisticsTracker.ArtefactsCollected = 2;

            }
        }


        public static void EzekielDead()
        {
            ezekielDead = true;
            GameManager.EnemiesFought = 5;
            Queue<string> amir2_2 = new Queue<string>();
            amir2_2.Enqueue("Amir >  Hey! Did you hear? Supposedly, a dead body showed up at one of the room’s entrances today.");
            amir2_2.Enqueue("Amir >  The guards think one of the monsters dragged it nearby but got scared and ran off seeing the lights from the camp.");
            amir2_2.Enqueue("Amir >  The chief tried to keep the discovery exclusive to the guards to avoid the spread of panic, but Rend opened his big mouth. That's how he is when he cooks, just can't stop talking.");
            amir2_2.Enqueue("Amir >  Well anyways, the good news is that it wasn't anyone from the camp. The guy, or at least what was left of him after the monsters had their fun, was a redhead. Nobody who's ever been through here has ever matched that description.");
            amir2_2.Enqueue("Amir >  You know what bothers me though? The cadaver had what looked like a sword wound going through its gut. I'd like to believe that one of those knight monsters made that gash but I've never heard of those dragging their victims around before. They usually avoid doing any further damage to their victims after they kill. Even keep other monsters away from the bodies too. Some crappy twisted mockery of the honour code.");
            amir2_2.Enqueue("Amir >  Well this is all from Rend so maybe he got some details wrong, he can be a bit of a meathead sometimes. Part of his charm I guess.");
            amir2_2.Enqueue("Amir >  Still... Poor Guy... Makes You Realise Just How Lucky You Really Are.");
            DialogueManager.amirRepeatingDialogue.Add(amir2_2);


            Queue<string> rend1_2 = new Queue<string>();

            rend1_2.Enqueue("Rendfield >  Hey! Look Who's Back. How Was The Trip Outside The Camp?");
            rend1_2.Enqueue("Rendfield >  You Would Not Believe The Day We Had Here Today. Lot Of Monster Attacks, And More Than That, They Brought A Present.");
            rend1_2.Enqueue("Rendfield >  A Crap... Nevermind, Im Gonna Get Another Earful From The Chief... And Some More Snarky Ass Remarks From Amir.");
            rend1_2.Enqueue("Rendfield >  Cookin' Always Gets Me Talking.");

            DialogueManager.rendfieldInitialDialogue.Add(rend1_2);

            DialogueManager.amirInitialDialogue.RemoveAt(0);
            DialogueManager.amirRepeatingDialogue.RemoveAt(0);

            DialogueManager.rendfieldInitialDialogue.RemoveAt(0);

            DialogueManager.zorinInitialDialogue.RemoveAt(0);
            DialogueManager.zorinRepeatingDialogue.RemoveAt(0);
            StatisticsTracker.ArtefactsCollected = 2;
        }

        public static void ReturnToCamp()
        {
            if(ezekielDead && GameManager.currentRoom.IsCamp)
            {
                returnToCamp = true;
            }
        }

        public static void CheckScoutsRoom()
        {
            if (GameManager.currentRoom.IsScoutRoom)
            {
                Console.Clear();
                while(DialogueManager.scoutsRoomDescription.Count > 0)
                {
                    Console.WriteLine(DialogueManager.scoutsRoomDescription.First());
                    Console.ReadKey();
                    Console.Clear();
                    DialogueManager.scoutsRoomDescription.Dequeue();
                    StatisticsTracker.ArtefactsCollected = 3;
                }
            }
        }

        public static void ReturnToCampFromScoutRoom()
        {
            if(hasFoundScouts == true && GameManager.currentRoom.IsCamp == true)
            {
                
                if(ezekielInCamp == true)
                {
                    GameManager.EnemiesFought = 5;
                    GameManager.currentRoom.Description = "Stepping into the camp, you notice that the usual liveliness is replaced by a sombre tone. Tents to the west of the room, are torn and collapsed. Next to the fire, is a white cloth, covering something large."; 
                    foreach(NPC npc in GameManager.currentRoom.Npcs)
                    {
                        if(npc is AmirNPC)
                        {
                            npc.Description = "Sitting on a wooden box, Amir is dressed in the same armour as the other members of the guard team. No longer wearing his bandages, his left eye is now exposed, scared and blind. At his side, a sword with saw-like teeth is holstered to his belt. He watches you approach, his expression grim.";
                        }
                        if(npc is ZorinNPC)
                        {
                            npc.Description = "Sitting by the fire, her back is turned to you. You approach her carefully, to not startle her but, knowing that you approach, she speaks, still facing away.";
                        }
                        if (npc is RendfieldNPC)
                        {
                            npc.Description = "Approaching the cook, you take a seat at his counter. He wears a bandage over his right arm, blood seeping through it, turning it red. His sword, still at his side, is bloodied and chipped. He cooks but seems distracted.";
                        }
                        if(npc is EzekielInCamp)
                        {
                            npc.Description = "At his station, the blacksmith rummages around collecting tools and materials, throwing them into bags.";
                        }

                    }
                    Queue<string> amirInitial = new Queue<string>();
                    amirInitial.Enqueue("Amir >  I'm guessing you couldn't find the scouting party?");
                    amirInitial.Enqueue("Reaching into your backpack, you pull out the cloth you found, showing it.");
                    amirInitial.Enqueue("Amir >  I see… We all had a feeling something had happened. No excursions outside this room ever lasts that long. We don't have the resources for drawn out trips like that.");
                    amirInitial.Enqueue("Amir >  Even so, we wanted to keep a strong front for the headmaster. Her dad was part of that party after all.");
                    amirInitial.Enqueue("Amir >  We had troubles of our own here too today.");
                    amirInitial.Enqueue("He points over to the fire where a body wrapped in cloth lays.");
                    amirInitial.Enqueue("Amir >  She was just a little girl you know. Same story as the rest of us. Separated from her family… her parents. She had been a part of this camp for a long time. Longer than me. The headmaster was the one who found her.");
                    amirInitial.Enqueue("Amir >  We tried our best but one of the monsters got past us and started rampaging through the camp. Ended up charging through those tents over there where she was playing. By the time I managed to take the beast down, it was already too late.");
                    amirInitial.Enqueue("Amir >  We already held a ceremony but if you would like, you can still go pay your respects before we burn the body. The best thing we can do here for the dead. Ground is too hard for burials.");
                    amirInitial.Enqueue("Amir >  After that, you should go talk to the headmaster. Even if it’ll be hard on her, letting her ride on false hope is probably worse.");
                    DialogueManager.amirInitialDialogue.Add(amirInitial);


                    Queue<string> amir = new Queue<string>();
                    amir.Enqueue("Amir >  After today, we all know that this camp isn't safe anymore.");
                    amir.Enqueue("Amir >  The headmaster’s been talking about leaving for a long time now. With three of the four pieces of the key now, it might be time to follow through with it.");
                    amir.Enqueue("Amir >  If we can find that last piece on the way, we can make a break straight for the exit.");
                    amir.Enqueue("Amir >  I just hope that her fathers hunch about that door is right. We can't stay in this place any longer.");
                    DialogueManager.amirRepeatingDialogue.Add(amir);


                    Queue<string> rend = new Queue<string>();
                    rend.Enqueue("Rendfield >  Oh hey. Welcome back.");
                    rend.Enqueue("Unusually, he doesn't speak much.");
                    rend.Enqueue("Filling your bowl, he passes it to you.");
                    rend.Enqueue("Rendfield >  Last of the food from the ceremony. I'm done cooking for today, so make this count.");
                    rend.Enqueue("Taking off his apron, he walks towards the tents.");
                    rend.Enqueue("You eat, but cant bring yourself to finish the bowl, uneasy by the situation. Never the less the food helps to bring back your strength.");
                    DialogueManager.rendfieldInitialDialogue.Add(rend);


                    Queue<string> ezekielInitial = new Queue<string>();
                    ezekielInitial.Enqueue("Ezekiel >  It's horrible isn't it. I heard she was just a little girl. Blasted monsters.");
                    ezekielInitial.Enqueue("Ezekiel >  This place truly is cruel.");
                    ezekielInitial.Enqueue("Ezekiel >  Amir is taking it pretty hard. Poor Bugger. He did what he could. I’m just glad I finished his weapon in time. Who knows how many more people we could have lost if Amir had not slayed the thing when he did.");
                    ezekielInitial.Enqueue("Ezekiel >  From what the headmaster and Amir been sayin’, looks like the camp is gonna be relocating.");
                    ezekielInitial.Enqueue("Ezekiel >  I'm going to have to start packing up my things too before long.");
                    ezekielInitial.Enqueue("Ezekiel >  Ah yeah, mate, one more thing. Check this out.");
                    ezekielInitial.Enqueue("He hands you a metal wrist brace with a blue crystal embedded into the top. Runes are engraved into its surface.");
                    ezekielInitial.Enqueue("Ezekiel >  I’ve seen some of those handy spells you cast, wicked stuff. I also noticed that ya can't use too many at once. This brace should help with that. That stone there stores extra mana in it so, when you drink one of those mana potions of yours, just splash a drop or two on this gem and it should recharge along with your personal mana reserves.");
                    ezekielInitial.Enqueue("Ezekiel >  Hope you find some use from it. Was quite a complex piece. I aint the best when it comes to magic stones so this one was a real bugger to get right.");
                    ezekielInitial.Enqueue("Ezekiel >  Anyways, i'm gonna start packing now. Cheers.");
                    DialogueManager.ezekielInitialDialogue.Add(ezekielInitial);


                    Queue<string> ezekielRepeating = new Queue<string>();
                    ezekielRepeating.Enqueue("Ezekiel >  Sorry mate, workstation isn't gonna be available. Been packing up all my tools, less i get another talking to by Amir for being too slow.");
                    DialogueManager.ezekielRepeatingDialogue.Add(ezekielRepeating);





                    Queue<string> zorinInitial = new Queue<string>();
                    zorinInitial.Enqueue("Zorin >  You came back alone… So everyone's suspicions about the scouting team were correct then.");
                    zorinInitial.Enqueue("Zorin >  They all tried to hide their concerns from me. As if I wasn't aware of what their drawn out trip implied.");
                    zorinInitial.Enqueue("Zorin >  Well, in any case, I should count myself lucky at the very least. Nobody is ever reunited with family here, but I, amongst the odds, was brought to this place accompanied by my father.");
                    zorinInitial.Enqueue("Zorin >  He always looked out for me, always having my best interest in mind. That's why he created the scouting team. To find a way back to our old lives… To save ME from this place. Even in the very end, he died trying to save me, to save us.");
                    zorinInitial.Enqueue("Reaching into your backpack you show her the cloth.");
                    zorinInitial.Enqueue("Taking it from your hand, she opens it revealing the gold piece. She places it with the others in her cloth, giving them to you.");
                    zorinInitial.Enqueue("Zorin >  I would like you to keep these with you. These pieces will be safer in your capable hands. Now, if you would excuse me, I would like some time alone please.");
                    zorinInitial.Enqueue("She embraces the cloth you gave her as you step away.");
                    DialogueManager.zorinInitialDialogue.Add(zorinInitial);


                    Queue<string> zorinRepeating = new Queue<string>();
                    zorinRepeating.Enqueue("Zorin >  I believe that it is time for us to leave the camp.");
                    zorinRepeating.Enqueue("Zorin >  I have spoken with Amir and he believes that it is best as well.");
                    zorinRepeating.Enqueue("Zorin >  He suspects that our constant battles defending this room are attracting more beasts.");
                    zorinRepeating.Enqueue("Zorin >  I have suggested that we relocate to the room where my father found the door. In any case, it will be some time before the monsters find us there, and that section of the dungeon has been mostly cleared out by the scouts so it will be safer.");
                    zorinRepeating.Enqueue("Zorin >  We will begin packing our things. If there is anything you wish to do before we leave. Now is a good time for that.");
                    DialogueManager.zorinRepeatingDialogue.Add(zorinRepeating);


                    DialogueManager.amirInitialDialogue.RemoveAt(0);
                    DialogueManager.amirRepeatingDialogue.RemoveAt(0);

                    DialogueManager.rendfieldInitialDialogue.RemoveAt(0);

                    DialogueManager.zorinInitialDialogue.RemoveAt(0);
                    DialogueManager.zorinRepeatingDialogue.RemoveAt(0);

                    DialogueManager.ezekielInitialDialogue.RemoveAt(0);
                    DialogueManager.ezekielRepeatingDialogue.RemoveAt(0);
                    



                }
                else
                {
                    GameManager.EnemiesFought = 5;
                    GameManager.currentRoom.Description = "There is nothing left for you here...";
                    GameManager.currentRoom.Npcs.Clear();
                    Console.Clear();
                    while (DialogueManager.destroyedCamp.Count > 0)
                    {
                        Console.WriteLine(DialogueManager.destroyedCamp.First());
                        Console.ReadKey();
                        Console.Clear();
                        DialogueManager.destroyedCamp.Dequeue();
                    }
                    
                }
                hasReportedScouts = true;
                returnToCamp = false;

            }
        }

        public static void InitiateDragonRoom()
        {
            if (GameManager.currentRoom.Enemy != null)
            {

                if (GameManager.currentRoom.Enemy.Type == EnemySpecification.DRAGON)
                {
                    Console.Clear();
                    while (DialogueManager.dragonRoomQueue.Count > 0)
                    {
                        Console.WriteLine(DialogueManager.dragonRoomQueue.First());
                        Console.ReadKey();
                        Console.Clear();
                        DialogueManager.dragonRoomQueue.Dequeue();
                    }
                }
            }
        }
    }
}
