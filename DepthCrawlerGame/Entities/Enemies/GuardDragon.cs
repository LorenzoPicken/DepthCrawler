using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Entities.Enemies
{
    public class GuardDragon : Enemy
    {
        public GuardDragon(string name, int hp, int strength, int defense, int agility, int accuracy, int selfPreservation, List<DamageTypes.damageType> weaknesses, List<DamageTypes.damageType> resistances, bool canBePoisoned, EnemySpecification type) : base(name, hp, strength, defense, agility, accuracy, selfPreservation, weaknesses, resistances, canBePoisoned, type)
        {

        }


        public override void Attack(int playerAgility, int enemyAgility)
        {
            if (playerAgility > enemyAgility && this.Target.IsBlocking == false)
            {
                Console.Clear();
                Console.WriteLine("As A Wall Of Flames Hurtle Your Way, You Duck Behind A Pillar, The Fire Wrapping Around You Cover, Just Barely Missing Your Arms.");
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
                        Console.WriteLine($"Pushing Against Your Weapon With Its Claw, The Dragon Attempts To Swipe At You With Its Other Paw. Seeing This, You Roll Underneath It, Allowing You To Land A Blow To Its Gut.  ");
                        Console.ReadKey();
                        this.IsStunned = true;
                        this.TakeDamage(this.Target.CurrentStrength);
                    }
                    else if (blockHit >= 4 && blockHit < 6)
                    {
                        Console.WriteLine($"Swipping Its Tail At You, The Force Is Too Much To Block. You Soar Into A Pillar Deeper In The Room");
                        Console.ReadKey();
                        this.Target.TakeDamage(this.Strength / 2);
                        this.IsStunned = true;
                    }
                    else
                    {
                        Console.WriteLine("Deflecting the Dragon's Talon, Its Claws Plunges Into The Floor Ripping Up Rock And Debris.");
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
                        Console.WriteLine($"Swinging At You With Its Wing, You Are Hit In The Head As You Try To Duck, Dropping To The Floor.");
                        Console.ReadKey();
                        this.Target.TakeDamage(this.Strength /2);
                    }
                    else if (hit > 80 && hit <= 100)
                    {
                        //BodyShot
                        Console.WriteLine($"Breathing A Fireball At You, The Mass Of Hot Flames Hits Your In The Chest, Severley Burning Your Skin.");
                        Console.ReadKey();
                        this.Target.TakeDamage(damageNegation);
                    }
                    else
                    {
                        Console.WriteLine($"The Dragon Attacks With Its Claw, Catching Your Shoulder In The Process. Your Arm Feels Broken, But You Can Still Move It.");
                        Console.ReadKey();
                        this.Target.TakeDamage(Convert.ToInt32(damageNegation *0.75));
                    }

                }

            }
        }

        public override void Flee(Rooms.Room room, int playerRoll, int enemyRoll)
        {
            if (enemyRoll > playerRoll)
            {
                Console.Clear();
                Console.WriteLine("Using Its Dense Body, The " + this.Name + " Leaps At You, Pushing You To The Floor. While You Recover, It Soars Into A Dark Corner Of The Room, Disapearing Through a Large Crack In The Wall.");
                Console.ReadKey();
                Console.WriteLine();
                Console.WriteLine("The Creature Has Fled");
                Console.ReadKey();
                room.Enemy = null;

            }
            else
            {
                Console.Clear();
                Console.WriteLine("Aiming for The Passageway Behind You, The Gargoyle Dives In Your Direction. Just Before Passing Your Head, You Catch It By The Leg, Stopping It In Its Tracks Before Throwing It Back In Front Of You. The Monster Catches Itself Just Before Hitting The Floor, Frustrated. ");
                Console.ReadKey();
            }
            this.HasAttemptedToFlee = true;
        }

        public override void Block()
        {
            Console.Clear();
            Console.WriteLine("The Beast Puts Forth Its Hard Claw, Reducing Your Angles Of Attack.");
            Console.ReadKey();
            this.IsBlocking = true;
        }

        public override void Special1()
        {
            string attack = $"Grabbing The Floor With Its Claw, It Pulls Up Causing The Ground To Break And Shift Under Your Feet. Your No Longer Have The Proper Footing To Fight At Full Capacity";
            Console.WriteLine(attack);
            
            Console.ReadKey();
            Console.Clear();
            this.Target.CurrentAgility = this.Target.CurrentAgility / 2;
            this.Target.CurrentDefense = this.Target.CurrentDefense * 2 / 3;

           
        }

        public override void Special2()
        {
            string attack = $"Flapping Its Wings, A Strong Gust Of Wind Knocks You Backwards Into The Wall. Stunning You and Breaking A Few Ribs ";

            Console.WriteLine(attack);
            Console.ReadKey();
            this.Target.TakeDamage(this.Strength /2);
            this.Target.IsStunned = true;
        }

        public override void Die()
        {
            Console.Clear();
            Console.WriteLine("Wailing, The Monsters Body Begins To Crack, Light, Shinning Through The Appearing Fissures In Its Skin. With A Loud Shatering Noise, The dragon Explodes Into A Bright Light, Leaving Floating Red Particles Suspended In The Air, Shimmering Before Fizzling Out.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Where The dragon Once Stood, The Altar Is Now Once Again Present. You Take The Final Artefact, Putting It In Your Bag.");
            Console.ReadKey();
            GameManager.currentRoom.Enemy = null;
            StatisticsTracker.EnemiesDefeated++;
            StatisticsTracker.ArtefactsCollected = 4;

        }
    }
}
