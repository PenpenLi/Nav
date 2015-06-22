﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum FIGHTSTATE
{
    Boom,
    Fly,
    Hit
}

public class Fight : MonoBehaviour
{
    public GameObject m_AttackTeam1;
    public GameObject m_Sheel;
    public GameObject m_BoomEffect;
    public GameObject m_HitEffect;
    GameObject m_AttackFormation;
    GameObject m_DefenceFormation;
    private int m_CurShipIndex = 0;
    public float FireSpeed = 1.0f / 60.0f;//攻击速度
    private List<GameObject> m_shipList = new List<GameObject>();
    FIGHTSTATE m_FightState = new FIGHTSTATE();
    TimeSpan m_ts;
    TimeSpan m_lastts;
    // Use this for initialization
    void Start()
    {
        m_FightState = FIGHTSTATE.Boom;
        LoadFormation(1, 0);//加载进攻阵型
        LoadFormation(1, 1);//加载防守阵型

        List<int> shipidList = TestShips();
        //Debug.Log ("shipidList.Count = " + shipidList.Count);
       // InitShips(shipidList);
    }

    // Update is called once per frame
    void Update()
    {
        //AttackLoop();
    }

    void LoopState()
    {
        switch(m_FightState)
        {
            case FIGHTSTATE.Boom:

                break;
            case FIGHTSTATE.Fly:

                break;
            case FIGHTSTATE.Hit:

                break;
        }
    }

    void BoomSate()
    {
        if (m_BoomEffect.activeSelf != true)
            m_BoomEffect.SetActive(true);

    }

    //测试函数
    List<int> TestShips()
    {
        List<int> shipsIDList = new List<int>();
        //id从1开始
        shipsIDList.Add(1);
        shipsIDList.Add(2);
        shipsIDList.Add(3);
        shipsIDList.Add(4);
        shipsIDList.Add(5);
        shipsIDList.Add(6);
        shipsIDList.Add(7);
        shipsIDList.Add(8);
        shipsIDList.Add(9);
        shipsIDList.Add(10);
        shipsIDList.Add(11);
        shipsIDList.Add(12);
        return shipsIDList;
    }

    //加载指定阵型
    bool LoadFormation(int Index, int type)
    {
        if (type == 0)
        {
            m_AttackFormation = Instantiate(Resources.Load("Prefab/Formation/Formation" + Convert.ToString(Index)), Vector3.one, Quaternion.identity) as GameObject;
            if (m_AttackFormation == null)
            {
                Debug.Log("Prefab/Formation/Formation" + Convert.ToString(Index) + " Load Failed");
                return false;
            }
            m_AttackFormation.transform.parent = this.transform;
            m_AttackFormation.transform.localPosition = Vector3.zero;
            m_AttackFormation.transform.localScale = Vector3.one;
            return true;
        }
        else
        {
            m_DefenceFormation = Instantiate(Resources.Load("Prefab/Formation/Formation" + Convert.ToString(Index)), Vector3.one, Quaternion.identity) as GameObject;
            if (m_DefenceFormation == null)
            {
                Debug.Log("Prefab/Formation/Formation" + Convert.ToString(Index) + " Load Failed");
                return false;
            }
            m_DefenceFormation.transform.parent = this.transform;
            m_DefenceFormation.transform.localPosition = Vector3.zero;
            m_DefenceFormation.transform.localScale = Vector3.one;
            return true;
        }
    }

    bool LoadShipData(int id)
    {
        //Vector3 test = new Vector3(525, -51,0);
        //m_TestFire.GetComponent<ShipFight>().OpenFire(test, 1);
        return true;
    }

