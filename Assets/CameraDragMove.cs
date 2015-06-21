using UnityEngine;
using System.Collections;

public class CameraDragMove : MonoBehaviour
{
    public Camera m_camera;
    private Vector3 ResetCamera;
    private Vector3 Origin;
    private Vector3 Diference;
    private Vector3 Dif;
    private bool Drag = false;
    void Start()
    {
        //ResetCamera = Camera.main.transform.position;
        ResetCamera = m_camera.transform.position;        
        //Debug.Log(ResetCamera);
        Dif = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_camera.transform.position.x < -5.5)
        {                                    
            Debug.Log("超出左边边界：" + m_camera.transform.position.x);
        }
        Debug.Log("m_camera.transform.position.x);"+ m_camera.transform.position.x);
    }
    void LateUpdate()
    {
        if (Input.GetMouseButton(0)) // 鼠标左键是否按下
        {
            Diference = (m_camera.ScreenToWorldPoint(Input.mousePosition)) - ResetCamera;
            if (Drag == false)
            {
                Drag = true;
                Origin = m_camera.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            Drag = false;
        }
        if (Drag == true)
        {
            ResetCamera = Origin - Diference;
            if (ResetCamera.x > -5.5)
            {
                m_camera.transform.position = ResetCamera;
            }
            else
            {
                ResetCamera.x = -5.4f;
                m_camera.transform.position = ResetCamera;
            }
            //Debug.Log("当前摄像机坐标 = " + ResetCamera);
            
        }
        //RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
        if (Input.GetMouseButton(1))// 鼠标右键是否按下
        {
            m_camera.transform.position = Dif;
        }
    }
}
