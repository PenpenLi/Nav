using UnityEngine;
using System.Collections;
using ConfigConst;
using System.Collections.Generic;

public enum GameScene
{
    Login,
    Loading,
    Lobby
}

public class SceneSwitch : MonoBehaviour
{
    private GameScene lastScene=GameScene.Login;
    private GameScene curScene=GameScene.Login;

    public static SceneSwitch GetSceneSwitch()
    {
        return PublicGameObject.publicGameObj.GetComponent<SceneSwitch>();
    }

    void Start()
    {
        //应用程序后台时是否应该被运行， 默认为false
        Application.runInBackground=true;
    }

    void Update()
    {

    }

    public void Switch(GameScene scene)
    {
        lastScene=curScene;
        curScene=scene;
        LoadScene();
    }

    public GameScene GetLastScene()
    {
        return lastScene;
    }

    public void ReturnLastScene()
    {
        Switch(lastScene);
        lastScene = curScene;
    }

    public GameScene GetCurScene()
    {
        return curScene;
    }

    private void LoadScene()
    {
        string sceneName=null;
        switch(curScene)
        {
            case GameScene.Login:
                sceneName="LoginScene";
                break;
            case GameScene.Loading:
                sceneName="Loading";
                break;
            case GameScene.Lobby:
                sceneName="Lobby";
                break;
            default:
                break;
        }

        if(sceneName!=null)
        {

            //Application.LoadLevel(sceneName);
            Application.LoadLevelAsync(sceneName);
			GameObject tagBGSoundObj = GameObject.FindGameObjectWithTag("BGSOUND");
			if (sceneName == "Fight") {				
				if (BGSound.markBGSOUND!=2) {
					tagBGSoundObj.GetComponent<BGSound>().PlayBGBattle();
					tagBGSoundObj.GetComponent<AudioSource>().Play();
				}
			}else if(sceneName == "BattleSettle"){		
                //BattleResult result = PlayerDataManager.GetInstance().GetBattleResult();
                //if (BGSound.markBGSOUND!=3) {
                //    if (result.win) {
                //        tagBGSoundObj.GetComponent<BGSound>().PlayBGStageWin();								
                //    }else{
                //        tagBGSoundObj.GetComponent<BGSound>().PlayBGStageLose();
                //    }
                //    tagBGSoundObj.GetComponent<AudioSource>().Play();		
				//}
			}
			else{				
				if (BGSound.markBGSOUND!=1) {
                    //tagBGSoundObj.GetComponent<BGSound>().PlayBGLobby();
                    //tagBGSoundObj.GetComponent<AudioSource>().Play();
				}
			}
        }
    }
}
