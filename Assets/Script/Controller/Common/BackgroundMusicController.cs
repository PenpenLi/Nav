using UnityEngine;
using System.Collections;

public class BackgroundMusicController : MonoBehaviour {
    static BackgroundMusicController m_bgMusicController;
	// Use this for initialization
    void Awake()
    {
        m_bgMusicController = this;
    }

	void Start () {
       if(PlayerPrefs.HasKey("music"))
        {
            if (PlayerPrefs.GetInt("music") > 0)
            {
//                AudioListener.pause = false;
                AudioListener.volume = 1;
            } else
            {
//                AudioListener.pause = true;
                AudioListener.volume = 0;
            }
        }
        else
        {
            PlayerPrefs.SetInt("music", 1);
//            AudioListener.pause = false;
            AudioListener.volume = 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
	 
	}

    static public void PlayMusic(int id,bool loop = true)
    {
        m_bgMusicController.PlayMusic1(id,loop);
    }

    static public void TurnOn()
    {
        m_bgMusicController.TurnOnSound();
    }

    static public void TurnOff()
    {
        m_bgMusicController.TurnOffSound();
    }

    void PlayMusic1(int id,bool loop = true)
    {
//        Fight.FightTools.PlayMusic(gameObject, id, loop);
    }

    void TurnOnSound()
    {
//        AudioListener.pause = false;
        AudioListener.volume = 1;
        PlayerPrefs.SetInt("music",1);
    }

    void TurnOffSound()
    {
//        AudioListener.pause = true;
        AudioListener.volume = 0;
        PlayerPrefs.SetInt("music",0);
    }

}
