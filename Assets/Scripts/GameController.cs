using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Button option1;
    public Button option2;
    public Button option3;
    public StatsAndEquipaments stats;
    public Text statsText;
    public bool showStats;
    public GameObject o;

    public string dropName;
    public int dropStats;
    public string dropType;
    void Start()
    {
        stats.leftButton.gameObject.SetActive(false);
        stats.rightButton.gameObject.SetActive(false);
        statsText.gameObject.SetActive(false);
        showStats = false;
    }

    // Update is called once per frame
    void Update()
    {
        statsText.text = "Crit. Rate: " + stats.critRate; 
    }

    public void Stats()
    {
        if(showStats == false) {
            statsText.gameObject.SetActive(true);
            stats.armorDefenseBox.gameObject.SetActive(false);
            stats.armorNameBox.gameObject.SetActive(false);
            stats.armorTypeBox.gameObject.SetActive(false);
            stats.weaponNameBox.gameObject.SetActive(false);
            stats.weaponTypeBox.gameObject.SetActive(false);
            stats.weaponDamageBox.gameObject.SetActive(false);

            showStats = true;
        } else if (showStats == true){
            statsText.gameObject.SetActive(false);
            stats.armorDefenseBox.gameObject.SetActive(true);
            stats.armorNameBox.gameObject.SetActive(true);
            stats.armorTypeBox.gameObject.SetActive(true);
            stats.weaponNameBox.gameObject.SetActive(true);
            stats.weaponTypeBox.gameObject.SetActive(true);
            stats.weaponDamageBox.gameObject.SetActive(true);
            showStats = false;
        }
    }

    public void ChooseOption(){
        GenerateEnemy();

        option1.gameObject.SetActive(false);
        option2.gameObject.SetActive(false);
        option3.gameObject.SetActive(false);

        stats.leftButton.gameObject.SetActive(true);
        stats.rightButton.gameObject.SetActive(true);

        stats.enemy.SetActive(true);
        stats.enemyLifeBox.gameObject.SetActive(true);
        stats.enemyDamageBox.gameObject.SetActive(true);
        stats.enemyNameBox.gameObject.SetActive(true);
        stats.gameStat = GameStat.Fight;
    }

    public void EndBattle(){
        option1.gameObject.SetActive(true);
        option2.gameObject.SetActive(true);
        option3.gameObject.SetActive(true);

        stats.leftButton.gameObject.SetActive(false);
        stats.rightButton.gameObject.SetActive(false);

        stats.enemy.SetActive(false);
        stats.enemyLifeBox.gameObject.SetActive(false);
        stats.enemyDamageBox.gameObject.SetActive(false);
        stats.enemyNameBox.gameObject.SetActive(false);

        stats.dropNameBox.gameObject.SetActive(false);
        stats.dropStatsBox.gameObject.SetActive(false);
        o.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void GenerateEnemy(){
        stats.enemyName = "Slime";
        stats.enemyDamage = 30;
        stats.enemyLife = 200;
    }

    public void Drop()
    {
        stats.gameStat = GameStat.DropItem;

        stats.enemyLifeBox.gameObject.SetActive(false);
        stats.enemyDamageBox.gameObject.SetActive(false);
        stats.enemyNameBox.gameObject.SetActive(false);
        o.GetComponent<SpriteRenderer>().color = Color.blue;
        stats.dropNameBox.gameObject.SetActive(true);
        stats.dropStatsBox.gameObject.SetActive(true);
        
        int choose = Random.Range(0, 2);

        Debug.Log(choose);

        if(choose == 0) {
            dropName = "Better Sword";
            dropStats = 50;
            dropType = "Great Sword";
        } else if (choose == 1) {
            dropName = "Better Armor";
            dropStats = 50;
            dropType = "Heavy";
        }

        stats.dropNameBox.text = dropName;
        stats.dropStatsBox.text = dropStats.ToString();
    }
}
