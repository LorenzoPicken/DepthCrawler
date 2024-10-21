using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepthCrawlerGame.Rooms;

namespace DepthCrawlerGame.Entities.Enemies
{
    public class Gargoyle: Enemy
    {
        public Gargoyle(string name, int hp, int strength, int defense, int agility, int accuracy, int selfPreservation, List<DamageTypes.damageType> weaknesses, List<DamageTypes.damageType> resistances, bool canBePoisoned, EnemySpecification type) : base(name, hp, strength, defense, agility, accuracy, selfPreservation, weaknesses, resistances, canBePoisoned, type)
        {

        }


        public override void Attack(int playerAgility, int enemyAgility)
        {
            if (playerAgility > enemyAgility && this.Target.IsBlocking == false)
            {
                Console.Clear();
                Console.WriteLine("As The Gargoyle Lunges For Your Head, You Slide Under The Creature, Just Barely Avoiding Its Talons.");
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
                        Console.WriteLine($"Wrestling You Weapon From The Creature's Claws, You Punch It In The Snout, Forcing It To Back Away.");
                        Console.ReadKey();
                        this.IsStunned = true;
                        this.TakeDamage(this.Target.CurrentStrength);
                    }
                    else if (blockHit >= 4 && blockHit < 6)
                    {
                        Console.WriteLine($"Getting Around Your {this.Target.CurrentEquippedWeapon.Name} The {this.Name} Latches Onto Your Face Clawing Away. Its Talons Scratch Up You Face And Split Your Eyelid Before You Are Able To Drive It Off.");
                        Console.ReadKey();
                        this.Target.TakeDamage(this.Strength);
                        this.IsStunned = true;
                    }
                    else
                    {
                        Console.WriteLine("Your Weapon Clashes With The Creature's Talons. The Sound Of The Clash Resonates Through The Chamber.");
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
                        Console.WriteLine($"Landing On Your Shoulder, It Bites Into Your Neck, Ripping Out A Piece In The Process. It Leaps Away Before You Can Caych It, Eating The Flesh.");
                        Console.ReadKey();
                        this.Target.TakeDamage(this.Strength*3/2);
                    }
                    else if (hit > 80 && hit <= 100)
                    {
                        //BodyShot
                        Console.WriteLine($"The {this.Name} Flies Around Your Strikes, Landing Forcefully On Your Chest, Driving Its Claws In Deep, Before Pouncing Off Back To Safety.");
                        Console.ReadKey();
                        this.Target.TakeDamage(this.Strength);
                    }
                    else
                    {
                        Console.WriteLine($"Diving Towards You, The {this.Name} Slashes, Its Claws Ripping Through The Skin On Your Forearm, And Up Your Shoulder.");
                        Console.ReadKey();
                        this.Target.TakeDamage(Convert.ToInt32(this.Strength*2/3));
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
            Console.WriteLine("Readying Its Talons, The Flying Beast Protects Itself Using Its Sharp Claws.");
            Console.ReadKey();
            this.IsBlocking = true;
        }

        public override void Special1()
        {
            string attack = $"Digging Its Sharp Claws Into The Ceiling, The {this.Name} Pulls Itself Into a Crouched Position Before Launching Itself Into A Nosedive, Hurtling Towards You ";
            Console.WriteLine(attack);
            int successRate = random.Next(1, 6);
            Console.ReadKey();
            Console.Clear();

            if(successRate == 1)
            {
                Console.WriteLine($"The Attack Strikes You In The Chest Sending You Tumbling Accross The Floor and Into A Wall");
                Console.ReadKey();
                Console.Clear();
                this.Target.TakeDamage(this.Strength * 2);
            }
            else
            {
                Console.WriteLine($"In An Instant, The Gargoyle Soar's Past You, Just Barely Missing Your Head, And Crashes Into The Wall Behind You.");
                Console.ReadKey();
                Console.Clear();
                this.TakeDamage(this.HP/2);
            }
        }

        public override void Special2()
        {
            string attack = $"Iritated by Your Presence, It Focuses All Its Energy Into Its Wings. It's Speed Increases ";

            Console.WriteLine(attack);
            Console.ReadKey();
            this.Agility += this.Agility / 2;
        }

        public override void Die()
        {
            Console.Clear();
            Console.WriteLine("The Monster Crashes Into The Wall Behind You Kicking Up Dust. As You Walk Over To The Injured Beast, It Spontaniously Catches Fire, Burning Blue. You Cover Your Face From The Heat, Raising Your Weapon, Expecting An Attack. Instead, As Quickly As It Had Started, It Ends, Leaving Behind Nothing But The Creatures Chared Outline On The Stone Flooring.");
            Console.ReadKey();
            GameManager.currentRoom.Enemy = null;
            StatisticsTracker.EnemiesDefeated++;

        }
    }
}
