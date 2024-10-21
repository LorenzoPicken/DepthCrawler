using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Entities.Enemies
{
    public class Bonedrike: Enemy
    {
        public Bonedrike(string name, int hp, int strength, int defense, int agility, int accuracy, int selfPreservation, List<DamageTypes.damageType> weaknesses, List<DamageTypes.damageType> resistances, bool canBePoisoned, EnemySpecification type) : base(name, hp, strength, defense, agility, accuracy, selfPreservation, weaknesses, resistances, canBePoisoned, type)
        {

        }

        public override void Attack(int playerAgility, int enemyAgility)
        {
            if (playerAgility > enemyAgility && this.Target.IsBlocking == false)
            {
                Console.Clear();
                Console.WriteLine("As The Creature Attempts To Ensnare You, You Leap Over Its Bony Body, Towards A Safer Part Of The Room");
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
                        Console.WriteLine($"Crawling Towards You, It Opens Its Mouth, Bitting Onto Your {this.Target.CurrentEquippedWeapon.Name}. Prying It Free From The Bonedrike's Jaws, You Drive The Weapon Into The Underside Of Its Bony Chin, Creating Stress Marks In The Bone.");
                        Console.ReadKey();
                        this.IsStunned = true;
                        this.TakeDamage(this.Target.CurrentStrength);
                    }
                    else if (blockHit >= 4 && blockHit < 6)
                    {
                        Console.WriteLine("As You Block, The Creature Grabs You With Its Arms, Pulling You Into A Hugging Position Where It Squeezes You Tightly, Your Spine On The Verge Of Break Before It Releases You.");
                        Console.ReadKey();
                        this.Target.TakeDamage(this.Strength / 2);
                        this.IsStunned = true;
                    }
                    else
                    {
                        Console.WriteLine("You Successfully Block Its Many Attempts To Grab You Causing It To Retreat A Few Feet, Observing With Its Hollow Eye Sockets.");
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
                        Console.WriteLine("Leaping Over You Like A Breaching Whale, It Grabs You By The Hair Dragging You Across The Floor An Into Walls And Debris Before, Releasing You Tumbling On The Ground.");
                        Console.ReadKey();
                        this.Target.TakeDamage(this.Strength / 2);
                    }
                    else if (hit > 80 && hit <= 100)
                    {
                        //BodyShot
                        Console.WriteLine("Bitting Down On Your Torso With Its Jaw, It Shakes You Around Before, Throwing You To The Ground.");
                        Console.ReadKey();
                        this.Target.TakeDamage(damageNegation);
                    }
                    else
                    {
                        Console.WriteLine("As It Circles You Quickly, You Are Unaware Of Its Back Hands Which Approach Your Ankles, Pulling You Backwards, Face First Into The Stone Floor.");
                        Console.ReadKey();
                        this.Target.TakeDamage(Convert.ToInt32(damageNegation * 0.75));
                    }

                }

            }
        }

        public override void Block()
        {
            Console.Clear();
            Console.WriteLine("Wrapping Its Arms Around Its Body, The " +this.Name + " Creates A Protective Casing Around Its Vertebraes." );
            Console.ReadKey();
            this.IsBlocking = true;
        }

        public override void Flee(Rooms.Room room, int playerRoll, int enemyRoll)
        {
            if(enemyRoll > playerRoll)
            {
                Console.Clear();
                Console.WriteLine("It Raises Itself On Its Back Arms, Towering Over You. Then, In One Swift Motion, Leaps To The Ground, Its Hundreds Of Arms Helping It To Quickly Dart For The Door Behind You. It Snakes Around Pillars As It Flees Before Shooting Out Of The Chamber, Disapearing Into The Dark Corridors Of The Dungeon, The Patter Of Its Bony Limbs Echoing Down The Hall.");
                Console.ReadKey();
                room.Enemy = null;

            }
            else
            {
                Console.Clear();
                Console.WriteLine("The Bonedrike, Beginning To Tire, Makes An Effort To Flee Through The Passage At The Other Side Of The Chamber. Seeing This, You Plant Yourself In Front Of The Beast, Preventing It's Escape.");
                Console.ReadKey();
            }
            this.HasAttemptedToFlee = true;
        }

        public override void Special1()
        {
            string attack = $"Snatching You By The Ankle, The {this.Name} Lifts You Off The Ground. Using Its Long Slender Body As A Whip, You Are Flung Upwards Into The Ceiling, Causing Rubble and Dust To Disperce Accross The Room";
            Console.WriteLine(attack);
            Console.ReadKey();
            this.Target.TakeDamage(this.Strength);
            this.HasUsedSpecial = true;
            

            


        }

        public override void Special2()
        {
            string attack = $"The {this.Name} Coils Around Your Body Like A Snake, Squeezing Your Frame Tightly As It Lifts You. Bringing Its Face To Yours, The {this.Name} Releases A Thick Black Mist Which Bellows From Its Mouth, Eyes And Nose, Forcing Its Way Into Your Lungs, Before Dropping You To The Ground Coughing. You Are Poisoned";


            Console.WriteLine(attack);
            Console.ReadKey();
            this.Target.IsPoisoned = true;
            this.HasUsedSpecial = true;
        }

        public override void Die()
        {
            Console.Clear();
            Console.WriteLine("With One Final Blow, The Creature's Skull Falls From Its Spine, Landing at Your Feet. The Rest Of The Body Begins To Crumble To Dust Until There Is Nothing Left But A Long Trail Of White Residue Snaking Across The Chamber.");
            GameManager.currentRoom.Enemy = null;
            Console.ReadKey();
            StatisticsTracker.EnemiesDefeated++;

        }
    }
}
