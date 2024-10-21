using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthCrawlerGame.Items
{
    public class Weapon: Item
    {
        
        private bool isTwoHanded;
        private bool isSecondary;

        private ItemType type = ItemType.WEAPON;
        

        private int damage;
        private int weight;
        private int defense;

        private WeaponType weaponType;
        private DamageTypes.damageType damageType;

        private bool isRanged;

        public Weapon(string name, bool isTwoHanded, bool isSecondary, int damage, int weight, int defense, WeaponType weapon, DamageTypes.damageType damageType) : base(name)
        {
            this.damage = damage;
            this.weight = weight;
            this.defense = defense;
            this.weaponType = weapon;
            this.damageType = damageType;
            this.isTwoHanded = isTwoHanded;
            this.isSecondary = isSecondary;

            if(weapon == WeaponType.BOW)
            {
                isRanged = true;
            }
            else
            {
                isRanged = false;
            }

            
        }

        public bool IsRanged { get => isRanged; set => isRanged = value; }
        public bool IsTwoHanded { get => isTwoHanded; set => isTwoHanded = value; }
        public ItemType Type { get => type; set => type = value; }
        public bool IsSecondary { get => isSecondary; set => isSecondary = value; }
        public DamageTypes.damageType DamageType { get => damageType; set => damageType = value; }
        public int Damage { get => damage; set => damage = value; }
        public int Weight { get => weight; set => weight = value; }
        public int Defense { get => defense; set => defense = value; }

        public enum WeaponType { LONGSWORD, SHORTSWORD, DAGGER, AXE, HAMMER, SHIELD, BOW, FLAIL};

       
        
    }
}
