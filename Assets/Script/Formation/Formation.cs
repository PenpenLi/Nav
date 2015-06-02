using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Formation : MonoBehaviour {
    public List<GameObject> m_PosList = new List<GameObject>();
	// Use this for initialization
	void Start () {
        Vector3 vec3 = GetIndexPos(0);
        //Debug.Log("vec3 = " + vec3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//返回指定索引的船体位置 1-5作为进攻方的阵型 6-10作为防守方的阵型（做这个Prefab必须要保证进攻方的位置和防守方的位置要做到镜像一样）
    public Vector3 GetIndexPos(int Index)
    {
        if (Index > GlobalVar.m_maxShips)
        {
            return Vector3.zero;
        }
        for (int i = 0; i < m_PosList.Count; i++)
        {
            //Debug.Log("m_PosList[i].transform.localPosition = " + m_PosList[i].transform.localPosition);
            if (Index == i)
            {
                Vector3 pos = m_PosList[i].transform.localPosition;
                return pos;
            }
        }
        return Vector3.zero;
    }

	public List<GameObject> GetAttackList(){
		List<GameObject> attackList = new List<GameObject>();
		for(int i = 0; i < 5; i++){
			attackList.Add(m_PosList[i]);
		}
		return attackList;
	}

	public List<GameObject> GetDefenceList(){
		List<GameObject> defenceList = new List<GameObject>();
		for(int i = 5; i < 10; i++){
			defenceList.Add(m_PosList[i]);
		}
		return defenceList;
	}
	
}
