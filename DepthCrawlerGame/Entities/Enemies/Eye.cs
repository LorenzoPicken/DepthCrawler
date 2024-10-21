using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Entities.Enemies
{
    public class Eye : Enemy
    {
        public Eye(string name, int hp, int strength, int defense, int agility, int accuracy, int selfPreservation, List<DamageTypes.damageType> weaknesses, List<DamageTypes.damageType> resistances, bool canBePoisoned, EnemySpecification type) : base(name, hp, strength, defense, agility, accuracy, selfPreservation, weaknesses, resistances, canBePoisoned, type)
        {

        }

        public override void Attack(int playerAgility, int enemyAgility)
        {
            if (playerAgility > enemyAgility && this.Target.IsBlocking == false)
            {
                Console.Clear();
                Console.WriteLine("Walking Backwards, Weapon At The Ready, You Sidestep And Duck A Flurry Of Rapid Tendril Strikes.");
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
                        Console.WriteLine($"As The Creature Grabs Your Weapon, You Kick It In The Center Of Its Eye, The Beast, Shuffling Back In Response.");
                        Console.ReadKey();
                        this.IsStunned = true;
                        this.TakeDamage(this.Target.CurrentStrength);
                    }
                    else if (blockHit >= 4 && blockHit < 6)
                    {
                        Console.WriteLine($"As It Flings Its Many Tendrils At You, You Struggle To Block Them All, Taking Blows To The Gut, Chest And Face.");
                        Console.ReadKey();
                        this.Target.TakeDamage(this.Strength);
                        this.IsStunned = true;
                    }
                    else
                    {
                        Console.WriteLine($"The {this.Name} Catches You Weapon, Before Pushing You Away. The Creature Remains In A Defensive Position.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    int damageNegation = this.Strength *2/3;
                    int hit = random.Next(1, 101);

                    if (hit > 0 && hit <= this.Accuracy)
                    {
                        //Headshot
                        Console.WriteLine($"Swipping At You Legs, The Creature Kncoks You To The Ground. As You Hit Your Head, It Strikes You In The Gut, Sending You Slidding Back.");
                        Console.ReadKey();
                        this.Target.TakeDamage(this.Strength);
                    }
                    else if (hit > 80 && hit <= 100)
                    {
                        //BodyShot
                        Console.WriteLine($"With SomeBuilt Up Momentum, The Creature, Drives Its Tendril Into Your Gut, Causing You To Stagger Backwards.");
                        Console.ReadKey();
                        this.Target.TakeDamage(damageNegation);
                    }
                    else
                    {
                        Console.WriteLine($"Using Its Limbs, The {this.Name} Whips Your Weapon Hand, Creating Red Mark, Your Hand Shaking From The Impact.");
                        Console.ReadKey();
                        this.Target.TakeDamage(Convert.ToInt32(damageNegation * 2 / 3));
                    }

                }

            }
        }

        public override void Flee(Rooms.Room room, int playerRoll, int enemyRoll)
        {
            if (enemyRoll > playerRoll)
            {
                Console.Clear();
                Console.WriteLine("Quickly, The Eye Scuttles To The Closest Wall, Climbing The Stone Surface. Unable To Hit It You watch As It Decends From Above An Exit, Crawling Upsidedown Through The Achway, Escaping Your Grasp.");
                Console.ReadKey();
                room.Enemy = null;

            }
            else
            {
                Console.Clear();
                Console.WriteLine("The Large Eye Begins To Dart Its Gaze Around the Room Intently, Looking For A Way Out. Unfortunately, It Doesnt Find A Good Opportunity To Flee.");
                Console.ReadKey();
            }
            this.HasAttemptedToFlee = true;
        }
        public override void Block()
        {
            Console.Clear();
            Console.WriteLine("Entangling Its Tendrils, The Giant Eye Uses Them As A Defensive Net In Front Of Itself.");
            Console.ReadKey();
            this.IsBlocking = true;
        }
        public override void Special1()
        {
            Console.Clear();
            string attack = $"Having Observed Your Fighting Throughout The Battle, The {this.Name} Beggins to Take Notice Of Your Habits. It's Becomes Harder To Hit As It Predicts Your Moves";

            this.Agility += 20;
            Console.WriteLine(attack);
            Console.ReadKey();
            this.HasUsedSpecial = true;


        }

        public override void Special2()
        {
            Console.Clear();
            string attack = $"The Giant Singular Pupil At The Creature's Center Contracts, Releasing A Bright Beam Of Concentrated Light. Disoriented You Stumble About Blindly. You Are Stunned";

            Console.WriteLine(attack);
            this.Target.IsStunned = true;
            Console.ReadKey();
            this.HasUsedSpecial = true;
        }

        public override void Die()
        {
            Console.Clear();
            Console.WriteLine($"Defeated, The {this.Name} Topples Over, Its Tendrils Flailing About. Blood Beggins To Fill Its Eye, As It Inflates Turnning More Red Each Moment. In A Loud Gushing Pops, The Creatures Eye Errupts Plastering You And The Surrounding Walls In Blood And Other Fluids.");
            Console.ReadKey();
            GameManager.currentRoom.Enemy = null;
            StatisticsTracker.EnemiesDefeated++;

        }
    }
}
