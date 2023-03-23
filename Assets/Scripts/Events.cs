using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Events : MonoBehaviour {
    public GameController game;
    public Button eventButton;
    public Text eventText;
    public Transform eventsIcon;
    public Transform clairvoyanceIcon;

    public int isBuffed;
    public int isNerfed;
    public int isConfused;
    public int clairvoyance;
    public int i;
    public string choose;
    public string resetText;

    void Start()
    {
        isBuffed = -1;
        isNerfed = -1;    
    }
    
    public void CureFount() {
        game.o1.gameObject.SetActive(false);
        game.o2.gameObject.SetActive(false);
        game.o3.gameObject.SetActive(false);
        game.option1.gameObject.SetActive(false);
        game.option2.gameObject.SetActive(false);
        game.option3.gameObject.SetActive(false);
        game.player.actualLife =  game.player.actualLife + (game.player.maxLife / 3);
        if(game.player.actualLife > game.player.maxLife)game.player.actualLife = game.player.maxLife;
        eventButton.gameObject.SetActive(true);
        eventText.gameObject.SetActive(true);
        game.o.SetActive(true);
        game.o.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ("Sprites/Icons/Events/fount");
        eventText.text = "You found a font" + "\n" + "You life has been restored";
        Debug.Log("Curado");
    }

    //evento q troca ou a habilidade da arma ou a passiva da armadura
    public void ChangeBasicAttack() {
        game.o1.gameObject.SetActive(false);
        game.o2.gameObject.SetActive(false);
        game.o3.gameObject.SetActive(false);
        game.option1.gameObject.SetActive(false);
        game.option2.gameObject.SetActive(false);
        game.option3.gameObject.SetActive(false);
        game.stats.leftButton.gameObject.SetActive(true);
        game.stats.rightButton.gameObject.SetActive(true);
        //eventButton.gameObject.SetActive(true);
        eventText.gameObject.SetActive(true);
        //game.e.SetActive(true);
        //game.o.GetComponent<SpriteRenderer>().color = Color.green;
        game.dropSprite.gameObject.SetActive(true);
        string h = "";
        choose = null;
        choose = game.abilityList.Ability[game.weaponList.Weapon.Find(x => x.name == game.weaponType).ability[Random.Range(0, 3)]].name;
        h = "ability to " + choose;
        Debug.Log(choose);
        // game.o.SetActive(true);
        // game.o.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ("Sprites/Icons/Events/clairvoyancestatue");
        game.dropSprite.sprite = Resources.Load<Sprite> ("Sprites/Icons/Abilitys/" + choose.ToLower());
        eventText.text = "Do you like to change your normal attack to " + choose;
        // game.o1.gameObject.SetActive(false);
        // game.o2.gameObject.SetActive(false);
        // game.o3.gameObject.SetActive(false);
        // game.option1.gameObject.SetActive(false);
        // game.option2.gameObject.SetActive(false);
        // game.option3.gameObject.SetActive(false);
        // eventButton.gameObject.SetActive(true);
        // eventText.gameObject.SetActive(true);
        // game.o.SetActive(true);
        // //game.o.GetComponent<SpriteRenderer>().color = Color.green;
        // eventText.text = "For a short period, you see your way";
        // game.o.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ("Sprites/Icons/Events/clairvoyancestatue");
        // //clairvoyanceIcon.gameObject.SetActive(true);
        // clairvoyance = 5;
    }

    public void Confusion() {
        game.o1.gameObject.SetActive(false);
        game.o2.gameObject.SetActive(false);
        game.o3.gameObject.SetActive(false);
        game.option1.gameObject.SetActive(false);
        game.option2.gameObject.SetActive(false);
        game.option3.gameObject.SetActive(false);
        eventButton.gameObject.SetActive(true);
        eventText.gameObject.SetActive(true);
        game.o.SetActive(true);
        game.o.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ("Sprites/Icons/Events/confusionstatue");
        eventText.text = "You are confused. Do not trust in your decisions";
        //]eventsIcon.GetChild(3).gameObject.SetActive(true);
        isConfused = 5;
    }

    public void Buff() {
        game.o1.gameObject.SetActive(false);
        game.o2.gameObject.SetActive(false);
        game.o3.gameObject.SetActive(false);
        game.option1.gameObject.SetActive(false);
        game.option2.gameObject.SetActive(false);
        game.option3.gameObject.SetActive(false);
        game.player.atk = (int)((float)game.player.classAtk * 1.5f) + (int)((float)game.weaponDamage* 1.5f);
        eventButton.gameObject.SetActive(true);
        eventText.gameObject.SetActive(true);
        //isBuffed = 3;
        game.o.SetActive(true);
        eventText.text = "You found a God's statue" + "\n" + "You are stronger";
        //eventsIcon.GetChild(0).gameObject.SetActive(true);
        game.o.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ("Sprites/Icons/Events/godstatue");
        Debug.Log("Buffado");
    }

    public void Debuff() {
        game.o1.gameObject.SetActive(false);
        game.o2.gameObject.SetActive(false);
        game.o3.gameObject.SetActive(false);
        game.option1.gameObject.SetActive(false);
        game.option2.gameObject.SetActive(false);
        game.option3.gameObject.SetActive(false);
        game.player.atk = (int)((float)game.player.classAtk * 0.75f) + (int)((float)game.weaponDamage* 0.75f);
        eventButton.gameObject.SetActive(true);
        eventText.gameObject.SetActive(true);
        //isNerfed = 3;
        game.o.SetActive(true);
        eventText.text = "You found a Devil's statue" + "\n" + "You are weaker";
        //eventsIcon.GetChild(1).gameObject.SetActive(true);
        game.o.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ("Sprites/Icons/Events/devilstatue");
        Debug.Log("Nerfado");
    }

    public void NotthingHappened(int i) 
    {
        game.o1.gameObject.SetActive(false);
        game.o2.gameObject.SetActive(false);
        game.o3.gameObject.SetActive(false);
        game.option1.gameObject.SetActive(false);
        game.option2.gameObject.SetActive(false);
        game.option3.gameObject.SetActive(false);
        eventButton.gameObject.SetActive(true);
        eventText.gameObject.SetActive(true);
        //isNerfed = 3;
        game.o.SetActive(true);
    }

    public void EventsReset() {
        if (isBuffed > 0) isBuffed --;
        if (isBuffed == 0) {game.player.atk = game.player.classAtk + game.weaponDamage; eventsIcon.GetChild(0).gameObject.SetActive(false); isBuffed = -1;}

        if (isNerfed > 0) isNerfed --;
        if (isNerfed == 0 || isBuffed > 0) {game.player.atk = game.player.classAtk + game.weaponDamage; isNerfed = -1;}

        isConfused --;
        if(isConfused <= 0) {isConfused = 0;}

        clairvoyance --;
        if(clairvoyance <= 0) {clairvoyance = 0; }

        if(isConfused <= 0) eventsIcon.GetChild(1).gameObject.SetActive(false);
        if(isBuffed == 0 && isNerfed == 0) eventsIcon.GetChild(0).gameObject.SetActive(false);
    }

    public void statsReset() {

    }
}