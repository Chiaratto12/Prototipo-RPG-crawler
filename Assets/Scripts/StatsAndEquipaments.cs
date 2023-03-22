using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Newtonsoft.Json;

public class StatsAndEquipaments : MonoBehaviour
{
    //classes
    public GameController game;
    public string json;
    public ClassList classList;
    public Dropdown chooseClass;

    //stats
    public Text levelBox;
    public Text level2Box;
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
    public Text enemyLevelBox;
    public Text enemyDefenseBox;

    //drop
    public Image dropPanel;
    public string dropName;
    public int dropStats;
    public string dropType;

    public Text dropNameBox;
    public Text dropStatsBox;
    public Text dropAbilityOrPassiveBox;

    //floor
    public Text floorBox;

    //buttons
    public Button leftButton;
    public Button rightButton;
    public Button abilityButton2;
    public Button playButton;

    //dropbar
    public GameObject dropbar;

    void Start()
    {
        json = File.ReadAllText(".\\Assets\\Data\\ClassData.json");
        //player = JsonUtility.FromJson<Player>(jsonScript);

        chooseClass = dropbar.gameObject.transform.GetComponent<Dropdown>();

        chooseClass.options.Clear();

        classList = new ClassList();

        classList = JsonConvert.DeserializeObject<ClassList>(json);

        foreach (Class Class in classList.Class)
        {
            chooseClass.options.Add(new Dropdown.OptionData() { text = Class.name });
        }

    }

    // Update is called once per frame
    void Update()
    {
        levelBox.text = game.xp + "/" + game.actualXp;
        level2Box.text = "Level: " + game.player.level;
        lifeBox.text = game.player.maxLife + "/" + game.player.actualLife;
        //atkBox.text = "Attack: " + game.player.atk;
        defBox.text = "Attack: " + game.player.atk + "\n" + "Defense: " + game.player.def;

        enemyNameBox.text = game.enemyName;
        enemyLifeBox.text = "Life: " + game.enemyLife;
        enemyLevelBox.text = "Level: " + game.player.level;

        armorNameBox.text = game.armorName + 
        "\n" + "Defense: " + game.armorDefense +
        "\n" + "Evasion: " + game.armorEvasion + "%";
        //armorTypeBox.text = "Defense: " + game.armorDefense;
        //armorDefenseBox.text = "Defense: " + game.armorDefense;

        //abilityText.text = game.abilityName;

        weaponNameBox.text = game.weaponName + 
        "\n" + "Damage: " + game.weaponDamage +
        "\n" + "Crit. Chance: " + game.weaponCritRate + "%" +
        "\n" + "Crit. Damage: " + game.weaponCritDmg + "%";
        //weaponTypeBox.text = "Passive: " + game.abilityName;
        //weaponDamageBox.text = "Damage: " + game.weaponDamage;
        //if (actualLife > maxLife) actualLife = maxLife;

        floorBox.text = "Floor: " + game.floor;

        /*if(game.gameStat == GameStat.Fight) {
            leftButton.gameObject.GetComponentInChildren<Text>().text = "Attack";
            rightButton.gameObject.GetComponentInChildren<Text>().text = game.player.abilityName;
            abilityButton2.gameObject.GetComponentInChildren<Text>().text = game.abilityName;
        } else if (game.gameStat == GameStat.DropItem || game.gameStat == GameStat.Event) {
            leftButton.gameObject.GetComponentInChildren<Text>().text = "Take";
            rightButton.gameObject.GetComponentInChildren<Text>().text = "Skip";
        }*/
    }

    public void UpdatedStats(){
        
        
    }
}
