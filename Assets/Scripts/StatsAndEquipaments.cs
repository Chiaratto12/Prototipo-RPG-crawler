using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsAndEquipaments : MonoBehaviour
{
    //classes
    public GameController game;

    //stats
    public Text lifeBox;
    public Text atkBox;
    public Text defBox;

    public int life;
    public int atk;
    public int def;

    //weapon
    string weaponName;
    string weaponType;
    int weaponDamage;

    public Text weaponNameBox;
    public Text weaponTypeBox;
    public Text weaponDamageBox;

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

    //buttons
    public Button atkButton;
    public Button defButton;

    void Start()
    {
        weaponName = "Basic Sword";
        weaponType = "Sword";
        weaponDamage = 25;

        atk = atk + weaponDamage;
    }

    // Update is called once per frame
    void Update()
    {
        lifeBox.text = "Life: " + life;
        atkBox.text = "Attack: " + atk;
        defBox.text = "Defense: " + def;

        enemyNameBox.text = enemyName;
        enemyLifeBox.text = "Life: " + enemyLife;
        enemyDamageBox.text = "Damage: " + enemyDamage;

        weaponNameBox.text = weaponName;
        weaponTypeBox.text = "Type: " + weaponType;
        weaponDamageBox.text = "Damage: " + weaponDamage;

        Debug.Log(enemyLife);
    }

    public void Attack(){
        if(enemyLife <= 0) Debug.Log("OK"); game.EndBattle();
        Debug.Log("Atacou");
        enemyLife = enemyLife - atk;
        EnemyAttack();
    }

    public void Defense(){
        Debug.Log("Defendeu");
        life = life - (enemyDamage - def);
    }

    public void EnemyAttack(){
        life = life - enemyDamage;
    }
}
