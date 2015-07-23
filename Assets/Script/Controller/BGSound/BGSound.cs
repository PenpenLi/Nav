using UnityEngine;
using System.Collections;

public class BGSound : MonoBehaviour {

	// Use this for initialization
	public AudioClip SoundBGLobby;
	public AudioClip SoundBGBattle;
	public AudioClip SoundBGStage;
	public static int markBGSOUND=0;//背景音乐mark标志
	void Awake(){
		markBGSOUND = 1;
		DontDestroyOnLoad(transform.gameObject);
	}		
	public void PlayBGLobby(){
		markBGSOUND = 1;
		transform.GetComponent<AudioSource>().clip =SoundBGLobby;

	}
	public void PlayBGBattle(){
		markBGSOUND = 2;
		transform.GetComponent<AudioSource>().clip =SoundBGBattle;
	}
	public void PlayBGStageWin(){
		markBGSOUND = 3;
		transform.GetComponent<AudioSource>().clip =SoundBGStage;
	}
	public void PlayBGStageLose(){
		markBGSOUND = 3;
		transform.GetComponent<AudioSource>().clip =null;
	}
}
