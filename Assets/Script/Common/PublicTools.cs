using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PublicTools
{
    /*************************************************************************************************/
    private static PublicTools instance = null;
    public static PublicTools GetInstance()
    {
        if (instance == null)
        {
            instance = new PublicTools();
        }
        return instance;
    }

    public static string GetSoldierImage(int baseID)
    {
        return "head_all_"+baseID.ToString();
    }

    public static Texture GetSoldierBodyImage(int roleId)
    {
        string path = "HeadAll/head_all_"+roleId.ToString();
        return Resources.Load<Texture>(path);
    }

    public static Texture GetPlayerHeadImage(int headImageID)
    {
        string path = "Head/head_"+headImageID.ToString();
        return Resources.Load<Texture>(path);
    }

    public static Texture GetSoldierHeadImage(int roleId)
    {
        string path = "Head/head_"+roleId.ToString();
        return Resources.Load<Texture>(path);
    }

    public static string GetBattleItemIcon(int id)
    {
        return "Instance_skill_"+id.ToString();
    }

    public static string GetTreasureFragmentIcon(int id)
    {
        return "bws_"+id.ToString();
    }

    public static string GetSoldierTypeDescription(int type)
    {
        string ret = "未知型";
        switch(type)
        {
            case 1:
                ret = "体力型";
                break;
            case 2:
                ret = "均衡型";
                break;
            case 3:
                ret = "攻击型";
                break;
            case 4:
                ret = "范围型";
                break;
            case 5:
                ret = "速度型";
                break;
            case 6:
                ret = "支援型";
                break;
            case 7:
            case 8:
                ret = "特殊型";
                break;
            default:
                break;
        }
        return ret;
    }

    public static string GetSoldierAtkSpeedDescription(double atks)
    {
        string description = null;
        if(atks>=3.0)
        {
            description = "非常快";
        }else if(atks>=2.5)
        {
            description = "快";
        }else if(atks>=2.0)
        {
            description = "普通";
        }else if(atks>=1.5)
        {
            description = "慢";
        }else
        {
            description = "非常慢";
        }
        return description;
    }

    public static string GetSoldierWalkSpeedDescription(double ws)
    {
        string description = null;
        if(ws>=140)
        {
            description = "非常快";
        }else if(ws>=110)
        {
            description = "快";
        }else if(ws>=80)
        {
            description = "普通";
        }else if(ws>=50)
        {
            description = "慢";
        }else
        {
            description = "非常慢";
        }
        return description;
    }

    public static string GetUpgradeOptionIcon(int iconID)
    {
        return "Upgrade_Icon_"+iconID.ToString("D2");
    }
    /*************************************************************************************************/

    private static int seed;

    public static string GetStoryHead(int headID)
    {
        string str = "juqing_" + headID.ToString();
        return str;
    }

    public static string CreateRandomNickname()
    {
        //List<RandomName> randomNameList = MyApp.Instance().GetDataManager().GetRandomNameList();
        string nickname = null;
        //do
        //{
        //    nickname = "";
        //    int[] partIndex = {0,0,0};
        //    int count = randomNameList.Count;
        //    for (int i=0; i<partIndex.Length; i++)
        //    {
        //        partIndex [i] = Random.Range(0, count);
        //    }
        //    nickname += randomNameList [partIndex [0]].Part1 + randomNameList [partIndex [1]].Part2 + randomNameList [partIndex [2]].Part3;
        //} while(nickname.Length>ConstData.NicknameMaxLength);
        return nickname;
    }

    public static string GetHeroColorFrame(int colour)
    {
        //string pRet = "dengjikuang_" + colour.ToString();
        string pRet = "wujiangkuang_" + colour.ToString();
        return pRet;
    }

    //New function
    public static string GetHeroFrame(int colour)
    {
        string ret = "wujiangkuang_" + colour.ToString();
        return ret;
    }

    public static void UpdateHeroStars(int colour, UIGrid starGrid)
    {
        for (int i=0; i<starGrid.transform.childCount; i++)
        {
            starGrid.transform.GetChild(i).gameObject.SetActive(i < colour);
        }
        starGrid.hideInactive = true;
        starGrid.Reposition();
    }
    
    public static string GetHeroImage(int heroID)
    {
        string pRet = "WJ_" + heroID.ToString();
        return pRet;
    }
    
    public static string GetHeroBackground(int colour)
    {
//        string pRet = "tubiaodiban_" + colour.ToString();
//        return pRet;
        return "wupindiban_3";
    }

    public static string GetHeroSkillIcon(int id)
    {
        return "JN_" + id.ToString();
    }

    public static int GetBarrackMainGrade(int grade)
    {
        int pRet = (grade - 1) / 5 + 1;
        return pRet;
    }
    
    public static string GetBarrackColorFrame(int grade)
    {
        int mGrade = GetBarrackMainGrade(grade);
        string pRet = "wupinkuang_" + mGrade.ToString();
        return pRet;
    }
    
    public static string GetBarrackImage(int barrackID)
    {
        //int mGrade = GetBarrackMainGrade(grade);
        //string pRet = "bingying_" + barrackID.ToString() + "_" + mGrade.ToString();
        //return pRet;
        return "test";
    }
    
    public static string GetBarrackBackground(int grade)
    {
//        int mGrade = GetBarrackMainGrade(grade);
//        string pRet = "tubiaodiban_" + mGrade.ToString();
//        return pRet;
        return "wupindiban_3";
    }
    
    public static string GetBarrackSolidifyTag(bool solidify)
    {
        string pRet = null;
        if (solidify)
        {
            pRet = "jiagu_2";
        } else
        {
            pRet = "jiagu_1";
        }
        return pRet;
    }
    
    
    //**************************************************
    public static string GetItemColorFrame(int colour)
    {
        string pRet = "wupinkuang_" + colour.ToString();
        return pRet;
    }
    
    public static string GetFragColorFrame(int color)
    {
        string path = "suipian0" + color.ToString();
        return path;
    }
    
    public static string GetItemFrameFromTypeAndColor(int type, int color)
    {
        if (type == 2)
        {
            return GetFragColorFrame(color);
        } else
        {
            return GetItemColorFrame(color);
        }
    }
    
    public static string GetItemBodyFromPath(int path)
    {
        string name = "WP" + path.ToString();
        return name;
    }
    
    public static string GetItemBackGroundFromType(int type)
    {
//        if (type == 2)
//        {
//            return "suipiandiban_1";
//        } else
//        {
//            return "wupindiban_3";
//        }
        return "wupindiban_3";
    }
    
    public static string GetItemTypeDescription(int type)
    {
        string ret = "未知";
        switch (type)
        {
            case 0:
                ret = "消耗品";
                break;
            case 1:
                ret = "材料";
                break;
            case 2:
                ret = "碎片";
                break;
            case 3:
                ret = "其他";
                break;
            default:
                break;
        }
        return ret;
    }
    
    public static string GetCitySkill(int id)
    {
        string name = "UISkill_" + id.ToString();
        return name;
    }
    
    static public GameObject LoadPrefab(string path, GameObject parent =null)
    {
        Object source = Resources.Load(path);
        if (source == null)
            return null;

        GameObject obj = GameObject.Instantiate(source) as GameObject;
        if (parent != null)
        {
            obj.transform.parent = parent.transform;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
        }
        return obj;
    }

    static public GameObject CloneObject(GameObject cloneObject,GameObject parent = null)
    {
        GameObject obj = GameObject.Instantiate(cloneObject) as GameObject;
        if(parent != null)
        {
            obj.transform.parent = parent.transform;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
        }
        return obj;
    }

    static public GameObject LoadCommonModel(string name)
    {
        GameObject parent = GameObject.FindWithTag("Scene");
        string path = "Prefeb/Common/" + name;
        GameObject obj =LoadPrefab(path, parent);

        return obj;
    }
    
    static public GameObject LoadCommonPrefab(string name, GameObject parent)
    {
        string path = "Prefeb/Common/" + name;
        return LoadPrefab(path, parent);
    }
    

    static public GameObject GetChildGameObjectFromName(GameObject Origine, string name)
    {
        Transform child = Origine.transform.FindChild(name);
        if (child != null)
        {
            return child.gameObject;
        } else
        {
            return null;
        }
    }

    static public string GetNumCharacter(int num)
    {
        string str = "";
        switch (num)
        {
            case 1:
                str = "一";
                break;
            case 2:
                str = "二";
                break;
            case 3:
                str = "三";
                break;
            case 4:
                str = "四";
                break;
            case 5:
                str = "五";
                break;
            case 6:
                str = "六";
                break;
            case 7:
                str = "七";
                break;
            case 8:
                str = "八";
                break;
            case 9:
                str = "九";
                break;
        }
        return str;
    }

    static public string GetSoldierGradeName(int grade)
    {
        string name = "";
        int largerank = (grade - 1) / 5;
        int smallrank = (grade - 1) % 5 + 1;
        switch (largerank)
        {
            case 0:
                name = "民兵" + "+" + smallrank.ToString();
                break;
            case 1:
                name = "精兵" + "+" + smallrank.ToString();
                break;
            case 2:
                name = "校尉" + "+" + smallrank.ToString();
                break;
            case 3:
                name = "虎尉" + "+" + smallrank.ToString();
                break;
            case 4:
                name = "锦尉" + "+" + smallrank.ToString();
                break;
            default:
                break;
        }
        return name;
    }
	
    static public string GetTimeStringFromSecond(int second)
    {
        string time = "";
        string hour = "";
        string minute = "";
        string sec = "";
        
        hour = ((second / 3600) > 9) ? (second / 3600).ToString() : "0" + (second / 3600).ToString();
        minute = (((second % 3600) / 60) > 9) ? ((second % 3600) / 60).ToString() : "0" + ((second % 3600) / 60).ToString();
        sec = ((second % 60) > 9) ? (second % 60).ToString() : "0" + (second % 60).ToString();

        time = hour + ":" + minute + ":" + sec;
        return time;
    }

    static public void ClearAllChild(GameObject parent)
    {
        int length = parent.transform.childCount;
        for (int i = 0; i < length; i++)
        {
            GameObject.DestroyImmediate(parent.transform.GetChild(i).gameObject);
            i--;
            length = parent.transform.childCount;
        }
    }

    static public void SetLocalX(GameObject obj, float x)
    {
        Transform trans = obj.transform;
        trans.localPosition = new Vector3(x, trans.localPosition.y, trans.localPosition.z);
    }

    static public void AddLocalX(GameObject obj, float x)
    {
        Transform trans = obj.transform;
        trans.localPosition += new Vector3(x, 0, 0);
    }

    static public void SetRandomSeed()
    {
        seed = NGUITools.RandomRange(1000, 1000000);
    }

    static public int Encrypt(int originnum)
    {
        int encryptnum = originnum;
        encryptnum ^= seed;
        return encryptnum;
    }

    static public int Decrypt(int encryptnum)
    {
        int decryptnum = encryptnum;
        decryptnum ^= seed;
        return decryptnum;
    }

    static public GameObject FindGameObjectFromModel(string name)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("CommonModel");
        foreach(GameObject obj in objs)
        {
            if (obj.name == name)
                return obj;
        }
        return null;
    }

    static public GameObject LoadCollisionPrefab(GameObject parent)
    {
        string path = "Prefeb/GuideCollide/Collision";
        GameObject obj =LoadPrefab(path, parent);
        return obj;
    }

    public static void CreateFile(string path, string name, string info)
    {
        StreamWriter sw;
        FileInfo t = new FileInfo(path + "//" + name);
        if (!t.Exists)
        {
            sw = t.CreateText();
        } else
        {
            t.Delete();
            sw = t.AppendText();
        }

        sw.WriteLine(info);
        sw.Close();
        sw.Dispose();
    }

}