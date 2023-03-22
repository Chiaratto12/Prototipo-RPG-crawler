using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public enum GameStat {Start, Exploration, Event, Fight, DropItem, GameOver}
public enum ChooseButton {Firt, Second, Thirth}

public class GameController : MonoBehaviour
{
    public string jsonPlayerScript;
    public string jsonEnemyScript;
    public string jsonDropsScript;
    public string jsonAbilitysAndPassivesScript;
    public Player player;
    public GameObject p;
    public Enemy enemy;
    public Events events;
    public EnemyList enemyList;
    public BossList bossList;
    public WeaponList weaponList;
    public ArmorList armorList;
    public AbilitysAndPassives abilitysAndPassives;
    public AbilityList abilityList;
    public PassiveList passiveList;
    public Text playButtonText;
    public Button option1;
    public Button option2;
    public Button option3;
    public StatsAndEquipaments stats;
    public Text statsText;
    public bool showStats;
    public GameObject o;
    public GameObject o1;
    public GameObject o2;
    public GameObject o3;
    public GameLog gameLog;
    public Text gameOverText;

    //stats
    public int level;
    public int xp;
    public int actualXp;
    public Slider xpBar;
    public int maxLife;
    public int actualLife;
    public Slider lifeBar;
    public int atk;
    public int def;
    public float critRate;
    public float critDamage;
    public float dodgeChance;
    public string className;

    //weapon
    public string weaponName;
    public string weaponType;
    public int weaponDamage;
    public float weaponCritRate;
    public float weaponCritDmg;
    public Image weaponSprite;

     //armor
    public string armorName;
    public string armorType;
    public int armorDefense;
    public float armorEvasion;
    public Image armorSprite;

    //ability
    public string abilityName;
    public int abilityCooldown;
    public int abilityCooldownClass;
    public string passiveName;
    public Image passiveIcon;
    public Text passiveDescription;
    //public int abilityDamage;

    //name
    public GameObject e;
    public string enemyName;
    public int enemyLife;
    public Slider enemyLifeBar;
    public int enemyDamage;
    public float enemyCritChance;
    public float enemyEvasionChance;
    public string dropName;
    public int dropStats;
    public float dropFirstSpecialStat;
    public float dropSecondSpecialStat;
    public string dropType;
    public string dropAbilityOrPassive;
    public Image dropSprite;
    public Image dropAbilityOrPassiveSprite;

    //controll
    public GameStat gameStat;
    public int choose;
    public int choose2;
    public bool classChoose;
    public int chooseEnemy;
    public int floor;
    public int room;
    public bool bossFight;
    public int chooseOption1;
    public int chooseOption2;
    public int chooseOption3;
    public int color1;
    public int color2;
    public int color3;
    public int cooldown1;
    public int cooldown2;
    public int playerDamage;
    public int playButtonCase;
    public int damage;
    public GameObject information;
    public Sprite[] button1Sprite;
    public Sprite[] button2Sprite;

    void Start()
    {
        gameLog = this.gameObject.GetComponent<GameLog>();

        jsonPlayerScript = File.ReadAllText(".\\Assets\\Data\\PlayerData.json");
        jsonEnemyScript = File.ReadAllText(".\\Assets\\Data\\EnemyData.json");
        jsonDropsScript = File.ReadAllText(".\\Assets\\Data\\DropsData.json");
        jsonAbilitysAndPassivesScript = File.ReadAllText(".\\Assets\\Data\\AbilitysAndPassivesData.json");
        player = JsonUtility.FromJson<Player>(jsonPlayerScript);
        //enemy = JsonUtility.FromJson<Enemy>(jsonEnemyScript);
        enemyList = new EnemyList();
        bossList = new BossList();
        weaponList = new WeaponList();
        armorList = new ArmorList();
        abilityList = new AbilityList();
        passiveList = new PassiveList();

        classChoose = true;

        enemyList = JsonConvert.DeserializeObject<EnemyList>(jsonEnemyScript);
        bossList = JsonConvert.DeserializeObject<BossList>(jsonEnemyScript);
        weaponList = JsonConvert.DeserializeObject<WeaponList>(jsonDropsScript);
        armorList = JsonConvert.DeserializeObject<ArmorList>(jsonDropsScript);
        abilityList = JsonConvert.DeserializeObject<AbilityList>(jsonAbilitysAndPassivesScript);
        passiveList = JsonConvert.DeserializeObject<PassiveList>(jsonAbilitysAndPassivesScript);

        /*weaponName = weaponList.Weapon[0].name;
        weaponType = weaponList.Weapon[0].type;
        weaponDamage = weaponList.Weapon[0].damage;

        armorName = armorList.Armor[0].name;
        armorType = armorList.Armor[0].type;
        armorDefense = armorList.Armor[0].defense;*/

        abilityName = player.abilityName;

        stats.leftButton.gameObject.SetActive(false);
        stats.rightButton.gameObject.SetActive(false);
        statsText.gameObject.SetActive(false);
        showStats = false;

        actualLife = player.actualLife;
        player.maxLife = actualLife;

        maxLife = player.maxLife;

        abilityName = abilityList.Ability[12].name;
        //passiveName = passiveList.Passive[3].name;
        //abilityCooldown = abilityList.Ability[12].cooldown;

        button1Sprite = new Sprite[2];
        button2Sprite = new Sprite[2];
        button1Sprite[0] = Resources.Load<Sprite> ("UI/yes");
        button2Sprite[0] = Resources.Load<Sprite> ("UI/no");
        button1Sprite[1] = Resources.Load<Sprite> ("Sprites/Icons/Abilitys/normalatk");

        actualXp = 0;

        floor = 1;
        room = 1;

        //playButtonCase = 0;        
        playButtonText.text = "Play";

        gameStat = GameStat.Start;
        
        //i.sprite = Resources.Load<Sprite> ("Sprites/Weapons/Axe/" + abilityList.Ability[weaponList.Weapon[1].ability[2]].adjetive.ToLower() + weaponList.Weapon[1].name.ToLower());
    }

