using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public enum GameStat {Exploration, Fight, DropItem}

public class GameController : MonoBehaviour
{
    public string jsonPlayerScript;
    public string jsonEnemyScript;
    public string jsonDropsScript;
    public Player player;
    public Enemy enemy;
    public EnemyList enemyList;
    public WeaponList weaponList;
    public ArmorList armorList;

    public Button option1;
    public Button option2;
    public Button option3;
    public StatsAndEquipaments stats;
    public Text statsText;
    public bool showStats;
    public GameObject o;

    //stats
    public int level;
    public int xp;
    public int actualXp;
    public int maxLife;
    public int actualLife;
    public int atk;
    public int def;
    public float critRate;
    public float critDamage;
    public float dodgeChance;

    //weapon
    public string weaponName;
    public string weaponType;
    public int weaponDamage;

     //armor
    public string armorName;
    public string armorType;
    public int armorDefense;

    //ability
    public string abilityName;
    public int abilityDamage;

    //name
    public GameObject e;
    public string enemyName;
    public int enemyLife;
    public int enemyDamage;

    public string dropName;
    public int dropStats;
    public string dropType;

    //controll
    public GameStat gameStat;
    public int choose;
    public bool classChoose;
    public int chooseEnemy;

    void Start()
    {
        jsonPlayerScript = File.ReadAllText(".\\Assets\\Data\\PlayerData.json");
        jsonEnemyScript = File.ReadAllText(".\\Assets\\Data\\EnemyData.json");
        jsonDropsScript = File.ReadAllText(".\\Assets\\Data\\DropsData.json");
        player = JsonUtility.FromJson<Player>(jsonPlayerScript);
        //enemy = JsonUtility.FromJson<Enemy>(jsonEnemyScript);
        enemyList = new EnemyList();
        weaponList = new WeaponList();
        armorList = new ArmorList();

        classChoose = true;

        enemyList = JsonConvert.DeserializeObject<EnemyList>(jsonEnemyScript);
        weaponList = JsonConvert.DeserializeObject<WeaponList>(jsonDropsScript);
        armorList = JsonConvert.DeserializeObject<ArmorList>(jsonDropsScript);

        weaponName = weaponList.Weapon[0].name;
        weaponType = weaponList.Weapon[0].type;
        weaponDamage = weaponList.Weapon[0].damage;

        armorName = armorList.Armor[0].name;
        armorType = armorList.Armor[0].type;
        armorDefense = armorList.Armor[0].defense;

        abilityName = player.abilityName;

        stats.leftButton.gameObject.SetActive(false);
        stats.rightButton.gameObject.SetActive(false);
        statsText.gameObject.SetActive(false);
        showStats = false;

        actualLife = player.actualLife;
        player.maxLife = actualLife;

        maxLife = player.maxLife;

        actualXp = 0;

        gameStat = GameStat.Exploration;
    }

    // Update is called once per frame
    void Update()
    {
        statsText.text = "Crit. Rate: " + player.critRate + "%" + 
        "\n" + "Crit. Damage: " + player.critDamage + "%" +
        "\n" + "Dodge Chance: " + player.dodgeChance + "%";

        if (player.actualLife < 0) player.actualLife = 0;

        xp = player.level * 100;

        if (actualXp >= xp) LevelUp();

        //if(classChoose == true) ClassChoose();
    
        //Debug.Log(enemyList.Enemy[0].name);
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

        e.SetActive(true);
        stats.enemyLifeBox.gameObject.SetActive(true);
        stats.enemyLevelBox.gameObject.SetActive(true);
        stats.enemyNameBox.gameObject.SetActive(true);
        gameStat = GameStat.Fight;
    }

