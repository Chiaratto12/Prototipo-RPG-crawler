using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability
{
    public string name;
    public string adjetive;
    public int cooldown;
    public string description;
}

public class Passive 
{
    public string name;
    public string adjetive;
    public string description;
}

public class AbilityList 
{
    public List<Ability> Ability;
}

public class PassiveList 
{
    public List<Passive> Passive;
}


public class AbilitysAndPassives : MonoBehaviour
{
    public GameController game;
    public int maxLife;
    public float evasionUp;
    public float critChanceUp;
    public float critDmgUp;
    public float defUp;
    public float strongAbility;
    public bool isCooldownLess;
    public bool isCooldownDouble;
    public bool block;
    public int isPoison;
    public bool lifeUp;
    public bool curseOfStrenght;

    //public int cLess;

    void Start() 
    {
        Reset();
    }

    void Update()
    {
        
        
        

        if(lifeUp == true)
        {
            
        }

        if(curseOfStrenght == true)
        {
            
        }
    }

    public void Ability(string ability) {
        switch (ability)
        {
            case "Normal Attack":
                //critic logic (by: Leo the Beast)
                if(Random.Range (0f, 1f) <= game.player.critRate / 100){ game.playerDamage = ((int)((float)game.player.atk * game.player.critDamage / 100)); game.isCritical = true;}//enemyLife = enemyLife - ((int)((float)player.atk * player.critDamage / 100));
                else game.playerDamage = game.player.atk;
                game.enemyLife -= game.playerDamage;
                break;
            case "UpperCut":
                if(Random.Range (0f, 1f) <= game.player.critRate / 100) {game.playerDamage = ((int)((float)game.player.atk * game.player.critDamage / 100 * (1.5f + (game.player.abilityPower / 100)))); game.isCritical = true;}
                else game.playerDamage = ((int)((float)game.player.atk * (1.5f + (game.player.abilityPower / 100))));
                game.enemyLife -= game.playerDamage;
                //Debug.Log(((int)((float)game.player.atk * (2f + (game.player.abilityPower / 100)))));
                break;
            case "Stone Barrier" :
                game.player.def = game.player.def + ((int)(((float)game.weaponDamage / 50) * (1 + (game.player.abilityPower / 100))));
                break;
            case "Fire Ball":
                game.playerDamage = ((int)((float)game.player.atk * (2f + (game.player.abilityPower / 100))));
                game.enemyLife -= game.playerDamage;
                //Debug.Log(((int)((float)game.player.atk * (2.5f + (game.player.abilityPower / 100)))));
                break;
            case "Dodge":
                evasionUp = evasionUp + (15f + (game.player.abilityPower / 100));
                //Debug.Log(game.player.dodgeChance + evasionUp);
                break;
            case "Slash":
                if(Random.Range (0f, 1f) <= game.player.critRate / 100) {game.playerDamage = ((int)((float)game.player.atk * game.player.critDamage / 100 * (1.35f + (game.player.abilityPower / 100)))); game.isCritical = true;}
                else game.playerDamage = ((int)((float)game.player.atk * (1.35f + (game.player.abilityPower / 100))));
                game.enemyLife -= game.playerDamage;
                break;
            case "Ice Spear":
                if(Random.Range (0f, 1f) <= game.player.critRate / 100) {game.playerDamage = ((int)((float)game.player.atk * game.player.critDamage / 100 * (1.75f + (game.player.abilityPower / 100)))); game.isCritical = true;}
                else game.playerDamage = ((int)((float)game.player.atk * (1.75f + (game.player.abilityPower / 100))));
                game.enemyLife -= game.playerDamage;
                break;
            case "Block" :
                defUp = ((float)game.weaponDamage / 75);
                game.player.def += (int)defUp;
                //game.player.def = game.player.def + ((int)(((float)game.weaponDamage / 10) * (1 + (game.player.abilityPower / 100))));
                //Debug.Log(game.player.def + (game.weaponDamage / 10));
                break;
            case "Vampirism" :
                if(Random.Range (0f, 1f) <= game.player.critRate / 100) {game.playerDamage = ((int)((float)game.player.atk * game.player.critDamage / 100)); game.isCritical = true;} //enemyLife = enemyLife - ((int)((float)player.atk * player.critDamage / 100));
                else game.playerDamage = game.player.atk;
                game.enemyLife -= game.playerDamage;
                game.player.actualLife = game.player.actualLife + ((int)(((float)game.enemyLife / 5) * (1 + (game.player.abilityPower / 100))));
                break;
            case "Stab" :
                if(Random.Range (0f, 1f) <= game.player.critRate / 100) {game.playerDamage = ((int)((float)game.player.atk * game.player.critDamage / 100 * (1.25f + (game.player.abilityPower / 100)))); game.isCritical = true;}
                else game.playerDamage = ((int)((float)game.player.atk * (1.25f + (game.player.abilityPower / 100))));
                game.enemyLife -= game.playerDamage;
                Debug.Log(game.playerDamage);
                break;
            case "Heal" :
                if(game.player.actualLife < game.player.maxLife) game.player.actualLife = game.player.actualLife + ((int)(((float)game.player.maxLife / 4) * (1 + (game.player.abilityPower / 100))));
                //game.enemyLife -= game.playerDamage;
                break;
            case "Lighting" :
                if(Random.Range (0f, 1f) <= game.player.critRate / 100) {game.playerDamage = ((int)((float)game.player.atk * game.player.critDamage / 100 * (2.25f + (game.player.abilityPower / 100)))); game.isCritical = true;}
                else game.playerDamage = ((int)((float)game.player.atk * (2.25f + (game.player.abilityPower / 100))));
                game.enemyLife -= game.playerDamage;
                break;
            case "Water Gun" :
                if(Random.Range (0f, 1f) <= game.player.critRate / 100) {game.playerDamage = ((int)((float)game.player.atk * game.player.critDamage / 100 * (1.5f + (game.player.abilityPower / 100)))); game.isCritical = true;}
                else game.playerDamage = ((int)((float)game.player.atk * (1.5f + (game.player.abilityPower / 100))));
                game.enemyLife -= game.playerDamage;
                break;
            case "Poison":
                if(Random.Range (0f, 1f) <= game.player.critRate / 100) {game.playerDamage = ((int)((float)game.player.atk * game.player.critDamage / 100)); game.isCritical = true;} //enemyLife = enemyLife - ((int)((float)player.atk * player.critDamage / 100));
                else game.playerDamage = game.player.atk;
                game.enemyLife -= game.playerDamage;
                isPoison = 2;
                break;
        }
    }

