/*创建者：鲁力源
 * 时间：2015/05/30 10：25
 * 功能说明：玩家基础属性管理类
 * 备注：
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerDataManager {

	public static PlayerDataManager instance = null;
    public List<ShipBase> m_selfShipList = new List<ShipBase>();
    public List<ShipBase> m_targetShipList = new List<ShipBase>();
    public List<ShipBase> m_fightShipList = new List<ShipBase>();
    
    //sq start----------------------------------------------------------
    public string GUID;
    
    //sq end------------------------------------------------------------

    public static PlayerDataManager GetInstance()
    {
        if(instance == null)
        {
            instance = new PlayerDataManager();
        }

        return instance;
    }

    public void SetSelfShipTeam(List<ShipBase> selfShips)
    {
        m_selfShipList = selfShips;
    }

    public bool AddSelfShip(ShipBase ship)
    {
        if(m_selfShipList.Count > GlobalVar.GetInstance().GetMaxShips())
        {
            return false;
        }
        m_selfShipList.Add(ship);
        return true;
    }

    public bool AddTargetShip(ShipBase ship)
    {
        if (m_targetShipList.Count > GlobalVar.GetInstance().GetMaxShips())
        {
            return false;
        }
        m_targetShipList.Add(ship);
        return true;
    }

    public void SetTargetShipTeam(List<ShipBase> selfShips)
    {
        m_targetShipList = selfShips;
    }

    //sq start---------------------------------------------------
    
    //sq end----------------------------------------------------
}
