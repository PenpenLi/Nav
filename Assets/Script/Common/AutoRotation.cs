using UnityEngine;
using System.Collections;

public class AutoRotation : MonoBehaviour
{
    public float speed = 0.0f;
    // Use this for initialization
    void Start()
    {
    
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back * Time.deltaTime * speed);
    }
}
