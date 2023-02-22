using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStat {Exploration, Fight, DropItem}
public class StatsAndEquipaments : MonoBehaviour
{
    //classes
    public GameController game;

    //stats
    public Text lifeBox;
    public Text atkBox;
    public Text defBox;

    public int maxLife;
    public int actualLife;
    public int atk;
    public int def;
    public float critRate;
    public float critDamage;
    public float dodgeChance;

    //weapon
    string weaponName;
    string weaponType;
    int weaponDamage;

    public Text weaponNameBox;
    public Text weaponTypeBox;
    public Text weaponDamageBox;

    //armor
    string armorName;
    string armorType;
    int armorDefense;

    public Text armorNameBox;
    public Text armorTypeBox;
    public Text armorDefenseBox;

    //ability
    string abilityName;
    int abilityDamage;
    public Text abilityText;

    //enemy
    public GameObject enemy;
    public string enemyName;
    public int enemyLife;
    public int enemyDamage;
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

    //controll
    public GameStat gameStat;

    void Start()
    {
        weaponName = "Basic Sword";
        weaponType = "Sword";
        weaponDamage = 25;

        armorName = "Basic Armor";
        armorType = "Heavy";
        armorDefense = 25;

        maxLife = actualLife;
        atk = atk + weaponDamage;
        def = def + armorDefense;

        abilityName = "Double Attack";
        abilityText.text = abilityName;
        abilityDamage = atk * 2;
        gameStat = GameStat.Exploration;
    }

    // Update is called once per frame
    void Update()
    {
        lifeBox.text = "Life: " + maxLife + "/" + actualLife;
        atkBox.text = "Attack: " + atk;
        defBox.text = "Defense: " + def;

        enemyNameBox.text = enemyName;
        enemyLifeBox.text = "Life: " + enemyLife;
        enemyDamageBox.text = "Damage: " + enemyDamage;

        armorNameBox.text = armorName;
        armorTypeBox.text = "Type: " + armorType;
        armorDefenseBox.text = "Damage: " + armorDefense;

        weaponNameBox.text = weaponName;
        weaponTypeBox.text = "Type: " + weaponType;
        weaponDamageBox.text = "Defense: " + weaponDamage;

        Debug.Log(enemyLife);

        if (actualLife < 0) actualLife = 0;
        //if (actualLife > maxLife) actualLife = maxLife;

        if(gameStat == GameStat.Fight) {
            leftButton.gameObject.GetComponentInChildren<Text>().text = "Attack";
            rightButton.gameObject.GetComponentInChildren<Text>().text = abilityName;
        } else if (gameStat == GameStat.DropItem) {
            leftButton.gameObject.GetComponentInChildren<Text>().text = "Take";
            rightButton.gameObject.GetComponentInChildren<Text>().text = "Skip";
        }
    }

    public void LeftButton(){
        if(gameStat == GameStat.Fight) {
            Debug.Log("Atacou");
            //critic logic (by: Leo the Beast)
            if(Random.Range (0f, 1f) <= critRate / 100) enemyLife = enemyLife - ((int)((float)atk * critDamage / 100));
            else enemyLife = enemyLife - atk;
            EnemyAttack();
        } else if (gameStat == GameStat.DropItem) {
            if(game.dropType == "Great Sword") {
                atk = atk + (game.dropStats - weaponDamage);
                weaponDamage = game.dropStats;
                weaponName = game.dropName;
                weaponType = game.dropType;
            } else if(game.dropType == "Heavy") {
                def = def + (game.dropStats - armorDefense);
                armorDefense = game.dropStats;
                armorName = game.dropName;
                armorType = game.dropType;
            }

            game.EndBattle();
        }
    }

    /*public void Defense(){
        Debug.Log("Defendeu");
        actualLife = actualLife - (enemyDamage - def);
    }*/

    public void RightButton(){
        if(gameStat == GameStat.Fight) {
            enemyLife = enemyLife - abilityDamage;
            EnemyAttack();
        } else if (gameStat == GameStat.DropItem) {
            game.EndBattle();
        }
    }

    public void EnemyAttack(){
        if(Random.Range (0f, 1f) <= dodgeChance / 100) {
            Debug.Log("Desviou");
        } else {
            if(enemyLife < 0) {Debug.Log("OK"); game.Drop();}
            else {
                int damage = enemyDamage - def;
                if(damage > 0) actualLife = actualLife - damage;
            }
        }
    }

    public void UpdatedStats(){
        
        
    }
}
