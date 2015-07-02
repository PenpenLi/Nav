using UnityEngine;
using System.Collections;

public class ChangeLayer : MonoBehaviour
{
    public int queue = 3000;
    public bool isLock = false;

    public bool always = false;

    private int lockQueue =0;
    // Use this for initialization
    void Start()
    {
        lockQueue = queue;
        SetQueue(queue);
    }
    
    // Update is called once per frame
    void Update()
    {
        if(always)
        {
            SetQueue(queue);
        }

    }

    public int GetQueue()
    {
        return queue;
    }
    public void SetQueue(int que)
    {
        if(isLock)
            queue = lockQueue;
        else
            queue = que;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            spriteRenderer.material.renderQueue = queue;
        
        SkeletonAnimation skeletonAnimation=GetComponent<SkeletonAnimation>();
        if (skeletonAnimation != null)
        {
            //Debug.Log("SetQueue queue =" + queue);
            skeletonAnimation.skeletonDataAsset.atlasAsset.materials[0].renderQueue = queue;
        }
        
        UIPanel uiPanel = GetComponent<UIPanel>();
        if(uiPanel != null)
        {
            uiPanel.startingRenderQueue = queue;
        }

//        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
//        if (meshRenderer != null)
//        {
//            meshRenderer.material.renderQueue = queue;
//        }
        if(GetComponent<Renderer>() != null)
            GetComponent<Renderer>().material.renderQueue = queue;
    }
}
