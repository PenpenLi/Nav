/*创建者：鲁力源
 * 时间：2015/05/30 23：12
 * 功能说明：全局基础变量管理类
 * 备注：
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GlobalVar {
    public static GlobalVar instance = null;
    public int m_fightProgress = 1;//当前的战斗索引
    public const int m_maxTeamShips = 6;//每组最大的舰船数量
	public const int m_maxShips = 12;
    public bool m_finishShell = true;//是否完成此次射击

    //sq Start---------------------------------------------
    
    public bool Sw = false; //标记地图缩放与拖动是否是由warning框关闭的

    public string BuildObjname = null;//建造对象索引
    public int BuildQueues = 0;//建造队列
    public DateTime BuildStartTime;//建造开始时间
    public int BuildID = 0;//建造对象ID
    public long BuildTime = 0; //建造耗时

    public string UpObjname = null;//升级对象索引
    public int UpgradeQueues = 0;//升级队列
    public DateTime UpgradeStartTime;//升级开始时间
    public int UpgradeID = 0;//升级对象ID
    public long UpgradeTime = 0; //升级耗时

    public string ceshi;
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
