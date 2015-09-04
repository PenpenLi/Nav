/*创建者：鲁力源
 * 时间：2015/05/30 23：12
 * 功能说明：全局基础变量管理类
 * 备注：
 */
using UnityEngine;
using System.Collections;
using System;

public class GlobalVar {
    public static GlobalVar instance = null;
    public int m_fightProgress = 1;//当前的战斗索引
    public const int m_maxTeamShips = 6;//每组最大的舰船数量
	public const int m_maxShips = 12;
    public bool m_finishShell = true;//是否完成此次射击

    //sq Start---------------------------------------------
    
    
    
    
    public UIAtlas Bobjatlas = null; //点击的建筑图集
    public string Bobjicon = null; //点击的建筑图标
    public int NowLev = 0; //点击的建筑等级
    public int MaxLev; //点击的建筑最大等级
    public string BName = null;//点击的建筑名称
    

    public bool BuySuccess = false;//购买成功
    public bool BuyFailure = false;//购买失败
    public bool Sw = false; //标记地图缩放与拖动是否是由warning框关闭的
    
    //====================================================================
    /// <summary>
    /// UpType：当前升级类型,"-1" 升级，"1"初次建造
    /// </summary>
    public int UpType;

    /// <summary>
    /// Bobjname：保存当前点击的建筑perfab对象名
    /// </summary>
    public string Bobjname = null; 

    public int BID = 0;//点击的建筑当前的建筑ID
    public int UpgradeTime = 0; //点击的建筑升到下级所需要的时间

    /// <summary>
    /// UpgradeQueues：当前升级队列中有无正在进行的，默认为无”0“
    /// </summary>
    public int UpgradeQueues = 0; 

    /// <summary>
    /// UpgradeQueues：当前建造队列中有无正在进行的，默认为无”0“
    /// </summary>
    public int BulidQueues = 0;

    /// <summary>
    /// StartTime：记录当前升级或建造开始的时间
    /// </summary>
    public DateTime StartTime;
    
    //sq end-----------------------------------------------


    public static GlobalVar GetInstance()
    {
        if(instance == null)
        {
            instance = new GlobalVar();
        }
        return instance;
    }

    public int GetFightProgress()
    {
        return m_fightProgress;
    }

    public void AddFightProgress()
    {
        if(m_fightProgress > m_maxShips)
        {
            m_fightProgress = 1;
        }
        m_fightProgress++;
    }

    public int GetMaxShips()
    {
        return m_maxShips;
    }

    public bool GetFinishShell()
    {
        return m_finishShell;
    }

    public void SetFinishShell(bool bFinish)
    {
        m_finishShell = bFinish;
    }
}
