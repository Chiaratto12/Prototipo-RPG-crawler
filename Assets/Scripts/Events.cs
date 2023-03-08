using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Events : MonoBehaviour {
    public GameController game;
    public Button eventButton;
    public Text eventText;

    public int isBuffed;
    public int isNerfed;

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
        eventText.text = "You find a font" + "\n" + "You restored your life";
        Debug.Log("Curado");
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
        eventText.text = "You find a God's statue" + "\n" + "You are more stronger";
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
        eventText.text = "You find a Devil's statue" + "\n" + "You are more weaker";
        game.o.GetComponent<SpriteRenderer>().color = Color.green;
        Debug.Log("Nerfado");
    }

    public void EventsReset() {
        if (isBuffed > 0) isBuffed --;
        if (isBuffed == 0) {game.player.atk = game.player.classAtk + game.weaponDamage; Debug.Log("Normal"); isBuffed = -1;}

        if (isNerfed > 0) isNerfed --;
        if (isNerfed == 0) {game.player.atk = game.player.classAtk + game.weaponDamage; Debug.Log("Normal"); isNerfed = -1;} 
    }

    public void statsReset() {

    }
}