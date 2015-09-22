using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
public class BuildTimer : MonoBehaviour {

    public List<GameObject> m_AnimationList = new List<GameObject>();
    private List<bbase> m_MyBB = new List<bbase>();
    private List<Item> m_MyItem = new List<Item>();
    
    public GameObject m_BuildingUpgraed;
    public GameObject m_TimeLine;
    public GameObject m_HouseName;
    public GameObject m_Buy;

    public UISprite m_ProgressBar;
    public UILabel m_UpgradeTime;
    public UILabel m_Level;

    private long Stime;
    private int ID;
    private string HouseName;
    private bool Ft = false;

    private DateTime Dt;
    // Use this for initialization
    void Start()
    {
        Debug.Log("开始建造！！！！！！");
        m_BuildingUpgraed.transform.parent.GetComponent<Building>().m_Sale.SetActive(false);
        m_MyBB = MyApp.GetInstance().GetDataManager().BB();
        m_MyItem = MyApp.GetInstance().GetDataManager().Item();

        ID = GlobalVar.GetInstance().BuildID;
        Dt = GlobalVar.GetInstance().BuildStartTime;
        GlobalVar.GetInstance().ceshi = ID.ToString();

        //点击动画显示建筑当前简略信息
        for (int i = 0; i < m_AnimationList.Count; i++)
        {
            UIEventListener.Get(m_AnimationList[i]).onClick = onAnimation;
        }
        Stime = m_MyBB[ID].UpgradeTime;

        TimeCount(Stime);
        if (Stime > 0)
        {
            m_AnimationList[0].SetActive(true);
        }
    }

    void onAnimation(GameObject go)//点击动画查看信息
    {
        for (int i = 0; i < m_AnimationList.Count; i++)
        {
            if (m_AnimationList[i].GetHashCode() == go.gameObject.GetHashCode())
            {
                if (Ft == false)
                {
                    m_TimeLine.transform.localPosition = new Vector3(0, 30f, 0);
                    m_HouseName.SetActive(true);
                    m_Buy.SetActive(true);
                    m_HouseName.GetComponent<UILabel>().text = m_MyBB[ID].Name;
                    m_Level.text = m_MyBB[ID].Lv.ToString();
                    Ft = true;
                }
                else if (Ft == true)
                {
                    m_TimeLine.transform.localPosition = new Vector3(0, 75f, 0);
                    m_HouseName.SetActive(false);
                    m_Buy.SetActive(false);
                    Ft = false;
                }
            }
        }        
    }

