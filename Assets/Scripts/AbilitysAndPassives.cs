using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability
{
    public string name;
    public string adjetive;
    public int cooldown;
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
    public float evasionUp;
    public float critChanceUp;
    public float critDmgUp;
    public float defUp;
    public float strongAbility;
    public bool isCooldownLess;
    public bool isCooldownDouble;
    public bool block;
    public int isPoison;
    //public int cLess;

    void Start() 
    {
        Reset();
    }

    public void Ability(string ability) {
        switch (ability)
        {
            case "UpperCut":
                game.playerDamage = ((int)((float)game.player.atk * (1.5f + game.player.abilityPower / 10)));
                game.enemyLife -= game.playerDamage;
                //Debug.Log(((int)((float)game.player.atk * (2f + game.player.abilityPower / 10))));
                break;
            case "Stone Barrier" :
                game.player.def = game.player.def + ((int)(((float)game.weaponDamage / 100) * (1 + game.player.abilityPower / 10)));
                break;
            case "Fire Ball":
                game.playerDamage = ((int)((float)game.player.atk * (2f + game.player.abilityPower / 10)));
                game.enemyLife -= game.playerDamage;
                //Debug.Log(((int)((float)game.player.atk * (2.5f + game.player.abilityPower / 10))));
                break;
            case "Dodge":
                evasionUp = evasionUp + (15f + game.player.abilityPower / 10);
                //Debug.Log(game.player.dodgeChance + evasionUp);
                break;
            case "Slash":
                game.playerDamage = ((int)((float)game.player.atk * (1.35f + game.player.abilityPower / 10)));
                game.enemyLife -= game.playerDamage;
                break;
            case "Ice Spear":
                game.playerDamage = ((int)((float)game.player.atk * (1.75f + game.player.abilityPower / 10)));
                game.enemyLife -= game.playerDamage;
                break;
            case "Block" :
                //game.player.def = game.player.def + ((int)(((float)game.weaponDamage / 10) * (1 + game.player.abilityPower / 10)));
                //Debug.Log(game.player.def + (game.weaponDamage / 10));
                break;
            case "Vampirism" :
                game.playerDamage = game.player.atk;
                game.enemyLife -= game.playerDamage;
                game.player.actualLife = game.player.actualLife + ((int)(((float)game.enemyLife / 10) * (1 + game.player.abilityPower / 10)));
                break;
            case "Stab" :
                game.playerDamage = ((int)((float)game.player.atk * (1.25f + + game.player.abilityPower / 10)));
                game.enemyLife -= game.playerDamage;
                Debug.Log(game.playerDamage);
                break;
            case "Heal" :
                game.player.actualLife = game.player.actualLife + ((int)(((float)game.player.maxLife / 10) * (1 + game.player.abilityPower / 10)));
                game.enemyLife -= game.playerDamage;
                break;
            case "Lighting" :
                game.playerDamage = ((int)((float)game.player.atk * (2.25f + game.player.abilityPower / 10)));
                game.enemyLife -= game.playerDamage;
                break;
            case "Water Gun" :
                game.playerDamage = ((int)((float)game.player.atk * (1.5f + game.player.abilityPower / 10)));
                game.enemyLife -= game.playerDamage;
                break;
            case "Poison":
                game.playerDamage = game.player.atk;
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
                critDmgUp = critDmgUp + 10f;
                game.player.critDamage = game.player.critDamage + critDmgUp;
                break;
            case "Magic Up":
                strongAbility = 15f;
                game.player.abilityPower = game.player.abilityPower + strongAbility;
                break;
            case "Curse of Magic":
                strongAbility = 40f;
                //game.player.dodgeChance = game.player.dodgeChance / 2;
                isCooldownDouble = true;
                game.player.abilityPower = game.player.abilityPower + strongAbility;
                break;
            case "Thorns":
                game.enemyLife = game.enemyLife - (game.armorDefense / 8);
                Debug.Log(game.enemyLife - (game.armorDefense / 8));
                break;
            case "Curse of Strength":
                game.player.atk = game.player.atk + (game.player.atk / 25);
                game.player.def = game.player.def / 2;
                //Debug.Log(game.player.def / 2);
                break;
            case "Crit Chance Up":
                critChanceUp = critChanceUp + 10f;
                game.player.critRate = game.player.critRate + critChanceUp;
                break;
            case "Regeneration":
                if(game.gameStat != GameStat.Start) game.player.actualLife = game.player.actualLife + (game.player.maxLife / 50);
                break;
            case "Life Up":
                game.player.maxLife = game.player.maxLife + (game.player.maxLife / 50);
                if(game.gameStat == GameStat.Start) game.player.actualLife = game.player.maxLife;
                break;
        } 
    }

    public void Reset() {
        evasionUp = 0f;
        critChanceUp = 0f;
        critDmgUp = 0f;
        defUp = 0f;
        strongAbility = 0f;
        //isPoison = false;
        //block = false;
    }

    //public void PassiveArmor(string passive){}
}