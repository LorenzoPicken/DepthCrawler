using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepthCrawlerGame.Ressources;
using DepthCrawlerGame.Items;
using DepthCrawlerGame.Entities;
using DepthCrawlerGame.Rooms;
using DepthCrawlerGame.Creators;

namespace DepthCrawlerGame
{
    class Program
    {
        
        static bool playerHasDied = false;
        static readonly Random random = new Random(Guid.NewGuid().GetHashCode());
        static bool spellWasCast = false;
        static bool playerSuccessfullyFled = false;

        


        static string ending = "";
        static void Main(string[] args)
        {

            DialogueManager.AssignDialogue();
            

            

            
            Queue<string> introDialogue = new Queue<string>();

            introDialogue.Enqueue("Welcome Back.");
            introDialogue.Enqueue("Although You May Not Remember, We Have Met Many Times Before.");
            introDialogue.Enqueue("But There Is No Place For Such Commodities Here.");
            introDialogue.Enqueue("It Is Time To Begin Your Tale.");
            introDialogue.Enqueue("A Story of Adventure, Renown, And Riches.");
            introDialogue.Enqueue("Or One Of Solitude, Tragedy, and Death.");
            introDialogue.Enqueue("Only You Can Truly Decide How This Tale Will End.");
            introDialogue.Enqueue("Shall We Begin? Destiny Awaits You.");
            




            


            while(true)
            {
                switch (GameManager.currentState)
                {
                    case ActionState.PLAYERSETUP:
                        
                        int count = 0;
                        while(count < 8)
                        {
                            Console.WriteLine(introDialogue.First());
                            introDialogue.Dequeue();
                            
                            Console.ReadKey();
                            Console.Clear();
                            count++;
                        }
                        Console.WriteLine("Pick Your Name");
                        GameManager.PlayerInstance.Name = Console.ReadLine();
                        introDialogue.Enqueue(GameManager.PlayerInstance.Name.ToUpper() + ". Yes, A Noble Name Indeed!");
                        introDialogue.Enqueue("But Without A Weapon, None May Survive In This World.");
                        introDialogue.Enqueue("A Weapon Is An Extension Of Ones Self. A Symbol Of Ones Pride, Conviction, And Wrath. Do Choose Wisely.");
                        introDialogue.Enqueue("An Excellent Choice! With This, I Will Send You On Your Way Now.");
                        introDialogue.Enqueue("Although Our Reunion Was Short, We Will Be Meeting Again Someday.");
                        introDialogue.Enqueue("For Your Sake, I Hope It Is Far From Now, But That Is For You To Insure.");
                        introDialogue.Enqueue("And, Before We Part, Some Words Of Advise...");
                        introDialogue.Enqueue("Your Decisions, No Matter How Insignificant You May Think They Are, Sway The World In Ways You Could Not Begin To Comprehend.");
                        introDialogue.Enqueue("The Time Has Come For You To Wake.");
                        introDialogue.Enqueue("Best Of Luck, " + GameManager.PlayerInstance.Name.ToUpper()+".");
                        Console.Clear();

                        while(count < 11)
                        {
                            Console.WriteLine(introDialogue.First());
                            introDialogue.Dequeue();

                            Console.ReadKey();
                            Console.Clear();
                            count++;
                        }


                        WeaponPicker();
                        Console.Clear();
                        while(introDialogue.Count > 0)
                        {
                            Console.WriteLine(introDialogue.First());
                            introDialogue.Dequeue();

                            Console.ReadKey();
                            Console.Clear();
                        }
                        CreateStartingRoom();
                        GameManager.currentState = ActionState.EXPLORING;
                        break;

                    case ActionState.EXPLORING:
                        GameManager.PlayerInstance.Rested();
                        while (true)
                        {
                            UpdateQuests();
                            bool executedSurpiseAttack = false;
                            Console.Clear();
                            string action;
                            DisplayPlayerStats();
                            Console.WriteLine(GameManager.currentRoom.Description);

                            if(GameManager.currentRoom.IsAltarRoom == true && GameManager.currentRoom.Enemy == null)
                            {
                                Console.Clear();
                                if (QuestManager.ezekielDead == true)
                                {
                                    while(DialogueManager.badEnding.Count > 0)
                                    {
                                        Console.WriteLine(DialogueManager.badEnding.First());
                                        Console.ReadKey();
                                        Console.Clear();
                                        DialogueManager.badEnding.Dequeue();
                                    }
                                    ending = "You Have Completed The PATH OF DECEIT Ending";
                                }
                                else
                                {
                                    while (DialogueManager.goodEnding.Count > 0)
                                    {
                                        Console.WriteLine(DialogueManager.goodEnding.First());
                                        Console.ReadKey();
                                        Console.Clear();
                                        DialogueManager.goodEnding.Dequeue();
                                    }
                                    ending = "You Have Completed The TEARS OF LIGHT Ending";
                                }
                                GameManager.currentState = ActionState.GAMEWIN;
                                break;
                            }
                            if (GameManager.currentRoom.IsScanned)
                            {
                                if (GameManager.currentRoom.Enemy != null && GameManager.currentRoom.Enemy.IsAware)
                                {
                                    
                                    GameManager.currentState = ActionState.BATTLING;
                                    break;
                                }
                            }

                            DisplayActions();

                            action = Console.ReadLine().ToLower();
                            action = TranslateAction(action);
                            Console.Clear();
                            bool enemyAware = false;
                            if (GameManager.currentRoom.IsScanned)
                            {
                                enemyAware = StealthCheck(GameManager.PlayerInstance, action);
                                if (enemyAware == true)
                                {
                                    GameManager.currentState = ActionState.BATTLING;
                                    break;
                                }

                                enemyAware = EnemyIsDistractedCheck(action);


                                if (enemyAware == true && action != "attack")
                                {
                                    GameManager.currentState = ActionState.BATTLING;
                                    break;
                                }
                            }



                            switch (action)
                            {
                                case "scan":
                                    Console.Clear();
                                    if (!(GameManager.currentRoom is QuestRoom))
                                    {
                                        ScanRoom();

                                    }
                                    else
                                    {
                                        Console.WriteLine("The Action You Sought To Perform Was Not In Accordance With Your Nature");
                                    }
                                    
                                    Console.ReadKey();
                                    
                                    break;

                                case "leave":
                                    if(GameManager.currentRoom.IsScanned == false)
                                    {
                                        Console.WriteLine("The Action You Sought To Perform Was Not In Accordance With Your Nature");
                                        Console.ReadKey();
                                        break;
                                    }
                                    Console.WriteLine("Looking Around The Room, You Identify Paths Leading Out Of The Chamber To The ");
                                    Console.WriteLine();
                                    if(GameManager.currentRoom.NorthConnectingRoom != null)
                                    {

                                        Console.WriteLine("NORTH");
                                        Console.WriteLine();
                                    }
                                    if (GameManager.currentRoom.SouthConnectingRoom != null)
                                    {
                                        Console.WriteLine("SOUTH");
                                        Console.WriteLine();
                                    }
                                    if (GameManager.currentRoom.EastConnectingRoom != null)
                                    {
                                        Console.WriteLine("EAST");
                                        Console.WriteLine();
                                    }
                                    if (GameManager.currentRoom.WestConnectingRoom != null)
                                    {
                                        Console.WriteLine("WEST");
                                        Console.WriteLine();
                                    }
                                    Console.WriteLine("You Decide To Leave By The ");
                                    action = Console.ReadLine().ToLower();
                                    switch (action)
                                    {
                                        case "north":
                                            if(GameManager.currentRoom.NorthConnectingRoom == null)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Your Eyes Decieve You As Upon Closer Look, No Exit Is Present, Or Has It Vanished? You Cannot Remember.");
                                                Console.ReadKey();
                                                break;
                                            }
                                            RoomCreator.exitDirection = Room.ConnectingDirections.NORTH;
                                            GameManager.currentRoom = RoomCreator.CreateNewRoom();
                                            
                                            
                                            
                                            break;

                                        case "south":
                                            if (GameManager.currentRoom.SouthConnectingRoom == null)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Your Eyes Decieve You As Upon Closer Look, No Exit Is Present, Or Has It Vanished? You Cannot Remember.");
                                                Console.ReadKey();
                                                break;
                                            }

                                            RoomCreator.exitDirection = Room.ConnectingDirections.SOUTH;
                                            GameManager.currentRoom = RoomCreator.CreateNewRoom();


                                            break;

                                        case "east":
                                            if (GameManager.currentRoom.EastConnectingRoom == null)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Your Eyes Decieve You As Upon Closer Look, No Exit Is Present, Or Has It Vanished? You Cannot Remember.");
                                                Console.ReadKey();
                                                break;
                                            }
                                            
                                            RoomCreator.exitDirection = Room.ConnectingDirections.EAST;
                                            GameManager.currentRoom = RoomCreator.CreateNewRoom();
                                            break;

                                        case "west":
                                            if (GameManager.currentRoom.WestConnectingRoom == null)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Your Eyes Decieve You As Upon Closer Look, No Exit Is Present, Or Has It Vanished? You Cannot Remember.");
                                                Console.ReadKey();
                                                break;
                                            }

                                            RoomCreator.exitDirection = Room.ConnectingDirections.WEST;
                                            GameManager.currentRoom = RoomCreator.CreateNewRoom();

                                            break;

                                        case "back":
                                            break;

                                        default:
                                            break;
                                    }

                                   


                                    break;

                                case "attack":

                                    if (GameManager.currentRoom.IsScanned == false || GameManager.currentRoom.Enemy == null)
                                    {
                                        Console.WriteLine("The Action You Sought To Perform Was Not In Accordance With Your Nature");
                                        Console.ReadKey();
                                        break;
                                    }
                                    SurpriseAttack(GameManager.PlayerInstance);
                                    executedSurpiseAttack = true;
                                    break;

                                case "approach":
                                    if(GameManager.currentRoom.Npcs == null)
                                    {
                                        Console.WriteLine("The Action You Sought To Perform Was Not In Accordance With Your Nature");
                                        Console.ReadKey();
                                        break;

                                    }
                                    else if(GameManager.currentRoom.Npcs.Count == 0)
                                    {
                                        Console.WriteLine("The Action You Sought To Perform Was Not In Accordance With Your Nature");
                                        Console.ReadKey();
                                        break;
                                    }
                                    TalkWithNPCS();
                                    Console.ReadKey();
                                    break;

                                    
                                    

                                case "search":
                                    if (GameManager.currentRoom.IsScanned == false || GameManager.currentRoom.Lootables == null)
                                    {
                                        Console.WriteLine("The Action You Sought To Perform Was Not In Accordance With Your Nature");
                                        Console.ReadKey();
                                        break;
                                    }
                                    else
                                    {

                                        SearchForLoot();
                                        
                                    }
                                    break;


                                case "inventory":
                                    EnterInventory();
                                    break;

                                case "journal":
                                    Console.WriteLine("You Speak The Command 'OPEN' To Your Journal. Its Pages Come To Life As The Book Floats From Your Hands, Hovering Before You.");
                                    Console.ReadKey();
                                    WriteInJournal();
                                    break;

                                 default:
                                    Console.WriteLine("The Action You Sought To Perform Was Not In Accordance With Your Nature");
                                    Console.ReadKey();
                                    break;

                            }
                            if(executedSurpiseAttack == true)
                            {
                                GameManager.currentState = ActionState.BATTLING;
                                break;
                            }
                            
                        }
                        break;

