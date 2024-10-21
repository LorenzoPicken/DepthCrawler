using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepthCrawlerGame.Items;

namespace DepthCrawlerGame.Rooms
{
    public class SearchableCreator
    {
        static string name;
        static bool isInfested;
        static Item loot;
        private static readonly Random random = new Random();
        public static List<Searchable> CreateSearchable(int numberOfSearchables)
        {
            List<Searchable> searchables = new List<Searchable>();
            List<string> names = new List<string>() { "Iron Chest", "Wooden Basket", "Large Vase", "Grand Bookshelf", "Leather Pouch", "Skeletal Remains", "Abandonned Bucket", "Wall Fissure"};
            for(int i =0; i < numberOfSearchables; i++)
            {
                int rng = random.Next(1, names.Count - 1);
                name = names[rng];

                rng =  random.Next(0, 5);
                if(rng == 0)
                {
                    isInfested = true;
                }
                else
                {
                    isInfested = false;
                }

                loot = RandomizeLoot();

                Searchable searchable = new Searchable(name, loot, isInfested);
                searchables.Add(searchable);


            }

            return searchables;
        }

        private static Item RandomizeLoot()
        {
            int rng = random.Next(1, 101);

            if(rng > 0 && rng < 50)
            {
                return null;
            }
            else if(rng >=50 && rng < 85)
            {
                int rng2 = random.Next(1, 5);

                if(rng2 == 1)
                {
                    HealthPotion smallHealthPotion = new HealthPotion("Small Health Potion", (GameManager.PlayerInstance.Maxhp * 1 / 4));
                    return smallHealthPotion;
                }
                else if(rng2 == 2)
                {
                    ManaPotion smallManaPotion = new ManaPotion("Small Mana Potion", (GameManager.PlayerInstance.MaxMana * 1 / 4));
                    return smallManaPotion;
                }
                else if(rng2 == 3)
                {
                    StaminaPotion smallStaminaPotion = new StaminaPotion("Small Stamina Potion", (GameManager.PlayerInstance.Fatigue * 1 / 5));
                    return smallStaminaPotion;
                }
                else
                {
                    PoisonCurePotion poisonCurePotion = new PoisonCurePotion("Poison Antidote Potion", 0);
                    return poisonCurePotion;
                }
            }
            else if(rng >=85 && rng < 96)
            {
                int rng2 = random.Next(1, 5);

                if(rng2 == 1)
                {
                    HealthPotion largelHealthPotion = new HealthPotion("Large Health Potion", (GameManager.PlayerInstance.Maxhp /2));
                    return largelHealthPotion;
                }
                else if (rng2 == 2)
                {
                    ManaPotion LargeManaPotion = new ManaPotion("Large Mana Potion", (GameManager.PlayerInstance.MaxMana /2));
                    return LargeManaPotion;
                }
                else if (rng2 == 3)
                {
                    StaminaPotion largeStaminaPotion = new StaminaPotion("Large Stamina Potion", (GameManager.PlayerInstance.Fatigue * 3/4));
                    return largeStaminaPotion;
                }
                else
                {
                    RageDust rageDust = new RageDust("Rage Dust", 0);
                    return rageDust;
                }
            }
            else if(rng >=96 && rng <= 100)
            {
                int rng2 = random.Next(1, 3);

                if(rng2 == 1)
                {
                    RestStone restStone = new RestStone("Rest Stone", 0);
                    return restStone;
                }
                else
                {
                    ClothOfTheImmovable cloth = new ClothOfTheImmovable("Cloth Of The Immovable", 0);
                    return cloth;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