    // Update is called once per frame
    void Update()
    {
        statsText.text = "Crit. Rate: " + player.critRate + "%" + 
        "\n" + "Crit. Damage: " + player.critDamage + "%" +
        "\n" + "Evasiness: " + player.dodgeChance +"%" +
        "\n" + "Ability Power: " + player.abilityPower + "%" +
        "\n" +
        "\n" + "Passives: ";

        if(passiveName != "") 
        {
            passiveDescription.text = passiveName + 
            "\n" + passiveList.Passive.Find(x => x.name == passiveName).description;
        }

        Debug.Log(passiveName);

        lifeBar.value = player.actualLife;

        if (player.actualLife < 0) player.actualLife = 0;

        xp = player.level * 100;
        xpBar.maxValue = xp;
        xpBar.value = actualXp;

        enemyLifeBar.value = enemyLife;

        if (actualXp >= xp) LevelUp();

        if(cooldown1 > 0) stats.rightButton.interactable = false;
        else stats.rightButton.interactable = true;
        if(cooldown2 > 0) stats.abilityButton2.interactable = false;
        else stats.abilityButton2.interactable = true;

        switch (color1)
        {
            case 0:
                //o1.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                break;
            case 1:
                o1.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case 2:
                o1.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
        }

        switch (color2)
        {
            case 0:
                //o2.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                break;
            case 1:
                o2.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case 2:
                o2.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
        }

        switch (color3)
        {
            case 0:
                //o3.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                break;
            case 1:
                o3.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case 2:
                o3.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
        }

        Color c = new Color();
        c = stats.armorDefenseBox.color;

        if (events.isBuffed > 0) stats.atkBox.color = Color.green;
        else if (events.isNerfed > 0) stats.atkBox.color = Color.red;
        else stats.atkBox.color = c;

        if(gameStat == GameStat.Fight) 
        {
            stats.leftButton.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = button1Sprite[1];
            stats.rightButton.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = button2Sprite[1];
        } else 
        {
            stats.leftButton.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = button1Sprite[0];
            stats.rightButton.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = button2Sprite[0];
        }

        if(player.actualLife <= 0) GameOver();

        //if(classChoose == true) ClassChoose();
    
        //Debug.Log(enemyList.Enemy[0].name);

        /*Debug.Log(events.isBuffed);
        Debug.Log(events.isNerfed);*/

        //Ability x = new Ability();

        //Debug.Log(weaponList.Weapon.Find(x => x.name == weaponType).ability[Random.Range(0, 3)]);
    }

