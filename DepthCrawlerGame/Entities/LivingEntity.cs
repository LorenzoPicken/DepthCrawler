using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Entities
{
    public class LivingEntity: IDamageable, IHealable
    {
        private string name;

        private int hp;

        private int maxhp;

        private int strength;

        private int defense;

        private int agility;

        private int accuracy;

        private bool isBlocking = false;

        private bool hasAttemptedToFlee = false;

        private bool isStunned = false;

        readonly public Random random = new Random();


        public LivingEntity(string name, int hp, int strength, int defense, int agility, int accuracy)
        {
            this.name = name;
            this.hp = hp;
            this.maxhp = hp;
            this.Strength = strength;
            this.Defense = defense;
            this.Agility = agility;
            this.Accuracy = accuracy;
            
        }

       
        public int HP { get => hp; set => hp = value; }
        public int Strength { get => strength; set => strength = value; }
        public int Defense { get => defense; set => defense = value; }
        public int Agility { get => agility; set => agility = value; }
        public int Accuracy { get => accuracy; set => accuracy = value; }
        public int Maxhp { get => maxhp; set => maxhp = value; }
        public string Name { get => name; set => name = value; }
        public bool IsBlocking { get => isBlocking; set => isBlocking = value; }
        public bool HasAttemptedToFlee { get => hasAttemptedToFlee; set => hasAttemptedToFlee = value; }
        public bool IsStunned { get => isStunned; set => isStunned = value; }

        public void Heal(int amount)
        {

            this.HP += amount;
        }

        public virtual void TakeDamage(int amount)
        {
             this.HP -= amount;
            if(this.HP <= 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {

        }
    }
}
