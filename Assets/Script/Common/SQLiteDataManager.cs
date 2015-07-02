//作者:鲁力源
//时间：2015/05/22
//功能描述：本地表读取类
//备注：
using UnityEngine;
using System.IO;
using System;
using System.Reflection;
using System.Collections.Generic;

public class SQLiteDataManager
{
    private string persistentFilePath;

    //所有数据库数据的缓存列表
    private List<CopiesDB> m_copiesList = new List<CopiesDB>();
    private List<SoldierBase> m_soldierList = new List<SoldierBase>();
    private List<MonsterBase> m_monsterList = new List<MonsterBase>();
    private List<SoldierCompoundCost> m_soldierCompoundCost = new List<SoldierCompoundCost>();
    private List<SoldierUpgrade> m_soldierUpgrade = new List<SoldierUpgrade>();
    private List<SoldierPrice> m_soldierPrice = new List<SoldierPrice>();
    private List<Propskill> m_propSkill = new List<Propskill>();
    private List<CopiesItem> m_copiesItem = new List<CopiesItem>();
    private List<PlayerUpgradeExp> m_playerMaxExp = new List<PlayerUpgradeExp>();
	private List<TreasureFragment> m_treasureFraments = new List<TreasureFragment>();//宝物碎片
	private List<TreasureDB> m_treasure = new List<TreasureDB>();  //宝物
    private List<Miscellaneous> m_miscellaneous = new List<Miscellaneous>();//杂项数据的本地表数据
    private List<Artillery> m_artillery = new List<Artillery>();//城防技能本地配置表
    private List<UpgradeBase> m_upgradeBase = new List<UpgradeBase>();
    private List<UpgradeCost> m_upgradeCost = new List<UpgradeCost>();


    public SQLiteDataManager(string persistentFilePath)
    {
        this.persistentFilePath = persistentFilePath;
        DBDataUtil.getInstance().setPerSistentPath(persistentFilePath);
        Debug.Log("persistentFilePath---=" + persistentFilePath);
        initDbData();

    }

    /// <summary>
    /// 初始化所有Db数据到缓存中
    /// </summary>
    public void initDbData()
    {
        Debug.Log("initDbData()");
        m_monsterList = DBDataUtil.getInstance().GetAllDataFromDB<MonsterBase>(new MonsterBase(), "monster");
        m_copiesList = DBDataUtil.getInstance().GetAllDataFromDB<CopiesDB>(new CopiesDB(), "copies");
        m_soldierList = DBDataUtil.getInstance().GetAllDataFromDB<SoldierBase>(new SoldierBase(), "soldier");
        m_soldierCompoundCost = DBDataUtil.getInstance().GetAllDataFromDB<SoldierCompoundCost>(new SoldierCompoundCost(), "soldiercompoundcost");
        m_soldierUpgrade = DBDataUtil.getInstance().GetAllDataFromDB<SoldierUpgrade>(new SoldierUpgrade(), "soldierupgrade");
        m_soldierPrice = DBDataUtil.getInstance().GetAllDataFromDB<SoldierPrice>(new SoldierPrice(), "soldierprice");
        m_propSkill = DBDataUtil.getInstance().GetAllDataFromDB<Propskill>(new Propskill(), "propskill");
        m_copiesItem = DBDataUtil.getInstance().GetAllDataFromDB<CopiesItem>(new CopiesItem(), "copiesitem");
        m_playerMaxExp = DBDataUtil.getInstance().GetAllDataFromDB<PlayerUpgradeExp>(new PlayerUpgradeExp(), "roleexp");
        m_treasureFraments = DBDataUtil.getInstance().GetAllDataFromDB<TreasureFragment>(new TreasureFragment(), "fragment");
		m_treasure = DBDataUtil.getInstance().GetAllDataFromDB<TreasureDB>(new TreasureDB(), "treasure");
        m_miscellaneous = DBDataUtil.getInstance().GetAllDataFromDB<Miscellaneous>(new Miscellaneous(), "miscellaneouse");
        m_artillery = DBDataUtil.getInstance().GetAllDataFromDB<Artillery>(new Artillery(), "artillery");
        m_upgradeBase = DBDataUtil.getInstance().GetAllDataFromDB<UpgradeBase>(new UpgradeBase(),"upgrade");
        m_upgradeCost = DBDataUtil.getInstance().GetAllDataFromDB<UpgradeCost>(new UpgradeCost(),"upgradecost");
    }