    public void Stats()
    {
        if(showStats == false) {
            statsText.gameObject.SetActive(true);
            stats.armorDefenseBox.gameObject.SetActive(false);
            stats.armorNameBox.gameObject.SetActive(false);
            stats.armorTypeBox.gameObject.SetActive(false);
            stats.weaponNameBox.gameObject.SetActive(false);
            //stats.weaponTypeBox.gameObject.SetActive(false);
            stats.weaponDamageBox.gameObject.SetActive(false);
            weaponSprite.gameObject.SetActive(false);
            armorSprite.gameObject.SetActive(false);

            showStats = true;
            passiveIcon.gameObject.SetActive(true);
        } else if (showStats == true){
            statsText.gameObject.SetActive(false);
            stats.armorDefenseBox.gameObject.SetActive(true);
            stats.armorNameBox.gameObject.SetActive(true);
            stats.armorTypeBox.gameObject.SetActive(true);
            stats.weaponNameBox.gameObject.SetActive(true);
            //stats.weaponTypeBox.gameObject.SetActive(true);
            stats.weaponDamageBox.gameObject.SetActive(true);
            weaponSprite.gameObject.SetActive(true);
            armorSprite.gameObject.SetActive(true);

            showStats = false;
            passiveIcon.gameObject.SetActive(false);
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

            if(option == 0) ;
            else if(option == 1) {
                Event(Random.Range(0, 5));
                gameStat = GameStat.Event;
            }
            else if(option == 2){
                GenerateEnemy();  gameStat = GameStat.Fight;

                o1.gameObject.SetActive(false);
                o2.gameObject.SetActive(false);
                o3.gameObject.SetActive(false);
                option1.gameObject.SetActive(false);
                option2.gameObject.SetActive(false);
                option3.gameObject.SetActive(false);

                stats.leftButton.gameObject.SetActive(true);
                stats.rightButton.gameObject.SetActive(true);
                stats.abilityButton2.gameObject.SetActive(true);

                e.SetActive(true);
                enemyLifeBar.gameObject.SetActive(true);
                stats.enemyLifeBox.gameObject.SetActive(true);
                stats.enemyLevelBox.gameObject.SetActive(true);
                stats.enemyNameBox.gameObject.SetActive(true);
            }
        }

        if(floor == 10 * room) {
            chooseOption1 = 2;
            chooseOption2 = 2;
            chooseOption3 = 2;
            bossFight = true;
        } else {
            chooseOption1 = Random.Range(0 , 3);
            chooseOption2 = Random.Range(0 , 3);
            chooseOption3 = Random.Range(0 , 3);
        }

        if(events.isConfused > 0) 
        {
            color1 = Random.Range(0, 3);
            color2 = Random.Range(0, 3);
            color3 = Random.Range(0, 3);
            Debug.Log("Confusão");
        } else if(events.clairvoyance > 0) 
        {
            color1 = chooseOption1;
            color2 = chooseOption2;
            color3 = chooseOption3;
        } else {
            int i = Random.Range(0, 3);

            switch (i)
            {
                case 0:
                    color1 = chooseOption1;
                    color2 = 0;
                    color3 = 0;
                    break;
                case 1:
                    color1 = 0;
                    color2 = chooseOption2;
                    color3 = 0;
                    break;
                case 2:
                    color1 = 0;
                    color2 = 0;
                    color3 = chooseOption3;
                    break;
            }
        }
    }

    public void EndBattle(){
        if(floor == 10 * room) {
            room ++;
        }

        cooldown1 = 0;
        cooldown2 = 0;
        abilitysAndPassives.isPoison = 0;
        abilitysAndPassives.block = false;

        o1.gameObject.SetActive(true);
        o2.gameObject.SetActive(true);
        o3.gameObject.SetActive(true);
        option1.gameObject.SetActive(true);
        option2.gameObject.SetActive(true);
        option3.gameObject.SetActive(true);

        stats.dropPanel.gameObject.SetActive(false);
        stats.leftButton.gameObject.SetActive(false);
        stats.rightButton.gameObject.SetActive(false);
        stats.abilityButton2.gameObject.SetActive(false);
        stats.dropAbilityOrPassiveBox.gameObject.SetActive(false);

        e.SetActive(false);
        enemyLifeBar.gameObject.SetActive(false);
        stats.enemyLifeBox.gameObject.SetActive(false);
        stats.enemyLevelBox.gameObject.SetActive(false);
        stats.enemyNameBox.gameObject.SetActive(false);

        dropSprite.gameObject.SetActive(false);
        
        stats.dropStatsBox.gameObject.SetActive(false);
        o.GetComponent<SpriteRenderer>().color = Color.red;

        events.eventButton.gameObject.SetActive(false);
        events.eventText.gameObject.SetActive(false);

        bossFight = false;

        if(events.isConfused > 0) {
            color1 = Random.Range(0, 3);
            color2 = Random.Range(0, 3);
            color3 = Random.Range(0, 3);
            Debug.Log("Confusão");
        }
    }

