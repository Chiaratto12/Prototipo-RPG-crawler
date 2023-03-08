using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability
{
    public string name;
}

public class Passive 
{
    public string name;
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

    void Start() 
    {
        Reset();
    }

    public void Ability(string ability) {
        switch (ability)
        {
            case "Double Attack":
                game.enemyLife = game.enemyLife - ((int)((float)game.player.atk * (2f + strongAbility)));
                //Debug.Log(((int)((float)game.player.atk * (2f + strongAbility))));
                break;
            case "Fire Ball":
                game.enemyLife = game.enemyLife - ((int)((float)game.player.atk * (2.5f + strongAbility)));
                //Debug.Log(((int)((float)game.player.atk * (2.5f + strongAbility))));
                break;
            case "Evasion":
                evasionUp = evasionUp + 15f;
                //Debug.Log(game.player.dodgeChance + evasionUp);
                break;
            case "Slash":
                game.enemyLife = game.enemyLife - ((int)((float)game.player.atk * (3f + strongAbility)));
                break;
            case "Crit Up":
                critChanceUp = critChanceUp + 15f;
                Debug.Log(game.player.critRate + critChanceUp);
                break;
        }
    }

    //public void AbilityWeapon(string ability) {}

    public void Passive(string passive) {
       switch (passive)
        {
            case "Def Up":
                defUp = defUp + 0.1f;
                //Debug.Log((int)((float)game.player.def * (1f + defUp)));
                game.player.def = (int)((float)game.player.def * (1f + defUp));
                break;
            case "Crit Damg Up":
                critDmgUp = critDmgUp + 10f;
                game.player.critDamage = game.player.critDamage + critDmgUp;
                break;
            case "Strong Abilitys":
                strongAbility = 0.15f;
                break;
            case "Evasion Up":
                evasionUp = 10f;
                game.player.dodgeChance = game.player.dodgeChance + evasionUp;
                Debug.Log(game.player.dodgeChance);
                break;
            case "Thorns":
                game.enemyLife = game.enemyLife - (game.armorDefense / 8);
                Debug.Log(game.enemyLife - (game.armorDefense / 8));
                break;
        } 
    }

    public void Reset() {
        evasionUp = 0f;
        critChanceUp = 0f;
        critDmgUp = 0f;
        defUp = 0f;
        strongAbility = 0f;
    }

    //public void PassiveArmor(string passive){}
}