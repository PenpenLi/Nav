//作者:鲁力源
//时间：2015/05/22
//功能描述：本地表读取类
//备注：
using UnityEngine;
using System.IO;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

public class SQLiteDataManager
{
    private string persistentFilePath;

    //所有数据库数据的缓存列表

   // private List<SoldierBase> m_soldierList = new List<SoldierBase>();
    private  List<BuildingInfo> m_BI = new List<BuildingInfo>();
    private List<B_Base> m_BB = new List<B_Base>();
    private List<B_Pos> m_BP = new List<B_Pos>();
    private List<Item> m_Item = new List<Item>();

    public SQLiteDataManager(string persistentFilePath)
    {
        this.persistentFilePath = persistentFilePath;
        DBDataUtil.getInstance().setPerSistentPath(persistentFilePath);
        Debug.Log("persistentFilePath---=" + persistentFilePath);
        initDbData();
        SetBI();
    }

    /// <summary>
    /// 初始化所有Db数据到缓存中
    /// </summary>
    public void initDbData()
    {
        m_BB = DBDataUtil.getInstance().GetAllDataFromDB<B_Base>(new B_Base(), "bbase");
        m_BP = DBDataUtil.getInstance().GetAllDataFromDB<B_Pos>(new B_Pos(), "bpos");
        m_Item = DBDataUtil.getInstance().GetAllDataFromDB<Item>(new Item(), "item");
    }

    public void Test()
    {
        Debug.Log("Test-------------------");
    }
    public  void  SetBI()//建筑数据
    {
        m_BI.Add(new BuildingInfo() { PosID = 0, Btype = 4, BuildingID = 5, Lev = 5, BeginUpTimes = "-1", ItemNu = -1 });
        m_BI.Add(new BuildingInfo() { PosID = 1, Btype = 1, BuildingID = -1, Lev = 1, BeginUpTimes = "-1", ItemNu = -1 });
        m_BI.Add(new BuildingInfo() { PosID = 2, Btype = 2, BuildingID = 12, Lev = 3, BeginUpTimes = "-1", ItemNu = -1 });
        m_BI.Add(new BuildingInfo() { PosID = 3, Btype = 3, BuildingID = -1, Lev = 2, BeginUpTimes = "-1", ItemNu = -1 });
        m_BI.Add(new BuildingInfo() { PosID = 4, Btype = 0, BuildingID = 22, Lev = 4, BeginUpTimes = "-1", ItemNu = -1 });
        m_BI.Add(new BuildingInfo() { PosID = 5, Btype = -1, BuildingID = -1, Lev = 3, BeginUpTimes = "-1", ItemNu = -1 });
        m_BI.Add(new BuildingInfo() { PosID = 6, Btype = -1, BuildingID = 18, Lev = 3, BeginUpTimes = "-1", ItemNu = -1 });
        m_BI.Add(new BuildingInfo() { PosID = 7, Btype = -1, BuildingID = -1, Lev = 2, BeginUpTimes = "-1", ItemNu = -1 });
    }
    public List<B_Base> BB()
    {    return m_BB;    }
    public List<B_Pos> BP()
    { return m_BP; }
    public List<Item> Item()
    { return m_Item; }
    public List<BuildingInfo> BI()
    { return m_BI; }
}