    // Update is called once per frame
    void Update()
    {
        MyTimer();
    }
    void MyTimer()
    {
        long DTdiff = DateTime.Now.Ticks - Dt.Ticks; //Debug.Log("DTdiff " + DTdiff);
        long DTnow = Stime - Convert.ToInt32(DTdiff / 10000000); //Debug.Log("DTnow " + DTnow);
        TimeCount(DTnow);
        m_ProgressBar.GetComponent<UISprite>().fillAmount = (1f / Stime) * (Stime - DTnow);

        if (DTnow < Stime / 3 * 2)
        {
            m_AnimationList[0].SetActive(false);
            m_AnimationList[1].SetActive(true);
        }
        if (DTnow < Stime / 3)
        {
            m_AnimationList[1].SetActive(false);
            m_AnimationList[2].SetActive(true);
        }
        if (DTnow <= 0)
        {
            GlobalVar.GetInstance().ceshi = m_MyItem[m_MyBB[ID].ItemID].Atlas;

            //建造结束初始化数据记录
            m_ProgressBar.GetComponent<UISprite>().fillAmount = 0;//进度条归零
            GlobalVar.GetInstance().BuildQueues = 0; //清空升级列表
            GlobalVar.GetInstance().BuildObjname = null;//当前建造建筑对象归零

            //为建造完成的建筑加载建筑数据
            UIAtlas Ua = Resources.Load("Atlas/House/" + m_MyBB[ID].Atlas, typeof(UIAtlas)) as UIAtlas;
            m_BuildingUpgraed.transform.parent.GetComponent<Building>().m_House.GetComponent<UISprite>().atlas = Ua;//图集
            m_BuildingUpgraed.transform.parent.GetComponent<Building>().m_House.GetComponent<UISprite>().spriteName = m_MyBB[ID].Icon;//图标
            m_BuildingUpgraed.transform.parent.GetComponent<Building>().m_BId.GetComponent<UILabel>().text = ID.ToString();//建筑ID
            m_BuildingUpgraed.transform.parent.FindChild("BUManager").FindChild("HName").GetComponent<UILabel>().text = m_MyBB[ID].Name;//建筑名称
            m_BuildingUpgraed.transform.parent.FindChild("BUManager").GetComponent<Upgrade>().m_HLev.GetComponent<UILabel>().text = m_MyBB[ID].Lv.ToString();//建筑等级

            //为建造完成的建筑加载产出数据
            UIAtlas Uia = Resources.Load("Atlas/Lobby/" + m_MyItem[m_MyBB[ID].ItemID].Atlas, typeof(UIAtlas)) as UIAtlas;
            m_BuildingUpgraed.transform.parent.FindChild("Items").FindChild("2").GetComponent<UISprite>().atlas = Uia;
            m_BuildingUpgraed.transform.parent.FindChild("Items").FindChild("2").GetComponent<UISprite>().spriteName = m_MyItem[m_MyBB[ID].ItemID].Icon;
            m_BuildingUpgraed.transform.parent.FindChild("Items").FindChild("ItemID").GetComponent<UILabel>().text = m_MyBB[ID].ItemID.ToString();//产物ID
 
            m_BuildingUpgraed.transform.parent.GetComponent<Building>().m_House.SetActive(true);

            //启动产物生产脚本
            m_BuildingUpgraed.transform.parent.GetComponent<ItemTimer>().enabled = true;  
           
            //关闭建造脚本
            m_BuildingUpgraed.GetComponent<BuildTimer>().enabled = false;
            
            //弹出建造成功提示框
            string WarningStr = "恭喜岛主" + m_MyBB[ID].Name + " 已经顺利建成！！";
            warning Warning = new warning(WarningStr);

            //销毁建造perfab
            Destroy(m_BuildingUpgraed);
        }
    }
    void TimeCount(long TimeS) //倒计时显示换算
    {
        long H_TimeDay = TimeS / (60 * 60 * 24);
        long H_TimeHour = (TimeS / 60 - TimeS / (60 * 60 * 24) * 24 * 60) / 60;
        long H_TimeMinute = (TimeS / 60) % 60;
        long H_TimeSecond = TimeS % 60;

        if (H_TimeDay > 0)
        {
            m_UpgradeTime.GetComponent<UILabel>().text = H_TimeDay + "天" + H_TimeHour + "小时" + H_TimeMinute + "分" + H_TimeSecond + "秒";
        }
        else
        {
            if (H_TimeHour > 0)
            {
                m_UpgradeTime.GetComponent<UILabel>().text = H_TimeHour + "小时" + H_TimeMinute + "分" + H_TimeSecond + "秒";
            }
            else
            {
                if (H_TimeMinute > 0)
                {
                    m_UpgradeTime.GetComponent<UILabel>().text = H_TimeMinute + "分" + H_TimeSecond + "秒";
                }
                else
                {
                    if (H_TimeSecond > 0)
                    {
                        m_UpgradeTime.GetComponent<UILabel>().text = H_TimeSecond + "秒";
                    }
                }
            }
        }
    }
    public BuildTimer(string Parent)
    {
        GameObject Obj = Instantiate(Resources.Load("Prefab/Building/BuildingUpgraed")) as GameObject;//get perfab; 
        Obj.transform.parent = GameObject.Find(Parent).transform;
        Obj.transform.localScale = Vector3.one;
        Obj.transform.localPosition = Vector3.zero;
        Obj.GetComponent<BuildTimer>().enabled = true;
    }
}
