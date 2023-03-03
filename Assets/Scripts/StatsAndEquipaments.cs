using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class StatsAndEquipaments : MonoBehaviour
{
    //classes
    public GameController game;

    //stats
    public Text levelBox;
    public Text lifeBox;
    public Text atkBox;
    public Text defBox;

    //weapon
    public Text weaponNameBox;
    public Text weaponTypeBox;
    public Text weaponDamageBox;

    //armor
    public Text armorNameBox;
    public Text armorTypeBox;
    public Text armorDefenseBox;

    //ability
    public Text abilityText;

    //enemy
    //public int enemyDefense;

    public Text enemyNameBox;
    public Text enemyLifeBox;
    public Text enemyDamageBox;
    public Text enemyDefenseBox;

    //drop
    public string dropName;
    public int dropStats;
    public string dropType;

    public Text dropNameBox;
    public Text dropStatsBox;
    //public Text dropTypeBox;

    //buttons
    public Button leftButton;
    public Button rightButton;

    void Start()
    {
        /*jsonScript = File.ReadAllText(".\\Assets\\Data\\PlayerData.json");
        player = JsonUtility.FromJson<Player>(jsonScript);*/
    }

    // Update is called once per frame
    void Update()
    {
        levelBox.text = "Level: " + game.player.level + " " + game.xp + "/" + game.actualXp;
        lifeBox.text = "Life: " + game.player.maxLife + "/" + game.player.actualLife;
        atkBox.text = "Attack: " + game.player.atk;
        defBox.text = "Defense: " + game.player.def;

        enemyNameBox.text = game.enemyName;
        enemyLifeBox.text = "Life: " + game.enemyLife;
        enemyDamageBox.text = "Damage: " + game.enemyDamage;

        armorNameBox.text = game.armorName;
        armorTypeBox.text = "Type: " + game.armorType;
        armorDefenseBox.text = "Damage: " + game.armorDefense;

        abilityText.text = game.abilityName;

        weaponNameBox.text = game.weaponName;
        weaponTypeBox.text = "Type: " + game.weaponType;
        weaponDamageBox.text = "Defense: " + game.weaponDamage;
        //if (actualLife > maxLife) actualLife = maxLife;

        if(game.gameStat == GameStat.Fight) {
            leftButton.gameObject.GetComponentInChildren<Text>().text = "Attack";
            rightButton.gameObject.GetComponentInChildren<Text>().text = game.abilityName;
        } else if (game.gameStat == GameStat.DropItem) {
            leftButton.gameObject.GetComponentInChildren<Text>().text = "Take";
            rightButton.gameObject.GetComponentInChildren<Text>().text = "Skip";
        }
    }

    public void UpdatedStats(){
        
        
    }
}
