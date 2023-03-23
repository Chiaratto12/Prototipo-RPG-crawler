using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        music = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Mute() 
    {
        if(music.volume > 0f) 
        {
            music.volume = 0f;
            this.GetComponent<Image>().sprite = Resources.Load<Sprite> ("UI/voloff");
        } else if (music.volume == 0f) 
        {
            music.volume = 0.4f;
            this.GetComponent<Image>().sprite = Resources.Load<Sprite> ("UI/volon");
        }
    }
}
