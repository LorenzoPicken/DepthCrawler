using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepthCrawlerGame.Entities;
using DepthCrawlerGame;

namespace DepthCrawlerGame.Rooms
{
    public static class EnemyScaler
    {
        readonly static Random random = new Random();

        private static int totalPlayerLevel;
        private static int baseEnemyLevel;

        public static int TotalPlayerLevel { get => totalPlayerLevel; set => totalPlayerLevel = value; }
        public static int BaseEnemyLevel { get => baseEnemyLevel; set => baseEnemyLevel = value; }

        public static void ScaleEnemy(LivingEntity enemy)
        {
            TotalPlayerLevel = CalculatingPlayerLevel();

            BaseEnemyLevel = CalculateEnemyBaseLevel(enemy);

            ScaleMonster(enemy);


            
        }


        private static int CalculatingPlayerLevel()
        {
            int hp = GameManager.PlayerInstance.HP;
            int strength = GameManager.PlayerInstance.Strength;
            int defense = GameManager.PlayerInstance.Defense;
            int agility = GameManager.PlayerInstance.Agility;
            int accuracy = GameManager.PlayerInstance.Accuracy;

            int totalPlayerLevel = hp + strength + defense + agility + accuracy;
            return totalPlayerLevel;
        }

        private static int CalculateEnemyBaseLevel(LivingEntity enemy)
        {
            int hp = enemy.HP;
            int strength = enemy.Strength;
            int defense = enemy.Defense;
            int agility = enemy.Agility;
            int accuracy = enemy.Accuracy;

            int totalEnemyLevel = hp + strength + defense + agility + accuracy;

            return totalEnemyLevel;
        }

        private static void ScaleMonster(LivingEntity enemy)
        {
            int randomNewMonsterLevel = random.Next(TotalPlayerLevel - 15, TotalPlayerLevel-5);

            while(BaseEnemyLevel < randomNewMonsterLevel)
            {
                enemy.HP += 1;
                enemy.Maxhp += 1;
                enemy.Strength += 1;
                enemy.Defense += 1;
                enemy.Agility += 1;
                enemy.Accuracy += 1;

                BaseEnemyLevel = CalculateEnemyBaseLevel(enemy);
            }
        }
    }
}