    public Artillery GetArtilleryData(int id)
    {
        for(int i = 0; i < m_artillery.Count; i++)
        {
            if(m_artillery[i].Id == id)
            {
                return m_artillery[i];
            }
        }

        return null;
    }

    public Artillery GetArtilleryDataByLel(int level)
    {
        for (int i = 0; i < m_artillery.Count; i++)
        {
            if (m_artillery[i].SkLevel == level)
            {
                return m_artillery[i];
            }
        }

        return null;
    }

    //查询怪物信息
    public MonsterBase GetMonsterData(int id)
    {

        //Debug.Log("m_monsterList.Count.Count = " + m_monsterList.Count + "id = " + id);
        for (int i = 0; i < m_monsterList.Count; i++)
        {

            //Debug.Log("m_monsterList[i].id == id id =" + m_monsterList[i].Id + "name = " + m_monsterList[i].Name + " m_monsterList[i].HP= " + m_monsterList[i].HP);
            if (m_monsterList[i].Id == id)
            {
                //Debug.Log("m_monsterList[i].id == id id =" + id);
                return m_monsterList[i];
            }
        }
        //Debug.Log("m_monsterList[i].id null");
        return null;
    }

    public Miscellaneous GetMiscellData(int id)
    {
        for (int i = 0; i < m_miscellaneous.Count; i++)
        {
            if (m_miscellaneous[i].Id == id)
            {
                return m_miscellaneous[i];
            }
        }
        return null;
    }

    //查询关卡的基础信息
    public CopiesDB GetCopiesData(int id)
    {
        //Debug.Log("id = " + id + " m_copiesList.Count = " + m_copiesList.Count);
        for (int i = 0; i < m_copiesList.Count; i++)
        {
            if (m_copiesList[i].Id == id)
            {
                return m_copiesList[i];
            }
        }
        return null;
    }

    public SoldierBase GetSoldierData(int id)
    {
        for (int i = 0; i < m_soldierList.Count; i++)
        {
            if (m_soldierList[i].Id == id)
            {
                return m_soldierList[i];
            }
        }
        return null;
    }

    public SoldierBase GetSoldierData(int baseID, int star)
    {
        for (int i = 0; i < m_soldierList.Count; i++)
        {
            if ((m_soldierList[i].BaseId == baseID) && (m_soldierList[i].Star == star))
            {
                return m_soldierList[i];
            }
        }
        return null;
    }

    public SoldierCompoundCost GetSoldierCompoundCostData(int level)
    {
        for (int i = 0; i < m_soldierCompoundCost.Count; i++)
        {
            if (m_soldierCompoundCost[i].Id == level)
            {
                return m_soldierCompoundCost[i];
            }
        }
        return null;
    }

    public SoldierUpgrade GetSoldierUpgradeData(int level)
    {
        for (int i = 0; i < m_soldierUpgrade.Count; i++)
        {
            if (m_soldierUpgrade[i].Id == level)
            {
                return m_soldierUpgrade[i];
            }
        }
        return null;
    }

    public SoldierPrice GetSoldierPrice(int level)
    {
        for (int i = 0; i < m_soldierPrice.Count; i++)
        {
            if (m_soldierPrice[i].Id == level)
            {
                return m_soldierPrice[i];
            }
        }
        return null;
    }

    public Propskill GetMaxLevelPropSkill(int copiesID, int type)
    {
        Propskill pSkill = null;
        for (int i = 0; i < m_propSkill.Count; i++)
        {
            if ((m_propSkill[i].SkType == type) && (m_propSkill[i].UnCopies <= copiesID))
            {
                if (pSkill != null)
                {
                    if (m_propSkill[i].SkLevel > pSkill.SkLevel)
                    {
                        pSkill = m_propSkill[i];
                    }
                }
                else
                {
                    pSkill = m_propSkill[i];
                }
            }
        }
        return pSkill;
    }

