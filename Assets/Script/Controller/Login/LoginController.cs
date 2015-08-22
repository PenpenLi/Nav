using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using NetManager;
using LitJson;

public class LoginController : MonoBehaviour
{
    private LoginDataQuickReq loginQuickReq;
    private LoginData loginData;

    private BuildingDataQuickReq buildingQuickReq;
    
    void Awake()
    {
        //UICamera.mainCamera.GetComponent<UICamera>().eventReceiverMask = ~(1<<LayerMask.NameToLayer("UI"));
    }

    // Use this for initialization
    void Start()
    {
        loginQuickReq=new LoginDataQuickReq();
        ConnectionManager.GetInstance().Connect(delegate (bool isSuc)
        {
            if(isSuc)
            {
            }
        });
        BackgroundMusicController.PlayMusic(6);
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    public void ClickQuickLogin()
    {

        string deviceID=SystemInfo.deviceUniqueIdentifier;
        Debug.Log("deviceID = "+deviceID);
        GetQuickLoginSucess(deviceID,"鲁力源小畜生");


    }

    public void MessageboxCallBack(bool result)
    {
        if(result)
        {
            Debug.Log("Ok");
        }
        else
        {
            Debug.Log("Close");
        }
    }

    public void ClickLogin()
    {
        Debug.Log("ClickLogin");
        UIMessageBox.ShowMessageBox("ClickLogin",null);
    }

    void GetQuickLoginSucess(string deviceID,string nickName)
    {
        Debug.Log("GetQuickLoginSucess");
        loginQuickReq.GetQuickLoginInf(deviceID,nickName,LoginEventCallback);
    }

    private void LoginEventCallback(EventBase eb)
    {
        Debug.Log("LoginEventCallback");
        string eventname=eb.eventName;
        object obj=eb.eventValue;
        
        if(CGNetConst.ROUTE_QUICKLOGIN.Equals(eventname))
        {
            if(obj!=null)
            {
                CommonResult<LoginData> commonResult=(CommonResult<LoginData>)obj;
                Debug.Log("PlayerBase.data ="+commonResult.data);
                int status = commonResult.data.status;
                PublicTimer.ResetServerTime(commonResult.data.curTime);
                //PlayerDataManager.GetInstance().SetPlayerInfo(commonResult.data.player);
                switch (status)
                {
                    case 0: //新注册用户
                        Debug.Log("新注册用户");
                        break;
                    case 1://登录成功
                        Debug.Log("登录成功");
                        break;
                    default:
                        break;
                }
                if (status != -1)
                {
                    PlayerDataManager.GetInstance().m_guid = commonResult.data.player.GUID;
                    buildingQuickReq = new BuildingDataQuickReq();
                    buildingQuickReq.GetBuilingBase(PlayerDataManager.GetInstance().m_guid, BuildingEventCallback);
                }
                SceneSwitch.GetSceneSwitch().Switch(GameScene.Lobby);
            }
        }

    }
     private void BuildingEventCallback(EventBase eb)
    {
        string eventname = eb.eventName;
        object obj = eb.eventValue;

        if (CGNetConst.ROUTE_BUILDING_UPDATE.Equals(eventname))
        {
            if (obj != null)
            {
                CommonResult<BuildingBase> BB = (CommonResult<BuildingBase>)obj;
                Debug.Log("BuildingBase.data =" + BB.data);

                PlayerDataManager.GetInstance().SetBuilding(BB);
            }
        }
    }
}
