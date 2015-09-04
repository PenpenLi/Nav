using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class UpgradeTimer : MonoBehaviour
{
    public List<GameObject> m_AnimationList = new List<GameObject>();

    public GameObject m_BuildingUpgraed;
    public GameObject m_TimeLine;
    public GameObject m_HouseName;
    public GameObject m_Buy;

    public UISprite m_ProgressBar;
    public UILabel m_UpgradeTime;
    public UILabel m_Level;

    private int NowTime;
    private int Stime;
    private string HouseName;
    private bool Ft = false;

    List<B_Base> m_BB = MyApp.GetInstance().GetDataManager().BB();

    // Use this for initialization
    void Start()
    {
        Debug.Log("开始升级！！！！！！");
        
        //点击动画显示建筑当前简略信息
        for (int i = 0; i < m_AnimationList.Count; i++)
        {
            UIEventListener.Get(m_AnimationList[i]).onClick = onAnimation;
        }
        Stime = GlobalVar.GetInstance().UpgradeTime;

        TimeCount(Stime);
        if (Stime > 0)
        {
            m_AnimationList[0].SetActive(true);
        }
    }

    void onAnimation(GameObject go)//点击动画查看信息
    {
        var BBase = from n in MyApp.GetInstance().GetDataManager().BB()
                    where n.ID == GlobalVar.GetInstance().BID
                    select new { n.B_LEV, n.B_NAME };
        foreach (var bbase in BBase)
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
                        m_HouseName.GetComponent<UILabel>().text = bbase.B_NAME;
                        m_Level.text =  bbase.B_LEV.ToString();
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
        
    }

    // Update is called once per frame
    void Update()
    {

        MyTimer();
    }
    void MyTimer()
    {
        long DTdiff = DateTime.Now.Ticks - GlobalVar.GetInstance().StartTime.Ticks;
        int DTnow = Stime - Convert.ToInt32(DTdiff / 10000000);
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
            m_ProgressBar.GetComponent<UISprite>().fillAmount = 0;

            if (GlobalVar.GetInstance().UpType == 1) //建造
            {
                Debug.Log(GlobalVar.GetInstance().UpType);
                BuildOver();
            }
            else if (GlobalVar.GetInstance().UpType == -1) //升级
            {
                Debug.Log(GlobalVar.GetInstance().UpType);
                UpgradeOver();
            }
            m_BuildingUpgraed.GetComponent<UpgradeTimer>().enabled = false;
            Destroy(m_BuildingUpgraed);
        }
    }

    void TimeCount(int TimeS) //倒计时显示换算
    {
        int H_TimeDay = TimeS / (60 * 60 * 24);
        int H_TimeHour = (TimeS / 60 - TimeS / (60 * 60 * 24) * 24 * 60) / 60;
        int H_TimeMinute = (TimeS / 60) % 60;
        int H_TimeSecond = TimeS % 60;

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


    void BuildOver()//购买建筑建造完成
    {
        Debug.Log("当前需要@建造@的建筑图集是：" + GlobalVar.GetInstance().Bobjatlas);
        m_BuildingUpgraed.transform.parent.GetComponent<Building>().m_House.GetComponent<UISprite>().atlas = GlobalVar.GetInstance().Bobjatlas;
        m_BuildingUpgraed.transform.parent.GetComponent<Building>().m_House.GetComponent<UISprite>().spriteName = GlobalVar.GetInstance().Bobjicon;

        m_BuildingUpgraed.transform.parent.GetComponent<Building>().m_House.SetActive(true);

        GlobalVar.GetInstance().BulidQueues = 0; //清空升级列表


        string WarningStr = "恭喜岛主" + GlobalVar.GetInstance().BName + " 已经顺利建成 ！";
        warning Warning = new warning(WarningStr);

        Destroy(m_BuildingUpgraed);
    }

    void UpgradeOver()//建筑升级结束
    {
        
        GlobalVar.GetInstance().UpgradeQueues = 0; //清空升级列表
        
        var BBase = from n in MyApp.GetInstance().GetDataManager().BB()
                       where n.ID == GlobalVar.GetInstance().BID + 1
                       select new { n.B_ICON_NAME, n.B_LEV,n.ID};
        foreach (var BIc in BBase)
        {
            m_BuildingUpgraed.transform.parent.GetComponent<Building>().m_House.GetComponent<UISprite>().spriteName = BIc.B_ICON_NAME;
            m_BuildingUpgraed.transform.parent.GetComponent<Building>().m_BId.GetComponent<UILabel>().text = BIc.ID.ToString();
            m_BuildingUpgraed.transform.parent.FindChild("UpgradeManager").GetComponent<BuildingUpgradeManager>().m_HLev.GetComponent<UILabel>().text = BIc.B_LEV.ToString();

            string WarningStr = "恭喜岛主" + GlobalVar.GetInstance().BName + " 已经顺利升级到 " + BIc.B_LEV + " 等级！";
            warning Warning = new warning(WarningStr);
        }

        m_BuildingUpgraed.transform.parent.GetComponent<Building>().m_House.SetActive(true);
        GlobalVar.GetInstance().Bobjname = null;

    }

    public UpgradeTimer(string Parent)
    {
        GameObject Obj = Instantiate(Resources.Load("Prefab/Building/BuildingUpgraed")) as GameObject;//get perfab; 
        Obj.transform.parent = GameObject.Find(Parent).transform;
        Obj.transform.localScale = Vector3.one;
        Obj.transform.localPosition = Vector3.zero;
    }
}
