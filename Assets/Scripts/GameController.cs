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
    void Start()
    {
        stats.atkButton.gameObject.SetActive(false);
        stats.defButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseOption(){
        GenerateEnemy();

        option1.gameObject.SetActive(false);
        option2.gameObject.SetActive(false);
        option3.gameObject.SetActive(false);

        stats.atkButton.gameObject.SetActive(true);
        stats.defButton.gameObject.SetActive(true);

        stats.enemy.SetActive(true);
        stats.enemyLifeBox.gameObject.SetActive(true);
        stats.enemyDamageBox.gameObject.SetActive(true);
        stats.enemyNameBox.gameObject.SetActive(true);
    }

    public void EndBattle(){
        option1.gameObject.SetActive(true);
        option2.gameObject.SetActive(true);
        option3.gameObject.SetActive(true);

        stats.atkButton.gameObject.SetActive(false);
        stats.defButton.gameObject.SetActive(false);

        stats.enemy.SetActive(false);
        stats.enemyLifeBox.gameObject.SetActive(false);
        stats.enemyDamageBox.gameObject.SetActive(false);
        stats.enemyNameBox.gameObject.SetActive(false);
    }

    public void GenerateEnemy(){
        stats.enemyName = "Slime";
        stats.enemyDamage = 10;
        stats.enemyLife = 200;
    }
}
