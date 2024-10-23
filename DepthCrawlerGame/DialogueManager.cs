using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame
{
    public static class DialogueManager
    {
        public static List<Queue<string>> zorinInitialDialogue = new List<Queue<string>>();
        public static List<Queue<string>> zorinRepeatingDialogue = new List<Queue<string>>();

        public static List<Queue<string>> ellaInitialDialogue = new List<Queue<string>>();
        public static List<Queue<string>> ellaRepeatingDialogue = new List<Queue<string>>();

        public static List<Queue<string>> amirInitialDialogue = new List<Queue<string>>();
        public static List<Queue<string>> amirRepeatingDialogue = new List<Queue<string>>();

        public static List<Queue<string>> rendfieldInitialDialogue = new List<Queue<string>>();
        public static List<Queue<string>> rendfieldRepeatingDialogue = new List<Queue<string>>();

        public static List<Queue<string>> ezekielInitialDialogue = new List<Queue<string>>();
        public static List<Queue<string>> ezekielRepeatingDialogue = new List<Queue<string>>();



        //Room Inspect
        public static Queue<string> scoutsRoomDescription = new Queue<string>();
        public static Queue<string> destroyedCamp = new Queue<string>();
        public static Queue<string> dragonRoomQueue = new Queue<string>();


        //End Game Queues

        public static Queue<string> goodEnding = new Queue<string>();
        public static Queue<string> badEnding = new Queue<string>();


        public static void AssignDialogue()
        {
            ZorinDialogue();
            AmirDialogue();
            RendFieldDialogue();
            EzekielDialogue();




            FillScoutRoomQueue();
            FillDestroyedCampQueue();
            FillDragonRoomQueue();



            FillGoodEndingQueue();
            FillBadEndingQueue();
        }


        private static void ZorinDialogue()
        {
            Queue<string> zorin1_1 = new Queue<string>();
            zorin1_1.Enqueue("Young Woman >  Oh, Hello There. You Are A New Face!");
            zorin1_1.Enqueue("Young Woman >  I Am Glad You Found Your Way To Our Camp.");
            zorin1_1.Enqueue("Young Woman >  Those Who Can Survive This Place Alone Are Few And Fa...");
            zorin1_1.Enqueue("Zorin >  I Apologise… How Rude Of Me. My Name Is Zorin.");
            zorin1_1.Enqueue("Zorin >  You May Make Yourself At Home Here If You Would Like. It Is One Of The Few Safe Places In This Maze. There Is An Empty Tent To The South Of The Room.");
            zorin1_1.Enqueue("Zorin >  You Must Be Tired, So Please, Rest. Once You Are Ready, Come Back To See Me. If You Wish To Stay, I Ask That You Help Our Camp, Especially Now, It Has Been Quite Hard For Us. If You Are Hungry, You Can Go See Renfield Over By The Cooking Pot. He Can Make You Something To Eat.");
            zorin1_1.Enqueue("Zorin >  Rest Well Traveler.");

            zorinInitialDialogue.Add(zorin1_1);


            Queue<string> zorin1_2 = new Queue<string>();
            zorin1_2.Enqueue("Zorin >  Ah, your back. Very good. I have some very important news to share.");
            zorin1_2.Enqueue("Zorin >  Have a look…");
            zorin1_2.Enqueue("Opening the white cloth on the table, it now contains two golden quarter circle artefacts, fitting together perfectly.");
            zorin1_2.Enqueue("Zorin >  My father and his team came across it during their search.");
            zorin1_2.Enqueue("Zorin >  Feeling that it would be safer to keep it here in camp, he requested that a scout return with it and a message. They were delivered while you were away.");
            zorin1_2.Enqueue("Zorin >  From the letter that my father wrote, It would seem that monsters have become more frequent in the other sections of the dungeon as well.");
            zorin1_2.Enqueue("Zorin >  My father states that they are managing fine, however the scout that returned today had some minor injuries and was quite tired.");
            zorin1_2.Enqueue("Zorin >  I've decided to discharge them in order to rest and recover.");
            zorin1_2.Enqueue("Zorin >  I ask that you please go and join my father and the others as quickly as you can.");

            zorinInitialDialogue.Add(zorin1_2);




            Queue<string> zorin2_1 = new Queue<string>();

            zorin2_1.Enqueue("Zorin >  Ah, Traveler, You Have Returned. I Hope Your Rest Was Revitalising.");
            zorin2_1.Enqueue("Zorin >  Having Made It This Far On Your Own, You Must Be Quite Strong. I Believe You Would Do Well As A Member Of Our Scouting Team.");
            zorin2_1.Enqueue("Zorin >  My Father Leads The Group, Although, Since They Are Out At The Moment, I Will Do My Best To Brief You.");
            zorin2_1.Enqueue("Unravelling A White Cloth, She Reveals A Gold Quarter Circle, The Size Of Her Hand.");

            zorin2_1.Enqueue("Zorin >  Our Scouting Party Came Upon This In The Dungeon Along With A Large Door With Four Slots, Identical To The Shape Of This Artefact.");
            zorin2_1.Enqueue("Zorin >  My Father Believes That, If We Can Find Three More, We May Be Able To Unlock The Door.");
            zorin2_1.Enqueue("Zorin >  He Suspects That Our Freedom Lies Beyond It.");
            zorin2_1.Enqueue("Zorin >  As Soon As You Have Recovered, Please, Try And Meet Up With My Father And His Team. Monster Attacks Have Been Worsening As Of Late. I Do Not Know How Much Longer Our Guard Team Can Protect Us. They Are Tiring.");

            zorinRepeatingDialogue.Add(zorin2_1);

            Queue<string> zorin2_2 = new Queue<string>();

            zorin2_2.Enqueue("Zorin >  Yes, is there anything I can do for you?");
            zorin2_2.Enqueue("Zorin >  I must press that you join the others as quickly as you can.");
            zorin2_2.Enqueue("Zorin >  Do not forget to have something to eat before, if Rendfield has some food leftover.");
            zorin2_2.Enqueue("Zorin >  You will need to be at your best out there in the maze and do be safe.");
            zorin2_2.Enqueue("Zorin >  Good Luck.");

            zorinRepeatingDialogue.Add(zorin2_2);
        }

        private static void AmirDialogue()
        {
            Queue<string> amir1_1 = new Queue<string>();
            amir1_1.Enqueue("Injured Man >  You’re A New face. The Scouting Team Find You Out There Too?");
            amir1_1.Enqueue("Injured Man >  Either Way, You Seem To Be Getting On Just Fine, Or At Least Better Than I Am.");
            amir1_1.Enqueue("Amir >  The Name’s Amir By The Way. Have You Spoken To The Headmaster Yet? She’s Over There By The Fire.");
            amir1_1.Enqueue("Amir >  Oh But Don't Tell Her I Called Her That. Although We All Treat Her Like Our Chief, She Doesn't Much Care For The Title.");
            amir1_1.Enqueue("Amir >  With Her Caretaking Skills And Level Head, We All Agreed That She Was The Best suited To Lead Us, Including Her Father. It’s Just Simpler This Way.");
            amir1_1.Enqueue("Amir >  She’s The One Who Patched Me Up.");
            amir1_1.Enqueue("He Lifts His Bandaged Arm.");
            amir1_1.Enqueue("Amir >  Cant say Im Doing Great, But I'd Be A Lot Worse Off Without Her. I Owe Her… We All Do.");

            amirInitialDialogue.Add(amir1_1);

            Queue<string> amir1_2 = new Queue<string>();

            amir1_2.Enqueue("Amir >  Ah, welcome back. An eventful excursion?");
            amir1_2.Enqueue("Amir >  Some pretty exciting news came in while you were out. The headmaster told me to let her know if ever you returned. She said she wanted to brief you on the details.");
            amir1_2.Enqueue("Amir >  Anyways, if you aren't busy, I'd recommend going to see her. She seemed a little eager… maybe a little bit worried too. Whatever was bothering her, I didn't ask. Seems it might involve you though.");
            amir1_2.Enqueue("Amir >  I'll leave you to it now. If ever you want to talk, you know where to find me.");
            amirInitialDialogue.Add(amir1_2);



            Queue<string> amir2_1 = new Queue<string>();

            amir2_1.Enqueue("Amir >  Isn't It Weird? This Place I Mean. Everyone Has The Same Story. You Wake Up One Day, Here, Alone, In The Dark.");
            amir2_1.Enqueue("Amir >  Makes You Wonder How Many Others There Are Out There.");
            amir2_1.Enqueue("Amir >  The Scouts Constantly Tell Us About The People They Find In This Place. Most, Already Dead.");
            amir2_1.Enqueue("Amir >  I Almost Joined Those Numbers Myself After Barely Escaping One Of Those Half Man Half Bull Beasts With My Life.");
            
            amir2_1.Enqueue("He Points At His Bandaged Head.");
            amir2_1.Enqueue("Amir >  If It Hadn't Been For The Chief’s Dad Finding Me, Id Be Dead Right Now, No Doubt.");
            amir2_1.Enqueue("Amir >  Once I Recover, I'm Assigned To Join The Guard Team. Monster Attacks Have been Getting Pretty Bad. We Can Use All The Help We Can Get, Even From A Cripple Like Me.");
            amir2_1.Enqueue("Amir >  Anyways, Do Me A Favour. If You Find Anyone Out There, Bring Them Back With You Would Ya. Every Person Has Something To Offer.");

            amirRepeatingDialogue.Add(amir2_1);

        }

        private static void RendFieldDialogue()
        {
            Queue<string> rend1_1 = new Queue<string>();
            rend1_1.Enqueue("Built Man >  Would you look at that, I haven't seen a newbie in quite a while.");
            rend1_1.Enqueue("Built Man >  Since the scouts aren't back yet, I assume you found your way here on your own?");
            rend1_1.Enqueue("Built Man >  A lucky one you are. No doubt about it.");
            rend1_1.Enqueue("Rendfield >  You can call me Rendfield or Rend like everyone else here does. I’m part of the guard team but I cook for the camp as well.");
            rend1_1.Enqueue("Rendfield >  Helps to calm the nerves and I can't have the others starving either anyways. The headmaster is a gem but her cooking skills need some work.");
            rend1_1.Enqueue("Rendfield >  I've heard horror stories from Amir about the kind of stuff she would serve the others before I came along. Amir swears he would have rather died in the dungeon than eat another one of her meals.");
            rend1_1.Enqueue("Rendfield >  Don't tell her I told you that though. Bless her soul, she tried her best for everyone.");
            rend1_1.Enqueue("Rendfield >  Anyways, you look like you could use a meal. You're in luck, today’s menu is one of my specials. Minotaur steak, garnished with dungeon mushrooms. Eat up. It’s good for ya!");
            rend1_1.Enqueue("You sit and eat, feeling mostly restored by the food and quite impressed.");

            rendfieldInitialDialogue.Add(rend1_1);

            





            Queue<string> rend2_1 = new Queue<string>();

            rend2_1.Enqueue("Rendfield >  How do I get my ingredients?");
            rend2_1.Enqueue("Rendfield >  Well that’s a dumb question. With the amount of monsters wandering into the camp recently, they practically jump into my cooking pot. I take whatever isn't too mangled to cook and butcher it. Combine that with some naturally growing plants in the dungeon, and you have everything you need for a good dish.");
            

            rendfieldRepeatingDialogue.Add(rend2_1);

        }

        private static void EzekielDialogue()
        {
            Queue<string> ezekiel1_1 = new Queue<string>();

            ezekiel1_1.Enqueue("Ezekiel >  Oi, How Goes It Ya Bugger?");
            ezekiel1_1.Enqueue("Ezekiel >  Thanks again for bringing me back with ya mate. I could get used to this… The people, the warm fire, the food… Even still being in this blasted dungeon, anything beats that dark cold room we met in.");
            ezekiel1_1.Enqueue("Ezekiel >  The chief was mighty nice to me too, as you can see, she set me up proper with this tent and work station. She even patched me up and got me some food. ");
            ezekiel1_1.Enqueue("Ezekiel >  She asked that I repair and maintain the camp's equipment, starting with guard team’s. I tell ya, they oughta take better care of their weapons. With the sorry state these are in, I got my work cut out for me.");
            ezekiel1_1.Enqueue("Ezekiel >  Well anyways, this gives me a chance to work my craft.");
            ezekiel1_1.Enqueue("Ezekiel >  Listen, I know I really owe ya big, so once i get the time, i'll make you something proper to show my thanks. Come check in with me when ya can.");
            ezekiel1_1.Enqueue("Ezekiel >  Cheers Mate!");

            ezekielInitialDialogue.Add(ezekiel1_1);


            Queue<string> ezekiel2_1 = new Queue<string>();

            ezekiel2_1.Enqueue("Ezekiel >  Oi, How Goes It Ya Bugger?");
            ezekiel2_1.Enqueue("Ezekiel >  Na, sorry mate, i dont got anything for ya right now. Been workin on it though. I think you'll facy it quite well.");
            ezekiel2_1.Enqueue("Ezekiel >  Cheers Mate!");
            ezekielRepeatingDialogue.Add(ezekiel2_1);
        }





        private static void FillScoutRoomQueue()
        {
            scoutsRoomDescription.Enqueue("Walking into the room, you smell the distinct scent of blood. A metallic scent easily distinguishable from the aroma of the damp stone walls and dusty air, and a strong one at that.");
            scoutsRoomDescription.Enqueue("At the back of the room, a shadow seems to be standing up against the wall, waiting for your approach.");
            scoutsRoomDescription.Enqueue("Drawing your weapon, you take a few steps before stepping on something obscured by your shadow and the dark room.");
            scoutsRoomDescription.Enqueue("Chanting a quick spell, a small fireball appears in your palm, burning dimly, brightening the ground a couple of feet ahead.");
            scoutsRoomDescription.Enqueue("Lifting your foot to see what impedes you reveals a severed hand sitting in a pool of red liquid.");
            scoutsRoomDescription.Enqueue("Sweeping the room ahead with your palm reveals a pair of feat, just barely within your small ring of light.");
            scoutsRoomDescription.Enqueue("You tread ahead finding a young man, lying motionless on the floor. A large gash across his back exposes a shattered spine and torn muscles.");
            scoutsRoomDescription.Enqueue("You Move on.");
            scoutsRoomDescription.Enqueue("Further up the room is another corpse, this one, a woman wearing armour. A large cut crosses from her flank, halfway up her chest, nearly severing her torso diagonally from her lower half. The armour that should have protected her is completely pried open by the attack.");
            scoutsRoomDescription.Enqueue("You notice that the cut, having come up diagonally, severed her right hand at the wrist, before continuing through the lower and middle ribs, only ending at the sternum.");
            scoutsRoomDescription.Enqueue("You look away, as you walk past two more mangled corpses before reaching the back of the room where the figure awaits.");
            scoutsRoomDescription.Enqueue("Raising your weapon, you strike at it only to stop short at the sight of an older man's face coming into light.");
            scoutsRoomDescription.Enqueue("Wearing a helmet, he stares blankly at you through greyed glassy eyes. His white beard is stained with dried blood, having once dripped from his red dyed lips.");
            scoutsRoomDescription.Enqueue("Stepping back to get a better look, you find a greataxe impaled through his breastplate, through the man's body and into the wall behind. Cracks snake across the wall in all directions behind his body.");
            scoutsRoomDescription.Enqueue("Attempting to unlodge the corpse from the wall, you pull on the axe but alas it doesn't budge.");
            scoutsRoomDescription.Enqueue("As you examine him, you spot a white cloth in his right hand which he grips tightly, even after death.");
            scoutsRoomDescription.Enqueue("Removing the cloth from his hand, and unravelling it, you find a golden artefact safely tucked inside, unscathed amongst the surrounding destruction. The cloth itself is also pristine, not a single spec of dust or drop of blood to be found on it.");
            scoutsRoomDescription.Enqueue("Folding the cloth back up, you place it in your backpack.");
            scoutsRoomDescription.Enqueue("You should return to the camp to report your findings.");
        }

        private static void FillDestroyedCampQueue()
        {
            destroyedCamp.Enqueue("Reentering the camp, you find the entire room in ruins.");
            destroyedCamp.Enqueue("Tents are destroyed and bodies litter the floor. The fire from the centre fire pit has begun to spread, lighting tents and equipment ablaze.");
            destroyedCamp.Enqueue("As you walk to the centre of the room, you step over members of the guard team, monster bodies, and a young child.");
            destroyedCamp.Enqueue("As you pass the cooking pot, you find Amir’s body, slumped over on the floor, his arm broken, face down on the stone. Rendfield’s sword lies next to him, snapped in half, but the owner is nowhere in sight.");
            destroyedCamp.Enqueue("A trail of blood leads to a tent nearby, having already completely burnt.");
            destroyedCamp.Enqueue("You reach the firepit and look down at the table. There, a sword through her back, you find Zorin’s body pinned down to the table by the blade, having skewered her to the table.");
            destroyedCamp.Enqueue("Ripping the sword out, you let her body tumble to the floor, revealing a white cloth she had been protecting.");
            destroyedCamp.Enqueue("Picking it up, you put it in your backpack then head back to the exit.");
            destroyedCamp.Enqueue("On your way, you hear heavy breathing coming from behind a wooden crate.");
            destroyedCamp.Enqueue("Throwing it out of the way reveals Rendfield, seated, hidden from sight.");
            destroyedCamp.Enqueue("He looks at you in disbelief and pain.");
            destroyedCamp.Enqueue("Rendfield >  I knew we wouldn't have held out much longer... but I thought we still had a little more time...");
            destroyedCamp.Enqueue("Rendfield >  Our equipment just couldn't hold out anymore. My stupid sword snapped in my own hands while I was trying to block a strike for Amir…");
            destroyedCamp.Enqueue("Rendfield >  Shit...");
            destroyedCamp.Enqueue("Looking at you, he holds out his hand, asking to be helped up.");
            destroyedCamp.Enqueue("Instead however, using the sword that had killed Zorin, you finish the man off yourself, before dropping the blade and walking away.");
        }


        private static void FillDragonRoomQueue()
        {
            dragonRoomQueue.Enqueue("The chamber you find yourself in is one of impossible beauty. Large pillars support a giant dome ceiling that extends far beyond any room you’ve been in before. At the top of the dome is a stained glass skylight, which casts the room in a golden hue. A second floor further up is filled with giant bookshelves and riches, adding to the rooms mystical nature. ");
            dragonRoomQueue.Enqueue("At its centre is a gold altar, on which, the final artefact is kept on display.");
            dragonRoomQueue.Enqueue("You take a step further into the room, drawn in by the golden piece.");
            dragonRoomQueue.Enqueue("Just then, a bright light shines down onto the altar, partially blinding you for a moment.");
            dragonRoomQueue.Enqueue("As you pear at it through your hands, the light begins to form a large shape.");
            dragonRoomQueue.Enqueue("Mending together, the bright rays form into a dragon, now standing where the altar had been.");
        }

        private static void FillGoodEndingQueue()
        {
            goodEnding.Enqueue("Injured From The Fight, You Stumble To The Chamber Exit, Leaning Against The Walls Of The Room As You Go. Once There, From Your Backpack, You Take Out A Map That Zorin Had Given You Before Parting Ways.");
            goodEnding.Enqueue("The Map, Hand Drawn With What Looks To Be Coal From A Fire, Traces A Path To A Door.");
            goodEnding.Enqueue("Taking One Last Look At The Radiant Skylight Of The Chamber, You Head Back Into The Dungeon Halls, Retracing Your Steps To The Camp To Then Follow the Map.");
            goodEnding.Enqueue("Eventually, After What Feels Like Hours, You Finally Make It To The Door.");
            goodEnding.Enqueue("Waiting For You There Are Ezekiel, Zorin, Amir, Rendfield And The Other’s, Joyous Smiles Forming On Their Faces At The Sight Of You Entering The Chamber.");
            goodEnding.Enqueue("Helping You To A Chair, They Sit You Down. Placing Your Bag On The Floor, You Remove The White Cloth, And Place It Down In Front Of You.");
            goodEnding.Enqueue("Placing All Four Pieces Together, They Mend Into One Large Gold Coin, Perfectly Identical To The Circular Hole In The Door.");
            goodEnding.Enqueue("Throwing An Arm Around Ezekiel’s Shoulder, He Helps You To Your Feet And Walks You Over To The Grand Exit.");
            goodEnding.Enqueue("Placing The Gold Disk Into The Cutout, You Turn It Until An Audible Click Is Echoed Back From The Other Side.");
            goodEnding.Enqueue("As The Doors Begin To Open, A Blinding Light Shines Through The Crack, Casting A Bright Ray Across The Room, Landing On Zorin’s Face. Looking Into Her Eyes You, You Can't Tell Which Is Prettier, The Illusive Rays Coming Through The Door, Or The Young Woman's Gleeful Eyes. As The Door  Fully Opens, You See a Tear Runs Down Her Cheek, Glistening As It Falls To The Floor.");
        }

        private static void FillBadEndingQueue()
        {
            badEnding.Enqueue("Having Grabbed The Artefact, You Make Your Way Out Of The Chamber And Into the Dark Corridors Of The Dungeon, Weapon Drawn Held At Your Side.");
            badEnding.Enqueue("As You Walk, Looking For The Door Zorin’s Dad Had Found, You Cut Down, Monsters Who Intersect Your Path, Unbothered By The Uncountable Injuries You Sustain.");
            badEnding.Enqueue("You Wander The Halls Of The Dungeon Aimlessly For Ages, Leaving Behind You A Trail Of Blood, Some Dripping From Your Wounds, Others From Your –Weapon Name.");
            badEnding.Enqueue("As You Run Out Of Supplies, You Leave Your Backpack, Migrating The Artefacts To Your Pocket.");
            badEnding.Enqueue("Finally, Upon Slaying A Gargoyle, You Enter Upon The Room You Seek.");
            badEnding.Enqueue("Walking Up To The Door, You Place Your Hand On It And Ponder A Moment.");
            badEnding.Enqueue("What Had You Come Here to Do? What Was It That You Really Sought To Do? You Had Collected These Keys, But for What?");
            badEnding.Enqueue("Because You were Asked To? Yes… But You Didn't Truly Care For Any Of This. The Camp, The People, The Righteous Goal. None Of It Really.");
            badEnding.Enqueue("Decisively, You Throw The Cloth Of Gold Pieces To The Floor In Front Of You Hitting Them With a Fireball, Then Another And Another, Until All That Is Left Is A Golden Puddle Of Molten Metal.");
            badEnding.Enqueue("You Look At Your Reflection In The Molten Puddle Before Leaving The Door. You Walk Back Into The Dungeon, Disappearing Once Again Into Its Dark Maze Of Corridors Because, Truly, You Are Just As Much Of A Monster As The Beasts Who Inhabit It.");
            badEnding.Enqueue("You Had Always Been. From The Very Start.");
            
        }
    }
}
