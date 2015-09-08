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
    }

    /// <summary>
    /// 初始化所有Db数据到缓存中
    /// </summary>
    public void initDbData()
    {
        m_BB = DBDataUtil.getInstance().GetAllDataFromDB<B_Base>(new B_Base(), "bbase");
        m_BP = DBDataUtil.getInstance().GetAllDataFromDB<B_Pos>(new B_Pos(), "bpos");
        m_Item = DBDataUtil.getInstance().GetAllDataFromDB<Item>(new Item(), "item");
        m_BI = DBDataUtil.getInstance().GetAllDataFromDB<BuildingInfo>(new BuildingInfo(), "binfo");
    }

    public void Test()
    {
        Debug.Log("Test-------------------");
    }
    public List<B_Base> BB()
    {    return m_BB;    }
    public List<B_Pos> BP()
    { return m_BP; }
    public List<Item> Item()
    { return m_Item; }
    public List<BuildingInfo> BI()
    { return m_BI; }

    //public void GetResBuildBase(int MyID)
    //{
    //    var BuildBase = from n in m_BB
    //                    where n.ID == MyID
    //                    select new
    //                    {
    //                        n.ID,
    //                        n.B_TYPE,
    //                        n.B_NAME,
    //                        n.B_ICON_NAME,
    //                        n.B_ATLAS_NAME,
    //                        n.B_LEV,
    //                        n.MAX_LEV,
    //                        n.LEV_UP_TIME,
    //                        n.ITEM_ID,
    //                        n.ITEMS_MAX_COUNT,
    //                        n.ITEMS_NEED_TIME,
    //                        n.BUY_NEED_GOLD,
    //                        n.NEED_TIME1,
    //                        n.COUNT1,
    //                        n.NEED_TIME2,
    //                        n.COUNT2,
    //                        n.NEED_TIME3,
    //                        n.COUNT3,
    //                        n.BUILD_STR
    //                    };
    //    foreach (var BB in BuildBase)
    //    {
    //        GlobalVar.GetInstance().MyBB.Add(new B_Base()
    //        {
    //            ID = BB.ID,
    //            B_TYPE = BB.B_TYPE,
    //            B_NAME = BB.B_NAME,
    //            B_ICON_NAME = BB.B_ICON_NAME,
    //            B_ATLAS_NAME = BB.B_ATLAS_NAME,
    //            B_LEV = BB.B_LEV,
    //            MAX_LEV = BB.MAX_LEV,
    //            LEV_UP_TIME = BB.LEV_UP_TIME,
    //            ITEM_ID = BB.ITEM_ID,
    //            ITEMS_MAX_COUNT = BB.ITEMS_MAX_COUNT,
    //            ITEMS_NEED_TIME = BB.ITEMS_NEED_TIME,
    //            BUY_NEED_GOLD = BB.BUY_NEED_GOLD,
    //            NEED_TIME1 = BB.NEED_TIME1,
    //            COUNT1 = BB.COUNT1,
    //            NEED_TIME2 = BB.NEED_TIME2,
    //            COUNT2 = BB.COUNT2,
    //            NEED_TIME3 = BB.NEED_TIME3,
    //            COUNT3 = BB.COUNT3,
    //            BUILD_STR = BB.BUILD_STR
    //        });

    //    }
    //}
    //public void GetResItemsBase(int ItemID)
    //{
    //    var ItemBase = from n in m_Item
    //                   where n.ITEM_ID == ItemID
    //                   select new
    //                   {
    //                       n.ITEM_ID,
    //                       n.ITEM_ATLAS_NAME,
    //                       n.ITEM_ICON_NAME,
    //                       n.ITEM_NAME
    //                   };
    //    foreach (var IB in ItemBase)
    //        Debug.Log("Test-----------------------");
    //}
}
