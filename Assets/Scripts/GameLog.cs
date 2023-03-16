using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLog : MonoBehaviour
{
    public GameController game;
    public Canvas canvas;
    public Text logText;
    public Text playerDamageText;
    public Text playerDodgeText;
    public Text enemyDamageText;
    public Text enemyDodgeText;
    public string[] informations;
    public int actualText;

    // Start is called before the first frame update
    void Start()
    {
        informations = new string[10];
        actualText = 0;
        informations[0] = "Test";
        informations[1] = "Test 2";
    }

    // Update is called once per frame
    void Update()
    {
        
        /*for (int i = 0; i < informations.Length; i++)
        {
            logText.text = logText.text + informations[i] + "\n";
        }*/
    }

    /*public void Log(int i) {
        //Text t = Instantiate(logText, new Vector3(Random.Range(0, 5), Random.Range(0, 5), Random.Range(0, 5)), Quaternion.identity, canvas.transform);
        switch(i) 
        {
            case 0:
                playerDamageText.text = game.playerDamage.ToString();
                playerDamageText.color = Color.black;
                break;
            case 1:
                enemyDamageText.text = game.damage.ToString();
                enemyDamageText.color = Color.black;
                break;
            case 2:  
                logText.text = "Nothing...";
                logText.color = Color.gray;
                break;
            case 3:
                logText.text = "Your god's blessing is over";
                logText.color = Color.green;
                break;
            case 4:
                logText.text = "Your devil's curse is over";
                logText.color = Color.red;
                break;
            case 5:
                logText.text = "Your confusion is over";
                logText.color = Color.red;
                break;
            case 6:
                playerDodgeText.text = "You dodged";
                playerDodgeText.color = Color.red;
                break;
            case 7:
                enemyDodgeText.text = game.enemyName + " dodged";
                enemyDodgeText.color = Color.red;
                break;
        }
    }*/

    /*public void AddText(int i, string ability) {
        if (actualText == 10) actualText = 0;

        switch (i)
        {
            case 0:
                informations[actualText] = "You go up the stairs. You found nothing" + game.events.resetText;
                game.events.resetText = "";
                break;
            case 1:
                informations[actualText] = "You found a " + game.enemyName + ", be careful" + game.events.resetText;
                game.events.resetText = "";
                break;
            case 2:
                informations[actualText] = "You killed a " + game.enemyName + ". Good Job!";
                game.events.resetText = "";
                break;
            case 3:
                informations[actualText] = game.events.eventText.text + game.events.resetText;
                game.events.resetText = "";
                break;
            case 4:
                //informations[actualText] = "You use your normal attack. You did " + game.playerDamage.ToString() + " of damage.";  
                break;
            case 5:
                //informations[actualText] = "You use " + ability + ". You did ";
                break;
            case 6:
                break;
            case 7:
                break;    
        }
    }*/
}
