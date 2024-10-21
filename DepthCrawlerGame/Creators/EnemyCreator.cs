using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepthCrawlerGame.Entities.Enemies;
using DepthCrawlerGame.Entities;
using DepthCrawlerGame.Creators;

namespace DepthCrawlerGame.Rooms
{
    public class EnemyCreator
    {
        static string name;
        static string description;
        readonly static Random random = new Random();

        public static Enemy CreateEnemy(RoomCreator.EnemyType enemyType)
        {
            
            if (enemyType == RoomCreator.EnemyType.SPIDER)
            {
                List<string> spiderNames = new List<string> { "ScytheTooth Spider", "Red-Eyed Dungeon Spider", "Weeping Widow's Spinner", "TenLegged Spine Spider", "Giant BroodMother" };

                List<string> spiderDescriptions = new List<string> { "It has Two, Long, Thin, Razor Sharp Fangs, The Size Of Human Arms. Through It's Translucent Skin, You Can See Organs And Blood Vessels Squirm, Confirming That The Creature is Indeed Alive.",
                "It’s Wide, Brimstone Colored Body Is Contrasted By Light Red Accents Which Define Its Joints. The Glow Of Over 20 Eyes Shine Through Its Thin Eyelids, Varied In Size.",
                "Black Like The Void, It Hangs, Suspended In Its Own Webbing, Perched On A Thick Line Of Twine By Its Back Legs. Draped Over The Spider’s Back, Like A Cape Of Sorts, Is A Thin Layer Of Web Which Gives The Illusion Of White Accents Against Its Black Shell.",
                "Sharp Protrusions Cover Its Entire Body, Creating A Protective Shell. Delicate Areas Like The Legs And Abdomen Having Even More Pronounced Spikes. An Extra Set Of Rear Legs Stretch High Above The Rest Of The Body, Most Likely Used For Pouncing Short Distances.",
                "The Spider Towers Over Anything You Have Ever Seen Before. Its Swollen Abdomen Drooping As It Shifts Its Weight, The Vivid Red Accents That Cover It Serving As Warning Signs For Approaching Foes."};

                AttributeRandomizer(spiderNames, spiderDescriptions);
                DungeonSpider spider = new DungeonSpider(name, 28, 15, 20, 25, 12, 6, 
                    new List<DamageTypes.damageType> { DamageTypes.damageType.BLUNT, DamageTypes.damageType.FIRE }, 
                    new List<DamageTypes.damageType> { DamageTypes.damageType.SLASHING, DamageTypes.damageType.PIERCING }, true, EnemySpecification.SPIDER);

                spider.Description += description;
                RandomizeEnemyState(spider);

                if (spider.IsSleeping)
                {
                    spider.Description += " The Large Beast Seems To Be In A Deep Sleep.";
                }
                else if(spider.IsAware == false)
                {
                    spider.Description += " It Faces Away From You, Not Having Noticed Your Approach.";
                }
                else if(spider.IsAware == true)
                {
                    spider.Description += " Its Many Eyes Remain Fixated On You As You Approach.";
                }
                EnemyScaler.ScaleEnemy(spider);
                
                
                
                return spider;
            }
            else if (enemyType == RoomCreator.EnemyType.KNIGHT)
            {
                List<string> knightNames = new List<string> { "Fallen Knight", "NightStrider Assassin", "Crested Royal Night" };

                List<string> knightDescriptions = new List<string> {"The Metal Plates That Make Up Its Armour Are Cracked And Rusted, The Leather Between The Joints, Discoloured And Faded. The Suit No Doubt Having Been Through Decades of Harsh Battles.",
                "Wearing Mainely Chainmail, Mended With Some Armoured Plates And Dark Fabrics, The Knight’s Silhouette Morph And Merges With The Dark Shadows Of The Room. An Open Face Helmet Under A Black Hood Reveals An Endless Void Where The Face Should Be From Which Visible Cold Breath Disperces.",
                "It Wears Armour Befitting Of A Noble, Decorated With Feathers And Intricate Designs. A Large Shield Is Attached To The Knight’s Back, Displaying The Crest Of An Unknown Master."};

                AttributeRandomizer(knightNames, knightDescriptions);
                Knight knight = new Knight(name, 32, 18, 20, 18, 15, 0,
                    new List<DamageTypes.damageType> { DamageTypes.damageType.BLUNT }, 
                    new List<DamageTypes.damageType> { DamageTypes.damageType.PIERCING, DamageTypes.damageType.FIRE }, false, EnemySpecification.KNIGHT);

                knight.Description += description;
                RandomizeEnemyState(knight);

                if (knight.IsSleeping)
                {
                    knight.Description += " Kneeling and Unmoving, The Night Does Not React to Your Approach.";
                }
                else if (knight.IsAware == false)
                {
                    knight.Description += " Concentrated On The Entrance Opposite To You, Its Sword Remains Sheathed.";
                }
                else if (knight.IsAware == true)
                {
                    knight.Description += " Drawing Its Sword, It Readies Itself, Hands Tightly Gripping The Weapon's Hilt.";
                }
                EnemyScaler.ScaleEnemy(knight);
                

                return knight;
            }
            else if (enemyType == RoomCreator.EnemyType.GARGOYLE)
            {
                List<string> gargoyleNames = new List<string> { "StoneFur Gargoyle", "RazorClaw Gargoyle" };
                List<string> gargoyleDescription = new List<string> { "Its Grey Leathery Skin Is Similar In Color To Stone, Making It Hard To See In The Cave Like Environment. Approximately The Size Of A Infant Child, It Hangs Upside Down By Its Talons.",
                "The Gargoyle, Unlike Others Of Its Kind, Has 6 Limbs, The Arms And Wings Separated From One Another. Long, Knife Like Claws Protrude From Its Fingers and Toes, Anchoring It To The Wall. "};

                AttributeRandomizer(gargoyleNames, gargoyleDescription);
                Gargoyle gargoyle = new Gargoyle(name, 18, 12, 10, 30, 20, 4,
                    new List<DamageTypes.damageType> { DamageTypes.damageType.PIERCING, DamageTypes.damageType.SLASHING },
                    new List<DamageTypes.damageType> { DamageTypes.damageType.FIRE }, true, EnemySpecification.GARGOYLE);

                gargoyle.Description += description;
                RandomizeEnemyState(gargoyle);

                if (gargoyle.IsSleeping)
                {
                    gargoyle.Description += " Wings Covering Its Face, Its Breathing Remains Deep And Steady.";
                }
                else if (gargoyle.IsAware == false)
                {
                    gargoyle.Description += " It Licks Its Wings And Paws, Which It Uses To Rub Its Head. As The Gargoyle Bathes Itself, You Enter The Chamber Unnoticed.";
                }
                else if (gargoyle.IsAware == true)
                {
                    gargoyle.Description += " Upon Spotting You, The Gargoyle Drops From Its Prech, Screetching And Taking Flight.";
                }
                EnemyScaler.ScaleEnemy(gargoyle);

                

                return gargoyle;
            }
            else if (enemyType == RoomCreator.EnemyType.EYE)
            {
                description = "The Large Eye Rests Atop A Pack Of Tenticle Like Tendrils Which It Uses To Move Around. It Gaze Is Unmoving, As Its Pupil Contracts And Dilates.";
                Eye eye = new Eye("Seeker Occulus", 15, 16, 5, 15, 60, 8,
                    new List<DamageTypes.damageType> { DamageTypes.damageType.PIERCING, DamageTypes.damageType.SLASHING, DamageTypes.damageType.FIRE },
                    new List<DamageTypes.damageType> { DamageTypes.damageType.BLUNT }, true, EnemySpecification.EYE);

                eye.Description += description;
                RandomizeEnemyState(eye);
                if (eye.IsSleeping)
                {
                    eye.Description += " Its Gaze Seems To Look Right Through You, The Monster Most Likely Having Fallen Alseep, Unable To Close Its Eye Lacking An Eyelid.";
                }
                else if (eye.IsAware == false)
                {
                    eye.Description += " Raising And Lowering Itself Using Its Tendrils, The " + eye.Name + " Is Too Fixated On Its Activity For Now To Notice Your Presence.";
                }
                else if (eye.IsAware == true)
                {
                    eye.Description += " As You Approach, Its Pupil Beggins To Dart Around the Room Taking In All That It Can See Before, Finally, Resting On You. What It Has Planned In Those Few Seconds, You Have No Idea.";
                }
                EnemyScaler.ScaleEnemy(eye);

                
                return eye;
            }
            else if (enemyType == RoomCreator.EnemyType.BONEDRIKE)
            {
                description = "The Creature, Entirely Made Of Bones, Spans A Dozen Feet Long, Its Body Consisting Of Many Large Vertebrae. The Head Resembles A Human Skull With Empty Eye Sockets And Long Thin Dry Black Hair Sprouting From Its Cranium. All Across Its Body Are Human Arms Varying In Size and Length, Which It uses As Legs To Move About.";
                Bonedrike bonedrike = new Bonedrike("Bonedrike", 22, 15, 15, 25, 15, 5,
                    new List<DamageTypes.damageType> { DamageTypes.damageType.BLUNT }, new List<DamageTypes.damageType> { DamageTypes.damageType.PIERCING }, false, EnemySpecification.BONEDRIKE);

                bonedrike.Description += description;
                RandomizeEnemyState(bonedrike);

                if (bonedrike.IsSleeping)
                {
                    bonedrike.Description += " It Hangs From The Cieling by Its Back Arms, Empty Eyes Looking In Your Direction, But Something Tells You Its Sleeping.";
                }
                else if (bonedrike.IsAware == false)
                {
                    bonedrike.Description += " Coiled Around Itself, The Bonedrike, Knaws On Its Own Arm. The Sound Of Bone Rubbing Bone Echos Through The Chamber, Masking The Sound Of Your Steps.";
                }
                else if (bonedrike.IsAware == true)
                {
                    bonedrike.Description += " Snaked Around A Pillar, It Watches You Enter The Chamber. Its Jaw Shift Side To Side, Grinding Its Teeth Eagerly.";
                }
                EnemyScaler.ScaleEnemy(bonedrike);

                

                return bonedrike;
            }
            else
            {
                List<string> minotaurNames = new List<string> { "Red-Haired Minotaur", "Bullman Gladiator", "Demi-human Executioner" };

                List<string> minotaurDescriptions = new List<string> { "Covered In Coarse Red Fur, It Wields A Crooked Greataxe, Bent At The Hilt. It’s Golden Nose Ring Sways As The Creature Breathes.",
                "On Its Head Is A Large Bronze Gladiator Helmet, Hiding Its Beastly Face. Scars Cover Its Chest, Arms And Back. One Of The Monster’s Great Horns Is Cracked Across The Middle, Held Together By A Sheet Of Pure Bronze Connecting Both Halves. A Long Axe Lays Strapped To The Bullman’s Back.",
                "It Wears A Dark Black Hood Over Its Head, Matching The Tar Like Color Of Its Fur. Its Horns Have Been Shortened And Dulled As If To Attempt To Hide The Monster’s Nature. It Carries A Heavy Bronze Executioner's Axe."};

                AttributeRandomizer(minotaurNames, minotaurDescriptions);
                Minotaur minotaur = new Minotaur(name, 20, 22, 16, 22, 10, 0,
                    new List<DamageTypes.damageType> { DamageTypes.damageType.SLASHING, DamageTypes.damageType.POISON },
                    new List<DamageTypes.damageType> { DamageTypes.damageType.BLUNT }, true, EnemySpecification.MINOTAUR);

                minotaur.Description += description;
                RandomizeEnemyState(minotaur);

                if (minotaur.IsSleeping)
                {
                    minotaur.Description += " Resting Against A Pillar, The " + minotaur.Name + " Snores, Shaking The Room, Causing Dust To Crumble From The Cieling.";
                }
                else if (minotaur.IsAware == false)
                {
                    minotaur.Description += " Using A Rock, It Sharpens It's Axe, Causing Sparks To Scatter Across The Cobble Floor. It's Too Absorbed In Its Craft To See You Approach.";
                }
                else if (minotaur.IsAware == true)
                {
                    minotaur.Description += " Seeing You In The Entrance, It Growls At You, Brandishing Its Axe.";
                }
                EnemyScaler.ScaleEnemy(minotaur);
                
                return minotaur;
            }
        }

        private static void AttributeRandomizer(List<string> names, List<string> descriptions)
        {
            int randomIndex = random.Next(0, names.Count);

            name = names[randomIndex];
            description = descriptions[randomIndex];
        }

        private static void RandomizeEnemyState(Enemy enemy)
        {
            int randomIndex = random.Next(1,4);

            if(randomIndex == 1)
            {
                enemy.IsSleeping = true;
            }
            else
            {
                int random2 = random.Next(1, 3);

                if(random2 == 1)
                {
                    enemy.IsAware = false;
                }
                else
                {
                    enemy.IsAware = true;
                }
            }

        }
    }
}
