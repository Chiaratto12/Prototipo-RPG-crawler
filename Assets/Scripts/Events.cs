using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Events : MonoBehaviour {
    public GameController game;
    public Button eventButton;
    public Text eventText;

    public int isBuffed;
    public int isNerfed;
    public int isConfused;
    public int i;
    public string choose;

    void Start()
    {
        isBuffed = -1;
        isNerfed = -1;    
    }
    
    public void CureFount() {
        game.option1.gameObject.SetActive(false);
        game.option2.gameObject.SetActive(false);
        game.option3.gameObject.SetActive(false);
        game.player.actualLife =  game.player.actualLife + (game.player.maxLife / 3);
        if(game.player.actualLife > game.player.maxLife)game.player.actualLife = game.player.maxLife;
        eventButton.gameObject.SetActive(true);
        eventText.gameObject.SetActive(true);
        game.e.SetActive(true);
        game.o.GetComponent<SpriteRenderer>().color = Color.green;
        eventText.text = "You found a font" + "\n" + "You life has been restored";
        Debug.Log("Curado");
    }

    //evento q troca ou a habilidade da arma ou a passiva da armadura
    public void ChangePassiveOrAbility() {
        game.option1.gameObject.SetActive(false);
        game.option2.gameObject.SetActive(false);
        game.option3.gameObject.SetActive(false);
        game.stats.leftButton.gameObject.SetActive(true);
        game.stats.rightButton.gameObject.SetActive(true);
        //eventButton.gameObject.SetActive(true);
        eventText.gameObject.SetActive(true);
        game.e.SetActive(true);
        game.o.GetComponent<SpriteRenderer>().color = Color.green;
        string h = "";
        choose = "";
        i = Random.Range(0, 1);
        if(i == 0) {
            choose = game.abilityList.Ability[game.weaponList.Weapon.Find(x => x.name == game.weaponType).ability[Random.Range(0, 3)]].name;
            h = "ability to " + choose;
            }
        else if (i == 1) {
            choose = game.passiveList.Passive[game.armorList.Armor.Find(x => x.name == game.armorType).passive[Random.Range(0, 3)]].name;
            h = "passive?";
            }
        eventText.text = "Do you like to change your actual" + h;
    }

    public void Confusion() {
        game.option1.gameObject.SetActive(false);
        game.option2.gameObject.SetActive(false);
        game.option3.gameObject.SetActive(false);
        eventButton.gameObject.SetActive(true);
        eventText.gameObject.SetActive(true);
        game.e.SetActive(true);
        game.o.GetComponent<SpriteRenderer>().color = Color.green;
        eventText.text = "You are confused. Do not trust more in your decisions";
        isConfused = 10;
    }

    public void Buff() {
        game.option1.gameObject.SetActive(false);
        game.option2.gameObject.SetActive(false);
        game.option3.gameObject.SetActive(false);
        game.player.atk = (int)((float)game.player.classAtk * 1.5f) + (int)((float)game.weaponDamage* 1.5f);
        eventButton.gameObject.SetActive(true);
        eventText.gameObject.SetActive(true);
        //isBuffed = 3;
        game.e.SetActive(true);
        eventText.text = "You found a God's statue" + "\n" + "You are stronger";
        game.o.GetComponent<SpriteRenderer>().color = Color.green;
        Debug.Log("Buffado");
    }

    public void Debuff() {
        game.option1.gameObject.SetActive(false);
        game.option2.gameObject.SetActive(false);
        game.option3.gameObject.SetActive(false);
        game.player.atk = (int)((float)game.player.classAtk * 0.75f) + (int)((float)game.weaponDamage* 0.75f);
        eventButton.gameObject.SetActive(true);
        eventText.gameObject.SetActive(true);
        //isNerfed = 3;
        game.e.SetActive(true);
        eventText.text = "You found a Devil's statue" + "\n" + "You are weaker";
        game.o.GetComponent<SpriteRenderer>().color = Color.green;
        Debug.Log("Nerfado");
    }

    public void EventsReset() {
        if (isBuffed > 0) isBuffed --;
        if (isBuffed == 0) {game.player.atk = game.player.classAtk + game.weaponDamage; Debug.Log("Normal"); isBuffed = -1;}

        if (isNerfed > 0) isNerfed --;
        if (isNerfed == 0) {game.player.atk = game.player.classAtk + game.weaponDamage; Debug.Log("Normal"); isNerfed = -1;}

        isConfused --;
        if(isConfused < 0) isConfused = 0; 
    }

    public void statsReset() {

    }
}