    public Propskill GetPropSkillData(int id)
    {
        for (int i = 0; i < m_propSkill.Count; i++)
        {
            if (m_propSkill[i].Id == id)
            {
                return m_propSkill[i];
            }
        }
        return null;
    }

    public List<CopiesItem> GetCopiesItem(int copiesID)
    {
        List<CopiesItem> items = new List<CopiesItem>();
        for (int i = 0; i < m_copiesItem.Count; i++)
        {
            if (m_copiesItem[i].CopiesId == copiesID)
            {
                items.Add(m_copiesItem[i]);
            }
        }
        return items;
    }

    public List<CopiesItem> GetCopiesItem(int copiesID, int type)
    {
        List<CopiesItem> items = new List<CopiesItem>();
        for (int i = 0; i < m_copiesItem.Count; i++)
        {
            if ((m_copiesItem[i].CopiesId == copiesID) && (m_copiesItem[i].GdsType == type))
            {
                items.Add(m_copiesItem[i]);
            }
        }
        return items;
    }

    public int GetPlayerMaxExp(int pType, int level)
    {
        for (int i = 0; i < m_playerMaxExp.Count; i++)
        {
            if ((m_playerMaxExp[i].Level == level) && (m_playerMaxExp[i].PType == pType))
            {
                return m_playerMaxExp[i].MaxExp;
            }
        }
        return -1;
    }

    public PlayerUpgradeExp GetPlayerUpgradeData(int pType, int level)
    {
        for (int i = 0; i < m_playerMaxExp.Count; i++)
        {
            if ((m_playerMaxExp[i].Level == level) && (m_playerMaxExp[i].PType == pType))
            {
                return m_playerMaxExp[i];
            }
        }
        return null;
    }

    public int GetSceneBackgroundID(int dupid)
    {
        for (int i = 0; i < m_copiesList.Count; i++)
        {
            if (m_copiesList[i].Id == dupid)
            {
                return m_copiesList[i].FightBg;
            }
        }
        return -1;
    }

    public TreasureFragment GetTreasureFragmentData(int id)
    {
        for (int i = 0; i < m_treasureFraments.Count; i++)
        {
            if (m_treasureFraments[i].Id == id)
            {
                return m_treasureFraments[i];
            }
        }
        return null;
    }
	public TreasureDB GetTreasureData(int id)
	{
		for (int i = 0; i < m_treasure.Count; i++)
		{
			if (m_treasure[i].Id == id)
			{
				return m_treasure[i];
			}
		}
		return null;
	}
	public List<TreasureDB> GetTreasureListData()
	{
		if (m_treasure!=null) {
			return m_treasure;
		}
		return null;
	}

	public int GetTreasureStar(int id){
		int  starInfo= 0;
		if (m_treasureFraments!=null) {
			for(int i=0; i<m_treasureFraments.Count; i++)
			{
				if(m_treasureFraments[i].Id == id)
				{
					starInfo = m_treasureFraments[i].Star;
					return starInfo;
				}
			}
		}
		return 0;
	}
	public List<TreasureFragment> GetTreasureFrameListData()
	{
		if (m_treasureFraments!=null) {
			return m_treasureFraments;
		}
		return null;
	}
	public string GetStageInfoData(int baseId,int star)
	{
		string stageInfo= "";
		if (m_treasureFraments!=null) {
			for(int i=0; i<m_treasureFraments.Count; i++)
			{
				if(m_treasureFraments[i].BaseId == baseId&&m_treasureFraments[i].Star == star)
				{
					stageInfo = m_treasureFraments[i].DropCop;
					return stageInfo;
				}
			}
		}
		return null;
	}

	public UpgradeBase GetUpgradeBaseData(int id)
    {
        for(int i=0; i<m_upgradeBase.Count; i++)
        {
            if(m_upgradeBase[i].Id == id)
            {
                return m_upgradeBase[i];
            }
        }
        return null;
    }

    public UpgradeCost GetUpgradeCostData(int type,int level)
    {
        for(int i=0; i<m_upgradeCost.Count; i++)
        {
            if(m_upgradeCost[i].Level == level && m_upgradeCost[i].Type == type)
            {
                return m_upgradeCost[i];
            }
        }
        return null;
    }
}
