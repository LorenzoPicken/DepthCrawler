using DepthCrawlerGame.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepthCrawlerGame.Ressources;

namespace DepthCrawlerGame.Entities
{
    public class Player: LivingEntity, IFatiguable
    {
        //Fields
        private int mana;
        private int maxMana;

        private int fatigue;
        private int maxFatigue = 100;

        private int stealth;


        private int currentStrength;
        private int currentDefense;
        private int currentAgility;

        private Enemy target;

        
        private bool isPoisoned = false;
        private bool cannotTakeDamage = false;
        private bool hasScannedRoom = false;
        

        private Items.Weapon currentEquippedWeapon;
        private Items.Weapon currentSecondaryWeapon;

        private List<Weapon> holsterInventory = new List<Weapon>();
        private Dictionary<Item, int> consumableInventory = new Dictionary<Item, int>();
        private List<string> journalContents = new List<string>();


        //Constructor
        public Player(string name, int hp, int strength, int defense, int agility, int accuracy, int mana, int fatigue, int stealth) : base(name, hp, strength, defense, agility, accuracy)
        {
            this.stealth = stealth;
            this.mana = mana;
            this.maxMana = mana;
            this.fatigue = fatigue;
        }


        //Accessors
        public int CurrentStrength { get => currentStrength; set => currentStrength = value; }
        public int CurrentDefense { get => currentDefense; set => currentDefense = value; }
        public int CurrentAgility { get => currentAgility; set => currentAgility = value; }
        public int Mana { get => mana; set => mana = value; }
        public int Fatigue { get => fatigue; set => fatigue = value; }
        public int Stealth { get => stealth; set => stealth = value; }

        public Weapon CurrentEquippedWeapon { get => currentEquippedWeapon; set => currentEquippedWeapon = value; }
        public Weapon CurrentSecondaryWeapon { get => currentSecondaryWeapon; set => currentSecondaryWeapon = value; }
        public Dictionary<Item, int> ConsumableInventory { get => consumableInventory; set => consumableInventory = value; }
        public Enemy Target { get => target; set => target = value; }
        public bool IsPoisoned { get => isPoisoned; set => isPoisoned = value; }
        public int MaxMana { get => maxMana; set => maxMana = value; }
        public int MaxFatigue { get => maxFatigue; set => maxFatigue = value; }
        public bool CannotTakeDamage { get => cannotTakeDamage; set => cannotTakeDamage = value; }
        public bool HasScannedRoom { get => hasScannedRoom; set => hasScannedRoom = value; }
        public List<string> JournalContents { get => journalContents; set => journalContents = value; }
        public List<Weapon> HolsterInventory { get => holsterInventory; set => holsterInventory = value; }





        //Functions
        public override void TakeDamage(int amount)
        {
            if(cannotTakeDamage == true)
            {
                cannotTakeDamage = false;
            }
            else
            {
                this.HP -= amount;
                if(this.HP <= 0)
                {
                    Die();
                }

            }

        }

        public void Block()
        {
            Console.WriteLine($"You Raise Your {this.currentEquippedWeapon.Name} In Preperation For an Attack.");
            this.IsBlocking = true;
        }

