using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Entities.Enemies
{
    public class DungeonSpider: Enemy
    {
        public DungeonSpider(string name, int hp, int strength, int defense, int agility, int accuracy, int selfPreservation, List<DamageTypes.damageType> weaknesses, List<DamageTypes.damageType> resistances, bool canBePoisoned, EnemySpecification type) : base(name, hp, strength, defense, agility, accuracy, selfPreservation, weaknesses, resistances, canBePoisoned, type)
        {

        }
        public override void Attack(int playerAgility, int enemyAgility)
        {
            if (playerAgility > enemyAgility && this.Target.IsBlocking == false)
            {
                Console.Clear();
                Console.WriteLine("Rolling Out Of The Way, The Spider's Leg Slams Into The Floor, Cracking the Ground And Kicking up Dust.");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();

                if (this.Target.IsBlocking)
                {
                    int blockHit = random.Next(1, 11);

                    if (blockHit > 0 && blockHit < 4)
                    {
                        Console.WriteLine($"Kocking The {this.Name}'s Mandible To The Side. You Bring Your Weapon Down On Its Other Fang, Blood Oosing from The Wound.");
                        Console.ReadKey();
                        this.IsStunned = true;
                        this.TakeDamage(this.Target.CurrentStrength);
                    }
                    else if (blockHit >= 4 && blockHit < 6)
                    {
                        Console.WriteLine("Using Its Heavy Body, The Spider Slams Its Leg Into You Defenses, Sending You Flying Into A Wall.");
                        Console.ReadKey();
                        this.Target.TakeDamage(this.Strength / 2);
                        this.IsStunned = true;
                    }
                    else
                    {
                        Console.WriteLine($"As The {this.Name} Brings Its Heavy Leg Down On You, You Kneele, Absorbing The Force, Before Pushing The Beast Back.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    int damageNegation = this.Strength * 2 / 3;
                    int hit = random.Next(1, 101);

                    if (hit > 0 && hit <= this.Accuracy)
                    {
                        //Headshot
                        Console.WriteLine("The Monster Charges At You, Slamming Into Your Shoulder As You Try Moving Out Of The Way. You Can feel It Momentarely Leave Its Socket Before Falling Back Into Place. You Are Put Off Balance.");
                        Console.ReadKey();
                        this.Target.TakeDamage(this.Strength / 2);
                    }
                    else if (hit > 80 && hit <= 100)
                    {
                        //BodyShot
                        Console.WriteLine("Dealing A Heavy Blow To Your Stomach With Its Front Leg, The Spider Sends You Tumbling To The Floor Winded.");
                        Console.ReadKey();
                        this.Target.TakeDamage(damageNegation);
                    }
                    else
                    {
                        Console.WriteLine($"Its Fang Hooking Onto Your Shirt, The {this.Name} Lifts You Off The Ground Before Slamming You Down Headfirst Into The Stone Flooring.");
                        Console.ReadKey();
                        this.Target.TakeDamage(Convert.ToInt32(damageNegation * 0.75));
                    }

                }

            }
        }

        public override void Block()
        {
            Console.Clear();
            Console.WriteLine("Tucking Its Legs Close To Its Body, The Spider Covers Its Abdoment And Head From Blows.");
            Console.ReadKey();
            this.IsBlocking = true;
            
        }
        public override void Special1()
        {
            string attack = $"The {this.Name} Shoots a Web, Hitting You Center Mass, Pinning You To The Floor. You Are Stunned.";
            HasUsedSpecial = true;
            this.Target.IsStunned = true;
            Console.WriteLine(attack);
            Console.ReadKey();


        }

        public override void Special2()
        {
            string attack = $"The {this.Name} Lunges At You As You Roll Out Of The Way, It's Fang Lightly Slicing Your Flesh. The Wound Begins To Burn. You Are Poisoned";
            HasUsedSpecial = true;
            Console.WriteLine(attack);
            this.Target.IsPoisoned = true;
            Console.ReadKey();
        }

        public override void Die()
        {
            Console.Clear();
            Console.WriteLine("As It Dies, The Spider Falls Over Onto Its Back, Legs Twitching About For A Few Seconds Before Curling Up And Tucking Into Its Body. It Emits A Small Hiss Before Going Still.");
            GameManager.currentRoom.Enemy = null;
            Console.ReadKey();
            StatisticsTracker.EnemiesDefeated++;

        }
    }
}