                    case ActionState.BATTLING:
                        if(GameManager.PlayerInstance.Fatigue >= 100)
                        {
                            GameManager.PlayerInstance.Fatigued();
                        }
                        GameManager.currentRoom.Enemy.IsAware = true;
                        playerSuccessfullyFled = false;
                        GameManager.PlayerInstance.IsPoisoned = false;
                        GameManager.currentRoom.Enemy.Target = GameManager.PlayerInstance;
                        GameManager.PlayerInstance.Target = GameManager.currentRoom.Enemy;
                        AIDecisionTree decisiontree = new AIDecisionTree();
                        Console.ReadKey();
                        while (GameManager.currentRoom.Enemy != null)
                        {
                            Console.Clear();
                            if(GameManager.currentRoom.Enemy == null || GameManager.PlayerInstance.Target == null)
                            {
                                break;
                            }
                            GameManager.currentRoom.Enemy.IsBlocking = false;
                            GameManager.PlayerInstance.IsStunned = false;
                            if (!GameManager.currentRoom.Enemy.IsStunned)
                            {
                                EnemyAttack(decisiontree, GameManager.currentRoom.Enemy.Type);
                                if(GameManager.PlayerInstance.HP <= 0)
                                {
                                    playerHasDied = true;
                                    break ;
                                }
                            }
                            if (GameManager.currentRoom.Enemy == null || GameManager.PlayerInstance.Target == null)
                            {
                                break;
                            }
                            GameManager.currentRoom.Enemy.IsStunned = false;
                            GameManager.PlayerInstance.IsBlocking = false;


                            if(GameManager.PlayerInstance.IsStunned != true)
                            {
                                PlayerBattleActions();
                                if (GameManager.PlayerInstance.HP <= 0)
                                {
                                    playerHasDied = true;
                                    break;
                                }

                            }
                            if(GameManager.PlayerInstance.IsPoisoned == true)
                            {
                                GameManager.PlayerInstance.HP -= 2;
                                Console.WriteLine("You Feel The Poison Burning In Your veins.");
                                Console.ReadKey();
                            }

                            

                            if(GameManager.PlayerInstance.Fatigue < 100)
                            {
                                GameManager.PlayerInstance.Fatigue++;

                            }
                            else
                            {
                                GameManager.PlayerInstance.Fatigued();
                            }
                            
                            
                        }
                        if(playerSuccessfullyFled == false && playerHasDied == false)
                        {
                            ScanRoom();

                        }
                        if(playerHasDied == true)
                        {
                            GameManager.currentState = ActionState.DYING;
                        }
                        else
                        {
                            GameManager.currentState = ActionState.EXPLORING;
                            GameManager.PlayerInstance.HasAttemptedToFlee = false;

                        }
                        break;



                    case ActionState.DYING:
                        Console.ReadKey();
                        Console.Clear();
                        GameOverScreen();


                        return;

                    case ActionState.GAMEWIN:
                        Console.Clear();
                        GameWinnerScreen(ending);


