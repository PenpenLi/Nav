using UnityEngine;
using System.Collections;

public class UITipBar : MonoBehaviour
{
    public UILabel tipLabel = null;

    private const string PrefabPath = "Prefeb/Common/UITipBar";
    // Use this for initialization
    void Start()
    {
    
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    public static void ShowTip(string tip)
    {
        GameObject tipBar = NGUITools.AddChild(PublicGameObject.publicGameObj,Resources.Load<GameObject>(PrefabPath));
        tipBar.GetComponent<UITipBar>().Show(tip);
    }

    public void Show(string tip)
    {
        tipLabel.text = tip;
    }

    public void OnFadeOut()
    {
        Destroy(gameObject);
    }
}
