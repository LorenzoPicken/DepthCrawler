using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepthCrawlerGame.Entities;
using DepthCrawlerGame.Entities.Enemies;
using DepthCrawlerGame.Entities.NPCs;
using DepthCrawlerGame;
using DepthCrawlerGame.Rooms;

namespace DepthCrawlerGame.Creators
{
    public static class RoomCreator
    {
        static readonly Random random = new Random(Guid.NewGuid().GetHashCode());
        public static Room.ConnectingDirections exitDirection;
        private static List<Room> roomList = new List<Room>();

        public static string originalRoomdescript = "";

        static List<string> randomDescript = new List<string>()
        {
            "The Room Is Cold And Damp. A Thin Layer Of Water Covers The Floor, Causing The Room To Smell Of Mould. Fungal Growths And Other Unsavoury Things Grow From The Ceiling And Walls, Their Biolucent Brightening Up The Environment. Collapsed Pillars And Debris Clutter The Room.",
            "This New Chamber Is Humid And Warm. In Some Areas Of The Room, The Walls Have Taken On A Rustic Orange Color And Radiate Heat. The Torches That Light The Room Burn Unnaturally Hot, Their Flames Dancing Like A Raging Inferno.",
            "The Chamber Is Baren Aside From One Corner Where, Wooden Chairs, Tables And Crates Are Stacked On Top Of Each Other Over A Dark Hole In The Wall, As If To Keep Something Out. Thick Dust And Cobwebs Cover The Stacked Mess.",
            "From The Ceiling, Water Flows Down The Rooms’ Supporting Pillars Like Waterfalls. Upon Hitting The Floor, The Water Flows Down Into Metal Grids That Are Found Near The Pillars’ Basses. Occasionally, Moss Or Rubble Can Be Spotted Flowing through The Otherwise Very Clear Water.",
            "At The Back Of The Room Is A Throne On Which Sits Mostly Unidentifiable Bone Remains. Torn Banners Line The Walls And Pillars, Having Faded And Been Covered In Grime With The Passage Of Time.",
            "The Chamber Has An Unusually High Flat Ceiling, With A Gaping Hole In Its Center. Rubble From The Collapsed Roof Is Scattered across the Middle Of the Room. The Air Is Dry And Dusty, Particles Floating Down From The Fissure Overhead, Swirling Around.",
            "This Room’s Stone Has A Dark Blueish Purple Color To It, Causing The Chamber To Appear Darker. Yellow GemStones Embedded In The Walls Lighten Up The Otherwise Sombre Room.",
            "Taking On The Appearance Of A Cave, The Room Has an Abundance Of Stalagmites and Stalactites Covering The Floor And Roof. Other Bigger Rock Formations Grow From The Ground, Supporting The Ceiling."
        };