    public void GenerateEnemy(){
        int multiplier = (int) ((float)player.level * 1.25f);
        /*if(room == 10) {
            enemyName = enemyList.Enemy[1].name;
            enemyDamage = enemyList.Enemy[1].damage * multiplier;
            enemyLife = enemyList.Enemy[1].life * multiplier;
        }*/
        chooseEnemy = Random.Range(0, 9);
        Debug.Log(enemyList.Enemy[chooseEnemy]);

        if(bossFight == true) {
            enemyName = bossList.Boss[chooseEnemy].name;
            enemyDamage = bossList.Boss[chooseEnemy].damage * multiplier;
            enemyLife = bossList.Boss[chooseEnemy].life * multiplier;
            enemyLifeBar.maxValue = bossList.Boss[chooseEnemy].life * multiplier;
            enemyCritChance = bossList.Boss[chooseEnemy].critChance;
            enemyEvasionChance = bossList.Boss[chooseEnemy].evasionChance;
        } else {
            enemyName = enemyList.Enemy[chooseEnemy].name;
            enemyDamage = enemyList.Enemy[chooseEnemy].damage * multiplier;
            enemyLife = enemyList.Enemy[chooseEnemy].life * multiplier;
            enemyLifeBar.maxValue = enemyList.Enemy[chooseEnemy].life * multiplier;
            enemyCritChance = enemyList.Enemy[chooseEnemy].critChance;
            enemyEvasionChance = enemyList.Enemy[chooseEnemy].evasionChance;
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
                events.isBuffed = 4;
                events.eventsIcon.GetChild(0).gameObject.SetActive(true);
                break;
            case 2:
                Debug.Log("Caso 2");
                events.Debuff();
                events.isNerfed = 4;
                events.eventsIcon.GetChild(1).gameObject.SetActive(true);
                break;
            case 3:
                events.Clairvoyance();
                events.eventsIcon.GetChild(2).gameObject.SetActive(true);
                break;
            case 4:
                events.Confusion();
                events.eventsIcon.GetChild(3).gameObject.SetActive(true);
                break;
        }
    }

    public void Drop()
    {
        gameStat = GameStat.DropItem;

        enemyLifeBar.gameObject.SetActive(false);
        stats.enemyLifeBox.gameObject.SetActive(false);
        stats.enemyLevelBox.gameObject.SetActive(false);
        stats.enemyNameBox.gameObject.SetActive(false);
        stats.abilityButton2.gameObject.SetActive(false);
        //o.GetComponent<SpriteRenderer>().color = Color.blue;
        e.SetActive(false);
        stats.dropPanel.gameObject.SetActive(true);
        dropSprite.gameObject.SetActive(true);
        stats.dropStatsBox.gameObject.SetActive(true);
        stats.dropAbilityOrPassiveBox.gameObject.SetActive(true);

        float multiplier = 1f;
        if (level > 1) multiplier = level * 1.25f;

        choose = Random.Range(0, 2);
        int choose3 = Random.Range(0, 3);
        if(choose == 0) {
            switch (className)
            {
                case "Warrior":
                    choose2 = Random.Range(0, 3);
                    break;
                case "Assassin":
                    choose2 = Random.Range(3, 6);
                    break;
                case "Mage":
                    choose2 = Random.Range(6, 9);
                    break;
            }
        } else if(choose == 1) {
            switch (className)
            {
                case "Warrior":
                    choose2 = Random.Range(0, 2);
                    break;
                case "Assassin":
                    choose2 = Random.Range(1, 3);
                    break;
                case "Mage":
                    choose2 = Random.Range(1, 3);
                    break;
            }
        }

        actualXp = actualXp + enemyList.Enemy[chooseEnemy].xp;

        Debug.Log(choose);

        if(choose == 0) {
            dropName = abilityList.Ability[weaponList.Weapon[choose2].ability[choose3]].adjetive + " " + weaponList.Weapon[choose2].name;
            dropStats = ((int)((float)weaponList.Weapon[choose2].damage * multiplier));
            dropFirstSpecialStat = weaponList.Weapon[choose2].critChance;
            dropSecondSpecialStat = weaponList.Weapon[choose2].critDmg;
            dropType = weaponList.Weapon[choose2].name;
            dropSprite.sprite = Resources.Load<Sprite> ("Sprites/Weapons/" + dropType + "/" + abilityList.Ability[weaponList.Weapon[choose2].ability[choose3]].adjetive.ToLower() + weaponList.Weapon[choose2].name.ToLower());
            dropAbilityOrPassiveSprite.sprite = Resources.Load<Sprite> ("Sprites/Icons/Abilitys/" + abilityList.Ability[weaponList.Weapon[choose2].ability[choose3]].name.ToLower());
            dropAbilityOrPassive = abilityList.Ability[weaponList.Weapon[choose2].ability[choose3]].name;

            stats.dropStatsBox.text = dropName + 
            "\n" + "Damage: " + dropStats.ToString() + 
            "\n" + "Crit. Chance: " + weaponList.Weapon[choose2].critChance + "%" + 
            "\n" + "Crit. Damage: " + weaponList.Weapon[choose2].critDmg + "%"  + 
            "\n" + "Ability:";
            stats.dropAbilityOrPassiveBox.text = dropAbilityOrPassive + "\n";
        } else if (choose == 1) {
            dropName = passiveList.Passive[armorList.Armor[choose2].passive[choose3]].adjetive + " " + armorList.Armor[choose2].name;
            dropStats = ((int)((float)armorList.Armor[choose2].defense * multiplier));
            dropFirstSpecialStat = armorList.Armor[choose2].evasion;
            dropType = armorList.Armor[choose2].name;
            dropSprite.sprite = Resources.Load<Sprite> ("Sprites/Armor/" + dropType + "/" + passiveList.Passive[armorList.Armor[choose2].passive[choose3]].adjetive.ToLower() + armorList.Armor[choose2].name.ToLower());
            dropAbilityOrPassiveSprite.sprite = Resources.Load<Sprite> ("Sprites/Icons/Passives/" + passiveList.Passive[armorList.Armor[choose2].passive[choose3]].name.ToLower());
            dropAbilityOrPassive = passiveList.Passive[armorList.Armor[choose2].passive[choose3]].name;
            stats.dropStatsBox.text = dropName + 
            "\n" + "Defense: " + dropStats.ToString() + 
            "\n" + "Evasion: " + armorList.Armor[choose2].evasion + "%" + 
            "\n" + "Passive:";
            stats.dropAbilityOrPassiveBox.text = dropAbilityOrPassive + "\n" + passiveList.Passive[armorList.Armor[choose2].passive[choose3]].description;
        }
    }

