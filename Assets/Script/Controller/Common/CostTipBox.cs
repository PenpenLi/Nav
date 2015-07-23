using UnityEngine;
using System.Collections;

public class CostTipBox : MonoBehaviour
{
    public UILabel contentLabel = null;
    public UILabel costLabel = null;

    private System.Action<bool> eventCallback = null;

    private const string PrefabPath = "Prefeb/Common/CostTipView";

    // Use this for initialization
    void Start()
    {
    
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    public static void ShowCostTip(string content,int cost, System.Action<bool> eventCallback)
    {
        GameObject tipObj = NGUITools.AddChild(PublicGameObject.publicGameObj,Resources.Load<GameObject>(PrefabPath));
        tipObj.GetComponent<CostTipBox>().UpdateInfo(content,cost,eventCallback);
    }

    public void UpdateInfo(string content,int cost,System.Action<bool> eventCallback)
    {
        contentLabel.text = content;
        costLabel.text = cost.ToString();
        this.eventCallback = eventCallback;
    }

    public void OnConfirmButtonClick()
    {
        if(eventCallback != null)
        {
            eventCallback.Invoke(true);
        }
        Close();
    }

    public void OnCancelButtonClick()
    {
        if(eventCallback != null)
        {
            eventCallback.Invoke(false);
        }
        Close();
    }

    private void Close()
    {
        Destroy(gameObject);
    }
}
