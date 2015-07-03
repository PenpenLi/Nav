using UnityEngine;
using System.Collections;

public class WarningClose : MonoBehaviour {
    public GameObject m_Close;
    public GameObject m_warning;

	// Use this for initialization
	void Start () {
        UIEventListener.Get(m_Close).onClick = onClose;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void onClose(GameObject go)
    {
        m_warning.SetActive(false);
    }
}