    public void PlayButton() {
        switch (gameStat)
        {
            case GameStat.Start:
                o1.gameObject.SetActive(true);
                o2.gameObject.SetActive(true);
                o3.gameObject.SetActive(true);
                option1.gameObject.SetActive(true);
                option2.gameObject.SetActive(true);
                option3.gameObject.SetActive(true);
                stats.floorBox.gameObject.SetActive(true);
                stats.playButton.gameObject.SetActive(false);
                stats.dropbar.gameObject.SetActive(false);

                chooseOption1 = Random.Range(0 , 3);
                chooseOption2 = Random.Range(0 , 3);
                chooseOption3 = Random.Range(0 , 3);

                int i = Random.Range(0, 3);

                switch (i)
                {
                    case 0:
                        color1 = chooseOption1;
                        color2 = 0;
                        color3 = 0;
                        break;
                    case 1:
                        color1 = 0;
                        color2 = chooseOption2;
                        color3 = 0;
                        break;
                    case 2:
                        color1 = 0;
                        color2 = 0;
                        color3 = chooseOption3;
                        break;
                }

                gameStat = GameStat.Exploration;
                break;
            case GameStat.GameOver:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
        }
    }

    public void Cooldown() {
        cooldown1 --;
        cooldown2 --;
    }
    public void ClassChoose(Dropdown dropdown) {
        if(showStats == false) {
            weaponSprite.gameObject.SetActive(true);
            armorSprite.gameObject.SetActive(true);
        }

        ResetStats();
        abilitysAndPassives.Reset();

        Class c = stats.classList.Class[dropdown.value];

        player.actualLife = c.life;
        player.maxLife = c.life;
        lifeBar.maxValue = player.maxLife;

        atk = player.atk;
        def = player.def;

        player.classAtk = c.atk;
        player.classDef = c.def;

        player.atk = player.classAtk + weaponDamage;
        player.def = player.classDef + armorDefense;

        player.classCritRate = c.critRate;
        player.classCritDmg = c.critDamage;
        player.classDodgeChance = c.dodgeChance;
        player.critRate = player.classCritRate;
        player.critDamage = player.classCritDmg;
        player.dodgeChance = player.classDodgeChance;
        player.abilityPower = c.abilityPower;

        Debug.Log(c.dodgeChance);

        player.abilityName = c.abilityName;
        //player.passiveName = c.passiveName;

        className = c.name;

        switch (className)
        {
            case "Warrior":
                weaponName = "Basic Sword";
                weaponType = "Sword";
                weaponDamage = weaponList.Weapon[0].damage;
                weaponSprite.sprite = Resources.Load<Sprite> ("Sprites/Weapons/" + weaponType + "/sharpsword");
                abilityName = "Slash";
                button2Sprite[1] = Resources.Load<Sprite> ("Sprites/Icons/Abilitys/block");
                stats.abilityButton2.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite> ("Sprites/Icons/Abilitys/" + abilityName.ToLower());
                abilityCooldown = 3;
                abilityCooldownClass = 2;

                armorName = "Iron Armor";
                armorType = "Armor";
                armorSprite.sprite = Resources.Load<Sprite> ("Sprites/Armor/Armor/basicarmor");
                armorDefense = armorList.Armor[0].defense;
                break;
            case "Assassin":
                weaponName = "Basic Knife";
                weaponType = "Knife";
                weaponDamage = weaponList.Weapon[3].damage;
                weaponSprite.sprite = Resources.Load<Sprite> ("Sprites/Weapons/" + weaponType + "/pointyknife");
                abilityName = "Stab";
                button2Sprite[1] = Resources.Load<Sprite> ("Sprites/Icons/Abilitys/dodge");
                stats.abilityButton2.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite> ("Sprites/Icons/Abilitys/" + abilityName.ToLower());
                abilityCooldown = 2;
                abilityCooldownClass = 2;

                armorName = "Leather Armor";
                armorType = "Light Armor";
                armorSprite.sprite = Resources.Load<Sprite> ("Sprites/Armor/Light Armor/basiclight armor");
                armorDefense = armorList.Armor[1].defense;
                break;
            case "Mage":
                weaponName = "Basic Staff";
                weaponType = "Staff";
                weaponDamage = weaponList.Weapon[6].damage;
                weaponSprite.sprite = Resources.Load<Sprite> ("Sprites/Weapons/" + weaponType + "/firestaff");
                abilityName = "Fire Ball";
                button2Sprite[1] = Resources.Load<Sprite> ("Sprites/Icons/Abilitys/dodge");
                stats.abilityButton2.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite> ("Sprites/Icons/Abilitys/" + abilityName.ToLower());
                abilityCooldown = 5;
                abilityCooldownClass = 2;

                armorName = "Leather Armor";
                armorType = "Light Armor";
                armorSprite.sprite = Resources.Load<Sprite> ("Sprites/Armor/Light Armor/basiclight armor");
                armorDefense = armorList.Armor[1].defense;
                break;
        }

        AllPassives();

        //abilityDamage = player.atk * 2;
    }