        public override void Die()
        {
            Console.Clear();
            
            if(this.target.Type == EnemySpecification.SPIDER)
            {
                Console.WriteLine($"Pouncing On You, The {this.target.Name} Delivers One Final Fatal Bite To Your Jugular, Before Wrapping Your Body In A Web Cocoon. The Poison, Flowing Throughout Your Now Dead Body, Begins To Melt Blood Vessels And Tissue, Slowly Turning What's Left Of Your Corpse Liquid.   You Will No Doubt Be Its Meal This Evening.");
            }
            else if(this.target.Type == EnemySpecification.EYE)
            {
                Console.WriteLine($"Latching Onto Your Face, The Creature Shoots A Beam Of Light From Its Giant Eye At Point Blank Into Your Retinas. The Light Is So Intense That You Can Feel Your Eyes Catching Fire Inside Your Skull. Your Legs Give Out As You Die From The Pain, Dropping To The Chamber Floor.");
            }
            else if(this.target.Type == EnemySpecification.GARGOYLE)
            {
                Console.WriteLine($"Like An Arrow, The {this.target.Name} Cuts Through The Air, Too Fast For You To Keep Up With, Slashing Away At Your Flesh With Every Pass By. Finally, Landing On Your Shoulder, It Rips Out Your Throat With Its Teeth While You're Still Dazed. As Blood Gushes From Your Neck, You Stumble Backwards Grabbing At The Creature, But You Are Fading Fast, Very Soon Plunging Into Dark Unconsciousness. You Die Moments Later.");
            }
            else if(this.target.Type == EnemySpecification.BONEDRIKE)
            {
                Console.WriteLine($"Pinning You Down To The Floor, The Bonedrike, Digs Its Bony Fingers Into Your Back And Begins To Strip Your Skin From Your Body. It Continues To Peel Your Flesh Even After You've Stopped Moving, Planning On Using Your Bones To Fix Its Broken Ones.");
            }
            else if (this.target.Type == EnemySpecification.KNIGHT)
            {
                Console.WriteLine($"Jabbing At Your Chest, Its Sword Pierces Your Heart. You Remain Standing, In Shock, Blood Dripping From The Sword. You Feel Your Heart Struggle To Beat, A Blade Now Wedged Through It Before Stopping All Together. As The {this.target.Name} Removes Its Weapon from Your Chest Cavity, You Drop To Your Knees, Vision Fading. Before You Pass, You Briefly see The Knight Take A Fencer Pose, Bowing As If To Show Its Respects For The Dual, As Everything Goes Dark.");
            }
            else if (this.target.Type == EnemySpecification.MINOTAUR)
            {
                Console.WriteLine($"Arm Bent At A Ninety Degree Angle, Hilt Of Its Axe Positioned Behind Its Back For Leverage, Hand High Near the Blade, The {this.target.Name} Steps Into Its Swing. It's Weapon Passes Through Your Waist The Dull Blade Struggling To Sever Your Torso From Your Legs Before Finally Passing All The Through, Killing You On Impact.  The Creature Lifts Your Legs Above Its Head Feasting On The Blood Gushing From Them.");
            }
            else if(this.target.Type == EnemySpecification.DRAGON)
            {
                Console.WriteLine($"As The Creature Sends A Wave Of Fire Your Way, You Are Unable To Find Cover, Instead Being Engulfed In Flames. Barely Alive You Remain Standing, Every Nerve In Your Body Screaming In Pain. Lowering Its Head towards You, The Creature Clamps Its Jaws Down On Your Head, Lifting You High In The Air Before Flicking You Up And Swallowing You Whole In One Bite.");
            }
        }

        public void Fatigued()
        {
            currentStrength = (this.Strength / 2) + CurrentEquippedWeapon.Damage; ;
            currentAgility = (this.Agility / 2) - CurrentEquippedWeapon.Weight;
            currentDefense = (this.Defense / 2) + currentEquippedWeapon.Defense;

            
        }

        public void Rested()
        {
            currentStrength = this.Strength + CurrentEquippedWeapon.Damage;
            currentAgility = this.Agility - CurrentEquippedWeapon.Weight;
            currentDefense = this.Defense + currentEquippedWeapon.Defense;

            
        }

        public void ChangeWeapon()
        {
            if(this.Fatigue >= 100)
            {
                currentStrength = (this.Strength / 2) + CurrentEquippedWeapon.Damage; ;
                currentAgility = (this.Agility / 2) - CurrentEquippedWeapon.Weight;
                currentDefense = (this.Defense / 2) + currentEquippedWeapon.Defense;
            }
            else
            {
                currentStrength = this.Strength + CurrentEquippedWeapon.Damage;
                currentAgility = this.Agility - CurrentEquippedWeapon.Weight;
                currentDefense = this.Defense + currentEquippedWeapon.Defense;
            }
        }
    }
}
