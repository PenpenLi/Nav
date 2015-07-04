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

   // private List<SoldierBase> m_soldierList = new List<SoldierBase>();
 


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
        //m_soldierList = DBDataUtil.getInstance().GetAllDataFromDB<SoldierBase>(new SoldierBase(), "soldier");
    }

    //public Artillery GetArtilleryData(int id)
    //{
    //    for(int i = 0; i < m_artillery.Count; i++)
    //    {
    //        if(m_artillery[i].Id == id)
    //        {
    //            return m_artillery[i];
    //        }
    //    }

    //    return null;
    //}

}
