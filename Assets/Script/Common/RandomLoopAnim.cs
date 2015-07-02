using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SkeletonAnimation))]
public class RandomLoopAnim : MonoBehaviour
{
    public float startDelay = 0.0f;
    public float minInterval = 5.0f;
    public float maxInterval = 8.0f;

    public string animName = null;

    // Use this for initialization
    void Start()
    {
        SkeletonAnimation anim = GetComponent<SkeletonAnimation>();
        anim.loop = false;
        anim.AddOnCompleteListener(OnAnimComplete);
        Invoke("PlayAnim",startDelay);
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    void PlayAnim()
    {
        SkeletonAnimation anim = GetComponent<SkeletonAnimation>();
        anim.SetAnim(animName,false);
    }

    void OnAnimComplete(Spine.AnimationState state, int trackIndex, int loopCount)
    {
        SkeletonAnimation anim = GetComponent<SkeletonAnimation>();
        anim.SetAnim(null,false);
        float interval = Random.Range(minInterval,maxInterval);
        Invoke("PlayAnim",interval);
    }

}
