using UnityEngine;
using System.Collections;

/// <summary>
/// 网络请求提示框控制
/// </summary>
public class NetDataRequestTip : MonoBehaviour {

	public static string NET_REQUEST_START = "net_request_start";
	public static string NET_REQUEST_END = "net_request_end";
    public static string NET_CONNECT_CLOSE = "net_connect_close";

	public GameObject loadingSprite;
	public GameObject background;
	public GameObject connectClose;
	public GameObject ReconnectButton;

	public static  EventDispatcher.EventCallback startReq;
	public static  EventDispatcher.EventCallback endReq;
	public static  EventDispatcher.EventCallback ConnectClose;

	private BoxCollider boxCollider;
	void Awake(){
		//Debug.Log ("NetDataRequestTip Awake");
		startReq = StartNetRequest;
		endReq = EndNetRequest;
		ConnectClose = ConnectCloseRequest;
	}
 
	void Start () 
	{
        DontDestroyOnLoad(gameObject);
		boxCollider = gameObject.GetComponent<BoxCollider> ();
		UIEventListener.Get (ReconnectButton).onClick = buttonclick;
	}

	void Update () {
		if(NGUITools.GetActive(loadingSprite)){
			loadingSprite.transform.Rotate(Vector3.back*Time.deltaTime*300);
		}
	}

	public void StartNetRequest(EventBase eb){
		Debug.Log ("StartNetRequest-----------------------------------");
		SetTipActive(true);
	}

	public void EndNetRequest(EventBase eb){
		Debug.Log ("EndNetRequest------------------------------------");
		SetTipActive(false);
	}

	public void ConnectCloseRequest(EventBase eb){
		Debug.Log ("ConnectCloseRequest------------------------------------");

		connectClose.SetActive(true);
	}

	private void SetTipActive(bool isActive){
		if(loadingSprite!=null){loadingSprite.SetActive(isActive);}
		if(boxCollider!=null){boxCollider.enabled = isActive;}//boxCollider.SetActive(isActive);}
		//if(background!=null){background.SetActive(isActive);}
	}

	private void buttonclick(GameObject button)
	{
		connectClose.SetActive (false);
		ConnectionManager.GetInstance().Connect (null);
	}
}