    public void LeftButton(){
        if(gameStat == GameStat.Fight) {
            if(Random.Range (0f, 1f) <= enemyEvasionChance / 100){
                Debug.Log("Esquiva inimigo");
                Information("Dodge", 1);
            }
            else {
                Debug.Log("Atacou");
                //critic logic (by: Leo the Beast)
                if(Random.Range (0f, 1f) <= player.critRate / 100) playerDamage = ((int)((float)player.atk * player.critDamage / 100)); //enemyLife = enemyLife - ((int)((float)player.atk * player.critDamage / 100));
                else playerDamage = player.atk; //enemyLife = enemyLife - player.atk;
                enemyLife = enemyLife - playerDamage;
                Information(playerDamage.ToString(), 1);
            }

            if(abilitysAndPassives.isPoison > 0) {
                enemyLife = enemyLife - (enemyLife / 10);
                abilitysAndPassives.isPoison --;
            }

            Cooldown();
            EnemyAttack();
        } else if (gameStat == GameStat.DropItem) {
            if(choose == 0) {
                //abilityDamage = player.atk * 2;
                weaponDamage = dropStats;
                if(events.isBuffed > 0) {
                    player.atk = (int)((float)player.classAtk * 1.5f) + (int)((float)weaponDamage* 1.5f);
                    Debug.Log("OK");
                }
                else if (events.isNerfed > 0) {
                    player.atk = (int)((float)player.classAtk * 0.75f) + (int)((float)weaponDamage* 0.75f);
                    Debug.Log("OK");
                }
                else player.atk = player.classAtk + weaponDamage;
                weaponCritRate = dropFirstSpecialStat;
                weaponCritDmg = dropSecondSpecialStat;
                player.critRate = player.classCritRate + weaponCritRate;
                player.critDamage = player.classCritDmg + weaponCritDmg;
                weaponName = dropName;
                weaponType = dropType;
                weaponSprite.sprite = dropSprite.sprite;
                abilityName = dropAbilityOrPassive;
                stats.abilityButton2.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = dropAbilityOrPassiveSprite.sprite;
                abilitysAndPassives.Reset();
            } else if(choose == 1) {
                armorDefense = dropStats;
                player.def = player.classDef + armorDefense;
                armorEvasion = dropFirstSpecialStat;
                player.dodgeChance = player.classDodgeChance + armorEvasion;
                armorName = dropName;
                armorType = dropType;
                armorSprite.sprite = dropSprite.sprite;
                passiveName = dropAbilityOrPassive;
                //passiveIcon.gameObject.SetActive(true);
                var color = passiveIcon.gameObject.transform.GetChild(1).GetComponent<Image>().color;
                color.a = 1f;
                passiveIcon.gameObject.transform.GetChild(1).GetComponent<Image>().color = color;
                passiveIcon.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = dropAbilityOrPassiveSprite.sprite;
                Debug.Log(dropAbilityOrPassive);
                abilitysAndPassives.Reset();
                //abilitysAndPassives.Ability(dropAbilityOrPassive);
                AllPassives();
            }

            EndBattle();
        } else if (gameStat == GameStat.Event) {
            if(events.i == 0) abilityName = events.choose;
            else if (events.i == 1) passiveName = events.choose;

            EndBattle();
        }
    }

