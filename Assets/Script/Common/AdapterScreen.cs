using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class AdapterScreen : MonoBehaviour {

	float m_aspect;
    public float m_standardAspect = 1.78f; //16:9
	int m_manualHeight;

    public Camera m_camera;

    private UIRoot m_root;
	// Use this for initialization
	void Start () {
		m_aspect = (float) Screen.width/Screen.height;
        m_root = NGUITools.GetRoot(gameObject).GetComponent<UIRoot>();

        Debug.Log("m_root = " + m_root);
        m_manualHeight = m_root.manualHeight;
        if(m_aspect<m_standardAspect )
		{
			int height = (int)(m_manualHeight*m_standardAspect/m_aspect);
            m_root.manualHeight = height;
		}
		else
		{
            ClipWidth();
            m_root.manualHeight = m_manualHeight;
		}
            
	}
	
	// Update is called once per frame
	void Update () {
		m_aspect = (float) Screen.width/Screen.height;
        if(m_aspect<m_standardAspect )
		{
            //Debug.Log("m_manualHeight = " + m_manualHeight + " m_standardAspect = " + m_standardAspect + " m_aspect = " + m_aspect + "  m_root.manualHeight = " + m_root.manualHeight);
			int height = (int)(m_manualHeight*m_standardAspect/m_aspect);
            m_root.manualHeight = height;
		}
		else
		{
            ClipWidth();
            m_root.manualHeight = m_manualHeight;
		}
	}

	void ClipWidth()
	{
        float clipSize = (m_aspect - m_standardAspect)/m_aspect;
        m_camera.GetComponent<Camera>().rect = new Rect(clipSize * 0.5f, 0, 1 - clipSize, 1);
	}

    void ClipHeight()
    {
        float aspectInverse = 1 / m_aspect;
        float clipSize = (aspectInverse - 1 / m_standardAspect) / aspectInverse;
        m_camera.GetComponent<Camera>().rect = new Rect(0, clipSize * 0.5f, 1, 1-clipSize);
    }
}
