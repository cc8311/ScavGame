
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MainScavScript : MonoBehaviour
{

    public Player player;
    public Enemy enemy;
    public Weapon weapon;

    public int riskLevel = 50;
    public int npcLevel = 50;
    public int rem1;
    public int rem2;

    // Start is called before the first frame update
    void Start()
    {
        
          //name,health,armor,damage,speed,distance,closechance,ranged,accuracy,critchance
        enemy = new Enemy("drifter", 50, 50, 5, 5, 20, 0, false, 0, 20);
        //name,type,ranged,damage,-crit,critchance,critmin,critmax,acuracy,health,armor,speed
        weapon = new Weapon("Knife", "melee", false, 5, 0, 20, 20, 50, 0, 5, 5, 5);
        //enemy,name,weapon,morale,health,armor,melee,strength,marksman,dext,speed,agility,defense,intell,stealth,luck,loot
        player = new Player(enemy, "player", weapon, 10, 100, 100, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10);



        //ScavStart();

        FightMain();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int dam)
    {
        int r = Random.Range(0, 100);
        if (r <= player.Enemy.Accuracy)
        {
            UnityEngine.Debug.Log("Miss");
        }
        else
        {
            if (player.Enemy.Ranged == true)
            {
                if (CriticalChanceEnemy())
                {
                    dam = dam * 3;
                    UnityEngine.Debug.Log("enemy crit!");
                }
                int rem1 = dam - player.Armor;
                if (player.Armor > 0)
                {
                    if (dam == player.Armor)
                    {
                        player.Armor = 0;
                        return;
                    }
                    if (dam < player.Armor)
                    {
                        player.Armor -= dam;
                    }
                    if (dam > player.Armor)
                    {
                        rem1 = (dam - player.Armor);
                        player.Armor = 0;
                        if (rem1 >= player.Health)
                        {
                            Death();
                        }
                        if (rem1 < player.Health)
                        {
                            player.Health -= rem1;
                        }

                    }
                    player.Morale -= (dam / 20);
                    //player.Armor = 0;
                    if (player.Health <= 0)
                    {
                        Death();
                    }
                }
                else
                {
                    if (dam >= player.Health)
                    {
                        Death();
                    }
                    if (dam < player.Health)
                    {
                        player.Health -= rem1;
                    }
                }
                //EnemyMovement();
            }
            else if (player.Enemy.Ranged == false)
            {
                if (player.Enemy.Distance <= 0)
                {
                    if (CriticalChanceEnemy())
                    {
                        dam = dam * 3;
                        UnityEngine.Debug.Log("enemy crit!");
                    }
                    int rem1 = dam - player.Armor;
                    if (player.Armor > 0)
                    {
                        if (dam == player.Armor)
                        {
                            player.Armor = 0;
                            return;
                        }
                        if (dam < player.Armor)
                        {
                            player.Armor -= dam;
                        }
                        if (dam > player.Armor)
                        {
                            rem1 = (dam - player.Armor);
                            player.Armor = 0;
                            if (rem1 >= player.Health)
                            {
                                Death();
                            }
                            if (rem1 < player.Health)
                            {
                                player.Health -= rem1;
                            }

                        }
                        player.Morale -= (dam / 20);
                        //player.Armor = 0;
                        if (player.Health <= 0)
                        {
                            Death();
                        }
                    }
                    else
                    {
                        if (dam >= player.Health)
                        {
                            Death();
                        }
                        if (dam < player.Health)
                        {
                            player.Health -= rem1;
                        }
                    }
                }
                else
                {
                    //EnemyMovement();
                }
            }
           
        }
        UnityEngine.Debug.Log("player armor" + player.Armor);
        UnityEngine.Debug.Log("player health" + player.Health);
    }
    public bool CriticalChance()
    {
        if (player.Enemy.Distance <= player.Weapon.CritRangeMax && player.Enemy.Distance >= player.Weapon.CritRangeMin)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CriticalChanceEnemy()
    {
        int r = Random.Range(0, 100);
        if (r <= player.Enemy.CriticalChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void GiveDamage(int dam)
    {
        int r = Random.Range(0, 100);
        if (r <= player.Weapon.Accuracy)
        {
            UnityEngine.Debug.Log("miss");
        }
        else
        {
            if (player.Weapon.Ranged == true)
            {
                if (CriticalChance())
                {
                    dam = dam * 3;
                    UnityEngine.Debug.Log("player crit!");
                }
                int rem2 = dam - player.Enemy.Armor;
                if (player.Enemy.Armor > 0)
                {
                    if (dam == player.Enemy.Armor)
                    {
                        player.Enemy.Armor = 0;
                        return;
                    }
                    if (dam < player.Enemy.Armor)
                    {
                        player.Enemy.Armor -= dam;
                    }
                    if (dam > player.Enemy.Armor)
                    {
                        rem2 = (dam - player.Enemy.Armor);
                        player.Enemy.Armor = 0;
                        if (rem2 > player.Enemy.Health)
                        {
                            EnemyDeath();
                        }
                        if (rem2 < player.Enemy.Health)
                        {
                            player.Enemy.Health -= rem2;
                        }

                    }
                    //player.Enemy.Armor = 0;
                    if (player.Enemy.Health <= 0)
                    {
                        EnemyDeath();
                    }
                }
                else 
                {
                    if (dam >= player.Enemy.Health)
                    {
                        EnemyDeath();
                    }
                    if (dam < player.Enemy.Health)
                    {
                        player.Enemy.Health -= rem2;
                    }
                }
                EnemyMovement();

            }
            else if (player.Weapon.Ranged == false)
            {
                if (CriticalChance())
                {
                    dam = dam * 3;
                    UnityEngine.Debug.Log("player crit!");
                }
                int rem2 = dam - player.Enemy.Armor;
                if (player.Enemy.Armor > 0)
                {
                    if (dam == player.Enemy.Armor)
                    {
                        player.Enemy.Armor = 0;
                        return;
                    }
                    if (dam < player.Enemy.Armor)
                    {
                        player.Enemy.Armor -= dam;
                    }
                    if (dam > player.Enemy.Armor)
                    {
                        rem2 = (dam - player.Enemy.Armor);
                        player.Enemy.Armor = 0;
                        if (rem2 > player.Enemy.Health)
                        {
                            EnemyDeath();
                        }
                        if (rem2 < player.Enemy.Health)
                        {
                            player.Enemy.Health -= rem2;
                        }

                    }
                    //player.Enemy.Armor = 0;
                    if (player.Enemy.Health <= 0)
                    {
                        EnemyDeath();
                    }
                }
                else
                {
                    if (dam >= player.Enemy.Health)
                    {
                        EnemyDeath();
                    }
                    if (dam < player.Enemy.Health)
                    {
                        player.Enemy.Health -= rem2;
                    }
                }
                EnemyMovement();
            }
            
        }
        UnityEngine.Debug.Log("enemy distance" + player.Enemy.Distance);
        UnityEngine.Debug.Log("enemy armor" + player.Enemy.Armor);
        UnityEngine.Debug.Log("enemy health" + player.Enemy.Health);
    }
    public void EnemyMovement()
    {
        if (player.Enemy.Distance <= 0)
        {
            //pass
        }
        else
        {
            player.Enemy.Distance = player.Enemy.Distance - player.Enemy.Speed;
            if (player.Enemy.Distance <= 0)
            {
                player.Enemy.Distance = 0;
            }
        }
        
    }
    public void EvadeAttack()
    {
        int stealth = player.Stealth + player.Intelligence + player.Luck;
        int attack = Random.Range(0, 100);
        if (attack <= stealth)
        {
            UnityEngine.Debug.Log("evaded enemy!");
        }
        else
        {
            ScavLoot();
        }
    }
    public void SetEnemyDistance()
    {
        int r = Random.Range(0, 100);
        if (r <= player.Enemy.CloseChance)
        {
            player.Enemy.Distance = 0;
        }
        else
        {
            int j = Random.Range(30, 150);
            player.Enemy.Distance = j;
        }
    }
    public void ScavStart()
    {
        int risk = Random.Range(0, 100);
        if (risk <= riskLevel)
        {
            UnityEngine.Debug.Log("Spotted!");
            UnityEngine.Debug.Log(risk);
            EvadeAttack();
        }
        else
        {
            //continue
            UnityEngine.Debug.Log("Cont");
            UnityEngine.Debug.Log(risk);
            ScavNpc();
        }
    }
    public void ScavNpc()
    {
        int npc = Random.Range(1, 100);
        if (npc <= npcLevel)
        {
            UnityEngine.Debug.Log("npc!");
        }
        else
        {
            UnityEngine.Debug.Log("no npc");
            ScavLoot();
        }
    }
    public void ScavLoot()
    {
        UnityEngine.Debug.Log("Scav Loot");
    }

    public void FightMain()
    {
        StartCoroutine(Fight());
    }
    public IEnumerator Fight()
    {
        GiveDamage(player.Weapon.Damage);
        TakeDamage(player.Enemy.Damage);
        yield return new WaitForSeconds(1);
        FightMain();
    }
    public void EscapeFight()
    {
        //pass
    }
    public void Death()
    {
        StopAllCoroutines();
        UnityEngine.Debug.Log("enemy wins");
    }
    public void EnemyDeath()
    {
        StopAllCoroutines();
        UnityEngine.Debug.Log("player wins");
    }

    public class Player
    {
        public Enemy Enemy;
        public string Name;
        public Weapon Weapon;
        public int Morale;
        public int Health;
        public int Armor;

        public int Melee;
        public int Strength;

        public int Marksman;
        public int Dexterity;

        public int Speed;
        public int Agility;
        public int Defense;

        public int Intelligence;
        public int Stealth;
        public int Luck;
        public int Loot;

        public Player(Enemy enemy, string na, Weapon w, int mo, int he, int ar, int me, int st, int ma, int de, int sp, int ag, int df, int it, int sl, int lu, int lo)
        {
            Enemy = enemy;
            Name = na;
            Weapon = w;
            Morale = mo;
            Health = he;
            Armor = ar;

            Melee = me;
            Strength = st;

            Marksman = ma;
            Dexterity = de;

            Speed = sp;
            Agility = ag;
            Defense = df;

            Intelligence = it;
            Stealth = sl;
            Luck = lu;
            Loot = lo;
        }

    }
    public class Enemy
    {
        public string Name;
        public int Health;
        public int Armor;
        public int Damage;
        public int Speed;
        public int Distance;
        public int CloseChance;
        public bool Ranged;
        public int Accuracy;
        public int CriticalChance;

        public Enemy(string na, int he, int ar, int da, int sp, int di, int cc, bool ra, int ac, int cch)
        {
            Name = na;
            Health = he;
            Armor = ar;
            Damage = da;
            Speed = sp;
            Distance = di;
            CloseChance = cc;
            Ranged = ra;
            Accuracy = ac;
            CriticalChance = cch;
        }

    }
    public class Weapon
    {
        public string Name;
        public string Type;
        public bool Ranged;
        public int Damage;
        public int Critical;
        public int CriticalChance;
        public int CritRangeMin;
        public int CritRangeMax;
        public int Accuracy;
        public int Health;
        public int Armor;
        public int Speed;

        public Weapon(string na, string ty, bool ra, int da, int cc, int crl, int crh, int cr, int ac, int he, int ar, int sp)
        {
            Name = na;
            Type = ty;
            Ranged = ra;
            Damage = da;
            Critical = Damage * 3;
            CriticalChance = cc;
            CritRangeMin = crl;
            CritRangeMax = crh;
            Accuracy = ac;
            Health = he;
            Armor = ar;
            Speed = sp;
        }

    }

}