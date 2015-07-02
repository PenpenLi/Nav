using UnityEngine;
using System.Collections;

public class AccelerateController : MonoBehaviour
{
    public Vector2 offset = Vector2.zero;

	private SmoothCaculate xSmoothCaculate = new SmoothCaculate(0.6f);
	private SmoothCaculate ySmoothCaculate = new SmoothCaculate(0.6f);
	private bool smoothEnable = true;
	private Vector3 originalPos = Vector3.zero;
	
    // Use this for initialization
    void Start()
    {
		originalPos = transform.localPosition;
    }
    
    // Update is called once per frame
    void Update()
    {
        float offsetX = offset.x * Input.acceleration.x;
        float offsetY = offset.y * Input.acceleration.y;

		if(smoothEnable)
		{
			xSmoothCaculate.SetNewData(offsetX);
			ySmoothCaculate.SetNewData(offsetY);
			offsetX = xSmoothCaculate.GetSecondExponentialValue();
			offsetY = ySmoothCaculate.GetSecondExponentialValue();
		}

        transform.localPosition = originalPos + new Vector3(offsetX,offsetY);
    }
}
