using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public enum GameStat {Start, Exploration, Event, Fight, DropItem}
public enum ChooseButton {Firt, Second, Thirth}

public class GameController : MonoBehaviour
{
    public string jsonPlayerScript;
    public string jsonEnemyScript;
    public string jsonDropsScript;
    public string jsonAbilitysAndPassivesScript;
    public Player player;
    public Enemy enemy;
    public Events events;
    public EnemyList enemyList;
    public BossList bossList;
    public WeaponList weaponList;
    public ArmorList armorList;
    public AbilitysAndPassives abilitysAndPassives;
    public AbilityList abilityList;
    public PassiveList passiveList;

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
    public string className;

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
    public int abilityCooldown;
    public int abilityCooldownClass;
    public string passiveName;
    //public int abilityDamage;

    //name
    public GameObject e;
    public string enemyName;
    public int enemyLife;
    public int enemyDamage;
    public float enemyCritChance;
    public float enemyEvasionChance;

    public string dropName;
    public int dropStats;
    public string dropType;
    public string dropAbilityOrPassive;

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

    void Start()
    {
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

        actualXp = 0;

        floor = 1;
        room = 1;

        gameStat = GameStat.Start;
    }

    // Update is called once per frame
    void Update()
    {
        statsText.text = "Crit. Rate: " + player.critRate + "%" + 
        "\n" + "Crit. Damage: " + player.critDamage + "%" +
        "\n" + "Evasiness: " + player.dodgeChance +"%" +
        "\n" + "Ability Power: " + player.abilityPower + "%" +
        "\n" +
        "\n" + "Passives: " + player.passiveName + 
        "\n" + passiveName;

        if (player.actualLife < 0) player.actualLife = 0;

        xp = player.level * 100;

        if (actualXp >= xp) LevelUp();

        if(cooldown1 > 0) stats.rightButton.interactable = false;
        else stats.rightButton.interactable = true;
        if(cooldown2 > 0) stats.abilityButton2.interactable = false;
        else stats.abilityButton2.interactable = true;

        switch (color1)
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

        switch (color2)
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

        switch (color3)
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
                Event(Random.Range(0, 5)); 
                gameStat = GameStat.Event;
            }
            else if(option == 2){
                GenerateEnemy();  gameStat = GameStat.Fight;

                option1.gameObject.SetActive(false);
                option2.gameObject.SetActive(false);
                option3.gameObject.SetActive(false);

                stats.leftButton.gameObject.SetActive(true);
                stats.rightButton.gameObject.SetActive(true);
                stats.abilityButton2.gameObject.SetActive(true);

                e.SetActive(true);
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

        color1 = chooseOption1;
        color2 = chooseOption2;
        color3 = chooseOption3;

        if(events.isConfused > 0) {
            color1 = Random.Range(0, 3);
            color2 = Random.Range(0, 3);
            color3 = Random.Range(0, 3);
            Debug.Log("Confusão");
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

        option1.gameObject.SetActive(true);
        option2.gameObject.SetActive(true);
        option3.gameObject.SetActive(true);

        stats.leftButton.gameObject.SetActive(false);
        stats.rightButton.gameObject.SetActive(false);
        stats.abilityButton2.gameObject.SetActive(false);
        stats.dropAbilityOrPassiveBox.gameObject.SetActive(false);

        e.SetActive(false);
        stats.enemyLifeBox.gameObject.SetActive(false);
        stats.enemyLevelBox.gameObject.SetActive(false);
        stats.enemyNameBox.gameObject.SetActive(false);

        stats.dropNameBox.gameObject.SetActive(false);
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
        chooseEnemy = Random.Range(0, 10);
        Debug.Log(multiplier);

        if(bossFight == true) {
            enemyName = bossList.Boss[chooseEnemy].name;
            enemyDamage = bossList.Boss[chooseEnemy].damage * multiplier;
            enemyLife = bossList.Boss[chooseEnemy].life * multiplier;
            enemyCritChance = bossList.Boss[chooseEnemy].critChance;
            enemyEvasionChance = bossList.Boss[chooseEnemy].evasionChance;
        } else {
            enemyName = enemyList.Enemy[chooseEnemy].name;
            enemyDamage = enemyList.Enemy[chooseEnemy].damage * multiplier;
            enemyLife = enemyList.Enemy[chooseEnemy].life * multiplier;
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
                break;
            case 2:
                Debug.Log("Caso 2");
                events.Debuff();
                events.isNerfed = 4;
                break;
            case 3:
                events.ChangePassiveOrAbility();
                break;
            case 4:
                events.Confusion();
                break;
        }
    }

    public void Drop()
    {
        gameStat = GameStat.DropItem;

        stats.enemyLifeBox.gameObject.SetActive(false);
        stats.enemyLevelBox.gameObject.SetActive(false);
        stats.enemyNameBox.gameObject.SetActive(false);
        stats.abilityButton2.gameObject.SetActive(false);
        o.GetComponent<SpriteRenderer>().color = Color.blue;
        stats.dropNameBox.gameObject.SetActive(true);
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
            dropType = weaponList.Weapon[choose2].name;
            dropAbilityOrPassive = abilityList.Ability[weaponList.Weapon[choose2].ability[choose3]].name;
        } else if (choose == 1) {
            dropName = passiveList.Passive[armorList.Armor[choose2].passive[choose3]].adjetive + " " + armorList.Armor[choose2].name;
            dropStats = ((int)((float)armorList.Armor[choose2].defense * multiplier));
            dropType = armorList.Armor[choose2].name;
            dropAbilityOrPassive = passiveList.Passive[armorList.Armor[choose2].passive[choose3]].name;
        }

        stats.dropNameBox.text = dropName;
        stats.dropStatsBox.text = dropStats.ToString();
        stats.dropAbilityOrPassiveBox.text = dropAbilityOrPassive;
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

        color1 = chooseOption1;
        color2 = chooseOption2;
        color3 = chooseOption3;

        gameStat = GameStat.Exploration;
    }

