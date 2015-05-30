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
    public int m_maxShips = 5;//每组最大的舰船数量

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

}
