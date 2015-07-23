using UnityEngine;
using System.Collections;

public class UIMessageBox : MonoBehaviour
{
    public UILabel contentLabel=null;

    public delegate void UIMessageBoxEvent(bool result);
    private UIMessageBoxEvent eventCallback=null;

    private const string PrefabPath = "Prefeb/Common/UIMessageBox";

    // Use this for initialization
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    public static void ShowMessageBox(string content,UIMessageBoxEvent callback)
    {
        GameObject messageBox = NGUITools.AddChild(PublicGameObject.publicGameObj,Resources.Load<GameObject>(PrefabPath));
        messageBox.GetComponent<UIMessageBox>().Show(content,callback);
    }

    public void OnConfirmButtonClick(GameObject go)
    {
        NoticeResult(true);
        Close();
    }

    public void OnCancelButtonClick(GameObject go)
    {
        NoticeResult(false);
        Close();
    }

    public void Show(string content,UIMessageBoxEvent callback)
    {
        contentLabel.text = content;
        eventCallback = callback;
    }

    private void Close()
    {
        Destroy(this.gameObject);
    }

    private void NoticeResult(bool result)
    {
        if(eventCallback==null)
        {
            return;
        }
        eventCallback(result);
    }
}