    public void Cooldown() {
        cooldown1 --;
        cooldown2 --;
    }

    public void ClassChoose(Dropdown dropdown) {
        ResetStats();
        abilitysAndPassives.Reset();

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
                abilityName = "Slash";
                abilityCooldown = 3;
                abilityCooldownClass = 2;

                armorName = "Iron Armor";
                armorType = "Armor";
                armorDefense = armorList.Armor[0].defense;
                break;
            case "Assassin":
                weaponName = "Basic Knife";
                weaponType = "Knife";
                weaponDamage = weaponList.Weapon[3].damage;
                abilityName = "Stab";
                abilityCooldown = 2;
                abilityCooldownClass = 2;

                armorName = "Leather Armor";
                armorType = "Light Armor";
                armorDefense = armorList.Armor[1].defense;
                break;
            case "Mage":
                weaponName = "Basic Staff";
                weaponType = "Staff";
                weaponDamage = weaponList.Weapon[6].damage;
                abilityName = "Fire Ball";
                abilityCooldown = 5;
                abilityCooldownClass = 2;

                armorName = "Leather Armor";
                armorType = "Light Armor";
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
            }
            else {
                Debug.Log("Atacou");
                //critic logic (by: Leo the Beast)
                if(Random.Range (0f, 1f) <= player.critRate / 100) enemyLife = enemyLife - ((int)((float)player.atk * player.critDamage / 100));
                else enemyLife = enemyLife - player.atk; 
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
                weaponName = dropName;
                weaponType = dropType;
                abilityName = dropAbilityOrPassive;
                abilitysAndPassives.Reset();
            } else if(choose == 1) {
                armorDefense = dropStats;
                player.def = player.classDef + armorDefense;
                armorName = dropName;
                armorType = dropType;
                passiveName = dropAbilityOrPassive;
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
            }
            else {
                //enemyLife = enemyLife - abilityDamage;
                abilitysAndPassives.Ability(player.abilityName);
                if(abilitysAndPassives.isCooldownLess == true) cooldown1 = abilityCooldownClass;
                else if(abilitysAndPassives.isCooldownDouble == true) cooldown1 = (abilityCooldownClass + 1) * 2;
                else cooldown1 = abilityCooldownClass + 1;
                EnemyAttack();
            }

            if(abilitysAndPassives.isPoison > 0) {
                enemyLife = enemyLife - (enemyLife / 10);
                abilitysAndPassives.isPoison --;
            }

            Cooldown();
        } else if (gameStat == GameStat.DropItem || gameStat == GameStat.Event) {
            EndBattle();
        }
    }

    public void Ability2Button(){
        if(gameStat == GameStat.Fight) {
            if(Random.Range (0f, 1f) <= enemyEvasionChance / 100){
                Debug.Log("Esquiva inimigo");
            }
            else {
                //enemyLife = enemyLife - abilityDamage;
                abilitysAndPassives.Ability(abilityName);
                if(abilitysAndPassives.isCooldownLess == true) cooldown2 = abilityCooldown;
                else if(abilitysAndPassives.isCooldownDouble == true) cooldown2 = (abilityCooldown + 1) * 2;
                else cooldown2 = abilityCooldown + 1;
                EnemyAttack();
            }

            if(abilitysAndPassives.isPoison > 0) {
                enemyLife = enemyLife - (enemyLife / 10);                
                abilitysAndPassives.isPoison --;
            }
        }
        /*} else if (gameStat == GameStat.DropItem) {
            EndBattle();
        }*/
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
                if(abilitysAndPassives.evasionUp != 0) abilitysAndPassives.evasionUp = 0;
            } else {
                int damage = enemyDamage - player.def;
                Debug.Log(damage);
                if(damage > 0) {
                    if(Random.Range (0f, 1f) <= enemyCritChance / 100) player.actualLife = player.actualLife - ((int)((float)enemyDamage * 1.25));
                    else {
                        if(abilitysAndPassives.block == true) {
                            player.actualLife = player.actualLife - (damage / (1 / 4));
                            abilitysAndPassives.block = false;
                        }
                        else player.actualLife = player.actualLife - damage;
                    }
                }
                if (passiveName == "Thorns") {
                    Debug.Log("Espinho");
                    abilitysAndPassives.Passive(passiveName); 
                }
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
}
