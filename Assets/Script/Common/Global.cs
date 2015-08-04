/*创建者：鲁力源
 * 时间：2015/05/30 23：12
 * 功能说明：全局基础变量管理类
 * 备注：
 */
using UnityEngine;
using System.Collections;

public class GlobalVar {
    public static GlobalVar instance = null;
    public int m_fightProgress = 1;//当前的战斗索引
    public const int m_maxTeamShips = 6;//每组最大的舰船数量
	public const int m_maxShips = 12;
    public bool m_finishShell = true;//是否完成此次射击

    //sq Start---------------------------------------------
    public string Bobjname = null; //建筑perfab对象名
    public int BobjS = 1; //用于关闭上一个建筑升级按钮菜单
    public int UpgradeTime = 0;//建筑升级所需时间
    public bool Sw = true; // 决定Warning框在关闭时，是否同时开启地图拖动与放大效果
    public int UpgradeQueues = 0; //升级队列
    public int BulidQueues = 0;//建筑队列
    public int AtlaseQueues = -1; //用于建筑队列结束后，传图集用
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