    //public void AbilityWeapon(string ability) {}

    public void Passive(string passive) {
       switch (passive)
        {
            case "Cooldown Down":
                isCooldownLess = true;
                break;
            case "Crit Damg Up":
                critDmgUp = 10f;
                game.player.critDamage = game.player.critDamage + critDmgUp;
                break;
            case "Magic Up":
                strongAbility = 15f;
                game.player.abilityPower = game.player.abilityPower + strongAbility;
                break;
            case "Curse of Magic":
                strongAbility = 40f;
                game.player.abilityPower = game.player.abilityPower + strongAbility;
                //game.player.dodgeChance = game.player.dodgeChance / 2;
                isCooldownDouble = true;
                break;
            case "Thorns":
                game.enemyLife = game.enemyLife - (game.armorDefense / 8);
                Debug.Log(game.enemyLife - (game.armorDefense / 8));
                break;
            case "Curse of Strength":
                curseOfStrenght = true;
                game.player.atk = game.player.atk + (game.player.atk / 4);
                game.player.def = (int)((float)game.player.def * 0.7);
                //Debug.Log(game.player.def / 2);
                break;
            case "Crit Chance Up":
                critChanceUp = 10f;
                game.player.critRate = game.player.critRate + critChanceUp;
                break;
            case "Regeneration":
                if(game.gameStat != GameStat.Start) game.player.actualLife = game.player.actualLife + (game.player.maxLife / 50);
                break;
            case "Life Up":
                lifeUp = true;
                game.player.maxLife = game.player.maxLife + (game.player.maxLife / 50);
                if(game.gameStat == GameStat.Start) game.player.actualLife = game.player.maxLife;
                break;
        } 
    }

    public void Reset() {
        if(curseOfStrenght == true)
        {
            game.player.atk = game.player.classAtk + game.weaponDamage;
            game.player.def = game.player.classDef + game.armorDefense;
        }
        if(lifeUp == true)
        {
           game.player.maxLife = maxLife;
        }
        if(strongAbility > 0f) game.player.abilityPower = game.player.abilityPower - strongAbility;
        if(critChanceUp > 0f)  game.player.critRate = game.player.critRate - critChanceUp;
        if(critDmgUp > 0f) game.player.critDamage = game.player.critDamage - critDmgUp;
        evasionUp = 0f;
        critChanceUp = 0f;
        critDmgUp = 0f;
        defUp = 0f;
        strongAbility = 0f;
        isCooldownDouble = false;
        isCooldownLess = false;
        lifeUp = false;
        curseOfStrenght = false;
        //isPoison = false;
        //block = false;
    }

    //public void PassiveArmor(string passive){}
}