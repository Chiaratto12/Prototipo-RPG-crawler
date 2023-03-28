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
    public string normalAtk;
    public Image normalAttackIcon;
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
    public Image abilityIcon;

     //armor
    public string armorName;
    public string armorType;
    public int armorDefense;
    public float armorEvasion;
    public Image armorSprite;
    public Image defenseAbilityIcon;

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
    public Color enemyColorStart;
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
    public int position;
    public int chooseOption1;
    public int chooseOption2;
    public int chooseOption3;
    public int color1;
    public int color2;
    public int color3;
    public int cooldownNormalAttack;
    public int cooldown1;
    public int cooldown2;
    public int playerDamage;
    public int playButtonCase;
    public int damage;
    public bool isCritical;
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
        normalAtk = "Normal Attack";

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

        enemyColorStart = e.gameObject.GetComponent<SpriteRenderer>().color;

        actualXp = 0;

        floor = 1;
        room = 1;
        position = 0;

        //playButtonCase = 0;        
        playButtonText.text = "Play";
        ClassChoose(stats.dropbar.GetComponent<Dropdown>());

        gameStat = GameStat.Start;
        
        //i.sprite = Resources.Load<Sprite> ("Sprites/Weapons/Axe/" + abilityList.Ability[weaponList.Weapon[1].ability[2]].adjetive.ToLower() + weaponList.Weapon[1].name.ToLower());
    }

    // Update is called once per frame
    void Update()
    {
        statsText.text = "Class: " + className + "\n" + 
        stats.defBox.text + "\n" +
        "Crit. Rate: " + player.critRate + "%" + 
        "\n" + "Crit. Damage: " + player.critDamage + "%" +
        "\n" + "Evasiness: " + player.dodgeChance +"%" +
        "\n" + "Ability Power: " + player.abilityPower + "%";

        // if(passiveName != "") 
        // {
        //     passiveDescription.text = passiveName + 
        //     "\n" + passiveList.Passive.Find(x => x.name == passiveName).description;
        // }

        //Debug.Log(passiveName);

        lifeBar.maxValue = player.maxLife;
        lifeBar.value = player.actualLife;

        if (player.actualLife < 0) player.actualLife = 0;

        xp = player.level * 100;
        xpBar.maxValue = xp;
        xpBar.value = actualXp;

        if (position > 9) position = 9;

        enemyLifeBar.value = enemyLife;

        normalAttackIcon.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = button1Sprite[1];
        defenseAbilityIcon.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = button2Sprite[1];
        abilityIcon.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite> ("Sprites/Icons/Abilitys/" + abilityName.ToLower());

        if (actualXp >= xp) LevelUp();

        if(cooldownNormalAttack > 0) stats.leftButton.interactable = false;
        else stats.leftButton.interactable = true;

        if(cooldown1 > 0) stats.rightButton.interactable = false;
        else stats.rightButton.interactable = true;

        if(cooldown2 > 0) stats.abilityButton2.interactable = false;
        else stats.abilityButton2.interactable = true;

        // if(floor == (10 * room) - 1)
        // {
        //     bossFight = true;
        // }

        // if(bossFight == true) 
        // {
        //     color1 = 2;
        //     color2 = 2;
        //     color3 = 2;
        // }

        Debug.Log("r: " + position);
        
        Color n = new Color(1f, 1f, 1f, 0f);

        switch (color1)
        {
            case 0:
                o1.gameObject.GetComponent<SpriteRenderer>().color = n;
                break;
            case 1:
                o1.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case 2:
                o1.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 3:
                o1.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
                break;
        }

        switch (color2)
        {
            case 0:
                o2.gameObject.GetComponent<SpriteRenderer>().color = n;
                break;
            case 1:
                o2.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case 2:
                o2.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 3:
                o2.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
                break;
        }

        switch (color3)
        {
            case 0:
                o3.gameObject.GetComponent<SpriteRenderer>().color = n;
                break;
            case 1:
                o3.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case 2:
                o3.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 3:
                o3.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
                break;
        }

        Color c = new Color();
        c = stats.armorDefenseBox.color;

        if (events.isBuffed > 0) stats.atkBox.color = Color.green;
        else if (events.isNerfed > 0) stats.atkBox.color = Color.red;
        else stats.atkBox.color = c;

        if(gameStat == GameStat.Fight) 
        {
            stats.leftButton.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = button1Sprite[1];
            stats.rightButton.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = button2Sprite[1];
        } else 
        {
            stats.leftButton.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = button1Sprite[0];
            stats.rightButton.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = button2Sprite[0];
        }

        if(player.actualLife <= 0) GameOver();

        //if(classChoose == true) ClassChoose();
    
        ////Debug.Log(enemyList.Enemy[0].name);

        /*//Debug.Log(events.isBuffed);
        //Debug.Log(events.isNerfed);*/

        //Ability x = new Ability();

        ////Debug.Log(weaponList.Weapon.Find(x => x.name == weaponType).ability[Random.Range(0, 3)]);
    }

    public void Stats()
    {
        if(showStats == false) {
            statsText.gameObject.SetActive(true);
            passiveIcon.gameObject.SetActive(true);
            normalAttackIcon.gameObject.SetActive(true);
            abilityIcon.gameObject.SetActive(true);
            defenseAbilityIcon.gameObject.SetActive(true);
            passiveDescription.gameObject.SetActive(true);
            stats.defBox.gameObject.SetActive(false);
            stats.armorDefenseBox.gameObject.SetActive(false);
            stats.armorNameBox.gameObject.SetActive(false);
            stats.armorTypeBox.gameObject.SetActive(false);
            stats.weaponNameBox.gameObject.SetActive(false);
            //stats.weaponTypeBox.gameObject.SetActive(false);
            stats.weaponDamageBox.gameObject.SetActive(false);
            weaponSprite.gameObject.SetActive(false);
            armorSprite.gameObject.SetActive(false);
            events.eventsIcon.gameObject.SetActive(false);

            showStats = true;
        } else if (showStats == true){
            statsText.gameObject.SetActive(false);
            passiveIcon.gameObject.SetActive(false);
            normalAttackIcon.gameObject.SetActive(false);
            abilityIcon.gameObject.SetActive(false);
            defenseAbilityIcon.gameObject.SetActive(false);
            passiveDescription.gameObject.SetActive(false);
            stats.defBox.gameObject.SetActive(true);
            stats.armorDefenseBox.gameObject.SetActive(true);
            stats.armorNameBox.gameObject.SetActive(true);
            stats.armorTypeBox.gameObject.SetActive(true);
            stats.weaponNameBox.gameObject.SetActive(true);
            //stats.weaponTypeBox.gameObject.SetActive(true);
            stats.weaponDamageBox.gameObject.SetActive(true);
            weaponSprite.gameObject.SetActive(true);
            armorSprite.gameObject.SetActive(true);
            events.eventsIcon.gameObject.SetActive(true);

            showStats = false;
            passiveIcon.gameObject.SetActive(false);
        }
    }

    public void ChooseOption(int chooseButton){
        floor ++;
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
        } else if (option == 3) 
        {
            GenerateBoss();  gameStat = GameStat.Fight;

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

        if(floor == (10 * room) - 1) 
        {
            chooseOption1 = 3;
            chooseOption2 = 3;
            chooseOption3 = 3;

            bossFight = true;
        
            //Debug.Log("BossFight"); 
        }
        else 
        {
            chooseOption1 = Random.Range(0 , 3);
            chooseOption2 = Random.Range(0 , 3);
            chooseOption3 = Random.Range(0 , 3);
        }

        if(events.isConfused > 0) 
        {
            color1 = Random.Range(0, 3);
            color2 = Random.Range(0, 3);
            color3 = Random.Range(0, 3);
            //Debug.Log("Confusão");
        } else {
            color1 = chooseOption1;
            color2 = chooseOption2;
            color3 = chooseOption3;
        }
    }

    public void EndBattle(){
        if(floor == 10 * room) {
            Debug.Log("OK");
            room ++;
            position ++;
            if(floor == 100 * (room / 10)) position = 0;
        }

        cooldown1 = 0;
        cooldown2 = 0;
        cooldownNormalAttack = 0;
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
        events.changeSprite.gameObject.SetActive(false);

        e.SetActive(false);
        e.gameObject.GetComponent<SpriteRenderer>().color = enemyColorStart;
        o.SetActive(false);
        enemyLifeBar.gameObject.SetActive(false);
        stats.enemyLifeBox.gameObject.SetActive(false);
        stats.enemyLevelBox.gameObject.SetActive(false);
        stats.enemyNameBox.gameObject.SetActive(false);

        dropSprite.gameObject.SetActive(false);
        
        stats.dropStatsBox.gameObject.SetActive(false);

        events.eventButton.gameObject.SetActive(false);
        events.eventText.gameObject.SetActive(false);

        bossFight = false;

        // if(floor == 10 * room) {
        //     chooseOption1 = 2;
        //     chooseOption2 = 2;
        //     chooseOption3 = 2;
        //     bossFight = true;
        // }

        if(events.isConfused > 0) {
            color1 = Random.Range(0, 3);
            color2 = Random.Range(0, 3);
            color3 = Random.Range(0, 3);
            //Debug.Log("Confusão");
        }

        abilitysAndPassives.Reset();
        AllPassives();
    }

    public void GenerateEnemy(){
        float multiplier = ((float)player.level * 0.5f);
        if(multiplier < 1f) multiplier = 1f;
        /*if(room == 10) {
            enemyName = enemyList.Enemy[1].name;
            enemyDamage = enemyList.Enemy[1].damage * multiplier;
            enemyLife = enemyList.Enemy[1].life * multiplier;
        }*/

        chooseEnemy = position;

        enemyName = enemyList.Enemy[chooseEnemy].name;
        enemyDamage = (int)((float)enemyList.Enemy[chooseEnemy].damage * multiplier);
        enemyLife = (int)((float)enemyList.Enemy[chooseEnemy].life * multiplier);
        enemyLifeBar.maxValue = (int)((float)enemyList.Enemy[chooseEnemy].life * multiplier);
        enemyCritChance = enemyList.Enemy[chooseEnemy].critChance;
        enemyEvasionChance = enemyList.Enemy[chooseEnemy].evasionChance;
        e.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ("Sprites/Enemys/" + enemyName.ToLower());
    }

     public void GenerateBoss(){
        float multiplier = ((float)player.level * 0.5f);
        if(multiplier < 1f) multiplier = 1f;
        /*if(room == 10) {
            enemyName = enemyList.Enemy[1].name;
            enemyDamage = enemyList.Enemy[1].damage * multiplier;
            enemyLife = enemyList.Enemy[1].life * multiplier;
        }*/
        
        chooseEnemy = position;//Random.Range(0, r);

        enemyName = bossList.Boss[chooseEnemy].name;
        enemyDamage = (int)((float)bossList.Boss[chooseEnemy].damage * multiplier);
        enemyLife = (int)((float)bossList.Boss[chooseEnemy].life * multiplier);
        enemyLifeBar.maxValue = (int)((float)bossList.Boss[chooseEnemy].life * multiplier);
        enemyCritChance = bossList.Boss[chooseEnemy].critChance;
        enemyEvasionChance = bossList.Boss[chooseEnemy].evasionChance;
        e.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ("Sprites/Bosses/" + enemyName.ToLower());
    }

    public void Event(int r) {
        switch (r)
        {
            case 0:
                //Debug.Log("Caso 0");
                events.CureFount();
                break;
            case 1:
                //Debug.Log("Caso 1");
                events.Buff();
                events.isBuffed = 4;
                events.eventsIcon.GetChild(0).gameObject.SetActive(true);
                events.eventsIcon.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite> ("Sprites/Icons/Events/atkup");
                break;
            case 2:
                //Debug.Log("Caso 2");
                events.Debuff();
                if(events.isBuffed == -1) 
                {   
                    events.isNerfed = 4;
                    events.eventsIcon.GetChild(0).gameObject.SetActive(true);
                    events.eventsIcon.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite> ("Sprites/Icons/Events/atkdown");
                }
                break;
            case 3:
                events.ChangeBasicAttack();
                break;
            case 4:
                events.Confusion();
                events.eventsIcon.GetChild(1).gameObject.SetActive(true);
                events.eventsIcon.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite> ("Sprites/Icons/Events/confusion");
                break;
        }
    }

    public void Drop()
    {
        gameStat = GameStat.DropItem;
        cooldownNormalAttack = 0;

        enemyLifeBar.gameObject.SetActive(false);
        stats.enemyLifeBox.gameObject.SetActive(false);
        stats.enemyLevelBox.gameObject.SetActive(false);
        stats.enemyNameBox.gameObject.SetActive(false);
        stats.abilityButton2.gameObject.SetActive(false);
        //o.GetComponent<SpriteRenderer>().color = Color.blue;
        e.SetActive(false);
        stats.rightButton.gameObject.SetActive(true);
        stats.leftButton.gameObject.SetActive(true);
        stats.dropPanel.gameObject.SetActive(true);
        dropSprite.gameObject.SetActive(true);
        stats.dropStatsBox.gameObject.SetActive(true);
        stats.dropAbilityOrPassiveBox.gameObject.SetActive(true);

        float multiplier = 1f;
        if (player.level > 1) multiplier = player.level * 0.6f;

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

        //Debug.Log(choose);

        if(choose == 0) {
            dropName = abilityList.Ability[weaponList.Weapon[choose2].ability[choose3]].adjetive + " " + weaponList.Weapon[choose2].name;
            dropStats = ((int)(((float)weaponList.Weapon[choose2].damage) * multiplier));
            //Debug.Log (((float)weaponList.Weapon[choose2].damage * multiplier) + ": " + multiplier);
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
            dropStats = ((int)(((float)armorList.Armor[choose2].defense) * multiplier));
            //Debug.Log(((int)(((float)armorList.Armor[choose2].defense) * multiplier)) + ": " + multiplier);
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
                stats.chooseYourClassText.gameObject.SetActive(false);

                chooseOption1 = Random.Range(0 , 3);
                chooseOption2 = Random.Range(0 , 3);
                chooseOption3 = Random.Range(0 , 3);

                // int i = Random.Range(0, 3);

                // switch (i)
                // {
                //     case 0:
                //         color1 = chooseOption1;
                //         color2 = 0;
                //         color3 = 0;
                //         break;
                //     case 1:
                //         color1 = 0;
                //         color2 = chooseOption2;
                //         color3 = 0;
                //         break;
                //     case 2:
                //         color1 = 0;
                //         color2 = 0;
                //         color3 = chooseOption3;
                //         break;
                // }

                color1 = chooseOption1;
                color2 = chooseOption2;
                color3 = chooseOption3;

                gameStat = GameStat.Exploration;
                break;
            case GameStat.GameOver:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
        }
    }

    public void Cooldown() {
        cooldownNormalAttack --;
        cooldown1 --;
        cooldown2 --;

        if(cooldownNormalAttack < 0) cooldownNormalAttack = 0;
        if(cooldown1 < 0) cooldown1 = 0;
        if(cooldown2 < 0) cooldown2 = 0;
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
        abilitysAndPassives.maxLife = player.maxLife;
        lifeBar.maxValue = player.maxLife;

        atk = player.atk;
        def = player.def;

        player.classAtk = c.atk;
        player.classDef = c.def;

        player.classCritRate = c.critRate;
        player.classCritDmg = c.critDamage;
        player.classDodgeChance = c.dodgeChance;
        player.critRate = player.classCritRate;
        player.critDamage = player.classCritDmg;
        player.dodgeChance = player.classDodgeChance;
        player.abilityPower = c.abilityPower;

        //Debug.Log(c.dodgeChance);

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
                stats.abilityButton2.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite> ("Sprites/Icons/Abilitys/" + abilityName.ToLower());
                abilityCooldown = abilityList.Ability.Find(x => x.name == abilityName).cooldown;
                abilityCooldownClass = 2;

                armorName = "Iron Armor";
                armorType = "Armor";
                armorSprite.sprite = Resources.Load<Sprite> ("Sprites/Armor/Armor/basicarmor");
                armorDefense = 10;
                break;
            case "Assassin":
                weaponName = "Basic Knife";
                weaponType = "Knife";
                weaponDamage = weaponList.Weapon[3].damage;
                weaponSprite.sprite = Resources.Load<Sprite> ("Sprites/Weapons/" + weaponType + "/pointyknife");
                abilityName = "Stab";
                button2Sprite[1] = Resources.Load<Sprite> ("Sprites/Icons/Abilitys/dodge");
                stats.abilityButton2.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite> ("Sprites/Icons/Abilitys/" + abilityName.ToLower());
                abilityCooldown = abilityList.Ability.Find(x => x.name == abilityName).cooldown;
                abilityCooldownClass = 2;

                armorName = "Leather Armor";
                armorType = "Light Armor";
                armorSprite.sprite = Resources.Load<Sprite> ("Sprites/Armor/Light Armor/basiclight armor");
                armorDefense = 5;
                break;
            case "Mage":
                weaponName = "Basic Staff";
                weaponType = "Staff";
                weaponDamage = weaponList.Weapon[6].damage;
                weaponSprite.sprite = Resources.Load<Sprite> ("Sprites/Weapons/" + weaponType + "/firestaff");
                abilityName = "Fire Ball";
                button2Sprite[1] = Resources.Load<Sprite> ("Sprites/Icons/Abilitys/dodge");
                stats.abilityButton2.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite> ("Sprites/Icons/Abilitys/" + abilityName.ToLower());
                abilityCooldown = abilityList.Ability.Find(x => x.name == abilityName).cooldown;
                abilityCooldownClass = 2;

                armorName = "Leather Armor";
                armorType = "Light Armor";
                armorSprite.sprite = Resources.Load<Sprite> ("Sprites/Armor/Light Armor/basiclight armor");
                armorDefense = 5;
                break;
        }

        player.atk = player.classAtk + weaponDamage;
        player.def = player.classDef + armorDefense;

        AllPassives();

        //abilityDamage = player.atk + 1;
    }

    public void LeftButton(){
        if(gameStat == GameStat.Fight) {
            if(Random.Range (0f, 1f) <= enemyEvasionChance / 100 & normalAtk != "Heal"){
                e.gameObject.GetComponent<Animation>().Play("dodge");
                //Debug.Log("Esquiva inimigo");
                Information("Dodge", 1);
            }
            else {
                //Debug.Log("Atacou");
                abilitysAndPassives.Ability(normalAtk);
                 //enemyLife = enemyLife - player.atk;
                //enemyLife = enemyLife - playerDamage;
                if(isCritical == true) 
                {
                    Information("Critical!" + "\n" + playerDamage.ToString(), 1);
                    isCritical = false;
                } else if (normalAtk != "Heal") Information(playerDamage.ToString(), 1);
                if (abilityName != "Heal") e.gameObject.GetComponent<Animation>().Play("damage");
            }

            if(abilitysAndPassives.isCooldownLess == true) cooldownNormalAttack = abilityList.Ability.Find(x => x.name == normalAtk).cooldown;
            else if(abilitysAndPassives.isCooldownDouble == true && normalAtk != "Normal Attack") cooldownNormalAttack = (abilityList.Ability.Find(x => x.name == normalAtk).cooldown + 1) + 1;
            else cooldownNormalAttack = abilityList.Ability.Find(x => x.name == normalAtk).cooldown + 1;

            if(abilitysAndPassives.isPoison > 0) {
                enemyLife = enemyLife - (enemyLife / 10);
                abilitysAndPassives.isPoison --;
            }

            Cooldown();
            EnemyAttack();
        } else if (gameStat == GameStat.DropItem) {
            if(choose == 0) {
                //abilityDamage = player.atk + 1;
                weaponDamage = dropStats;
                if(events.isBuffed > 0) {
                    player.atk = (int)((float)player.classAtk * 1.5f) + (int)((float)weaponDamage* 1.5f);
                    //Debug.Log("OK");
                }
                else if (events.isNerfed > 0) {
                    player.atk = (int)((float)player.classAtk * 0.75f) + (int)((float)weaponDamage* 0.75f);
                    //Debug.Log("OK");
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
                abilityCooldown = abilityList.Ability.Find(x => x.name == abilityName).cooldown;
                stats.abilityButton2.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = dropAbilityOrPassiveSprite.sprite;
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
                var color = passiveIcon.gameObject.transform.GetChild(0).GetComponent<Image>().color;
                color.a = 1f;
                passiveIcon.gameObject.transform.GetChild(0).GetComponent<Image>().color = color;
                passiveIcon.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = dropAbilityOrPassiveSprite.sprite;
                //Debug.Log(dropAbilityOrPassive);
                
                //abilitysAndPassives.Ability(dropAbilityOrPassive);
            }
            abilitysAndPassives.Reset();
            AllPassives();
            EndBattle();
        } else if (gameStat == GameStat.Event) {
            normalAtk = events.choose;
            button1Sprite[1] = events.changeSprite.sprite;
            EndBattle();
        }
    }

    /*public void Defense(){
        //Debug.Log("Defendeu");
        actualLife = actualLife - (enemyDamage - def);
    }*/

    public void Ability1Button(){
        if(gameStat == GameStat.Fight) {
            if(Random.Range (0f, 1f) <= enemyEvasionChance / 100){
                //Debug.Log("Esquiva inimigo");
                Information("Dodge", 1);
            }
            else {
                //enemyLife = enemyLife - abilityDamage;
                abilitysAndPassives.Ability(player.abilityName);
                //gameLog.playerDamageText.text = playerDamage.ToString();
                //Information(playerDamage.ToString(), 0);
            }

            if(abilitysAndPassives.isPoison > 0) {
                enemyLife = enemyLife - (enemyLife / 10);
                abilitysAndPassives.isPoison --;
            }

            //if (enemyLife <= 0) e.gameObject.GetComponent<Animation>().Play("death");

            EnemyAttack();
            Cooldown();
        } else if (gameStat == GameStat.DropItem || gameStat == GameStat.Event) {
            EndBattle();
        }
    }

    public void Ability2Button(){
        if(gameStat == GameStat.Fight) {
            if(Random.Range (0f, 1f) <= enemyEvasionChance / 100 && abilityName != "Heal"){
                //Debug.Log("Esquiva inimigo");
                e.gameObject.GetComponent<Animation>().Play("dodge");
                Information("Dodge", 1);
            }
            else {
                //enemyLife = enemyLife - abilityDamage;
                abilitysAndPassives.Ability(abilityName);
                if(isCritical == true) 
                {
                    Information("Critical!" + "\n" + playerDamage.ToString(), 1);
                    isCritical = false;
                } else if (abilityName != "Heal")Information(playerDamage.ToString(), 1);
                if (abilityName != "Heal") e.gameObject.GetComponent<Animation>().Play("damage");
                //gameLog.Log(0);
            }

            if(abilitysAndPassives.isCooldownLess == true) cooldown2 = abilityCooldown;
            else if(abilitysAndPassives.isCooldownDouble == true) cooldown2 = (abilityCooldown + 1) + 1;
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
        if(enemyLife <= 0 && player.actualLife > 0) {
            if(bossList.Boss.Find(x => x.name == enemyName) != null) actualXp = actualXp + bossList.Boss[chooseEnemy].xp;
            else actualXp = actualXp + enemyList.Enemy[chooseEnemy].xp;
            stats.abilityButton2.gameObject.SetActive(false);
            stats.rightButton.gameObject.SetActive(false);
            stats.leftButton.gameObject.SetActive(false);
            e.gameObject.GetComponent<Animation>().Play("death");
            float d = Random.Range(0f, 1f);
            if(Random.Range(0f, 1f) <= d || bossList.Boss.Find(x => x.name == enemyName) != null) { Invoke(nameof(Drop), 1.5f);}
            else Invoke(nameof(EndBattle), 1.5f);
        }
        else {
            if(Random.Range (0f, 1f) <= (player.dodgeChance + abilitysAndPassives.evasionUp) / 100) {
                //Debug.Log("Desviou");
                Information("Dodge", 0);
                if(abilitysAndPassives.evasionUp != 0) abilitysAndPassives.evasionUp = 0;
            } else {
                damage = enemyDamage - player.def;
                //Debug.Log(damage);
                if(damage > 0) {
                    if(Random.Range (0f, 1f) <= enemyCritChance / 100) 
                    {
                        damage = ((int)((float)enemyDamage * 1.25)) - player.def; 
                        player.actualLife = player.actualLife - damage;
                        Information("Critical!" + "\n" + damage.ToString(), 0);
                        }  //player.actualLife = player.actualLife - ((int)((float)enemyDamage * 1.25));
                    else {
                        if(abilitysAndPassives.block == true) {
                            damage = damage / (1 / 4);
                            player.actualLife = player.actualLife - damage;
                            abilitysAndPassives.block = false;
                        }
                        else player.actualLife = player.actualLife - damage;

                        Information(damage.ToString(), 0);
                    }
                } else 
                {
                    Information("Blocked", 0);
                }
                if (passiveName == "Thorns") {
                    //Debug.Log("Espinho");
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
                    go.gameObject.GetComponent<Animation>().Play("popup2");
                    break;
                case 1:
                    var g = Instantiate(information, e.transform.position, Quaternion.identity, gameLog.canvas.transform);
                    g.GetComponent<Text>().text = s;
                    g.GetComponent<Text>().color = Color.red;
                    g.gameObject.GetComponent<Animation>().Play("popup");
                    break;
            }
        }
    }

    public void ShowAbilityOrPassive(int i) 
    {
        switch (i)
        {
            case 0:
                if (passiveName == "") passiveDescription.text = "No passive yet";
                else 
                {
                    passiveDescription.text = passiveName + 
                    "\n" + passiveList.Passive.Find(x => x.name == passiveName).description;
                }
                break;
            case 1:
                passiveDescription.text = normalAtk + 
                "\n" + abilityList.Ability.Find(x => x.name == normalAtk).description +
                "\n" + "Cooldown: " + abilityList.Ability.Find(x => x.name == normalAtk).cooldown;
                break;
            case 2:
                passiveDescription.text = player.abilityName + 
                "\n" + abilityList.Ability.Find(x => x.name == player.abilityName).description +
                "\n" + "Cooldown: 0";
                break;
            case 3:
                passiveDescription.text = abilityName + 
                "\n" + abilityList.Ability.Find(x => x.name == abilityName).description +
                "\n" + "Cooldown: " + abilityList.Ability.Find(x => x.name == abilityName).cooldown;
                break;
        }
    }

    public void LevelUp()
    {
        //Debug.Log("Upou");
        player.level = player.level + 1;
        actualXp = actualXp - xp;
        player.maxLife = (int) ((float)player.maxLife * 1.2f);
        abilitysAndPassives.maxLife = player.maxLife;
        player.classAtk = (int) ((float)player.classAtk * 1.2f);
        player.classDef = (int) ((float)player.classDef * 1.25f);
        if(events.isBuffed > 0) {
            player.atk = (int)((float)player.classAtk * 1.5f) + (int)((float)weaponDamage* 1.5f);
            //Debug.Log("OK");
        }
        else if (events.isNerfed > 0) {
            player.atk = (int)((float)player.classAtk * 0.75f) + (int)((float)weaponDamage* 0.75f);
            //Debug.Log("OK");
        }
        else {
            player.atk = player.classAtk + weaponDamage;
        }
        player.def = player.classDef + armorDefense;
        player.critRate = (int) ((float)player.critRate * 1.1f);
        player.critDamage = (int) ((float)player.critDamage * 1.1f);
        player.dodgeChance = (int) ((float)player.dodgeChance * 1.1f);

        //Debug.Log(player.maxLife);
        //Debug.Log(player.atk);
        //Debug.Log(player.def);

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
        o.gameObject.SetActive(false);

        events.eventButton.gameObject.SetActive(false);
        events.eventText.gameObject.SetActive(false);

        stats.playButton.gameObject.SetActive(true);

        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "Game Over" + "\n" + "Max floor: " + floor;

        playButtonText.text = "Restart";
    }
}