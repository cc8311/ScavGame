
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


public class MainScavScript : MonoBehaviour
{
    public float EnemyArmor;
    public float PlayerDamage;

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
    float Pdam;
    float Edam;
    float ac;

    // Start is called before the first frame update
    void Start()
    {
        //string na, string ty, bool ra, float dam, float cc, float crl, float crh, float cr, float ach, float acb, float he, float ar, float sp, float amm, float mr, float mrp
        //name,health,armor,damage,speed,distance,closechance,ranged,accuracy,critchance
        enemy = new Enemy("drifter", 1000, 1000, 5, 5, 7, 0, false, 0, 20);
        //name,type,ranged,Damage,-crit,critchance,critmin,critmax,acuracyhead,acuraccybody,health,armor,speed, ammoamt,maxrange,maxrangepenalty
        weapon = new Weapon("Knife", "melee", true, 100, 100, 90, 5, 10, 50, 5, 75, 3, 3, 100, 5);
        //enemy,name,weapon,morale,health,armor,melee,strength,marksman,dext,speed,agility,defense,inttell,stealth,luck,loot
        player = new Player(enemy, "player", weapon, 10, 100, 100, 5, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, false);

        PlayerInfoPanel.text = "llllll";
        headatt.text = (player.Weapon.AccuracyHead + player.Marksman) + "%";
        bodyatt.text = (player.Weapon.AccuracyBody + player.Marksman) + "%";
        //ScavStart();
        EnemyArmor = player.Enemy.Health;
        PlayerDamage = player.Weapon.Damage;
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



    public float CriticalChance()
    {
        if (player.Enemy.Distance <= player.Weapon.CritRangeMax && player.Enemy.Distance >= player.Weapon.CritRangeMin)
        {
            int r = Random.Range(0, 100);
            if (r >= player.Weapon.CriticalChance + player.Luck)
            {
                return 1;
            }
            else
            {
                return 3;
            }
        }
        else
        {
            return 1;
        }
    }
    public bool CriticalChanceEnemy()
    {
        int r = Random.Range(0, 100);
        if (r >= player.Enemy.CriticalChance)
        {
            return false;
        }
        else
        {
            return true;
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
        public float Damage;
        public float CriticalChance;
        public float CritRangeMin;
        public float CritRangeMax;
        public float AccuracyHead;
        public float AccuracyBody;
        public float Health;
        public float Armor;
        public float Speed;
        public float AmmoAmount;
        public float MaxRange;
        public float MaxRangePenalty;

        public Weapon(string na, string ty, bool ra, float dam, float cc, float crm, float crmax, float ach, float acb, float he, float ar, float sp, float amm, float mr, float mrp)
        {
            Name = na;
            Type = ty;
            Ranged = ra;
            Damage = dam;
            CriticalChance = cc;
            CritRangeMin = crm;
            CritRangeMax = crmax;
            AccuracyHead = ach;
            AccuracyBody = acb;
            Health = he;
            Armor = ar;
            Speed = sp;
            AmmoAmount = amm;
            MaxRange = mr;
            MaxRangePenalty = mrp;
        }

    }
    public bool AmmoCheck()
    {
        if (player.Weapon.AmmoAmount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void AttackStart(float id)
    {
        PlayerInfoPanel.text = "";
        //UnityEngine.Debug.Log("start");
        //enemyatts.text = "Enemy Health " + player.Enemy.Health + "\n Enemy Armor " + player.Enemy.Armor;
        if (Ranged())
        {
            AttacksPerTurn(id);
           // PlayerInfoPanel.text = "";
            //UnityEngine.Debug.Log("start1");
        }
        else if (!Ranged())
        {
            if (EnemyClose())
            {
                AttacksPerTurn(id);
                //PlayerInfoPanel.text = "";
               // UnityEngine.Debug.Log("start2");
            }
            else if (!EnemyClose())
            {
                //out of range
                //UnityEngine.Debug.Log("outrange");
            }
        }
        
    }

    public void AttacksPerTurn(float id)
    {
        if (Ranged())
        {
            if (AmmoCheck())
            {
                StartCoroutine(AttackTimer(id));
                
                
            }
            else if (!AmmoCheck())
            {
                //out of ammo
            }

        }
        else if (!Ranged())
        {
            for (int i = 0; i < player.Weapon.Speed; i++)
            {
                StartCoroutine(AttackTimer(id));

            }

        }
        //EnemyMovement();
    }
    public void PlayerAttack(float id)
    {
        MissChance(id);
        if (!Miss(id))
        {
            float PD = (player.Weapon.Damage * id);
            PD = PD * CriticalChance();
            if (!WeaponRangeCheck())
            {
                PD -= MaxRangeDamPenalty();
            }
            if (Ranged())
            {
                player.Enemy.Health -= PD;

                
            }
            else if (!Ranged())
            {
                player.Enemy.Health -= PD;

                
            }

            EnemyArmor = player.Enemy.Health;
            PlayerDamage = player.Weapon.Damage;
            TurnResults(PD);

        }
        else
        {
            PlayerInfoPanel.text = PlayerInfoPanel.text + "\nPlayer Missed";
        }
        //CriticalRange(id);
        


    }
    public float MissChance(float id)
    {
        if (Ranged())
        {
            if (id == 1)
            {
                ac = player.Dexterity + player.Weapon.AccuracyBody;
            }

            else if (id == 2)
            {
                ac = player.Dexterity + player.Weapon.AccuracyHead;
            }
        }
        else //not ranged
        {
            if (id == 1)
            {
                ac = player.Melee + player.Weapon.AccuracyBody;
            }

            else if (id == 2)
            {
                ac = player.Melee + player.Weapon.AccuracyHead;
            }
        }
        return ac;

    }
    public float AccPenalty()
    {
        if (player.Enemy.Distance > player.Weapon.MaxRange)
        {
            float d = player.Enemy.Distance - player.Weapon.MaxRange;
            ac = ac - d * player.Weapon.MaxRangePenalty;
            return ac;
        }
        else
        {
            return 1;
        }
        UnityEngine.Debug.Log(ac);
  
    }
    public bool Miss(float id)
    {
        
        float p = MissChance(id) - AccPenalty();
        int r = Random.Range(0, 100);
        if (r <= ac)
        {
            UnityEngine.Debug.Log(ac);
            return false;
        }
        else
        {
            UnityEngine.Debug.Log(ac);
            return true;
        }
    }
    public bool WeaponRangeCheck()
    {
        if (player.Enemy.Distance <= player.Weapon.MaxRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public float MaxRangeDamPenalty()
    {
        if (player.Enemy.Distance > player.Weapon.MaxRange)
        {
            float p = player.Enemy.Distance - player.Weapon.MaxRange;
            return p;
        }
        else
        {
            return 0;
        }
    }
   
    IEnumerator AttackTimer(float id)
    {
        
        for (int i = 0; i < player.Weapon.Speed; i++)
        {
            //PlayerInfoPanel.text = "";
            PlayerAttack(id);
            yield return new WaitForSeconds(.5f);
        }
        //PlayerInfoPanel.text = "";

    }
    public void TurnResults(float PD)
    {
        dist.text = player.Enemy.Distance.ToString();
        enemyatts.text = "Enemy Health " + player.Enemy.Health + "\n Enemy Armor " + player.Enemy.Armor;
        PlayerInfoPanel.text = PlayerInfoPanel.text + "\nHit Enemy For " + PD; 

    }
}