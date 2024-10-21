using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Entities.Enemies
{
    public class Knight: Enemy
    {
        public Knight(string name, int hp, int strength, int defense, int agility, int accuracy, int selfPreservation, List<DamageTypes.damageType> weaknesses, List<DamageTypes.damageType> resistances, bool canBePoisoned, EnemySpecification type) : base(name, hp, strength, defense, agility, accuracy, selfPreservation, weaknesses, resistances, canBePoisoned, type)
        {

        }

        public override void Attack( int playerAgility, int enemyAgility)
        {
            if(playerAgility > enemyAgility && this.Target.IsBlocking == false)
            {
                Console.Clear();
                Console.WriteLine("Just Barely Avoiding The Attack, You Jump Back As The Knight Brings Its Sword Down On You.");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();

                if (this.Target.IsBlocking)
                {
                    int blockHit = random.Next(1, 11);

                    if(blockHit > 0 && blockHit < 4)
                    {
                        Console.WriteLine("Parrying " + this.Name + "'s Sword, You Guide The Blade Aside, Creating An Oppening, Allowing For A Counter Attack.");
                        Console.ReadKey();
                        this.IsStunned = true;
                        this.TakeDamage(this.Target.CurrentStrength);
                    }
                    else if (blockHit >= 4 && blockHit < 6)
                    {
                        Console.WriteLine("Seeing Your Block, The Knight Delivers A Heavy Kick To Your Gut, Breaking Your Guard. It Then Delivers A Slashing Blow Across You Chest, Drawing Blood.");
                        Console.ReadKey();
                        this.Target.TakeDamage(this.Strength/2);
                        this.IsStunned = true;
                    }
                    else
                    {
                        Console.WriteLine("Your Weapons Clash, Sparks Flying As They Grind Together.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    int damageNegation = this.Strength * 2 / 3;
                    int hit = random.Next(1, 101);

                    if(hit >0 && hit<= this.Accuracy)
                    {
                        //Headshot
                        Console.WriteLine("Slashing At You, The Sharp Blade Catches Your Cheek, Leaving A Bleeding Gash.");
                        Console.ReadKey();
                        this.Target.TakeDamage(this.Strength/2);
                    }
                    else if (hit > 80 && hit <= 100)
                    {
                        //BodyShot
                        Console.WriteLine("The " + this.Name + " Swings Its Weapon, Which Hacks At Your Side. The Blade Makes Deep Cut.");
                        Console.ReadKey();
                        this.Target.TakeDamage(damageNegation);
                    }
                    else
                    {
                        Console.WriteLine("The Knight Swings Its Sword, Lodging It Into Your Forarm, Cracking Bone.");
                        Console.ReadKey();
                        this.Target.TakeDamage(Convert.ToInt32(damageNegation * 0.75));
                    }

                }

            }
        }

        public override void Block()
        {
            Console.Clear();
            Console.WriteLine("Leading Its Right Foot, The Knight Angles Its Sword Horizontaly In Front Of Its Body, Attempting To Block Any Attacks That May Come Its Way.");
            Console.ReadKey();
            this.IsBlocking = true;
        }
        public override void Special1()
        {
            string attack = $"Reaching Into A Leather Pouch Around Its Waist, The {this.Name} Pulls Out A Glass Vial And Pours The Contents Down Into Its Helmet, Then, Throws The Vial At You. The {this.Name}'s Vitality Goes Up As The Glass Vial Shatters Upon Hitting Your Chest.";

            Console.WriteLine(attack);
            Console.ReadKey();
            this.HP += Maxhp / 3;
            this.Target.TakeDamage(1);
            this.HasUsedSpecial = true;

        }

        public override void Special2()
        {
            string attack = $"Bending Its Knees, The {this.Name} Gets Into A Defensive Stance. Changing It's Sword Grip, It Watches You, Unmoving, Waiting For A Response. The {this.Name}'s Defense and Evasion Raise";
            Console.WriteLine(attack);
            this.Agility += this.Agility / 4;
            this.Defense += this.Defense / 4;
            this.HasUsedSpecial = true;
            Console.ReadKey();
        }

        public override void Die()
        {
            Console.Clear();
            Console.WriteLine("The Knight Falls To Its Knees, The Armour Clanking Against The Stone Floor. From The Joints And Cracks In The Armour, Black Mist Flows Out And Onto The Ground Like A Fog.The Mist Then Merges With The Dark Corners Of The Chamber, As The Now Hollow set Of Armour, Falls Appart And Clatters To The Floor.");
            GameManager.currentRoom.Enemy = null;
            Console.ReadKey();
            StatisticsTracker.EnemiesDefeated++;

        }
    }
}
