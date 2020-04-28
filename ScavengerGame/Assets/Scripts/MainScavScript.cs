

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


public class MainScavScript : MonoBehaviour
{
    public Text PlayerInfoPanel;
    public Text EnemyInfoPanel;
    public Text playeratts;
    public Text enemyatts;
    public Text dist;
    public Text acchead;
    public Text accbody;
    
    public Text bodyatt;
    public Text headatt;
    public Player player;
    public Enemy enemy;
    public Weapon weapon;

    public int riskLevel = 50;
    public int npcLevel = 50;
    public int rem1;
    public float rem2;
    float Cdamm;
    float Cdam;

    // Start is called before the first frame update
    void Start()
    {
        
        //name,health,armor,damage,speed,distance,closechance,ranged,accuracy,critchance
        enemy = new Enemy("drifter", 1000, 1000, 5, 5, 20, 0, false, 0, 20);
        //name,type,ranged,damagehead,damagebody,-crit,critchance,critmin,critmax,acuracyhead,acuraccybody,health,armor,speed
        weapon = new Weapon("Knife", "melee", false, 50, 10, 20, 5, 50, 0, 5, 75, 5, 5, 5);
        //enemy,name,weapon,morale,health,armor,melee,strength,marksman,dext,speed,agility,defense,inttell,stealth,luck,loot
        player = new Player(enemy, "player", weapon, 10, 100, 100, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, false);

        PlayerInfoPanel.text = "Start";
        //headatt.text = (player.Weapon.AccuracyHead + player.Marksman)  + "%";
        //bodyatt.text = (player.Weapon.AccuracyBody + player.Marksman) + "%";
        //ScavStart();

        //FightMain();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void WeaponAccuracyCheck()
    {
        if (player.Weapon.Ranged == true)
        {
            headatt.text = (player.Weapon.AccuracyHead + player.Marksman) + "%";
            bodyatt.text = (player.Weapon.AccuracyBody + player.Marksman) + "%";
        }
        if (player.Weapon.Ranged == false)
        {
            headatt.text = (player.Weapon.AccuracyHead + player.Melee) + "%";
            bodyatt.text = (player.Weapon.AccuracyBody + player.Melee) + "%";
        }
    }
    public void GiveDamage(float dam)
    {
        {
            if (player.Weapon.Ranged == true)
            {
                dam = dam + player.Dexterity;
                if (CriticalChance())
                {
                    dam = dam * 3;
                    UnityEngine.Debug.Log("player crit!");
                }
                float rem2 = dam - player.Enemy.Armor;
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
                if (CriticalChance())
                {
                    dam = dam * 3;
                    UnityEngine.Debug.Log("player crit!");
                }
                float rem2 = dam - player.Enemy.Armor;
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

        }

        //PlayerInfoPanel.text = "Player attacked for " + dam;
        UnityEngine.Debug.Log("enemy distance" + player.Enemy.Distance);
        UnityEngine.Debug.Log("enemy armor" + player.Enemy.Armor);
        UnityEngine.Debug.Log("enemy health" + player.Enemy.Health);
    }
    public void TakeDamage(float damm)
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
                float rem1 = damm - player.Armor;
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
                    float rem1 = damm - player.Armor;
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


        //EnemyInfoPanel.text = "Enemy attacked for " + damm;
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
    public void PlayerTurnResults(float dam)
    {
        if (player.Missed == true)
        {
            if (player.Weapon.Ranged == false && !EnemyClose())
            {
                PlayerInfoPanel.text = "ooot of range";
            }
            else
            {
                PlayerInfoPanel.text = "Player attacked for " + dam;
            }
            playeratts.text = "Health " + player.Health + "\n Armor " + player.Health;
            dist.text = "Distance " + player.Enemy.Distance;
            WeaponAccuracyCheck();
        }
        else
        {
            PlayerInfoPanel.text = "Player Missed!";
            playeratts.text = "Health " + player.Health + "\n Armor " + player.Health;
            dist.text = "Distance " + player.Enemy.Distance;
            
            WeaponAccuracyCheck();

        }
        
        
        

    }
    public void EnemyTurnResults(float damm)
    {
        EnemyInfoPanel.text = "Enemy attacked for " + damm;
        enemyatts.text = "Enemy Health " + player.Enemy.Health + "\n Enemy Armor " + player.Enemy.Health;
        dist.text = "Distance " + player.Enemy.Distance;
    }
    public void PreGiveDamageHead()
    {
        if (player.Weapon.Ranged == true)
        {
            float r = Random.Range(0, 100);
            if (r > player.Weapon.AccuracyHead + player.Marksman)
            {
                player.Missed = true;
              
            }
            else
            {
                player.Missed = false;
            }
        }
       
        else if (player.Weapon.Ranged == false)
        {
            if (EnemyClose())
            {
                float r = Random.Range(0, 100);
                if (r > player.Weapon.AccuracyHead + player.Melee)
                {
                    player.Missed = true;
                }
                else
                {
                    player.Missed = false;
                }
            }
            else
            {
                player.Missed = true;
                PlayerInfoPanel.text = PlayerInfoPanel.text + "\nOut of range!";
            }
        }
        
    }
    public bool Ranged()
    {
        if (player.Weapon.Ranged == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void PreGiveDamageBody()
    {
        if (player.Weapon.Ranged == true)
        {
            float r = Random.Range(0, 100);
            if (r > player.Weapon.AccuracyBody + player.Marksman)
            {
                player.Missed = true;

            }
            else
            {
                player.Missed = false;
            }
        }
        
        else if (player.Weapon.Ranged == false)
        {
            if (EnemyClose())
            {
                float r = Random.Range(0, 100);
                if (r > player.Weapon.AccuracyBody + player.Melee)
                {
                    player.Missed = true;
                }
                else
                {
                    player.Missed = false;
                }
            }
            else
            {
                player.Missed = true;
                PlayerInfoPanel.text = PlayerInfoPanel.text + "\nOut of range!";
            }
            
        }
       
    }
    public bool EnemyClose()
    {
        if (player.Enemy.Distance <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
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
        float stealth = player.Stealth + player.Intelligence + player.Luck;
        float attack = Random.Range(0, 100);
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
            if (player.Missed == false)
            {
                GiveDamage(player.Weapon.DamageHead);

                TakeDamage(player.Enemy.Damage);
                EnemyMovement();
                PlayerTurnResults(Cdam);
                EnemyTurnResults(Cdamm);
            }
            else
            {
                TakeDamage(player.Enemy.Damage);
                EnemyMovement();
                PlayerTurnResults(Cdam);
                EnemyTurnResults(Cdamm);
            }
            

        }
        if (id == 1)
        {
            PreGiveDamageBody();
            if (player.Missed == false)
            {
                GiveDamage(player.Weapon.DamageBody);
                TakeDamage(player.Enemy.Damage);
                EnemyMovement();
                PlayerTurnResults(Cdam);
                EnemyTurnResults(Cdamm);
            }
            else
            {
                TakeDamage(player.Enemy.Damage);
                EnemyMovement();
                PlayerTurnResults(Cdam);
                EnemyTurnResults(Cdamm);
            }
            

        }
        //FightMain();
    }
    public void EscapeFight()
    {
        //pass
    }
    public void Death()
    {
        
        UnityEngine.Debug.Log("enemy wins");
    }
    public void EnemyDeath()
    {
        player.Enemy.Health = 0;
        player.Enemy.Armor = 0;
        UnityEngine.Debug.Log("player wins");
    }

    public class Player
    {
        public Enemy Enemy;
        public string Name;
        public Weapon Weapon;
        public float Morale;
        public float Health;
        public float Armor;

        public float Melee;
        public float Strength;

        public float Marksman;
        public float Dexterity;

        public float Speed;
        public float Agility;
        public float Defense;

        public float Intelligence;
        public float Stealth;
        public float Luck;
        public float Loot;
        public bool Missed;

        public Player(Enemy enemy, string na, Weapon w, float mo, float he, float ar, float me, float st, float ma, float de, float sp, float ag, float df, float it, float sl, float lu, float lo, bool mi)
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
            Missed = mi;
        }

    }
    public class Enemy
    {
        public string Name;
        public float Health;
        public float Armor;
        public float Damage;
        public float Speed;
        public float Distance;
        public float CloseChance;
        public bool Ranged;
        public float Accuracy;
        public float CriticalChance;

        public Enemy(string na, float he, float ar, float da, float sp, float di, float cc, bool ra, float ac, float cch)
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
    {//add efective range
        public string Name;
        public string Type;
        public bool Ranged;
        public float DamageHead;
        public float DamageBody;
        public float Critical;
        public float CriticalChance;
        public float CritRangeMin;
        public float CritRangeMax;
        public float AccuracyHead;
        public float AccuracyBody;
        public float Health;
        public float Armor;
        public float Speed;

        public Weapon(string na, string ty, bool ra, float dah, float dab, float cc, float crl, float crh, float cr, float ach, float acb, float he, float ar, float sp)
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