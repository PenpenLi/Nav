using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Formation : MonoBehaviour {
    public List<GameObject> m_PosList = new List<GameObject>();
	// Use this for initialization
	void Start () {
        Vector3 vec3 = GetIndexPos(0);
        Debug.Log("vec3 = " + vec3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //返回指定索引的船体位置 1-5进攻方 6-10防守方（做这个Prefab必须要保证进攻方的位置和防守方的位置要做到镜像一样）
    public Vector3 GetIndexPos(int Index)
    {
        if (Index > 10 || Index < 1)
        {
            return Vector3.zero;
        }
        for (int i = 0; i < m_PosList.Count; i++)
        {
            Debug.Log("m_PosList[i].transform.localPosition = " + m_PosList[i].transform.localPosition);
            if (Index == i)
            {
                Vector3 pos = m_PosList[i].transform.localPosition;
                //Debug.Log("pos = " + pos);
                return pos;
            }
        }
        return Vector3.zero;
    }
}
