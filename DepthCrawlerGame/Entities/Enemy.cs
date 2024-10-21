using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Entities
{
    public class Enemy: LivingEntity
    {
        private string description;

        private int selfPreservationLevel;

        private bool isAware = false;
        private bool isSleeping = false;
        private bool canBePoisoned;
        private bool isPoisoned = false;
        private bool hasUsedSpecial = false;

        private EnemySpecification type;
        


        private Player target;

        private List<DamageTypes.damageType> weaknesses = new List<DamageTypes.damageType>();
        private List<DamageTypes.damageType> resistances = new List<DamageTypes.damageType>();

        public Enemy(string name, int hp, int strength, int defense, int agility, int accuracy, int selfPreservationLevel, List<DamageTypes.damageType> weaknesses, List<DamageTypes.damageType> resistances, bool canBePoisoned, EnemySpecification type) : base(name, hp, strength, defense, agility, accuracy)
        {
            this.selfPreservationLevel = selfPreservationLevel;
            this.weaknesses = weaknesses;
            this.resistances = resistances;
            this.canBePoisoned = canBePoisoned;
            this.type = type;
        }

        public Player Target { get => target; set => target = value; }
        public bool IsPoisoned { get => isPoisoned; set => isPoisoned = value; }
        public string Description { get => description; set => description = value; }
        public bool IsAware { get => isAware; set => isAware = value; }
        public bool IsSleeping { get => isSleeping; set => isSleeping = value; }
        public bool HasUsedSpecial { get => hasUsedSpecial; set => hasUsedSpecial = value; }
        public EnemySpecification Type { get => type; }
        public List<DamageTypes.damageType> Weaknesses { get => weaknesses; }
        public List<DamageTypes.damageType> Resistances { get => resistances;}

        public virtual void Special1()
        {

        }

        public virtual void Special2()
        {

        }

        public virtual void Block()
        {

        }

        public virtual void Attack(int playerAgility, int enemyAgility)
        {

        }

        public virtual void Flee(Rooms.Room room, int playerRoll, int enemyRoll)
        {

        }

        public override void Die()
        {
            
        }
    }
}
