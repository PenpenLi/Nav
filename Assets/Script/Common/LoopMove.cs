using UnityEngine;
using System.Collections;

public class LoopMove : MonoBehaviour
{
    public float duration = 1.0f;
    public float interval = 1.0f;
    public Vector3 from = Vector3.zero;
    public Vector3 to = Vector3.zero;

    private TweenPosition tweenPos = null;

    void Start()
    {
        GetComponent<TweenPosition>().AddOnFinished(OnTweenFinished);
    }

    void Update()
    {

    }

    void OnTweenFinished()
    {
        tweenPos = TweenPosition.Begin(gameObject,duration,to);
        tweenPos.from = from;
        tweenPos.to = to;
        tweenPos.duration = duration;
        tweenPos.delay = interval;
    }
}