                        return;

                        
                }
            }


            

            

            
            
        }


        





        #region UI

        private static void DisplayPlayerStats()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Health: {GameManager.PlayerInstance.HP}/{GameManager.PlayerInstance.Maxhp}".PadRight(Console.WindowWidth - 20));
            Console.ResetColor();

            if (GameManager.PlayerInstance.Mana > 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue; 
                Console.Write($"Mana: {GameManager.PlayerInstance.Mana}/{GameManager.PlayerInstance.MaxMana}".PadLeft(20)); 
                Console.ResetColor(); 

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"Mana: {GameManager.PlayerInstance.Mana}/{GameManager.PlayerInstance.MaxMana}".PadLeft(20));
                Console.ResetColor();
            }

            if (GameManager.PlayerInstance.Fatigue < GameManager.PlayerInstance.MaxFatigue)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(0, Console.CursorTop + 1); 
                Console.Write($"Fatigue: {GameManager.PlayerInstance.Fatigue}/{GameManager.PlayerInstance.MaxFatigue}".PadLeft(Console.WindowWidth)); 
                Console.ResetColor(); 
            }

            else if (GameManager.PlayerInstance.Fatigue == GameManager.PlayerInstance.MaxFatigue)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(0, Console.CursorTop + 1); 
                Console.Write($"EXHAUSTED".PadLeft(Console.WindowWidth)); 
                Console.ResetColor();
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void DisplayActions()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Take Action!");
            Console.WriteLine();
            Console.WriteLine("-------------------------------");
            if (!(GameManager.currentRoom is QuestRoom))
            {
                Console.WriteLine("SCAN Surroundings");
                Console.WriteLine();

            }
            if (GameManager.currentRoom.Npcs != null)
            {
                if(GameManager.currentRoom.Npcs.Count != 0)
                {
                    Console.WriteLine("APPROACH Someone");
                    Console.WriteLine();

                }
            }
            if (GameManager.currentRoom.IsScanned == true)
            {
                Console.WriteLine("LEAVE Room");
                Console.WriteLine();
                if(GameManager.currentRoom.Enemy != null)
                {
                    Console.WriteLine("Surprise ATTACK");
                    Console.WriteLine();

                }
                if(GameManager.currentRoom.Lootables == null || GameManager.currentRoom.Lootables?.Count ==0)
                {
                }
                else
                {
                    Console.WriteLine("SEARCH For Valuables");
                    Console.WriteLine();

                }
                


            }
            Console.WriteLine("Check INVENTORY");
            Console.WriteLine();
            Console.WriteLine("Open JOURNAL");
            Console.WriteLine("-------------------------------");
        }

        private static void BattleUI()
        {
            DisplayPlayerStats();
            DisplayEnemyStats();
            DisplayBattleOptions();
        }

        private static void DisplayEnemyStats()
        {
            Console.WriteLine(GameManager.PlayerInstance.Target.Name + " Stands Before You. HEALTH: " + GameManager.PlayerInstance.Target.HP + " / " + GameManager.PlayerInstance.Target.Maxhp);
        }

        private static void GameOverScreen()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Clear();

            string gameOverText = "GAME OVER";
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;

            
            int centerX = (screenWidth - gameOverText.Length) / 2;
            int centerY = 3; // Three lines down from the top

            
            Console.SetCursorPosition(centerX, centerY);
            Console.WriteLine(gameOverText);

            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Player Name : " + GameManager.PlayerInstance.Name);
            Console.WriteLine();
            Console.WriteLine("Killed In Battle By: " + GameManager.currentRoom.Enemy.Name.ToUpper());
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Enemies Defeated: " + StatisticsTracker.EnemiesDefeated);
            Console.WriteLine();
            Console.WriteLine("Rooms Explored: " + StatisticsTracker.RoomsExplored);
            Console.WriteLine();
            Console.WriteLine("Items Looted: " + StatisticsTracker.ItemsLooted);
            Console.WriteLine();
            Console.WriteLine("Artefacts Collected: " + StatisticsTracker.ArtefactsCollected);
            Console.ReadKey();
        }

        private static void GameWinnerScreen(string ending)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();

            string gameOverText = "GAME WON";
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;


            int centerX = (screenWidth - gameOverText.Length) / 2;
            int centerY = 3; 


            Console.SetCursorPosition(centerX, centerY);
            Console.WriteLine(gameOverText);

            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Player Name : " + GameManager.PlayerInstance.Name);
            Console.WriteLine();
            Console.WriteLine("Enemies Defeated: " + StatisticsTracker.EnemiesDefeated);
            Console.WriteLine();
            Console.WriteLine("Rooms Explored: " + StatisticsTracker.RoomsExplored);
            Console.WriteLine();
            Console.WriteLine("Items Looted: " + StatisticsTracker.ItemsLooted);
            Console.WriteLine();
            Console.WriteLine("Artefacts Collected: " + StatisticsTracker.ArtefactsCollected);
            Console.ReadKey();
            Console.Clear();
            centerX = (screenWidth - ending.Length) / 2;
            centerY = 3;
            Console.SetCursorPosition(centerX, centerY);
            Console.WriteLine(ending);
            Console.WriteLine();
            Console.WriteLine("Thank You For Playing");
            Console.ReadKey();
        }

        private static void DisplayBattleOptions()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Take Action!");
            Console.WriteLine();
            Console.WriteLine("-------------------------------");
            Console.WriteLine("ATTACK " + GameManager.PlayerInstance.Target.Name);
            Console.WriteLine("Lauch An Attack On Your Opponent.");
            Console.WriteLine();
            Console.WriteLine("Cast SPELL");
            Console.WriteLine("Use Mana To Cast A Spell.");
            Console.WriteLine();
            Console.WriteLine("Raise GUARD");
            Console.WriteLine("Prepare For An Attack From Your Opponent.");
            Console.WriteLine();
            if(GameManager.PlayerInstance.HasAttemptedToFlee != true)
            {
                Console.WriteLine("Attempt ESCAPE");
                Console.WriteLine("Attempt To Flee The Current Battle.");
                Console.WriteLine();

            }
            Console.WriteLine("Open INVENTORY");
            Console.WriteLine("Access You Inventory To Use Items Or Change Weapons");
            Console.WriteLine();
            
            Console.WriteLine("-------------------------------");
        }

        #endregion

        #region Game Setup

        private static void CreateStartingRoom()
        {
            Room startingRoom = new Room(new Room(null, null, null, null, null, null, null), null, null, null, null, null, null);
            GameManager.currentRoom = startingRoom;
            startingRoom.Description = "The cries of bats overhead startle you awake, " +
                "as they swarm and fly out of the room, through an archway to the south. As they fly, torches are extinguished, embers still lightly glowing. " +
                "Before you are plunged into darkness, you make out a few of the chamber's features. The ceiling is low, with a dome like rise at its centre. " +
                "Four large pillars erupt from the stone floor, connecting to the extremities of the dome around the room's middle. " +
                "The floor is dry and cracked, dust scattering across the cobble tiles, lifted by the gusts produced by a dozen leathery wings. " +
                "As your eyes adjust to the dark, a hugh is visible to the south of the room through an arched doorway.";
            startingRoom.Id = 999999999;

            

        }
        private static void WeaponPicker()
        {
            bool looper = true;
            while (looper == true)
            {
                
                Console.WriteLine("Shortsword:");
                Console.WriteLine("A Well Rounded Option. Sacrifices Destructive Power For Passible Defense and Good Handling");
                Console.WriteLine();

                Console.WriteLine("Longsword:");
                Console.WriteLine("A Heavy Slashing Weapon With Average Defense But Sluggish Handling");
                Console.WriteLine();

                Console.WriteLine("Dagger:");
                Console.WriteLine("A Light, Agile Weapon. Ideal For Delivering Rapid Flurries of Pucture Wounds");
                Console.WriteLine();

                Console.WriteLine("Great Axe:");
                Console.WriteLine("A Large Two Handed Axe With Devastating Attack Potency, Sluggish Handling and Poor Defensive Abilities");
                Console.WriteLine();

                Console.WriteLine("Battle Hammer:");
                Console.WriteLine("A Heavy Blunt Weapon With Good Defense and Above Average Damage But Long Recovery");
                Console.WriteLine();

                Console.WriteLine("Flail:");
                Console.WriteLine("A LightWeight Blunt Weapon That Trades Defense For Solid Destructive Power");
                Console.WriteLine();
                
                string weapon = Console.ReadLine();
                Console.Clear();
                switch (weapon.ToLower())
                {
                    case "shortsword":
                        Weapon shortSword = new Weapon("Bronze ShortSword", false,false, 4, 3, 4, Weapon.WeaponType.SHORTSWORD, DamageTypes.damageType.SLASHING);
                        
                        GameManager.PlayerInstance.HolsterInventory.Add(shortSword);
                        GameManager.PlayerInstance.CurrentEquippedWeapon = shortSword;
                        looper = false;
                        
                        
                        break;

                    case "longsword":

                        Weapon longSword = new Weapon("LongSword", true,false, 7, 5, 5, Weapon.WeaponType.LONGSWORD, DamageTypes.damageType.SLASHING);

                        GameManager.PlayerInstance.HolsterInventory.Add(longSword);
                        GameManager.PlayerInstance.CurrentEquippedWeapon = longSword;
                        looper = false;
                        

                        break;

                    case "dagger":

                        Weapon dagger = new Weapon("Highlander's Parry Dagger", false,false, 3, 1, 1, Weapon.WeaponType.DAGGER, DamageTypes.damageType.PIERCING);

                        GameManager.PlayerInstance.HolsterInventory.Add(dagger);
                        GameManager.PlayerInstance.CurrentEquippedWeapon = dagger;
                        looper = false;
                        

                        break;


                    case "flail":

                        Weapon flail = new Weapon("Flail", false,false, 6, 3, 1, Weapon.WeaponType.FLAIL, DamageTypes.damageType.BLUNT);

                        GameManager.PlayerInstance.HolsterInventory.Add(flail);
                        GameManager.PlayerInstance.CurrentEquippedWeapon = flail;
                        looper = false;
                        

                        break;


                    case "great axe":

                        Weapon axe = new Weapon("Barbarian GreatAxe", true,false, 8, 5, 3, Weapon.WeaponType.AXE, DamageTypes.damageType.SLASHING);

                        GameManager.PlayerInstance.HolsterInventory.Add(axe);
                        GameManager.PlayerInstance.CurrentEquippedWeapon = axe;
                        looper = false;
                        

                        break;


                    case "battle hammer":

                        Weapon hammer = new Weapon("Stone Battle Hammer", true,false, 9, 7, 5, Weapon.WeaponType.HAMMER, DamageTypes.damageType.BLUNT);

                        GameManager.PlayerInstance.HolsterInventory.Add(hammer);
                        GameManager.PlayerInstance.CurrentEquippedWeapon = hammer;
                        looper = false;
                        

                        break;


                    default:
                        Console.Clear();
                        Console.WriteLine("You Must Choose From On Of These Great Armaments! No Others Will Do.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }


        #endregion

        #region Actions
        private static string TranslateAction(string action)
        {
            if (action == "scan surroundings")
            {
                return action = "scan";
            }
            else if (action == "leave room")
            {
                return action = "leave";
            }
            else if ( action == "surprise attack")
            {
                return action  = "attack";
            }
            else if(action == "search for valuables")
            {
                return action  = "search";
            }
            else if (action == "check inventory")
            {
                return action  = "inventory";
            }
            else if(action == "open journal")
            {
                return action = "journal";
            }
            else if (action == "approach someone")
            {
                return action = "approach";
            }
            return action;
        }
        private static void ScanRoom()
        {
            Console.WriteLine("You Look Around The Room Intently, Familiarising Yourself With What Is Visible To You");
            string lootablesDescrip = string.Empty;
            GameManager.currentRoom.IsScanned = true;
            if(GameManager.currentRoom.Lootables != null)
            {
                if(GameManager.currentRoom.Lootables.Count > 0)
                {
                    lootablesDescrip = "Scattered Around The Room Is Furnature and Other Objects. ";
                }

            }
            List<Room> exitList = new List<Room>();
            exitList.Add(GameManager.currentRoom.NorthConnectingRoom);
            exitList.Add(GameManager.currentRoom.SouthConnectingRoom);
            exitList.Add(GameManager.currentRoom.WestConnectingRoom);
            exitList.Add(GameManager.currentRoom.EastConnectingRoom);
            string numExits = string.Empty;
            int count = 4;
            for (int i = 0; i < exitList.Count; )
            {
                if(exitList[i] == null)
                {
                    exitList.Remove(exitList[i]);
                    count--;
                }
                else
                {
                    i++;
                }
            }
            
            
            if(count == 1)
            {
                numExits = "One Exit Leads Out Of The Chamber";
            }
            else if(count == 2)
            {
                numExits = "Two Exits Lead Out Of The Chamber";
            }
            else if(count == 3)
            {
                numExits = "Three Exits Lead Out Of The Chamber";
            }
            else
            {
                numExits = "Four Exits Lead Out Of The Chamber";
            }
                
            
            string enemyDescript = string.Empty;
            if(GameManager.currentRoom.Enemy != null)
            {
                enemyDescript = "You Are Faced With A " + GameManager.currentRoom.Enemy.Name +". " + GameManager.currentRoom.Enemy.Description + " ";
            }
            else
            {
                enemyDescript = RoomCreator.originalRoomdescript;
            }
            
            GameManager.currentRoom.Description = enemyDescript + lootablesDescrip +  numExits;
        }

        private  static void SurpriseAttack(Player player)
        {
            Console.WriteLine("Unnoticed By The Creature, You Charge It, Landing A Powerful Blow.");

            if (GameManager.currentRoom.Enemy.Type == EnemySpecification.SPIDER)
            {
                Console.WriteLine("Off Guard, The Spider Shoots A Web At You, Which You Avoid.");
                GameManager.currentRoom.Enemy.TakeDamage(player.CurrentStrength);
                GameManager.currentRoom.Enemy.IsSleeping = false;
                GameManager.currentRoom.Enemy.IsAware = true;


            }
            else if (GameManager.currentRoom.Enemy.Type == EnemySpecification.GARGOYLE)
            {
                Console.WriteLine("Having Been Knocked To The Floor, The Gargoyle Insteadily Takes Flight.");
                GameManager.currentRoom.Enemy.TakeDamage(player.CurrentStrength);
                GameManager.currentRoom.Enemy.IsSleeping = false;
                GameManager.currentRoom.Enemy.IsAware = true;
            }
            else if (GameManager.currentRoom.Enemy.Type == EnemySpecification.KNIGHT)
            {
                Console.WriteLine("Slashing Its Sword In Your Direction, The Knight Forces You Back, Creating Space.");
                GameManager.currentRoom.Enemy.TakeDamage(player.CurrentStrength);
                GameManager.currentRoom.Enemy.IsSleeping = false;
                GameManager.currentRoom.Enemy.IsAware = true;
            }
            else if (GameManager.currentRoom.Enemy.Type == EnemySpecification.BONEDRIKE)
            {
                Console.WriteLine("The Beast Quickly Slithers In The Opposite Direction From You, A Bit Confused From The Trauma");
                GameManager.currentRoom.Enemy.TakeDamage(player.CurrentStrength);
                GameManager.currentRoom.Enemy.IsSleeping = false;
                GameManager.currentRoom.Enemy.IsAware = true;
            }
            else if (GameManager.currentRoom.Enemy.Type == EnemySpecification.MINOTAUR)
            {
                Console.WriteLine("As It Swings Wildly, You Roll Out Of The Way Of The Monsters Axe, The Beast Roaring In Pain.");
                GameManager.currentRoom.Enemy.TakeDamage(player.CurrentStrength);
                GameManager.currentRoom.Enemy.IsSleeping = false;
                GameManager.currentRoom.Enemy.IsAware = true;
            }
            else if (GameManager.currentRoom.Enemy.Type == EnemySpecification.EYE)
            {
                Console.WriteLine("Retreating, The Creature Holds Its Injury.");
                GameManager.currentRoom.Enemy.TakeDamage(player.CurrentStrength);
                GameManager.currentRoom.Enemy.IsSleeping = false;
                GameManager.currentRoom.Enemy.IsAware = true;
            }
        }
        private static void WriteInJournal()
        {
            
            bool isWritting = true;

            while (isWritting == true)
            {
                int line = 0;
                Console.Clear();
                foreach(string note in GameManager.PlayerInstance.JournalContents)
                {
                    line++;
                    Console.WriteLine(line + ".   " +note);
                }
                Console.WriteLine();
                Console.WriteLine("--------------------------------------");
                Console.WriteLine();
                Console.WriteLine("INSTRUCTIONS");
                Console.WriteLine("Speak The Following Commands To Use This Journal:");
                Console.WriteLine();
                Console.WriteLine("ERASE......Used To Erase The Last Line From The Journal");
                Console.WriteLine("REMOVE.....Used To Erase A Line From The Journal");
                Console.WriteLine("EMPTY......Used To Clear All Pages Of The Journal");
                Console.WriteLine("CLOSE......Causes The Journal To Go Dormant");
                Console.WriteLine("All Other Commands Will Be Inscribed In Its Pages");
                Console.WriteLine();
                string command = Console.ReadLine().ToLower();
                switch (command)
                {
                    case "empty":
                        
                        GameManager.PlayerInstance.JournalContents.Clear();
                        break;

                    case "close":
                        Console.Clear();
                        Console.WriteLine("The Journal Closes Itself And Floats Back Into Your Hands. You Secure It To Your Belt.");
                        Console.ReadKey();
                        return;

                    case "":
                        Console.Clear();
                        Console.WriteLine("The Journal Closes Itself And Floats Back Into Your Hands. You Secure It To Your Belt.");
                        Console.ReadKey();
                        return;

                    case "erase":
                        if(GameManager.PlayerInstance.JournalContents.Count != 0)
                        {
                            GameManager.PlayerInstance.JournalContents.Remove(GameManager.PlayerInstance.JournalContents.Last());

                        }
                        break;

                    case "remove":
                        
                        Console.WriteLine("Invoke The Number Of The Line You Wish To Erase");
                        int lineNum =0;
                        if(!int.TryParse(Console.ReadLine(), out lineNum) || lineNum > GameManager.PlayerInstance.JournalContents.Count || lineNum < 1)
                        {
                            Console.Clear();
                            Console.WriteLine("You Speak The Command But The Journal Does Not React. How Did The Chant Go Again?");
                            Console.ReadKey();
                        }
                        else
                        {
                            GameManager.PlayerInstance.JournalContents.RemoveAt(lineNum - 1);
                        }
                        break;

                    default:
                        GameManager.PlayerInstance.JournalContents.Add(command.ToUpper());
                        break;
                }


            }
        }
        private static void EnterInventory()
        {
            Console.WriteLine("You Remove Your Backpack And Place It Down In Front Of You, Unbuttoning The Leather Strap That Holds It Shut");
            Console.ReadKey();
            bool isInInventory = true;

            while(isInInventory == true)
            {
                Console.Clear();
                Console.WriteLine("You Backpack Sits In Front Of You");
                Console.WriteLine();
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Draw Weapon From HOLSTER");
                Console.WriteLine();
                Console.WriteLine("Retrieve Item From BACKPACK");
                Console.WriteLine();
                Console.WriteLine("CLOSE Backpack");
                Console.WriteLine("-----------------------------");
                string command = Console.ReadLine().ToLower();
                switch (command)
                {
                    case "close":

                        Console.Clear();
                        Console.WriteLine("Closing Your Backpack, You Throw It Back Over Your Shoulders.");
                        Console.ReadKey();
                        isInInventory = false;
                        return;

                    case "backpack":
                        Console.Clear();
                        Console.WriteLine("BAKCPACK CONTENTS");
                        Console.WriteLine("----------------------------");
                        Console.WriteLine();
                        if(GameManager.PlayerInstance.ConsumableInventory.Count == 0)
                        {
                            Console.WriteLine("Your Backpack Is Empty");
                            Console.WriteLine();
                        }
                        foreach (KeyValuePair<Item, int> consumable in GameManager.PlayerInstance.ConsumableInventory)
                        {
                            Console.WriteLine(consumable.Key.Name + " x" + consumable.Value);
                            Console.WriteLine();
                        }
                        Console.WriteLine("BACK");
                        Console.WriteLine();
                        Console.WriteLine("----------------------------");
                        string consumableChoice = Console.ReadLine().ToLower();
                        if (consumableChoice == "back" || consumableChoice == "")
                        {
                            break;
                        }
                        int count = 0;
                        bool consumed = false;
                        foreach (KeyValuePair<Item, int> consumable in GameManager.PlayerInstance.ConsumableInventory)
                        {
                            
                            if(consumableChoice == consumable.Key.Name.ToLower())
                            {
                                Console.Clear();
                                Console.WriteLine("Would You Like To Consume 1 " + consumable.Key.Name.ToUpper() + "?");
                                Console.WriteLine();
                                Console.WriteLine("--------------------");
                                Console.WriteLine("CONSUME");
                                Console.WriteLine();
                                Console.WriteLine("CANCEL");
                                Console.WriteLine("--------------------");
                                Console.WriteLine();
                                string consume = Console.ReadLine().ToLower();

                                switch (consume)
                                {
                                    case "consume":
                                        consumed = true;
                                        consumable.Key.Consume(GameManager.PlayerInstance);
                                        if (consumable.Value == 1)
                                        {
                                            GameManager.PlayerInstance.ConsumableInventory.Remove(consumable.Key);
                                            Console.Clear();
                                            Console.WriteLine("You Use Your Last " + consumable.Key.Name + ". ");
                                            Console.ReadKey();

                                        }
                                        else
                                        {
                                            if (GameManager.PlayerInstance.ConsumableInventory.ContainsKey(consumable.Key))
                                            {
                                                GameManager.PlayerInstance.ConsumableInventory.Remove(consumable.Key);
                                                GameManager.PlayerInstance.ConsumableInventory.Add(consumable.Key, consumable.Value - 1);
                                                
                                            }
                                            Console.Clear();
                                            Console.WriteLine("You Use One Of Your " + consumable.Key.Name + "s.");
                                            Console.ReadKey();

                                        }
                                     break;

                                    case "cancel":
                                        Console.Clear();
                                        Console.WriteLine("You Place The " + consumable.Key.Name + " Back Into the Bag.");
                                        Console.ReadKey();
                                        break;

                                    default:
                                        Console.Clear();
                                        Console.WriteLine("What Had You wanted To Do With This Item? You Cannot Remember.");
                                        Console.ReadKey();
                                        break;
                                }

                                
                                
                            }
                            
                            else
                            {
                                count++;
                            }
                            if(consumed == true)
                            {
                                break;
                            }

                            
                        }
                        if(count == GameManager.PlayerInstance.ConsumableInventory.Count && consumed == false)
                        {
                            Console.Clear();
                            Console.WriteLine("You Search Your Backpack But Cant Find An Items Matching What You Are Looking For.");
                            Console.ReadKey();
                        }
                        break;

                    case "holster":
                        Console.Clear();
                        Console.WriteLine("HOLSTER CONTENTS");
                        Console.WriteLine("----------------------------");
                        Console.WriteLine();
                        foreach(Weapon weapon in GameManager.PlayerInstance.HolsterInventory)
                        {
                            Console.WriteLine(weapon.Name);
                            Console.WriteLine();
                        }
                        Console.WriteLine("BACK");
                        Console.WriteLine();
                        Console.WriteLine("----------------------------");
                        string weaponChoice = Console.ReadLine().ToLower();
                        if (weaponChoice == "back" || weaponChoice == "")
                        {
                            break;
                        }
                        int count2 = 0;
                        foreach(Weapon weapon in GameManager.PlayerInstance.HolsterInventory)
                        {
                            if(weapon.Name.ToLower() == weaponChoice && weapon.Name != null)
                            {
                                Console.Clear();
                                Console.WriteLine("Would You Like To Equip Your " + weapon.Name.ToUpper() + "?");
                                Console.WriteLine();
                                Console.WriteLine("--------------------");
                                Console.WriteLine("EQUIP");
                                Console.WriteLine("PUT AWAY");
                                Console.WriteLine("--------------------");
                                Console.WriteLine();
                                string confirm = Console.ReadLine().ToLower();

                                switch (confirm)
                                {
                                    case "equip":
                                        if (weapon.IsTwoHanded)
                                        {
                                            GameManager.PlayerInstance.CurrentEquippedWeapon = weapon;
                                            GameManager.PlayerInstance.CurrentSecondaryWeapon = null;
                                            Console.Clear();
                                            Console.WriteLine("Reaching Into The Bottomless Holster, You Draw You " + weapon.Name + ".");
                                            Console.ReadKey();
                                            GameManager.PlayerInstance.ChangeWeapon();
                                        }
                                        else if(weapon.IsSecondary)
                                        {
                                            if(GameManager.PlayerInstance.CurrentEquippedWeapon.IsTwoHanded == true)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Cannot Equip While Primary Weapon Is A Two Handed Weapon");
                                                Console.ReadKey();
                                                break;
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                GameManager.PlayerInstance.CurrentSecondaryWeapon = weapon;
                                                Console.WriteLine("Reaching Into The Bottomless Holster, You Draw You " + weapon.Name + ".");
                                                Console.ReadKey();
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            GameManager.PlayerInstance.CurrentEquippedWeapon = weapon;
                                            Console.WriteLine("Reaching Into The Bottomless Holster, You Draw You " + weapon.Name + ".");
                                            Console.ReadKey();
                                            break;
                                        }
                                        break;

                                    case "put away":
                                        Console.Clear();
                                        Console.WriteLine("You Confirm That You Still Have Your " + weapon.Name + " Before Putting It Back Into The Holster.");
                                        Console.ReadKey();
                                        break;

                                    default:
                                        Console.Clear();
                                        Console.WriteLine("Confused For A Moment, You Arent Quite Sure What To Do With The Weapon.");
                                        Console.ReadKey();
                                        break;
                                       
                                }
                                break;
                            }
                            else
                            {
                                count2++;
                            }
                        }
                        if(count2 == GameManager.PlayerInstance.HolsterInventory.Count)
                        {
                            Console.Clear();
                            Console.WriteLine("You Rummage Around For A Bit But Cant Seem To Find What You Were Looking For");
                            Console.ReadKey();
                        }
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("You Pat Yourself Down But Can Find What You Wanted To Search");
                        Console.ReadKey();
                        break;
                }

            }
        }
        private static void SearchForLoot()
        {
            
            while (GameManager.currentRoom.Lootables.Count > 0)
            {
                List<string> lootableNamesList = new List<string>();
                Console.Clear();
                Console.WriteLine("Looking Around The Room, You See...");
                Console.WriteLine();
                Console.WriteLine("---------------------------");
                int itemIndex = 1;
                for (int i =0; i < GameManager.currentRoom.Lootables.Count; i++)
                {
                   
                    if (lootableNamesList.Contains(GameManager.currentRoom.Lootables[i].Name))
                    {
                        Console.WriteLine($"{itemIndex}. Another {GameManager.currentRoom.Lootables[i].Name.ToUpper()}");
                    }
                    else
                    {
                        Console.WriteLine($"{itemIndex}. {GameManager.currentRoom.Lootables[i].Name.ToUpper()}");
                        lootableNamesList.Add(GameManager.currentRoom.Lootables[i].Name);
                    }
                    Console.WriteLine();
                    itemIndex++;
                }
                Console.WriteLine((GameManager.currentRoom.Lootables.Count + 1) + ". BACK");
                Console.WriteLine("---------------------------");
                Console.WriteLine();
                Console.WriteLine("You Choose To Search Item Number:");
                if(int.TryParse(Console.ReadLine(), out int choice) && choice > 0)
                {
                    if(choice> GameManager.currentRoom.Lootables.Count + 1)
                    {
                        Console.Clear();
                        Console.WriteLine("No Item In The Room Matches The Description You are Looking For.");
                        Console.ReadKey();
                    }
                    else if(choice > GameManager.currentRoom.Lootables.Count)
                    {
                        Console.Clear();
                        Console.WriteLine("You Decide That Now Might Not Be The Best Time to Loot");
                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        choice--;
                        Console.Clear();
                        if(GameManager.currentRoom.Lootables[choice].ContainedLoot == null)
                        {
                            Console.WriteLine("You search The " + GameManager.currentRoom.Lootables[choice].Name.ToUpper() + " But Find Nothing.");
                            GameManager.currentRoom.Lootables.RemoveAt(choice);
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("You search The " + GameManager.currentRoom.Lootables[choice].Name.ToUpper()+ ". Rummaging Around, You Find:");
                            Console.WriteLine();
                            Console.WriteLine("A " + GameManager.currentRoom.Lootables[choice].ContainedLoot.Name.ToUpper());
                            StatisticsTracker.ItemsLooted++;
                            int count = 0;
                            foreach(KeyValuePair<Item, int> item in GameManager.PlayerInstance.ConsumableInventory)
                            {
                                if(item.Key.Name == GameManager.currentRoom.Lootables[choice].ContainedLoot.Name)
                                {
                                    int amount = item.Value + 1;
                                    GameManager.PlayerInstance.ConsumableInventory.Remove(item.Key);
                                    GameManager.PlayerInstance.ConsumableInventory.Add(GameManager.currentRoom.Lootables[choice].ContainedLoot, amount);
                                    Console.WriteLine();
                                    Console.WriteLine("You Add It To Your Backpack.");
                                    Console.ReadKey();
                                    break;
                                }
                                else
                                {
                                    count++;
                                }
                            }
                            if (count == GameManager.PlayerInstance.ConsumableInventory.Count)
                            {
                                GameManager.PlayerInstance.ConsumableInventory.Add(GameManager.currentRoom.Lootables[choice].ContainedLoot, 1);
                                Console.WriteLine();
                                Console.WriteLine("You Add It To Your Backpack.");
                                Console.ReadKey();


                            }
                            GameManager.currentRoom.Lootables.RemoveAt(choice);



                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Not Item In The Room Matches The Description You are Looking For.");
                    Console.ReadKey();
                }
                    


            }

        }

        private static void TalkWithNPCS()
        {
            while (true)
            {
                if (GameManager.currentRoom.Npcs.Count == 1)
                {
                    GameManager.currentRoom.Npcs[0].Talk();
                    break;
                }
                Console.Clear();
                Console.WriteLine("You Decide To Approach...");
                Console.WriteLine();
                Console.WriteLine("---------------------");
                foreach(NPC npc in GameManager.currentRoom.Npcs)
                {
                    Console.WriteLine(npc.Name.ToUpper());
                    Console.WriteLine();
                }
                Console.WriteLine("BACK");
                Console.WriteLine();
                Console.WriteLine("---------------------");
                Console.WriteLine();
                string choice = Console.ReadLine();
                Console.Clear();
                int count = 0;
                
                foreach(NPC npc in GameManager.currentRoom.Npcs)
                {
                    if(npc.Name.ToLower() == choice)
                    {
                        npc.Talk();
                        break;
                        
                    }
                    else if(choice == "back")
                    {
                        Console.WriteLine("You Decide That You Don't Really want To Talk Right Now.");
                        
                        return;
                    }
                    else
                    {
                        count++;
                    }
                }
                if(count == GameManager.currentRoom.Npcs.Count)
                {
                    Console.WriteLine("You Look Around But Cant Find Anyone Who Matches That Description.");
                    Console.ReadKey();
                }

            }
        }

        private static bool EnemyIsDistractedCheck(string action)
        {
            if (GameManager.currentRoom.Enemy != null && GameManager.currentRoom.Enemy.IsSleeping == false && GameManager.currentRoom.Enemy.IsAware == false)
            {
                if (action == "leave" || action == "search")
                {
                    

                    Console.WriteLine("As You Attempt To " + action.ToUpper() + " The " + GameManager.currentRoom.Enemy.Name + " Notices You.");

                    if(GameManager.currentRoom.Enemy.Type == EnemySpecification.SPIDER)
                    {
                        Console.WriteLine("Turning Itself To You, The Spider Lets Out A Loud Wail, Saliva And Poison Covering The Floor In Front Of It.");
                    }
                    else if (GameManager.currentRoom.Enemy.Type == EnemySpecification.GARGOYLE)
                    {
                        Console.WriteLine("The Gargoyle Drops From Its Perch And Takes Flight.");
                    }
                    else if (GameManager.currentRoom.Enemy.Type == EnemySpecification.KNIGHT)
                    {
                        Console.WriteLine("Stepping Towards You, The Knight Draws Its Sword, The Sound Of The Blade Being Unsheathed, Echoing Off The Room Walls.");
                    }
                    else if (GameManager.currentRoom.Enemy.Type == EnemySpecification.BONEDRIKE)
                    {
                        Console.WriteLine("Coiling Itself Like A Snake, It Stares At You, Its Head Upsidedown, Before, Rotating It.");
                    }
                    else if (GameManager.currentRoom.Enemy.Type == EnemySpecification.MINOTAUR)
                    {
                        Console.WriteLine("Standing Up, The Minotaur Roars, Banging Its Chest With A Closed Fist.");
                    }
                    else if (GameManager.currentRoom.Enemy.Type == EnemySpecification.EYE)
                    {
                        Console.WriteLine("Turning Around, Its Large Pupil Fixates On You, Starring You Down.");
                    }

                    
                }
                return true;
            }
            return false;
        }

        private static bool StealthCheck(Player player, string action)
        {
            if (GameManager.currentRoom.Enemy != null && GameManager.currentRoom.Enemy.IsSleeping == true)
            {
                
                if (action == "leave" || action == "search")
                {
                    int stealth = random.Next(1, 101);

                    if (stealth > 0 && stealth <= player.Stealth)
                    {
                        Console.WriteLine("The Monster Does Not Wake As You " + action.ToUpper() + ".");
                        Console.ReadKey();
                        Console.Clear();
                        return false;
                    }
                    else
                    {
                        GameManager.currentRoom.Enemy.IsSleeping = false;
                        GameManager.currentRoom.Enemy.IsAware = true;
                        Console.WriteLine("As You Attempt To " + action.ToUpper() + " The " + GameManager.currentRoom.Enemy.Name + " Wakes.");

                        if (GameManager.currentRoom.Enemy.Type == EnemySpecification.SPIDER)
                        {
                            Console.WriteLine("Lowering Itself To The Floor, The Spider Blocks Your Path.");
                        }
                        else if (GameManager.currentRoom.Enemy.Type == EnemySpecification.GARGOYLE)
                        {
                            Console.WriteLine("Removing Its Wings From Its Face, It Spots You, Screatching And Taking Flight.");
                        }
                        else if (GameManager.currentRoom.Enemy.Type == EnemySpecification.KNIGHT)
                        {
                            Console.WriteLine("Hand On Its Scabard, The Knight, Gets To Its Feet, Drawing Its Weapon.");
                        }
                        else if (GameManager.currentRoom.Enemy.Type == EnemySpecification.BONEDRIKE)
                        {
                            Console.WriteLine("Lifting Itself Back Up To The Ceiling, It Crawls Across The Roof And Down A Wall, Taking Position In The Middle Of The Chamber.");
                        }
                        else if (GameManager.currentRoom.Enemy.Type == EnemySpecification.MINOTAUR)
                        {
                            Console.WriteLine("Groagy, The Creature Grunts And Clears Its Throat. Helping Itself Get Up, It Grabs The Pillar, Which Cracks Under The Beasts Grip.");
                        }
                        else if (GameManager.currentRoom.Enemy.Type == EnemySpecification.EYE)
                        {
                            Console.WriteLine("Its Pupil, Shrinking, It Begins To Stare At You, Watching Your Movements");
                        }

                        return true;
                    }
                }
            }
            return false;
        }


        #endregion



        #region Battle Functions

        private static string TranslateBattleAction(string action)
        {
            if (action == "raise guard")
            {
                return action = "guard";
            }
            else if (action == "cast spell")
            {
                return action = "spell";
            }
            else if (action == "attempt escape")
            {
                return action = "escape";
            }
            else if (action == "open inventory")
            {
                return action = "inventory";
            }
            
            return action;
        }

        private static void PlayerBattleActions()
        {
            bool isPlayersTurn = true;
            while (isPlayersTurn)
            {
                spellWasCast = false;
                Console.Clear();
                BattleUI();

                string action = Console.ReadLine();

                action = TranslateBattleAction(action);

                switch(action.ToLower())
                {
                    case "attack":
                        Console.Clear();
                        PlayerAttack(GameManager.PlayerInstance);
                        isPlayersTurn = false;
                        break;

                    case "guard":
                        Console.Clear();
                        PlayerGuard(GameManager.PlayerInstance);
                        Console.ReadKey();
                        isPlayersTurn = false;
                        break;


                    case "spell":
                        Console.Clear();
                        PlayerCastSpell(GameManager.PlayerInstance);
                        if(spellWasCast == true)
                        {
                            isPlayersTurn = false;

                        }
                        break;

                    case "escape":
                        Console.Clear();
                        if(GameManager.PlayerInstance.HasAttemptedToFlee == false && GameManager.currentRoom.Enemy.Type != EnemySpecification.DRAGON)
                        {
                            PlayerEscape(GameManager.PlayerInstance);
                            isPlayersTurn = false;
                        
                        }
                        if(GameManager.currentRoom.Enemy.Type == EnemySpecification.DRAGON)
                        {
                            Console.Clear();
                            Console.WriteLine("You Need That Artefact. Fleeing Is Not An Option In This Fight.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("The Action That You Were About To Chose No Longer Seems Ideal In The Current Situation.");
                            Console.ReadKey();
                        }
                        break;

                    case "inventory":
                        Console.Clear();
                        spellWasCast = BattleInventory(GameManager.PlayerInstance);
                        if (spellWasCast == true)
                        {
                            isPlayersTurn = false;

                        }
                        
                        break;



                    default:
                        Console.Clear();
                        Console.WriteLine("The Action That You Were About To Chose No Longer Seems Ideal In The Current Situation.");
                        Console.ReadKey();
                        break;
                }
            }
        }



        private static void PlayerAttack(Player player)
        {
            string avoidAttack = "";

            string counterAttack = "";
            string failedBlock = "";
            string clash = "";

            string headDamage = "";
            string bodyDamage = "";
            string limbDamage = "";


            AssignEnemyReacions(player, ref avoidAttack, ref counterAttack, ref failedBlock, ref clash, ref headDamage, ref bodyDamage, ref limbDamage);
            
            int playerAgility = random.Next(1, player.CurrentAgility);
            int enemyAgility = random.Next(1, player.Target.Agility);

            if (playerAgility < enemyAgility && player.Target.IsBlocking == false)
            {
                Console.Clear();
                Console.WriteLine(avoidAttack);
                Console.ReadKey();
            }
            else
            {
                Console.Clear();

                if (player.Target.IsBlocking)
                {
                    int blockHit = random.Next(1, 11);

                    if (blockHit > 0 && blockHit < 4)
                    {
                        Console.WriteLine(counterAttack);
                        player.TakeDamage(player.Target.Strength/2);
                        player.Target.IsStunned = true;
                        Console.ReadKey();
                    }
                    else if (blockHit >= 4 && blockHit < 6)
                    {
                        Console.WriteLine(failedBlock);
                        player.Target.TakeDamage(player.CurrentStrength);
                        player.Target.IsStunned = true;
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine(clash);
                        player.Target.IsStunned = true;
                        Console.ReadKey();
                    }
                }
                else
                {
                    int damageNegation = player.CurrentStrength*3/4;

                    foreach(DamageTypes.damageType damageTypes in player.Target.Resistances)
                    {
                        if(player.CurrentEquippedWeapon.DamageType == damageTypes)
                        {
                            damageNegation = player.CurrentStrength / 2;
                            break;
                        }
                    }
                    foreach (DamageTypes.damageType damageTypes in player.Target.Weaknesses)
                    {
                        if (player.CurrentEquippedWeapon.DamageType == damageTypes)
                        {
                            damageNegation = player.CurrentStrength;
                            break;
                        }
                    }
                    int hit = random.Next(1, 101);

                    if (hit > 0 && hit <= player.Accuracy)
                    {
                        //Headshot
                        Console.WriteLine(headDamage);
                        player.Target.TakeDamage(player.CurrentStrength);
                        player.Target.IsStunned = true;
                        Console.ReadKey();
                    }
                    else if (hit > 80 && hit <= 100)
                    {
                        //BodyShot
                        Console.WriteLine(bodyDamage);
                        player.Target.TakeDamage(damageNegation);
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine(limbDamage);
                        player.Target.TakeDamage(Convert.ToInt32(damageNegation * 0.75));
                        Console.ReadKey();
                    }

                }
            }
        }

        private static void PlayerGuard(Player player)
        {
            player.Block();
        }

        private static void PlayerEscape(Player player)
        {
            int playerAgility = random.Next(1, player.CurrentAgility);
            int enemyAgility = random.Next(1, player.Target.Agility);

            if (playerAgility > enemyAgility)
            {
                while (true)
                {
                    playerSuccessfullyFled = true;
                    Console.Clear();
                    Console.WriteLine("Quickly Looking Around The Chamber, You Spot exits To The...");
                    Console.WriteLine("------------------------");
                    if(GameManager.currentRoom.NorthConnectingRoom != null)
                    {
                        Console.WriteLine("NORTH");
                        Console.WriteLine();
                    }
                    if (GameManager.currentRoom.SouthConnectingRoom != null)
                    {
                        Console.WriteLine("SOUTH");
                        Console.WriteLine();
                    }
                    if (GameManager.currentRoom.EastConnectingRoom != null)
                    {
                        Console.WriteLine("EAST");
                        Console.WriteLine();
                    }
                    if (GameManager.currentRoom.WestConnectingRoom != null)
                    {
                        Console.WriteLine("WEST");
                        Console.WriteLine();
                    }
                    string action;
                    Console.WriteLine("------------------------");
                    Console.WriteLine();
                    Console.WriteLine("You Escape By The ");
                    action = Console.ReadLine().ToLower();
                    switch (action)
                    {
                        case "north":
                            if (GameManager.currentRoom.NorthConnectingRoom == null)
                            {
                                Console.Clear();
                                Console.WriteLine("Your Eyes Decieve You As Upon Closer Look, No Exit Is Present, Or Has It Vanished? You Cannot Remember.");
                                Console.ReadKey();
                                break;
                            }
                            RoomCreator.exitDirection = Room.ConnectingDirections.NORTH;
                            GameManager.currentRoom = RoomCreator.CreateNewRoom();
                            player.Target = null;
                            player.HasAttemptedToFlee = true;



                            return;

                        case "south":
                            if (GameManager.currentRoom.SouthConnectingRoom == null)
                            {
                                Console.Clear();
                                Console.WriteLine("Your Eyes Decieve You As Upon Closer Look, No Exit Is Present, Or Has It Vanished? You Cannot Remember.");
                                Console.ReadKey();
                                break;
                            }
                            
                            RoomCreator.exitDirection = Room.ConnectingDirections.SOUTH;
                            GameManager.currentRoom = RoomCreator.CreateNewRoom();
                            
                            player.Target = null;
                            player.HasAttemptedToFlee = true;


                            return;

                        case "east":
                            if (GameManager.currentRoom.EastConnectingRoom == null)
                            {
                                Console.Clear();
                                Console.WriteLine("Your Eyes Decieve You As Upon Closer Look, No Exit Is Present, Or Has It Vanished? You Cannot Remember.");
                                Console.ReadKey();
                                break;
                            }

                            RoomCreator.exitDirection = Room.ConnectingDirections.EAST;
                            GameManager.currentRoom = RoomCreator.CreateNewRoom();
                            player.Target = null;
                            player.HasAttemptedToFlee = true;
                            return;

                        case "west":
                            if (GameManager.currentRoom.WestConnectingRoom == null)
                            {
                                Console.Clear();
                                Console.WriteLine("Your Eyes Decieve You As Upon Closer Look, No Exit Is Present, Or Has It Vanished? You Cannot Remember.");
                                Console.ReadKey();
                                break;
                            }

                            RoomCreator.exitDirection = Room.ConnectingDirections.WEST;
                            GameManager.currentRoom = RoomCreator.CreateNewRoom();
                            player.Target = null;
                            player.HasAttemptedToFlee = true;

                            return;


                        default:
                            Console.Clear();
                            Console.WriteLine("The Action You Wish To Make Is Not In Accordance WithYour Nature.");
                            Console.ReadKey();
                            
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("You Attempt To Flee But Are Blocked By Your Foe.");
                player.HasAttemptedToFlee = true;
                Console.ReadKey();
            }
                
        }

        private static void PlayerCastSpell(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Quickly, You Decise To Cast...");
                Console.WriteLine();
                Console.WriteLine("--------------------");
                if(player.Mana >= 3)
                {
                    Console.WriteLine("BLAZE");
                    Console.WriteLine("Cost: 3 MANA");
                    Console.WriteLine("Large Fireball That Does Devastating Damage To Enemies Who are week To It.");
                    Console.WriteLine();
                }
                if (player.Mana >= 2)
                {
                    Console.WriteLine("HOLY LIGHT");
                    Console.WriteLine("Cost: 2 MANA");
                    Console.WriteLine("Creates A Blinding Flash Of Light Which Stuns Enemies. Effective Against Guarding Foes.");
                    Console.WriteLine();
                }
                if (player.Mana >= 5)
                {
                    Console.WriteLine("URGENT HEAL");
                    Console.WriteLine("Cost: 5 MANA");
                    Console.WriteLine("Heals 10 Health In An Instant. A Good Alternative to Potions");
                    Console.WriteLine();
                }
                if (player.Mana <= 1)
                {
                    Console.WriteLine("Insufficient Amount Of Mana For Spell Casting.");
                    Console.WriteLine();
                }
                Console.WriteLine("BACK");
                

                Console.WriteLine("--------------------");
                string spell = Console.ReadLine();

                switch (spell.ToLower())
                {
                    case "blaze":
                        Console.Clear();
                        if(player.Mana < 3)
                        {
                            Console.Clear();
                            Console.WriteLine("You Attempt To Cast BLAZE But Are Unsuccessful.");
                            Console.ReadKey();
                            spellWasCast = false;
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Conjuring A Giant Fireball, You Lob It Towards The Foe, Who is Engulfed In Flames And Smoke.");
                            Console.ReadKey();
                            Console.Clear();
                            player.Mana -= 3;
                            
                            foreach (DamageTypes.damageType damageType in player.Target.Resistances)
                            {
                                if(damageType == DamageTypes.damageType.FIRE)
                                {
                                    player.Target.TakeDamage(3);
                                    if (GameManager.currentRoom.Enemy != null)
                                    {
                                        Console.WriteLine($"Emerging From The Smoke, The {player.Target.Name} Is Chared But Otherwise Mostly Unaffected.");
                                        Console.ReadKey();
                                        spellWasCast = true;
                                        return;
                                    }
                                    else
                                    {
                                        spellWasCast = true;
                                        return;
                                    }
                                }
                            }
                            foreach(DamageTypes.damageType damageType in player.Target.Weaknesses)
                            {
                                if (damageType == DamageTypes.damageType.FIRE)
                                {
                                    player.Target.TakeDamage(10);
                                    player.Target.IsStunned = true;
                                    if (GameManager.currentRoom.Enemy != null)
                                    {
                                        Console.WriteLine($"As The smoke Clears, You Can See The {player.Target.Name} Writhing In Pain, Burns Covering Its Body.");
                                        Console.ReadKey();
                                        spellWasCast = true;
                                        return;
                                    }
                                    else
                                    {
                                        spellWasCast = true;
                                        return;
                                    }
                                }
                            }

                            player.Target.TakeDamage(6);
                            
                            if (GameManager.currentRoom.Enemy != null)
                            {
                                Console.WriteLine($"Having Been Knocked Over By The Explosion, The {player.Target.Name} Takes Its Footing Back, Shaking Off Ash And Debris.");
                                Console.ReadKey();
                                spellWasCast = true;
                                return;
                            }
                            else
                            {
                                spellWasCast = true;
                                return;
                            }

                            
                        }
                        

                    case "holy light":
                        Console.Clear();
                        if (player.Mana < 2)
                        {
                            Console.Clear();
                            Console.WriteLine("You Attempt To Cast HOLY LIGHT But Are Unsuccessful.");
                            Console.ReadKey();
                            spellWasCast = false;
                            return;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("The Room Is Flooded With A Bright Light, Dissorienting Your Enemy.");
                            Console.ReadKey();
                            player.Mana -= 2;
                            player.Target.IsStunned = true;
                            spellWasCast = true;
                            player.Fatigue -= 1;
                        }
                        return;

                    case "urgent heal":
                        Console.Clear();
                        if (player.Mana < 5)
                        {
                            Console.Clear();
                            Console.WriteLine("You Attempt To Cast URGENT HEAL But Are Unsuccessful.");
                            Console.ReadKey();
                            spellWasCast = false;
                            return;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Placing Your Palm On The Floor And Shouting The Command, You Feel Your Body Beggin To Rejuvinate.");
                            Console.ReadKey();
                            player.Mana -= 5;
                            if (player.HP + 10 > player.Maxhp)
                            {
                                player.HP = player.Maxhp;
                            }
                            else
                            {
                                player.HP += 10;
                            }
                            spellWasCast = true;
                        }
                        return;

                    case "back":
                        spellWasCast = false;
                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine($"You Attempt To Cast {spell.ToUpper()} But Are Unsuccessful.");
                        Console.ReadKey();
                        spellWasCast = false;
                        return; 

                }

            }
            
        }

        private static bool BattleInventory(Player player)
        {
            Console.WriteLine("Quickly, You Remove Your Backpack");
            Console.ReadKey();
            bool isInInventory = true;

            while (isInInventory == true)
            {
                Console.Clear();
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Draw Weapon From HOLSTER");
                Console.WriteLine();
                Console.WriteLine("Retrieve Item From BACKPACK");
                Console.WriteLine();
                Console.WriteLine("CLOSE Backpack");
                Console.WriteLine("-----------------------------");
                string command = Console.ReadLine().ToLower();
                switch (command)
                {
                    case "close":

                        Console.Clear();
                        Console.WriteLine("Closing Your Backpack, You Throw It Back Over Your Shoulders.");
                        Console.ReadKey();
                        isInInventory = false;
                        return false;

                    case "backpack":
                        Console.Clear();
                        Console.WriteLine("BAKCPACK CONTENTS");
                        Console.WriteLine("----------------------------");
                        Console.WriteLine();
                        if (GameManager.PlayerInstance.ConsumableInventory.Count == 0)
                        {
                            Console.WriteLine("Your Backpack Is Empty");
                            Console.WriteLine();
                        }
                        foreach (KeyValuePair<Item, int> consumable in GameManager.PlayerInstance.ConsumableInventory)
                        {
                            Console.WriteLine(consumable.Key.Name + " x" + consumable.Value);
                            Console.WriteLine();
                        }
                        Console.WriteLine("BACK");
                        Console.WriteLine();
                        Console.WriteLine("----------------------------");
                        string consumableChoice = Console.ReadLine().ToLower();
                        if (consumableChoice == "back" || consumableChoice == "")
                        {
                            break;
                        }
                        int count = 0;
                        bool consumed = false;
                        foreach (KeyValuePair<Item, int> consumable in GameManager.PlayerInstance.ConsumableInventory)
                        {

                            if (consumableChoice == consumable.Key.Name.ToLower())
                            {
                                Console.Clear();
                                Console.WriteLine("Would You Like To Consume 1 " + consumable.Key.Name.ToUpper() + "?");
                                Console.WriteLine();
                                Console.WriteLine("--------------------");
                                Console.WriteLine("CONSUME");
                                Console.WriteLine();
                                Console.WriteLine("CANCEL");
                                Console.WriteLine("--------------------");
                                Console.WriteLine();
                                string consume = Console.ReadLine().ToLower();

                                switch (consume)
                                {
                                    case "consume":
                                        consumed = true;
                                        consumable.Key.Consume(GameManager.PlayerInstance);
                                        if (consumable.Value == 1)
                                        {
                                            GameManager.PlayerInstance.ConsumableInventory.Remove(consumable.Key);
                                            Console.Clear();
                                            Console.WriteLine("You Use Your Last " + consumable.Key.Name + ". ");
                                            Console.ReadKey();
                                            

                                        }
                                        else
                                        {
                                            if (GameManager.PlayerInstance.ConsumableInventory.ContainsKey(consumable.Key))
                                            {
                                                GameManager.PlayerInstance.ConsumableInventory.Remove(consumable.Key);
                                                GameManager.PlayerInstance.ConsumableInventory.Add(consumable.Key, consumable.Value - 1);

                                            }
                                            Console.Clear();
                                            Console.WriteLine("You Use One Of Your " + consumable.Key.Name + "s.");
                                            Console.ReadKey();

                                        }
                                        return true;

                                    case "cancel":
                                        Console.Clear();
                                        Console.WriteLine("You Place The " + consumable.Key.Name + " Back Into the Bag.");
                                        Console.ReadKey();
                                        break;

                                    default:
                                        Console.Clear();
                                        Console.WriteLine("The Stress Of Battle Causes You To Forget What You Wanted To Do With The Item.");
                                        Console.ReadKey();
                                        break;
                                }



                            }

                            else
                            {
                                count++;
                            }
                            if (consumed == true)
                            {
                                break;
                            }


                        }
                        if (count == GameManager.PlayerInstance.ConsumableInventory.Count && consumed == false)
                        {
                            Console.Clear();
                            Console.WriteLine("You Search Your Backpack But Cant Find An Items Matching What You Are Looking For.");
                            Console.ReadKey();
                        }
                        break;

                    case "holster":
                        Console.Clear();
                        Console.WriteLine("HOLSTER CONTENTS");
                        Console.WriteLine("----------------------------");
                        Console.WriteLine();
                        foreach (Weapon weapon in GameManager.PlayerInstance.HolsterInventory)
                        {
                            Console.WriteLine(weapon.Name);
                            Console.WriteLine();
                        }
                        Console.WriteLine("BACK");
                        Console.WriteLine();
                        Console.WriteLine("----------------------------");
                        string weaponChoice = Console.ReadLine().ToLower();
                        if (weaponChoice == "back" || weaponChoice == "")
                        {
                            break;
                        }
                        int count2 = 0;
                        foreach (Weapon weapon in GameManager.PlayerInstance.HolsterInventory)
                        {
                            if (weapon.Name.ToLower() == weaponChoice && weapon.Name != null)
                            {
                                Console.Clear();
                                Console.WriteLine("Would You Like To Equip Your " + weapon.Name.ToUpper() + "?");
                                Console.WriteLine();
                                Console.WriteLine("--------------------");
                                Console.WriteLine("EQUIP");
                                Console.WriteLine("PUT AWAY");
                                Console.WriteLine("--------------------");
                                Console.WriteLine();
                                string confirm = Console.ReadLine().ToLower();

                                switch (confirm)
                                {
                                    case "equip":
                                        if (weapon.IsTwoHanded)
                                        {
                                            GameManager.PlayerInstance.CurrentEquippedWeapon = weapon;
                                            GameManager.PlayerInstance.CurrentSecondaryWeapon = null;
                                            Console.Clear();
                                            Console.WriteLine("Reaching Into The Bottomless Holster, You Draw You " + weapon.Name + ".");
                                            Console.ReadKey();
                                            return true;
                                        }
                                        else if (weapon.IsSecondary)
                                        {
                                            if (GameManager.PlayerInstance.CurrentEquippedWeapon.IsTwoHanded == true)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Cannot Equip While Primary Weapon Is A Two Handed Weapon");
                                                Console.ReadKey();
                                                break;
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                GameManager.PlayerInstance.CurrentSecondaryWeapon = weapon;
                                                Console.WriteLine("Reaching Into The Bottomless Holster, You Draw You " + weapon.Name + ".");
                                                Console.ReadKey();
                                                return true;
                                            }
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            GameManager.PlayerInstance.CurrentEquippedWeapon = weapon;
                                            Console.WriteLine("Reaching Into The Bottomless Holster, You Draw You " + weapon.Name + ".");
                                            Console.ReadKey();
                                            return true; ;
                                        }
                                        

                                    case "put away":
                                        Console.Clear();
                                        Console.WriteLine("You Decide That Your " + weapon.Name + " Isnt The Right Weapon For The Job.");
                                        Console.ReadKey();
                                        break;

                                    default:
                                        Console.Clear();
                                        Console.WriteLine("Confused For A Moment, You Arent Quite Sure What To Do With The Weapon.");
                                        Console.ReadKey();
                                        break;

                                }
                                break;
                            }
                            else
                            {
                                count2++;
                            }
                        }
                        if (count2 == GameManager.PlayerInstance.HolsterInventory.Count)
                        {
                            Console.Clear();
                            Console.WriteLine("You Rummage Around For A Bit But Cant Seem To Find What You Were Looking For");
                            Console.ReadKey();
                        }
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("You Pat Yourself Down But Can Find What You Wanted To Search");
                        Console.ReadKey();
                        break;
                }

            }
            return false;
        }



        private static void AssignEnemyReacions(Player player, ref string avoidAttack, ref string counterAttack, ref string failedBlock, ref string clash, ref string headDamage, ref string bodyDamage, ref string limbDamage)
        {
            if (player.Target.Type == EnemySpecification.SPIDER)
            {
                avoidAttack = "Pouncing To The Side, The " + player.Target.Name + " Avoids Your Attack. It Clumsily Stummbles Backwards A Few Feet Before Catching Itself.";
                counterAttack = $"Catching Your {player.CurrentEquippedWeapon.Name} In Its Mandibles, The Spider Swings It Head, Throwing You Across The Room With Your Weapon.";
                failedBlock = $"Luckily, Your Weapon Strikes Between The Spider's Defenses, Causing It To Wail In Pain, Backing Away.";

                clash = $"Swinging Your {player.CurrentEquippedWeapon.Name}, The Beast Blocks With Its Hard-Shelled Legs. You Both Back Away from Eachother.";

                headDamage = "You Hit The Beast In The Head, Blood Pours From One Of The Spider's Eyes. It Shakes It Head In Shock.";
                bodyDamage = $"Your Blow Connecting With The {player.Target.Name}'s Abdomment, It Becomes Unsteady On Its Legs, Winded From The Blow.";
                limbDamage = $"You Attack Its Leg. The Beast Stumbles Backwards, Instinctevely Avoiding Putting Pressure On The Limb.";
            }
            else if (player.Target.Type == EnemySpecification.EYE)
            {
                avoidAttack = $"Sticking To The Wall, The Giant Eye, Climbs Out Of Reach Before Clinging To The Cieling And Dropping Back Down In A More Favorable Position.";
                counterAttack = $"Slashing At You With Its Tendrils, The Flurry Of Whipping strikes Bring You To Your Knees.";
                failedBlock = $"Barely Slipping Between Its Tentacles, Your Weapon Grazes The Eyes, Scratching The Thin Film That Protects It.";

                clash = $"Blocking Your {player.CurrentEquippedWeapon.Name} The Creature Attempts To Block Your Strike. You Push On The Weapon Hard Forcing The {player.Target.Name} To Dissengage.";

                headDamage = $"Using A Faint, You Are Able To Trick The Beast Into Protecting Its Side, Allowing You To Attack The Upper Part Of Its Eye. The Creature Retreats In Pain.";
                bodyDamage = $"A Well Placed Foot To The {player.Target.Name}'s Eye Forces It To Back Away A Few Feet.";
                limbDamage = $"As It Reaches Towards You, You Crush On Of Its Tendrils Under Your Weapon.";
            }
            else if (player.Target.Type == EnemySpecification.KNIGHT)
            {
                avoidAttack = "Raising It's Sword, The Knight Easily Deflects Your Attack To The Side, Stepping Backwards In The Process.";
                counterAttack = $"Catching You Weapon With The Armoured Palm Of Its Glove, It Drives Its Sword Into Your Gut, Just Missing Your Vitals.";
                failedBlock = $"Getting Around Its Sword, You Hit A Weak Point In The Armour, Driving The {player.Target.Name} Back.";

                clash = $"Swinging Your {player.CurrentEquippedWeapon.Name}, The Knight Catches Your Wrist, You, Doing The Same, Catch His. You Momentarely Remain At A Struggling Standstill Before Disengaging.";

                headDamage = $"Driving Your {player.CurrentEquippedWeapon.Name} Upwards, You Hit The {player.Target.Name} In The Neck, Crushing Metal Plaiting And Chainmail.";
                bodyDamage = $"With A Strong Blow, You Connect With The Knight's Breastplate, Denting It At The Sternum. The Opponent Stumbles Momentarely.";
                limbDamage = $"Your Weapon Strikes At a Joint On The Knight's Shoulder, Breaking Its Stance.";
            }
            else if (player.Target.Type == EnemySpecification.BONEDRIKE)
            {
                avoidAttack = "Watching You Swing Your Weapon, The Bonedrike Dodges, Weaving Around Your Strikes.";
                counterAttack = $"Catching Your Weapon In Its Hands, It Uses Its Body As A Whip, Hitting You With Its Tail Causing You To Roll To The Ground And Into Some Nearby Debris.";
                failedBlock = $"With All Your Might, You Attack The Creature's Defenses, Breaking A Few Of Its Arms In The Process.";

                clash = $"Catching Your Weapon In Its Teeth, You Struggle For Power Over The Beast Before Freeing It From Its Jaws And Backing Away.";

                headDamage = $"Driving Your Weapon Into Its Eye Socket, Bone Breaks Away. A Crack Exstends From The Bonedrike's Cheek Bone Down To Its Upper Jaw, Sending The Monster Rolling In Pain.";
                bodyDamage = $"Your {player.CurrentEquippedWeapon.Name} Logdes Itself Between Two Of Its Vertebraes Causing Them To Shift, Painfully, The Joints Cracking.";
                limbDamage = $"A Forceful Blow From Your {player.CurrentEquippedWeapon.Name} To One Of The Bonedrike's Hands Causes The Plam To Fracture. The Creature Tucks It Under Itself To Protect It.";
            }
            else if (player.Target.Type == EnemySpecification.MINOTAUR)
            {
                avoidAttack = $"Swatting Your Weapon Aside With Its Large Axe, The Beast Glares At You.";
                counterAttack = $"Using Its Axe, The {player.Target.Name} Hooks Onto Your {player.CurrentEquippedWeapon.Name} Pulling You Towards It And Right Into Its Hand. Picking You Up Off The Floor, Its Slams You Into The Ground Repeatedly Before Tossing You Away.";
                failedBlock = $"Out Manuvering The Hulking Monster, You Slash Your Weapon Into Its Stomach, The Beast Falling Into A Kneeling Position as A Result.";

                clash = $"Charging At You With Its Horns, You Catch Them, One In Each Hand And Wrestle With It As You Are Pushed Backwards. At The Last Moment, You Roll Out From Under The {player.Target.Name}'s Horns, To Safety.";

                headDamage = $"Running Up To The Beast, It Slams Its Axe Into The Ground In An Attempt To Kill You. Using this, You Pounce Off The Weapon And Up To Its Head where You Bring Your Own Armament Down On Its Skull, Causing Blood To Rush Down Its Snout.";
                bodyDamage = $"Taking Advantage Of An Oppening, You Swing At The {player.Target.Name}'s Gut. The Beast, Huntching Over From The Impact.";
                limbDamage = $"As The Monster Extends Its Arm To Grab You, You Drive Your {player.CurrentEquippedWeapon.Name} Into It, Breaking Its Thumb.";
            }
            else if (player.Target.Type == EnemySpecification.GARGOYLE)
            {
                avoidAttack = $"Using Its Sharp Claws, The Creature Guides Your Weapon Aside, Taunting You In The Process.";
                counterAttack = $"Catching Your Weapon, The {player.Target.Name} Leaps From It Soaring Past Your Head. It Slashes A Deep Cut Across The Side f Your Neck As It Passes By.";
                failedBlock = $"Attacking With A Heavy Blow, The Gargoyle Is Caught Offguard, Flinging It Into A Wall Further Back.";

                clash = $"The Creature Dives Towards You, Clashing With Your Weapon, Before Attempting Another Two Dives Which You Deflect As Well.";

                headDamage = $"As The Gargoyle Attempts To Back Away, Your Weapon Bluntly Connects With The Side Of Its Face, Sending It Spiraling Into A Nearby Pillar.";
                bodyDamage = $"Leaping Into The Air, You Manage To Strike The Small Monster In The Gut. It Flies Back In Shock.";
                limbDamage = $"As The {player.Target.Name} Makes An Attempt To Attack Your Legs, You Kick It Mid Flight, Side Stepping It In One Motion.";
            }
            else if(player.Target.Type == EnemySpecification.DRAGON)
            {
                avoidAttack = "Flapping Its Wings, The Gust Blows You Off-Balance Preventing You From Landing The Attack.";
                counterAttack = $"Blocking Your {player.CurrentEquippedWeapon.Name} With Its Snout Horn, The Dragon Blows Fire At You In Retaliation, Momentarely Ingulfing You Before You Escape.";
                failedBlock = $"Striking Between Its Finger, The Dragon Recoils In Pain.";

                clash = $"As You Go In For A Blow, The Beast Swipes At You, Your Weapon Bouncing Of Its Claw.";

                headDamage = "Getting Behind The Creature, You Climb Up Its Back, And Onto Its Neck, Driving Your Weapon Down On It Before Being shaken Off By The Creature As It Wails In Pain.";
                bodyDamage = $"Sliding Under Its Body, You Strike Up At Its Gut, Drawing Blood.";
                limbDamage = $"You Attack Its Wing, Puncturing The Thin Skin That Covers It.";
            }
        }


        private static void EnemyAttack(AIDecisionTree decisionTree, EnemySpecification type)
        {
            string situation;
            if(type == EnemySpecification.MINOTAUR)
            {
               
                situation = decisionTree.MakeDecision(GameManager.currentRoom.Enemy.HP, GameManager.currentRoom.Enemy.Maxhp / 2);

                MinotaurAtk(situation, GameManager.currentRoom.Enemy);
            }


            else if (type == EnemySpecification.BONEDRIKE)
            {

                situation = decisionTree.MakeDecision(GameManager.currentRoom.Enemy.HP, GameManager.currentRoom.Enemy.Maxhp *3/4);

                BonedrikeAtk(situation, GameManager.currentRoom.Enemy);
            }


            else if (type == EnemySpecification.KNIGHT)
            {

                situation = decisionTree.MakeDecision(GameManager.currentRoom.Enemy.HP, GameManager.currentRoom.Enemy.Maxhp /2);

                KnightAtk(situation, GameManager.currentRoom.Enemy);
            }


            else if (type == EnemySpecification.EYE)
            {

                situation = decisionTree.MakeDecision(GameManager.currentRoom.Enemy.HP, GameManager.currentRoom.Enemy.Maxhp * 2 / 5);

                OcculusAtk(situation, GameManager.currentRoom.Enemy);
            }


            else if (type == EnemySpecification.GARGOYLE)
            {

                situation = decisionTree.MakeDecision(GameManager.currentRoom.Enemy.HP, GameManager.currentRoom.Enemy.Maxhp * 3 / 4);

                GargoyleAtk(situation, GameManager.currentRoom.Enemy);
            }


            else if (type == EnemySpecification.SPIDER)
            {

                situation = decisionTree.MakeDecision(GameManager.currentRoom.Enemy.HP, GameManager.currentRoom.Enemy.Maxhp /2);

                SpiderAtk(situation, GameManager.currentRoom.Enemy);
            }
            else if(type == EnemySpecification.DRAGON)
            {
                situation = decisionTree.MakeDecision(GameManager.currentRoom.Enemy.HP, GameManager.currentRoom.Enemy.Maxhp / 2);
                DragonAtk(situation, GameManager.currentRoom.Enemy);
            }
        }


        private static void BonedrikeAtk(string situation, Enemy enemy)
        {
            int choice = 0;
            switch (situation.ToLower())
            {
                case "high health":
                    choice = random.Next(1, 101);
                    if (choice > 0 && choice <= 70)
                    {
                        enemy.Block();
                    }
                    else if (choice > 70 && choice <= 85)
                    {
                        if (enemy.HasUsedSpecial == false)
                        {
                            enemy.Special2();

                        }
                        else
                        {
                            int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                            int enemyRoll = random.Next(1, enemy.Agility);
                            enemy.Attack(playerRoll, enemyRoll);
                        }
                    }
                    else
                    {
                        int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                        int enemyRoll = random.Next(1, enemy.Agility);
                        enemy.Attack(playerRoll, enemyRoll);
                    }
                    break;

                case "low health":
                    choice = random.Next(1, 101);
                    if(choice > 0 && choice <= 5)
                    {
                        enemy.Block();
                    }
                    else if(choice > 5 && choice <= 20)
                    {
                        if (enemy.HasAttemptedToFlee == false)
                        {
                            int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                            int enemyRoll = random.Next(1, enemy.Agility);
                            enemy.Flee(GameManager.currentRoom, playerRoll, enemyRoll);
                        }
                        else
                        {
                            enemy.Block();
                        }
                    }
                    else if(choice > 20 && choice <= 40)
                    {
                        if(enemy.HasUsedSpecial == false)
                        {
                            enemy.Special2();

                        }
                        else
                        {
                            int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                            int enemyRoll = random.Next(1, enemy.Agility);
                            enemy.Attack(playerRoll, enemyRoll);
                        }
                    }
                    else if(choice > 40 && choice <= 70)
                    {
                        if (enemy.HasUsedSpecial == false)
                        {
                            enemy.Special1();

                        }
                        else
                        {
                            int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                            int enemyRoll = random.Next(1, enemy.Agility);
                            enemy.Attack(playerRoll, enemyRoll);
                        }
                    }
                    else
                    {
                        int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                        int enemyRoll = random.Next(1, enemy.Agility);
                        enemy.Attack(playerRoll, enemyRoll);
                    }
                    break;
            }
        }

        private static void KnightAtk(string situation, Enemy enemy)
        {
            int choice = 0;
            switch (situation.ToLower())
            {
                case "high health":
                    choice = random.Next(1, 101);
                    if (choice > 0 && choice <= 50)
                    {
                        enemy.Block();
                    }
                    else
                    {
                        int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                        int enemyRoll = random.Next(1, enemy.Agility);
                        enemy.Attack(playerRoll, enemyRoll);
                    }
                    break;

                case "low health":
                    choice = random.Next(1, 101);
                    if(enemy.HP < enemy.Maxhp/2 && enemy.HP > enemy.Maxhp / 4)
                    {
                        if (choice > 0 && choice <= 5)
                        {
                            enemy.Block();
                        }
                        else if (choice > 5 && choice <= 20)
                        {
                            if (enemy.HasAttemptedToFlee == false)
                            {
                                int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                                int enemyRoll = random.Next(1, enemy.Agility);
                                enemy.Flee(GameManager.currentRoom, playerRoll, enemyRoll);
                            }
                            else
                            {
                                enemy.Block();
                            }
                        }
                        else if (choice > 20 && choice <= 40)
                        {
                            if (enemy.HasUsedSpecial == false)
                            {
                                enemy.Special2();

                            }
                            else
                            {
                                int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                                int enemyRoll = random.Next(1, enemy.Agility);
                                enemy.Attack(playerRoll, enemyRoll);
                            }
                        }
                        else if (choice > 40 && choice <= 70)
                        {
                            if (enemy.HasUsedSpecial == false)
                            {
                                enemy.Special1();

                            }
                            else
                            {
                                int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                                int enemyRoll = random.Next(1, enemy.Agility);
                                enemy.Attack(playerRoll, enemyRoll);
                            }
                        }
                        else
                        {
                            int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                            int enemyRoll = random.Next(1, enemy.Agility);
                            enemy.Attack(playerRoll, enemyRoll);
                        }

                    }
                    else
                    {
                        if(choice > 0 && choice <= 40)
                        {
                            enemy.Block();
                        }
                        else if(choice > 40 && choice <= 80)
                        {
                            if (enemy.HasUsedSpecial == false)
                            {
                                enemy.Special1();

                            }
                            else
                            {
                                int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                                int enemyRoll = random.Next(1, enemy.Agility);
                                enemy.Attack(playerRoll, enemyRoll);
                            }
                        }
                        else
                        {
                            int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                            int enemyRoll = random.Next(1, enemy.Agility);
                            enemy.Attack(playerRoll, enemyRoll);
                        }
                    }
                    break;
            }
        }

        private static void MinotaurAtk(string situation, Enemy enemy)
        {
            int choice = 0;
            switch (situation.ToLower())
            {
                case "high health":
                    choice = random.Next(1, 101);
                    if(choice > 0 && choice <= 20)
                    {
                        enemy.Block();
                    }
                    else if(choice > 20 && choice <= 50)
                    {
                        if(enemy.HasUsedSpecial == false)
                        {
                            enemy.Special1();

                        }
                        else
                        {
                            int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                            int enemyRoll = random.Next(1, enemy.Agility);
                            enemy.Attack(playerRoll, enemyRoll);
                        }
                    }
                    else
                    {
                        int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                        int enemyRoll = random.Next(1, enemy.Agility);
                        enemy.Attack(playerRoll, enemyRoll);
                    }
                    break;

                case "low health":
                    choice = random.Next(1, 101);
                    if (choice > 0 && choice <= 50)
                    {
                        if (enemy.HasUsedSpecial == false)
                        {
                            enemy.Special2();

                        }
                        else
                        {
                            int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                            int enemyRoll = random.Next(1, enemy.Agility);
                            enemy.Attack(playerRoll, enemyRoll);
                        }
                    }
                    
                    else
                    {
                        int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                        int enemyRoll = random.Next(1, enemy.Agility);
                        enemy.Attack(playerRoll, enemyRoll);
                    }
                    break;
            }
        }

        private static void OcculusAtk(string situation, Enemy enemy)
        {
            int choice = 0;
            switch (situation.ToLower())
            {
                case "high health":
                    choice = random.Next(1, 101);
                    if (choice > 0 && choice <= 30)
                    {
                        if (enemy.HasUsedSpecial == false)
                        {
                            enemy.Special1();

                        }
                        else
                        {
                            int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                            int enemyRoll = random.Next(1, enemy.Agility);
                            enemy.Attack(playerRoll, enemyRoll);
                        }
                    }
                    else if (choice > 30 && choice <= 90)
                    {
                        int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                        int enemyRoll = random.Next(1, enemy.Agility);
                        enemy.Attack(playerRoll, enemyRoll);
                    }
                    else
                    {
                        enemy.Block();
                    }
                    break;



                case "low health":
                    choice = random.Next(1, 101);
                    if (choice > 0 && choice <= 40)
                    {
                        if (enemy.HasUsedSpecial == false)
                        {
                            enemy.Special1();

                        }
                        else
                        {
                            int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                            int enemyRoll = random.Next(1, enemy.Agility);
                            enemy.Attack(playerRoll, enemyRoll);
                        }
                    }
                    else if(choice > 40 && choice <= 60)
                    {
                        int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                        int enemyRoll = random.Next(1, enemy.Agility);
                        enemy.Attack(playerRoll, enemyRoll);
                    }
                    else
                    {
                        if (enemy.HasAttemptedToFlee == false)
                        {
                            int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                            int enemyRoll = random.Next(1, enemy.Agility);
                            enemy.Flee(GameManager.currentRoom, playerRoll, enemyRoll);
                        }
                        else
                        {
                            enemy.Block();
                        }
                    }
                    break;
            }
        }

        private static void GargoyleAtk(string situation, Enemy enemy)
        {
            int choice = 0;
            switch (situation.ToLower())
            {
                case "high health":
                    choice = random.Next(1, 101);
                    if (choice > 0 && choice <= 10)
                    {
                        if (enemy.HasAttemptedToFlee == false)
                        {
                            int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                            int enemyRoll = random.Next(1, enemy.Agility);
                            enemy.Flee(GameManager.currentRoom, playerRoll, enemyRoll);
                            enemy.HasAttemptedToFlee = true;
                        }
                        else
                        {
                            enemy.Block();
                        }
                    }
                    else if (choice > 10 && choice <= 40)
                    {
                        if (enemy.HasUsedSpecial == false)
                        {
                            enemy.Special2();
                            enemy.HasUsedSpecial = true;

                        }
                        else
                        {
                            int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                            int enemyRoll = random.Next(1, enemy.Agility);
                            enemy.Attack(playerRoll, enemyRoll);
                        }
                    }
                    else if(choice  > 40 && choice <= 70)
                    {
                        enemy.Block();
                    }
                    else
                    {
                        int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                        int enemyRoll = random.Next(1, enemy.Agility);
                        enemy.Attack(playerRoll, enemyRoll);
                    }
                    break;



                case "low health":
                    choice = random.Next(1, 101);
                    if (choice > 0 && choice <= 40)
                    {
                        if (enemy.HasUsedSpecial == false)
                        {
                            enemy.Special1();
                            enemy.HasUsedSpecial = true;

                        }
                        else
                        {
                            int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                            int enemyRoll = random.Next(1, enemy.Agility);
                            enemy.Attack(playerRoll, enemyRoll);
                        }
                    }
                    else if (choice > 40 && choice <= 80)
                    {
                        int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                        int enemyRoll = random.Next(1, enemy.Agility);
                        enemy.Attack(playerRoll, enemyRoll);
                    }
                    else
                    {
                        if(enemy.HasAttemptedToFlee == false)
                        {
                            int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                            int enemyRoll = random.Next(1, enemy.Agility);
                            enemy.Flee(GameManager.currentRoom, playerRoll, enemyRoll);
                            enemy.HasAttemptedToFlee = true;
                        }
                        else
                        {
                            enemy.Block();
                        }
                    }
                    break;
            }
        }

        private static void SpiderAtk(string situation, Enemy enemy)
        {
            int choice = 0;
            switch (situation.ToLower())
            {
                case "high health":
                    choice = random.Next(1, 101);
                    if (choice > 0 && choice <= 35)
                    {
                        int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                        int enemyRoll = random.Next(1, enemy.Agility);
                        enemy.Attack(playerRoll, enemyRoll);
                    }
                    else if (choice > 35 && choice <= 70)
                    {
                        enemy.Block();
                    }
                    else if (choice > 70 && choice <= 85)
                    {
                        if (enemy.HasUsedSpecial == false)
                        {
                            enemy.Special1();

                        }
                        else
                        {
                            int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                            int enemyRoll = random.Next(1, enemy.Agility);
                            enemy.Attack(playerRoll, enemyRoll);
                        }
                    }
                    else
                    {
                        if (enemy.HasUsedSpecial == false)
                        {
                            enemy.Special2();

                        }
                        else
                        {
                            enemy.Block();
                        }
                    }
                    break;



                case "low health":
                    choice = random.Next(1, 101);
                    if (choice > 0 && choice <= 35)
                    {
                        int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                        int enemyRoll = random.Next(1, enemy.Agility);
                        enemy.Attack(playerRoll, enemyRoll);
                    }
                    else if (choice > 35 && choice <= 55)
                    {
                        enemy.Block();
                    }
                    else if (choice > 55 && choice <= 80)
                    {
                        if (enemy.HasUsedSpecial == false)
                        {
                            enemy.Special1();

                        }
                        else
                        {
                            int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                            int enemyRoll = random.Next(1, enemy.Agility);
                            enemy.Attack(playerRoll, enemyRoll);
                        }
                    }
                    else
                    {
                        if (enemy.HasUsedSpecial == false)
                        {
                            enemy.Special2();

                        }
                        else
                        {
                            enemy.Block();
                        }
                    }
                    break;
            }
        }

        private static void DragonAtk(string situation, Enemy enemy)
        {
            int choice = 0;
            switch (situation.ToLower())
            {
                case "high health":
                    choice = random.Next(1, 101);
                    if (choice > 0 && choice <= 30)
                    {
                        enemy.Block();
                    }
                    else if (choice > 30 && choice <= 50)
                    {
                        if (enemy.HasUsedSpecial == false)
                        {
                            enemy.Special1();
                            enemy.HasUsedSpecial = true;

                        }
                        else
                        {
                            enemy.Block();
                        }
                    }
                    else
                    {
                        int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                        int enemyRoll = random.Next(1, enemy.Agility);
                        enemy.Attack(playerRoll, enemyRoll);
                    }
                    break;

                case "low health":
                    choice = random.Next(1, 101);
                    if (choice > 0 && choice <= 10)
                    {
                        enemy.Block();
                    }
                    else if (choice > 10 && choice <= 20)
                    {
                        enemy.Special2();
                    }
                    else
                    {
                        int playerRoll = random.Next(1, enemy.Target.CurrentAgility);
                        int enemyRoll = random.Next(1, enemy.Agility);
                        enemy.Attack(playerRoll, enemyRoll);
                    }
                    break;
            }
        }




        #endregion

        private static void UpdateQuests()
        {
            QuestManager.EzekielInCamp();
            QuestManager.CheckScoutsRoom();
            QuestManager.ReturnToCampFromScoutRoom();
            QuestManager.InitiateDragonRoom();
            QuestManager.ReturnToCamp();
        }


    }
}