    //初始化双方的战船，根据阵型数据来设置船体数据
    bool InitShips(List<int> shipIDList)
    {
        Debug.Log("InitShips---------------------");
        if (shipIDList.Count > GlobalVar.m_maxShips)
        {
            return false;
        }
        //Debug.Log ("shipIDList.Count != 10");
        //如果没有初始化战斗和防守阵型就不能初始化战舰队列
        if (m_AttackFormation == null || m_DefenceFormation == null)
        {
            return false;
        }
        //战斗的敌方在左上方，战斗的本家在右下方
        for (int i = 0; i < GlobalVar.m_maxShips; i++)
        {//
            //Debug.Log("shipIDList = " + shipIDList[i] + "GetIndexPos = " + m_AttackFormation.GetComponent<Formation>().GetIndexPos(i));
            if (i < (GlobalVar.m_maxShips / 2))
            {
                m_AttackTeam1.GetComponent<Team>().AddShip(shipIDList[i], m_AttackFormation.GetComponent<Formation>().GetIndexPos(i), UnityEngine.Random.Range(1, 5), UnityEngine.Random.Range(1, 4));
            }
            else
            {
                m_AttackTeam1.GetComponent<Team>().AddShip(shipIDList[i], m_DefenceFormation.GetComponent<Formation>().GetIndexPos(i), UnityEngine.Random.Range(1, 5), UnityEngine.Random.Range(1, 4));
            }

        }
        //m_shipList = m_AttackTeam1.GetComponent<Team>().GetShipList();
        return true;
    }

    public void OpenFireTest()
    {
        LoadShipData(1);
        //m_TestFire.GetComponent<ShipFight>().OpenFire(shipsIDList[8]
    }

    private void AttackLoop()
    {
        if (GlobalVar.GetInstance().GetFinishShell())//上一次的炮弹已经打完了
        {
            //Debug.Log("Begin Fire");
            Vector3 targetPos = new Vector3();
            int type = 0;
            //Debug.Log("m_CurShipIndex = " + m_CurShipIndex + " GlobalVar.m_maxTeamShips -1 = " + (GlobalVar.m_maxTeamShips));
            if (m_CurShipIndex >= GlobalVar.m_maxTeamShips)
            {
                //Debug.Log("m_CurShipIndex > GlobalVar.m_maxTeamShips");
                targetPos = m_shipList[m_CurShipIndex - GlobalVar.m_maxTeamShips].transform.localPosition;
                type = 0;
            }
            else
            {
                //Debug.Log("else m_CurShipIndex > GlobalVar.m_maxTeamShips");
                targetPos = m_shipList[m_CurShipIndex + GlobalVar.m_maxTeamShips].transform.localPosition;
                type = 5;
            }
            //m_shipList[m_CurShipIndex].GetComponent<ShipFight>().OpenFire(targetPos, type);
            m_Sheel.GetComponent<Shell>().SetShellTrack(targetPos, m_shipList[m_CurShipIndex].transform.localPosition, type);
            m_Sheel.GetComponent<Shell>().SetShellType(m_shipList[m_CurShipIndex].GetComponent<ShipFight>().GetShellType());

            m_Sheel.SetActive(true);
            m_CurShipIndex++;
            //Debug.Log("m_CurShipIndex = " + m_CurShipIndex);
            if (m_CurShipIndex >= m_shipList.Count)
            {
                m_CurShipIndex = 0;
            }
            //Debug.Log("m_CurShipIndex = " + m_CurShipIndex + " m_Sheel.activeSelf = " + m_Sheel.activeSelf);
            GlobalVar.GetInstance().SetFinishShell(false);
        }
        if (m_Sheel.activeSelf && GlobalVar.GetInstance().GetFinishShell() == false)
        {
            m_Sheel.transform.localPosition = m_Sheel.GetComponent<Shell>().AddCubicPos(FireSpeed);

            if (m_Sheel.GetComponent<Shell>().GetCubicPos() >= 1.0f)
            {
                GlobalVar.GetInstance().SetFinishShell(true);
                m_Sheel.GetComponent<Shell>().SetCubicPos(0.0f);
                m_Sheel.SetActive(false);

            }
            //Debug.Log(" m_Sheel.GetComponent<Shell>().GetCubicPos() = " + m_Sheel.GetComponent<Shell>().GetCubicPos());
        }
    }
}