    public void EndBattle(){
        option1.gameObject.SetActive(true);
        option2.gameObject.SetActive(true);
        option3.gameObject.SetActive(true);

        stats.leftButton.gameObject.SetActive(false);
        stats.rightButton.gameObject.SetActive(false);

        e.SetActive(false);
        stats.enemyLifeBox.gameObject.SetActive(false);
        stats.enemyLevelBox.gameObject.SetActive(false);
        stats.enemyNameBox.gameObject.SetActive(false);

        stats.dropNameBox.gameObject.SetActive(false);
        stats.dropStatsBox.gameObject.SetActive(false);
        o.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void GenerateEnemy(){
        chooseEnemy = Random.Range(0, 2);
        int multiplier = (int) ((float)player.level * 1.25f);
        Debug.Log(multiplier);

        enemyName = enemyList.Enemy[chooseEnemy].name;
        enemyDamage = enemyList.Enemy[chooseEnemy].damage * multiplier;
        enemyLife = enemyList.Enemy[chooseEnemy].life * multiplier;
    }

    public void Drop()
    {
        gameStat = GameStat.DropItem;

        stats.enemyLifeBox.gameObject.SetActive(false);
        stats.enemyLevelBox.gameObject.SetActive(false);
        stats.enemyNameBox.gameObject.SetActive(false);
        o.GetComponent<SpriteRenderer>().color = Color.blue;
        stats.dropNameBox.gameObject.SetActive(true);
        stats.dropStatsBox.gameObject.SetActive(true);
        
        choose = Random.Range(0, 2);
        int choose2 = Random.Range(1, 3);

        actualXp = actualXp + enemyList.Enemy[chooseEnemy].xp;

        Debug.Log(choose);

        if(choose == 0) {
            dropName = weaponList.Weapon[choose2].name;
            dropStats = weaponList.Weapon[choose2].damage;
            dropType = weaponList.Weapon[choose2].type;
        } else if (choose == 1) {
            dropName = armorList.Armor[choose2].name;
            dropStats = armorList.Armor[choose2].defense;
            dropType = armorList.Armor[choose2].type;
        }

        stats.dropNameBox.text = dropName;
        stats.dropStatsBox.text = dropStats.ToString();
    }

    public void PlayButton() {
        option1.gameObject.SetActive(true);
        option2.gameObject.SetActive(true);
        option3.gameObject.SetActive(true);
        stats.playButton.gameObject.SetActive(false);
        stats.dropbar.gameObject.SetActive(false);
    }

    public void ClassChoose(Dropdown dropdown) {
        Class c = stats.classList.Class[dropdown.value];

        player.actualLife = c.life;
        player.maxLife = c.life;

        atk = player.atk;
        def = player.def;

        player.atk = c.atk + weaponDamage;
        player.def = c.def + armorDefense;

        player.critRate = c.critRate;
        player.critDamage = c.critDamage;
        player.dodgeChance = c.dodgeChance;

        abilityDamage = player.atk * 2;
    }

    public void LeftButton(){
        if(gameStat == GameStat.Fight) {
            Debug.Log("Atacou");
            //critic logic (by: Leo the Beast)
            if(Random.Range (0f, 1f) <= player.critRate / 100) enemyLife = enemyLife - ((int)((float)player.atk * player.critDamage / 100));
            else enemyLife = enemyLife - player.atk;
            EnemyAttack();
        } else if (gameStat == GameStat.DropItem) {
            if(choose == 0) {
                player.atk = player.atk + (dropStats - weaponDamage);
                abilityDamage = player.atk * 2;
                weaponDamage = dropStats;
                weaponName = dropName;
                weaponType = dropType;
            } else if(choose == 1) {
                player.def = player.def + (dropStats - armorDefense);
                armorDefense = dropStats;
                armorName = dropName;
                armorType = dropType;
            }

            EndBattle();
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
            EndBattle();
        }
    }

    public void EnemyAttack(){
        if(enemyLife <= 0) Drop();
        else {
            if(Random.Range (0f, 1f) <= player.dodgeChance / 100) {
                Debug.Log("Desviou");
            } else {
                int damage = enemyDamage - player.def;
                Debug.Log(damage);
                if(damage > 0) player.actualLife = player.actualLife - damage;
            }
        }
    }

    public void LevelUp()
    {
        Debug.Log("Upou");
        player.level = player.level + 1;
        actualXp = actualXp - xp;
        player.maxLife = (int) ((float)player.maxLife * 1.25f);
        player.atk = (int) ((float)player.atk * 1.25f);
        player.def = (int) ((float)player.def * 1.25f);
        player.critRate = (int) ((float)player.critRate * 1.2f);
        player.critDamage = (int) ((float)player.critDamage * 1.2f);
        player.dodgeChance = (int) ((float)player.dodgeChance * 1.2f);

        Debug.Log(player.maxLife);
        Debug.Log(player.atk);
        Debug.Log(player.def);

        player.actualLife = player.maxLife;
    }
}
