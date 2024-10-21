using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Entities.Enemies
{
    public class Minotaur : Enemy
    {
        public Minotaur(string name, int hp, int strength, int defense, int agility, int accuracy, int selfPreservation, List<DamageTypes.damageType> weaknesses, List<DamageTypes.damageType> resistances, bool canBePoisoned, EnemySpecification type) : base(name, hp, strength, defense, agility, accuracy, selfPreservation, weaknesses, resistances, canBePoisoned, type)
        {

        }

        public override void Attack(int playerAgility, int enemyAgility)
        {
            if (playerAgility > enemyAgility && this.Target.IsBlocking == false)
            {
                Console.Clear();
                Console.WriteLine("Ducking, The Beast's Axe Soars Over Your Head, Trimming The Hairs It Touches.");
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
                        Console.WriteLine($"Intecepting The Axe With Your Weapon, You Slide Your {this.Target.CurrentEquippedWeapon.Name} Up The Underside Of The Large Weapon And Swing It Into The Monster's Snout.");
                        Console.ReadKey();
                        this.IsStunned = true;
                        this.TakeDamage(this.Target.CurrentStrength);
                    }
                    else if (blockHit >= 4 && blockHit < 6)
                    {
                        Console.WriteLine("With Its Fist, The Minotaur Punches Through Your Guard, Sending You Rolling Backwards Several Meters.");
                        Console.ReadKey();
                        this.Target.TakeDamage(this.Strength / 2);
                        this.IsStunned = true;
                    }
                    else
                    {
                        Console.WriteLine($"Holding Your Weapon By The Hilt, The Other Hand's Palm On The Weapon's Head, You Deflect The Incoming Axe To The Left, Kicking It Away Further Afterwards. You Leap Back To Create Space.");
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
                        Console.WriteLine($"Charging You It Grabs You By The Waist And Slams You Into The Closest Wall Dragging you Along It For A Few Seconds, Then Kicks You Towards The Opposite Side Of The Room.");
                        Console.ReadKey();
                        this.Target.TakeDamage(this.Strength/2);
                    }
                    else if (hit > 80 && hit <= 100)
                    {
                        //BodyShot
                        Console.WriteLine($"As You Jump Away, The {this.Name} Releases It Hold On The Axe For A Moment, Gaining A Bit Of Distance On Its Swing, The Blade, Slashing Across Your Chest.");
                        Console.ReadKey();
                        this.Target.TakeDamage(damageNegation*3/4);
                    }
                    else
                    {
                        Console.WriteLine("The Dull Side Of The Monster's Axe Sweaps You Off Your Feet Causing You To Tumble To The Ground.");
                        Console.ReadKey();
                        this.Target.TakeDamage(Convert.ToInt32(damageNegation * 0.5));
                    }

                }

            }
        }

        public override void Block()
        {
            Console.Clear();
            Console.WriteLine("The " + this.Name + " Points Its Axe Towards Your Direction, Creating Distance Between The Both Of You.");
            Console.ReadKey();
            this.IsBlocking = true;
        }
        public override void Special1()
        {
            string attack = $"Flaring Its Nostrils, The {this.Name} Lets Out A Bellow As A Strong Aura Rips Through The Room, Kicking Up Dust and Shaking The Walls. Its Strength Increases With Its Rage";

            Console.WriteLine(attack);
            this.Strength += this.Strength *1/4;
            this.HasUsedSpecial = true;
            Console.ReadKey();


        }

        public override void Special2()
        {
            string attack = $"The {this.Name} Charges At You, Axe Winding Up For A Devastating Swing";
            Console.WriteLine(attack);
            Console.ReadKey();

            int successRate = random.Next(1, 5);

            if(successRate == 1 || successRate==2)
            {
                Console.WriteLine($"The Flat Side Of Its Axe, Connects With Your Shoulder, Causing You To Collide With And Destroy A Support Pillar.");
                this.Target.TakeDamage(Target.Maxhp / 4);
            }
            else
            {
                Console.WriteLine($"As You Leap BackWards Out Of The Way, The {this.Name}'s BattleAxe, Inbeds Itself Into The Floor, Inches From Where You Had Stood, Creating A Large Crack Which Extends Between Your Feet And Up The Wall Behind.");
            }
            this.HasUsedSpecial = true;
            Console.ReadKey();
        }

        public override void Die()
        {
            Console.Clear();
            Console.WriteLine($"Eyes Rolling Into The Back Of Its Head, The {this.Name} Drops Its Axe, Which Imbeds Itself Into The Floor From Its Weight. The Beast Then Falls Over With A Loud Thud, Shaking the Chamber Walls.");
            GameManager.currentRoom.Enemy = null;
            Console.ReadKey();
            StatisticsTracker.EnemiesDefeated++;

        }
    }

}