    /*public void Defense(){
        Debug.Log("Defendeu");
        actualLife = actualLife - (enemyDamage - def);
    }*/

    public void Ability1Button(){
        if(gameStat == GameStat.Fight) {
            if(Random.Range (0f, 1f) <= enemyEvasionChance / 100){
                Debug.Log("Esquiva inimigo");
                Information("Dodge", 1);
            }
            else {
                //enemyLife = enemyLife - abilityDamage;
                abilitysAndPassives.Ability(player.abilityName);
                //gameLog.playerDamageText.text = playerDamage.ToString();
                //Information(playerDamage.ToString(), 0);
            }

            if(abilitysAndPassives.isCooldownLess == true) cooldown1 = abilityCooldownClass;
            else if(abilitysAndPassives.isCooldownDouble == true) cooldown1 = (abilityCooldownClass + 1) * 2;
            else cooldown1 = abilityCooldownClass + 1;

            if(abilitysAndPassives.isPoison > 0) {
                enemyLife = enemyLife - (enemyLife / 10);
                abilitysAndPassives.isPoison --;
            }
            EnemyAttack();
            Cooldown();
        } else if (gameStat == GameStat.DropItem || gameStat == GameStat.Event) {
            EndBattle();
        }
    }

    public void Ability2Button(){
        if(gameStat == GameStat.Fight) {
            if(Random.Range (0f, 1f) <= enemyEvasionChance / 100){
                Debug.Log("Esquiva inimigo");
                Information("Dodge", 1);
            }
            else {
                //enemyLife = enemyLife - abilityDamage;
                abilitysAndPassives.Ability(abilityName);
                Information(playerDamage.ToString(), 1);
                //gameLog.Log(0);
            }

            if(abilitysAndPassives.isCooldownLess == true) cooldown2 = abilityCooldown;
            else if(abilitysAndPassives.isCooldownDouble == true) cooldown2 = (abilityCooldown + 1) * 2;
            else cooldown2 = abilityCooldown + 1;

            if(abilitysAndPassives.isPoison > 0) {
                enemyLife = enemyLife - (enemyLife / 10);                
                abilitysAndPassives.isPoison --;
            }
        }
        /*} else if (gameStat == GameStat.DropItem) {
            EndBattle();
        }*/
        EnemyAttack();
        Cooldown();
    }

