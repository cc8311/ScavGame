
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


public class MainScavScript : MonoBehaviour
{
    public Text InfoPanel;

    public Player player;
    public Enemy enemy;
    public Weapon weapon;

    public int riskLevel = 50;
    public int npcLevel = 50;
    public int rem1;
    public int rem2;
    int Cdamm;
    int Cdam;

    // Start is called before the first frame update
    void Start()
    {
        
        //name,health,armor,damage,speed,distance,closechance,ranged,accuracy,critchance
        enemy = new Enemy("drifter", 50, 50, 5, 5, 20, 0, false, 0, 20);
        //name,type,ranged,damagehead,damagebody,-crit,critchance,critmin,critmax,acuracyhead,acuraccybody,health,armor,speed
        weapon = new Weapon("Knife", "melee", false, 50, 10, 20, 20, 50, 0, 100, 100, 5, 5, 5);
        //enemy,name,weapon,morale,health,armor,melee,strength,marksman,dext,speed,agility,defense,intell,stealth,luck,loot
        player = new Player(enemy, "player", weapon, 10, 10, 100, 100, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10);

        InfoPanel.text = "Welcome";

        //ScavStart();

        //FightMain();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damm)
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
                    damm = damm * 3;
                    UnityEngine.Debug.Log("enemy crit!");
                }
                int rem1 = damm - player.Armor;
                if (player.Armor > 0)
                {
                    if (damm == player.Armor)
                    {
                        player.Armor = 0;
                        return;
                    }
                    if (damm < player.Armor)
                    {
                        player.Armor -= damm;
                    }
                    if (damm > player.Armor)
                    {
                        rem1 = (damm - player.Armor);
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
                    player.Morale -= (damm / 20);
                    //player.Armor = 0;
                    if (player.Health <= 0)
                    {
                        Death();
                    }
                }
                else
                {
                    if (damm >= player.Health)
                    {
                        Death();
                    }
                    if (damm < player.Health)
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
                        damm = damm * 3;
                        UnityEngine.Debug.Log("enemy crit!");
                    }
                    int rem1 = damm - player.Armor;
                    if (player.Armor > 0)
                    {
                        if (damm == player.Armor)
                        {
                            player.Armor = 0;
                            return;
                        }
                        if (damm < player.Armor)
                        {
                            player.Armor -= damm;
                        }
                        if (damm > player.Armor)
                        {
                            rem1 = (damm - player.Armor);
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
                        player.Morale -= (damm / 20);
                        //player.Armor = 0;
                        if (player.Health <= 0)
                        {
                            Death();
                        }
                    }
                    else
                    {
                        if (damm >= player.Health)
                        {
                            Death();
                        }
                        if (damm < player.Health)
                        {
                            player.Health -= rem1;
                        }
                    }
                    Cdamm = damm;
                }
                
                else
                {
                    Cdamm = 0;
                }
            }
           
        }
        
        
        //InfoPanel.text = "Player attacked for " + damm;
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
    
    public void TurnResults(int damm, int dam)
    {
        InfoPanel.text = "Player attacked for " + dam + "\n Enemy attacked for " + damm + "\n Distance " + player.Enemy.Distance;
    }
    public void PreGiveDamageHead()
    {
        int r = Random.Range(0, 100);
        if (r > player.Weapon.AccuracyHead)
        {
            UnityEngine.Debug.Log("miss");

        }
        else
        {
            GiveDamage(player.Weapon.DamageHead);
        }
    }
    public void PreGiveDamageBody()
    {
        int r = Random.Range(0, 100);
        if (r > player.Weapon.AccuracyBody)
        {
            UnityEngine.Debug.Log("miss");
        }
        else
        {
            GiveDamage(player.Weapon.DamageBody);
        }
        
    }
    public void GiveDamage(int dam)
    {
        
        
        {
            if (player.Weapon.Ranged == true)
            {
                dam = dam + player.Marksman;
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
                //EnemyMovement();

            }
            else if (player.Weapon.Ranged == false)
            {

                dam = dam + player.Strength;
                if (player.Enemy.Distance <= 0)
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
                    Cdam = dam;
                }
               
                else
                {
                    Cdam = 0;
                }
            }
            
        }
        
        InfoPanel.text = "Enemy attacked for " + dam;
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

    
    
    public void temp(int id)
    {
        if (id == 0)
        {
            PreGiveDamageHead();
            TakeDamage(player.Enemy.Damage);
            EnemyMovement();
            TurnResults(Cdamm, Cdam);
        }
        if (id == 1)
        {
            PreGiveDamageBody();
            TakeDamage(player.Enemy.Damage);
            EnemyMovement();
            TurnResults(Cdamm, Cdam);
        }
        //FightMain();
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
        public int DamageHead;
        public int DamageBody;
        public int Critical;
        public int CriticalChance;
        public int CritRangeMin;
        public int CritRangeMax;
        public int AccuracyHead;
        public int AccuracyBody;
        public int Health;
        public int Armor;
        public int Speed;

        public Weapon(string na, string ty, bool ra, int dah, int dab, int cc, int crl, int crh, int cr, int ach, int acb, int he, int ar, int sp)
        {
            Name = na;
            Type = ty;
            Ranged = ra;
            DamageHead = dah;
            DamageBody = dab;
            Critical = DamageHead * 3;
            CriticalChance = cc;
            CritRangeMin = crl;
            CritRangeMax = crh;
            AccuracyHead = ach;
            AccuracyBody = acb;
            Health = he;
            Armor = ar;
            Speed = sp;
        }

    }

}