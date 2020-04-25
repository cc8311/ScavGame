using System.Collections;
using System.Collections.Generic;
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
        enemy = new Enemy("drifter", 50, 50, 5, 50);
        weapon = new Weapon("Knife", "melee", 5, 25, 10, 25);
        player = new Player(enemy, "player", weapon, 10, 100, 0, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10);



        //ScavStart();

        FightMain();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int dam)
    {
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
        UnityEngine.Debug.Log("player health" + player.Health);
    }
    public void GiveDamage(int dam)
    {
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
        UnityEngine.Debug.Log("enemy armor" + player.Enemy.Armor);
        UnityEngine.Debug.Log("enemy health" + player.Enemy.Health);
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
    public void ScavStart()
    {
        int risk = Random.Range(1, 100);
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
        //UnityEngine.Debug.Log("fightstart!");
        //StartCoroutine(FightP());
        StartCoroutine(FightE());
    }
    public IEnumerator FightE()
    {
        GiveDamage(player.Weapon.Damage);
        TakeDamage(player.Enemy.Damage);
        //UnityEngine.Debug.Log(player.Armor);
        //UnityEngine.Debug.Log("player health" + player.Health);
        //UnityEngine.Debug.Log("enemy health" + player.Enemy.Health);
        yield return new WaitForSeconds(1);
        FightMain();
    }
    public IEnumerator FightP()
    {

        GiveDamage(player.Weapon.Damage);
        //UnityEngine.Debug.Log(player.Enemy.Armor);
        UnityEngine.Debug.Log("enemy health" + player.Enemy.Health);
        yield return new WaitForSeconds(1);
        FightMain();
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

        public Enemy(string na, int he, int ar, int da, int sp)
        {
            Name = na;
            Health = he;
            Armor = ar;
            Damage = da;
            Speed = sp;
        }

    }
    public class Weapon
    {
        public string Name;
        public string Type;
        public int Health;
        public int Armor;
        public int Damage;
        public int Speed;

        public Weapon(string na, string ty, int he, int ar, int da, int sp)
        {
            Name = na;
            Type = ty;
            Health = he;
            Armor = ar;
            Damage = da;
            Speed = sp;
        }

    }
}