    public void EnemyAttack(){
        if(enemyLife <= 0) {
            float d = Random.Range(0f, 1f);
            if(Random.Range(0f, 1f) <= d || bossFight == true) Drop();
            else EndBattle();
        }
        else {
            if(Random.Range (0f, 1f) <= (player.dodgeChance + abilitysAndPassives.evasionUp) / 100) {
                Debug.Log("Desviou");
                Information("Dodge", 0);
                if(abilitysAndPassives.evasionUp != 0) abilitysAndPassives.evasionUp = 0;
            } else {
                damage = enemyDamage - player.def;
                Debug.Log(damage);
                if(damage > 0) {
                    if(Random.Range (0f, 1f) <= enemyCritChance / 100) damage = ((int)((float)enemyDamage * 1.25)) - player.def;  //player.actualLife = player.actualLife - ((int)((float)enemyDamage * 1.25));
                    else {
                        if(abilitysAndPassives.block == true) {
                            damage = damage / (1 / 4);
                            player.actualLife = player.actualLife - damage;
                            abilitysAndPassives.block = false;
                        }
                        else player.actualLife = player.actualLife - damage;

                        Information(damage.ToString(), 0);
                    }
                }
                if (passiveName == "Thorns") {
                    Debug.Log("Espinho");
                    abilitysAndPassives.Passive(passiveName); 
                }
                //gameLog.Log(1); 
            }
        }
    }

    public void Information(string s, int i) 
    {
        if (gameStat == GameStat.Fight) 
        {
            switch (i)
            {
                case 0:
                    var go = Instantiate(information, p.transform.position, Quaternion.identity, gameLog.canvas.transform);
                    go.GetComponent<Text>().text = s;
                    go.GetComponent<Text>().color = Color.red;
                    break;
                case 1:
                    var g = Instantiate(information, e.transform.position, Quaternion.identity, gameLog.canvas.transform);
                    g.GetComponent<Text>().text = s;
                    g.GetComponent<Text>().color = Color.red;
                    break;
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
        if(events.isBuffed > 0) {
            player.atk = (int)((float)player.classAtk * 1.5f) + (int)((float)weaponDamage* 1.5f);
            Debug.Log("OK");
        }
        else if (events.isNerfed > 0) {
            player.atk = (int)((float)player.classAtk * 0.75f) + (int)((float)weaponDamage* 0.75f);
            Debug.Log("OK");
        }
        else {
            player.atk = player.classAtk + weaponDamage;
        }
        player.def = player.classDef + armorDefense;
        player.critRate = (int) ((float)player.critRate * 1.2f);
        player.critDamage = (int) ((float)player.critDamage * 1.2f);
        player.dodgeChance = (int) ((float)player.dodgeChance * 1.2f);

        Debug.Log(player.maxLife);
        Debug.Log(player.atk);
        Debug.Log(player.def);

        player.actualLife = player.maxLife;
    }

    public void AllPassivesAndAbilitys() {
        abilitysAndPassives.Ability(player.abilityName);
        abilitysAndPassives.Ability(abilityName);
        abilitysAndPassives.Passive(player.passiveName);
        abilitysAndPassives.Passive(passiveName);
    }

    public void AllPassives() {
        abilitysAndPassives.Passive(player.passiveName);
        abilitysAndPassives.Passive(passiveName);
    }

    public void AllAbilitys() {
        abilitysAndPassives.Ability(player.abilityName);
        abilitysAndPassives.Ability(abilityName);
    }

    public void ResetStats() {
        player.actualLife = 0;
        player.maxLife = 0;
        player.atk = 0;
        player.def = 0;
        player.classAtk = 0;
        player.classDef = 0;
        player.critDamage = 0f;
        player.critRate = 0f;
        player.dodgeChance = 0f;
        player.abilityName = "";
        player.passiveName = "";
    }

    public void GameOver() 
    {
        gameStat = GameStat.GameOver;

        o1.gameObject.SetActive(false);
        o2.gameObject.SetActive(false);
        o3.gameObject.SetActive(false);
        option1.gameObject.SetActive(false);
        option2.gameObject.SetActive(false);
        option3.gameObject.SetActive(false);

        stats.leftButton.gameObject.SetActive(false);
        stats.rightButton.gameObject.SetActive(false);
        stats.abilityButton2.gameObject.SetActive(false);
        stats.dropAbilityOrPassiveBox.gameObject.SetActive(false);

        e.SetActive(false);
        enemyLifeBar.gameObject.SetActive(false);
        stats.enemyLifeBox.gameObject.SetActive(false);
        stats.enemyLevelBox.gameObject.SetActive(false);
        stats.enemyNameBox.gameObject.SetActive(false);

        dropSprite.gameObject.SetActive(false);
        
        stats.dropStatsBox.gameObject.SetActive(false);
        o.GetComponent<SpriteRenderer>().color = Color.red;

        events.eventButton.gameObject.SetActive(false);
        events.eventText.gameObject.SetActive(false);

        stats.playButton.gameObject.SetActive(true);

        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "Game Over" + "\n" + "Max floor: " + floor;

        playButtonText.text = "Restart";
    }
}