using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public enum GameStat {Exploration, Event, Fight, DropItem}
public enum ChooseButton {Firt, Second, Thirth}

public class GameController : MonoBehaviour
{
    public string jsonPlayerScript;
    public string jsonEnemyScript;
    public string jsonDropsScript;
    public Player player;
    public Enemy enemy;
    public Events events;
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
    public int floor;
    public int room;
    public int chooseOption1;
    public int chooseOption2;
    public int chooseOption3;

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

        floor = 1;
        room = 1;

        gameStat = GameStat.Exploration;
    }

    // Update is called once per frame
    void Update()
    {
        statsText.text = "Crit. Rate: " + player.critRate + "%" + 
        "\n" + "Crit. Damage: " + player.critDamage + "%" +
        "\n" + "Evasiness: " + player.dodgeChance + "%";

        if (player.actualLife < 0) player.actualLife = 0;

        xp = player.level * 100;

        if (actualXp >= xp) LevelUp();

        switch (chooseOption1)
        {
            case 0:
                option1.gameObject.GetComponent<Image>().color = Color.gray;
                break;
            case 1:
                option1.gameObject.GetComponent<Image>().color = Color.green;
                break;
            case 2:
                option1.gameObject.GetComponent<Image>().color = Color.red;
                break;
        }

        switch (chooseOption2)
        {
            case 0:
                option2.gameObject.GetComponent<Image>().color = Color.gray;
                break;
            case 1:
                option2.gameObject.GetComponent<Image>().color = Color.green;
                break;
            case 2:
                option2.gameObject.GetComponent<Image>().color = Color.red;
                break;
        }

        switch (chooseOption3)
        {
            case 0:
                option3.gameObject.GetComponent<Image>().color = Color.gray;
                break;
            case 1:
                option3.gameObject.GetComponent<Image>().color = Color.green;
                break;
            case 2:
                option3.gameObject.GetComponent<Image>().color = Color.red;
                break;
        }

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

    public void ChooseOption(int chooseButton){
        floor ++;
        if(floor == 10 * room) {
            GenerateEnemy();
        }
        else {
            int option = 0;
            if(chooseButton == 1) option = chooseOption1;
            else if(chooseButton == 2) option = chooseOption2;
            else if(chooseButton == 3) option = chooseOption3;

            if(option == 0);
            else if(option == 1) {
                Event(Random.Range(0, 3)); 
                gameStat = GameStat.Event;
            }
            else if(option == 2){
                GenerateEnemy();  gameStat = GameStat.Fight;

                option1.gameObject.SetActive(false);
                option2.gameObject.SetActive(false);
                option3.gameObject.SetActive(false);

                stats.leftButton.gameObject.SetActive(true);
                stats.rightButton.gameObject.SetActive(true);

                e.SetActive(true);
                stats.enemyLifeBox.gameObject.SetActive(true);
                stats.enemyLevelBox.gameObject.SetActive(true);
                stats.enemyNameBox.gameObject.SetActive(true);
            }
        }
        chooseOption1 = Random.Range(0 , 3);
        chooseOption2 = Random.Range(0 , 3);
        chooseOption3 = Random.Range(0 , 3);
    }

    public void EndBattle(){
        if(floor == 11) {
            room ++;
        }

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

        events.eventButton.gameObject.SetActive(false);
        events.eventText.gameObject.SetActive(false);
    }

    public void GenerateEnemy(){
        int multiplier = (int) ((float)player.level * 1.25f);
        if(room == 10) {
            enemyName = enemyList.Enemy[1].name;
            enemyDamage = enemyList.Enemy[1].damage * multiplier;
            enemyLife = enemyList.Enemy[1].life * multiplier;
        }
        else {
            chooseEnemy = Random.Range(0, 2);
            Debug.Log(multiplier);

            enemyName = enemyList.Enemy[chooseEnemy].name;
            enemyDamage = enemyList.Enemy[chooseEnemy].damage * multiplier;
            enemyLife = enemyList.Enemy[chooseEnemy].life * multiplier;
        }
    }

    public void Event(int r) {
        switch (r)
        {
            case 0:
                Debug.Log("Caso 0");
                events.CureFount();
                break;
            case 1:
                Debug.Log("Caso 1");
                events.Buff();
                break;
            case 2:
                Debug.Log("Caso 2");
                events.Debuff();
                break;
        }
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
        stats.floorBox.gameObject.SetActive(true);
        stats.playButton.gameObject.SetActive(false);
        stats.dropbar.gameObject.SetActive(false);

        chooseOption1 = Random.Range(0 , 3);
        chooseOption2 = Random.Range(0 , 3);
        chooseOption3 = Random.Range(0 , 3);
    }

    public void ClassChoose(Dropdown dropdown) {
        Class c = stats.classList.Class[dropdown.value];

        player.actualLife = c.life;
        player.maxLife = c.life;

        atk = player.atk;
        def = player.def;

        player.classAtk = c.atk;
        player.classDef = c.def;

        player.atk = player.classAtk + weaponDamage;
        player.def = player.classDef + armorDefense;

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
        player.classAtk = (int) ((float)player.classAtk * 1.25f);
        player.classDef = (int) ((float)player.classDef * 1.25f);
        player.atk = player.classAtk + weaponDamage;
        player.def = player.classDef + armorDefense;
        player.critRate = (int) ((float)player.critRate * 1.2f);
        player.critDamage = (int) ((float)player.critDamage * 1.2f);
        player.dodgeChance = (int) ((float)player.dodgeChance * 1.2f);

        Debug.Log(player.maxLife);
        Debug.Log(player.atk);
        Debug.Log(player.def);

        player.actualLife = player.maxLife;
    }
}