        public static Room CreateNewRoom()
        {
            int count = 0;
            foreach(Room room in roomList)
            {
                if(GameManager.currentRoom.Id != room.Id)
                {
                    count++;
                }

            }
            if(count == roomList.Count)
            {
                roomList.Add(GameManager.currentRoom);

            }

            RoomType newRoomType;
            Enemy newRoomTarget;

            Room southRoom = null;
            Room northRoom = null;
            Room eastRoom = null;
            Room westRoom = null;


            List<Room.ConnectingDirections> directions = new List<Room.ConnectingDirections>() { Room.ConnectingDirections.NORTH, Room.ConnectingDirections.SOUTH, Room.ConnectingDirections.EAST, Room.ConnectingDirections.WEST };
            if (exitDirection == Room.ConnectingDirections.NORTH)
            {
              
                foreach(Room room in roomList)
                {
                    if(room.Id == GameManager.currentRoom.NorthConnectingRoom.Id)
                    {
                        
                        return GameManager.currentRoom.NorthConnectingRoom;
                    }
                }
            }
            else if (exitDirection == Room.ConnectingDirections.SOUTH)
            {
                
                foreach (Room room in roomList)
                {
                    if (room.Id == GameManager.currentRoom.SouthConnectingRoom.Id)
                    {
                        room.IsScanned = true;
                        return GameManager.currentRoom.SouthConnectingRoom;
                    }
                }
            }
            else if (exitDirection == Room.ConnectingDirections.WEST)
            {
                
                foreach (Room room in roomList)
                {
                    if (room.Id == GameManager.currentRoom.WestConnectingRoom.Id)
                    {
                        room.IsScanned = true;
                        return GameManager.currentRoom.WestConnectingRoom;
                    }
                }
            }
            else if (exitDirection == Room.ConnectingDirections.EAST)
            {
                
                foreach (Room room in roomList)
                {
                    if (room.Id == GameManager.currentRoom.EastConnectingRoom.Id)
                    {
                        room.IsScanned = true;
                        return GameManager.currentRoom.EastConnectingRoom;
                    }
                }
            }

            newRoomType = DecideRoomType();



            int numberOfconnectingRooms;

            numberOfconnectingRooms = random.Next(2, 5);


            if (exitDirection == Room.ConnectingDirections.NORTH)
            {

                southRoom = GameManager.currentRoom;
                directions.Remove(Room.ConnectingDirections.SOUTH);
            }
            else if (exitDirection == Room.ConnectingDirections.SOUTH)
            {
                //northroom of next room
                northRoom = GameManager.currentRoom;
                directions.Remove(Room.ConnectingDirections.NORTH);
            }
            else if (exitDirection == Room.ConnectingDirections.EAST)
            {
                westRoom = GameManager.currentRoom;
                directions.Remove(Room.ConnectingDirections.WEST);
            }
            else
            {
                eastRoom = GameManager.currentRoom;
                directions.Remove(Room.ConnectingDirections.EAST);
            }

            ShuffleList(directions);

            for (int i = 0; i < 4 - numberOfconnectingRooms; i++)
            {
                directions.RemoveAt(i);
            }





            foreach(Room.ConnectingDirections direction in directions)
            {
                if(direction == Room.ConnectingDirections.NORTH)
                {
                    northRoom = new Room(null, null, null, null, null, null,null);
                }
                else if (direction == Room.ConnectingDirections.SOUTH)
                {
                    southRoom = new Room(null, null, null, null, null, null,null);
                }
                else if (direction == Room.ConnectingDirections.WEST)
                {
                    westRoom = new Room(null, null, null, null, null, null,null);
                }
                else if (direction == Room.ConnectingDirections.EAST)
                {
                    eastRoom = new Room(null, null, null, null, null, null,null);
                }
            }

            if (newRoomType == RoomType.DUNGEON)
            {
                StatisticsTracker.RoomsExplored++;
                GameManager.timeSinceLastQuestRoom++;
                //Create an enemy for the room
                List<EnemyType> enemyList = new List<EnemyType> { EnemyType.SPIDER, EnemyType.KNIGHT, EnemyType.GARGOYLE, EnemyType.EYE, EnemyType.BONEDRIKE, EnemyType.MINOTAUR };
                int rng = random.Next(0, enemyList.Count);

                EnemyType selectedEnemy = enemyList[rng];
                newRoomTarget = EnemyCreator.CreateEnemy(selectedEnemy);
                

                //Create lootable furniture
                List<Searchable> lootables = new List<Searchable>();
                rng = random.Next(0, 4);

                lootables = SearchableCreator.CreateSearchable(rng);



                Room newRoom = new DungeonRoom(southRoom, northRoom, eastRoom, westRoom, lootables, newRoomTarget,null);
                newRoom.Description = RandomizeRoomDescription();
                newRoom.Id = GameManager.roomID;
                GameManager.roomID++;
                AssignNewRoomToOld(newRoom);
                return newRoom;




            }
            else if (newRoomType == RoomType.LOOT)
            {
                StatisticsTracker.RoomsExplored++;
                GameManager.timeSinceLastQuestRoom++;
                int rng;
                //Create lootable furniture
                List<Searchable> lootables = new List<Searchable>();
                rng =random.Next(2, 6);

                lootables = SearchableCreator.CreateSearchable(rng);

                Room newRoom = new LootRoom(southRoom, northRoom, eastRoom, westRoom, lootables, null, null);
                newRoom.Id = GameManager.roomID;
                GameManager.roomID++;
                newRoom.IsCleared = true;
                newRoom.Description = RandomizeRoomDescription();
                AssignNewRoomToOld(newRoom);
                return newRoom;
            }
            else if (newRoomType == RoomType.QUEST)
            {
                StatisticsTracker.RoomsExplored++;
                if (GameManager.timeSinceLastQuestRoom >=2)
                {
                    GameManager.timeSinceLastQuestRoom = 0;
                    if(QuestManager.hasFoundCamp == false)
                    {
                        QuestManager.hasFoundCamp = true;
                        string npcDescription = "She Wears A Worn Light Blue And White Dress, Her Blond Hair Is Tied Back In A Bun. She Stands By A Fire Behind A Wooden Table, Noding her Head As She Talks To An Armoured Woman. As You Approach She Thanks The Woman, Sending Her Away.";
                        ZorinNPC zorin = new ZorinNPC("The Young Woman", npcDescription);
                    
                        npcDescription = "Sitting Against A Wooden Box, The Man Has A Dark Beard, Scruffy Black Hair And Lightly Tanned Skin. He Wears Bandages Over His Left Eye, Chest And Left Arm.";
                        AmirNPC amir = new AmirNPC("The Injured Man", npcDescription);
                    
                        npcDescription = "The Man Is Tall And Muscular With Dark Chocolat Skin. Mixing A Large Cooking Pot Over A Small Fire, A Sword Is Holstered At His Side, Light Armoured Plates Cover His Shins And Forearms. This Is Contrasted By The Stained White Appron He Wears Over His Torso.";
                        RendfieldNPC rendfield = new RendfieldNPC("Man By The Cooking Pot", npcDescription);
                    
                        List<NPC> npcList = new List<NPC>() { zorin, amir, rendfield };
                    
                        string roomDescription = "You Step Into A Large Room, Lit By A Fire At Its Middle. Tents Extend All Around Its Extremities. A Dozen Or So People Work And Talk With One Another, Armed Guards Patrol The Opposite Entrance.";
                    
                        QuestRoom questRoom = new QuestRoom(southRoom, northRoom, eastRoom, westRoom, null, null, npcList);
                        questRoom.Id = GameManager.roomID;
                        GameManager.roomID++;
                        questRoom.IsCleared = true;
                        questRoom.Description = roomDescription;
                        AssignNewRoomToOld(questRoom);
                        questRoom.IsScanned = true;
                        questRoom.IsCamp = true;
                        return questRoom;
                    }
                    if(QuestManager.hasFoundBlackSmith == false)
                    {
                        QuestManager.hasFoundBlackSmith = true;
                        string npcDescription = "The man has a light complexion with red hair and a scruffy beard. He lies in a pool of his own blood propped up on a pillar. A burn scar circles his right eye. As you approach, he coughs, blood, dripping from his mouth into the puddle under him.";
                        DungeonEzekielNPC ezekiel = new DungeonEzekielNPC("The Dying Man", npcDescription);

                        List<NPC> npcList = new List<NPC>() { ezekiel };

                        string roomDescription = "The Room Is Small And Damp. A Large Crystal Formation Hangs From The Ceiling Overhead, Basking The Center Of The Chamber In A Faint Light, The Walls And Corners Still Obscured. Moss Covers Supporting Pillars And Water Drips, Leaving The Floor Wet And Cold. Propped Against One Of The Pillars Is A Red Haired Man, A Sword Runs Through His Gut. His Blood Drips To The Stone Floor, Mixing With The Shallow Puddle He Lays In, Turning It Black.";

                        QuestRoom questRoom = new QuestRoom(southRoom, northRoom, eastRoom, westRoom, null, null, npcList);
                        questRoom.Id = GameManager.roomID;
                        GameManager.roomID++;
                        questRoom.IsCleared = true;
                        questRoom.Description = roomDescription;
                        AssignNewRoomToOld(questRoom);
                        questRoom.IsScanned = true;
                        return questRoom;

                    }
                    if((QuestManager.hasFoundScouts == false && QuestManager.returnToCamp == true) || (QuestManager.hasFoundScouts == false && QuestManager.ezekielInCamp == true))
                    {
                        QuestManager.hasFoundScouts = true;
                        
                        string roomDescription = "You Extinguish Your Flame, Leaving The Dead To Rest.";

                        QuestRoom questRoom = new QuestRoom(southRoom, northRoom, eastRoom, westRoom, null, null, null);

                        questRoom.Id = GameManager.roomID;
                        GameManager.roomID++;
                        questRoom.IsCleared = true;
                        questRoom.Description = roomDescription;
                        AssignNewRoomToOld(questRoom);
                        questRoom.IsScanned = true;
                        questRoom.IsScoutRoom = true;
                        return questRoom;


                    }
                    if (QuestManager.hasReportedScouts == true)
                    {
                        GuardDragon dragon = new GuardDragon("The Guard Dragon", 100, 30, 22, 10, 12, 0,
                            new List<DamageTypes.damageType> { DamageTypes.damageType.PIERCING },
                        new List<DamageTypes.damageType> {DamageTypes.damageType.FIRE }, false, EnemySpecification.DRAGON);

                        string roomDescription = "A Beast of Beasts Stands Before You. The Dragon Towers High Above You, Its Red Scally Skin Glistening In The Rooms Golden Light. It Has Two Horns On Its Head And One One Its Snout, Pointed and Sharp. Two Wings Sprout From Its Back, Bony And Leathery, Each Equipped With A Claw. As It Roars, You Can See A Orange Glow Coming From The Inside Of Its Throat.";
                        dragon.Description = "The Dragon Towers High Above You, Its Red Scally Skin Glistening In The Rooms Golden Light. It Has Two Horns On Its Head And One One Its Snout, Pointed and Sharp. Two Wings Sprout From Its Back, Bony And Leathery, Each Equipped With A Claw. As It Roars, You Can See A Orange GlowComing From The Inside Of Its Throat.";
                        QuestRoom dragonRoom = new QuestRoom(southRoom, northRoom, eastRoom, westRoom, null, dragon, null);

                        dragonRoom.Id = GameManager.roomID;
                        GameManager.roomID++;
                        dragonRoom.Description = roomDescription;
                        AssignNewRoomToOld(dragonRoom);
                        dragonRoom.IsScanned = true;
                        dragonRoom.IsAltarRoom = true;
                        dragon.IsAware = true;
                        dragon.IsSleeping = false;
                        return dragonRoom;


                    }

                }
                
                GameManager.timeSinceLastQuestRoom++;
                int rng;
                //Create lootable furniture
                List<Searchable> lootables = new List<Searchable>();
                rng = random.Next(2, 6);

                lootables = SearchableCreator.CreateSearchable(rng);

                LootRoom newRoom = new LootRoom(southRoom, northRoom, eastRoom, westRoom, lootables, null, null);
                newRoom.Id = GameManager.roomID;
                GameManager.roomID++;
                newRoom.IsCleared = true;
                newRoom.Description = RandomizeRoomDescription();
                AssignNewRoomToOld(newRoom);
                return newRoom;

            }
            else
            {
                StatisticsTracker.RoomsExplored++;
                //Create an enemy for the room
                List<EnemyType> enemyList = new List<EnemyType> { EnemyType.SPIDER, EnemyType.KNIGHT, EnemyType.GARGOYLE, EnemyType.EYE, EnemyType.BONEDRIKE, EnemyType.MINOTAUR };
                int rng = random.Next(0, enemyList.Count);

                EnemyType selectedEnemy = enemyList[rng];
                newRoomTarget = EnemyCreator.CreateEnemy(selectedEnemy);

                //Create lootable furniture
                List<Searchable> lootables = new List<Searchable>();
                rng =random.Next(0, 4);

                lootables = SearchableCreator.CreateSearchable(rng);



                Room newRoom = new DungeonRoom(southRoom, northRoom, eastRoom, westRoom, lootables, newRoomTarget, null);
                newRoom.Description = RandomizeRoomDescription();
                newRoom.Id = GameManager.roomID;
                GameManager.roomID++;
                AssignNewRoomToOld(newRoom);
                return newRoom;
            }
        }
        private static RoomType DecideRoomType()
        {
            
            int randomRoomNum = random.Next(1, 26);

            if (randomRoomNum > 0 && randomRoomNum < 16)
            {
                return RoomType.DUNGEON;
            }
            else if (randomRoomNum >= 16 && randomRoomNum <= 22)
            {
                return RoomType.LOOT;
            }
            else
            {
                return RoomType.QUEST;
            }
        }


        private static string RandomizeRoomDescription()
        {
            int index = random.Next(0, randomDescript.Count);
            originalRoomdescript = randomDescript[index];
            return originalRoomdescript;
        }
        private static void AssignNewRoomToOld(Room newRoom)
        {
            if(exitDirection == Room.ConnectingDirections.NORTH)
            {
                GameManager.currentRoom.NorthConnectingRoom = newRoom;
            }
            else if(exitDirection == Room.ConnectingDirections.SOUTH)
            {
                GameManager.currentRoom.SouthConnectingRoom = newRoom;
            }
            else if (exitDirection == Room.ConnectingDirections.WEST)
            {
                GameManager.currentRoom.WestConnectingRoom = newRoom;
            }
            else if (exitDirection == Room.ConnectingDirections.EAST)
            {
                GameManager.currentRoom.EastConnectingRoom = newRoom;
            }
        }
        private static void ShuffleList<T>(List<T> directions)
        {
            
            int n = directions.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = directions[k];
                directions[k] = directions[n];
                directions[n] = value;
            }
        }
        public enum RoomType { DUNGEON, LOOT, QUEST }
        public enum EnemyType { SPIDER, KNIGHT, GARGOYLE, EYE, BONEDRIKE, MINOTAUR }

    }

}
