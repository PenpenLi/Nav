using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Fight : MonoBehaviour {
    public GameObject m_AttackTeam1;
    public GameObject m_DefenceTeam2;
    GameObject m_AttackFormation;
	GameObject m_DefenceFormation;
	// Use this for initialization
	void Start () {
		LoadFormation(1,0);//加载进攻阵型
		LoadFormation(1,1);//加载防守阵型

		List<int> shipidList = TestShips();
		Debug.Log ("shipidList.Count = " + shipidList.Count);
		InitShips(shipidList);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//测试函数
	List<int> TestShips(){
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
		return shipsIDList;
	}

    //加载指定阵型
    bool LoadFormation(int Index, int type)
    {
		if(type == 0)
		{
			m_AttackFormation = Instantiate(Resources.Load ("Prefab/Formation/Formation" + Convert.ToString(Index)), Vector3.one, Quaternion.identity) as GameObject;
			if(m_AttackFormation == null){
				Debug.Log ("Prefab/Formation/Formation" + Convert.ToString(Index) + " Load Failed");
				return false;
			}
			m_AttackFormation.transform.parent = this.transform;
			m_AttackFormation.transform.localPosition = Vector3.zero;
			m_AttackFormation.transform.localScale = Vector3.one;
			return true;
		}else{
			m_DefenceFormation = Instantiate(Resources.Load ("Prefab/Formation/Formation" + Convert.ToString(Index)), Vector3.one, Quaternion.identity) as GameObject;
			if(m_DefenceFormation == null){
				Debug.Log ("Prefab/Formation/Formation" + Convert.ToString(Index) + " Load Failed");
				return false;
			}
			m_DefenceFormation.transform.parent = this.transform;
			m_DefenceFormation.transform.localPosition = Vector3.zero;
			m_DefenceFormation.transform.localScale = Vector3.one;
			return true;
		}
		return false;
    }

	//初始化双方的战船，根据阵型数据来设置船体数据
	bool InitShips(List<int> shipIDList){
		Debug.Log ("InitShips---------------------");
		if(shipIDList.Count != 10){
			return false;
		}
		Debug.Log ("shipIDList.Count != 10");
		//如果没有初始化战斗和防守阵型就不能初始化战舰队列
		if(m_AttackFormation == null || m_DefenceFormation == null){
			return false;
		}
		//战斗的敌方在左上方，战斗的本家在右下方
		for(int i = 0; i < 5; i++){//
			Debug.Log ("shipIDList = " + shipIDList[i] + " i = " + i);
			m_AttackTeam1.GetComponent<Team>().AddShip(shipIDList[i], m_AttackFormation.GetComponent<Formation>().GetIndexPos(i));
		}

		for(int i = 5; i < 10; i++){
			Debug.Log ("shipIDList = " + shipIDList[i] + " i = " + i);
			m_DefenceTeam2.GetComponent<Team>().AddShip(shipIDList[i], m_DefenceFormation.GetComponent<Formation>().GetIndexPos(i));
		}
		return true;
	}
}
