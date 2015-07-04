using System.Collections;
using System;
using SimpleJson;
using UnityEngine;
using Pomelo.DotNetClient;
using Pomelo;
using NetManager;
using LitJson;
using System.Collections.Generic;

/***************************************************鲁力源(2015.4.21)***********************************************************/

public class RequestInfo
{
    public enum DataType
    {
        Type_JsonObject,
        Type_String
    }

    public delegate void RequestDelegate();

    public DataType dataType{set;get;}
    public string route{set;get;}
    public object data{set;get;}
    public EventDispatcher.EventCallback eventCallback{set;get;}
    public RequestDelegate request{set;get;}
    public long reqTicks = 0;

    public RequestInfo(string route,JsonObject data, EventDispatcher.EventCallback eventCallback)
    {
        this.dataType = DataType.Type_JsonObject;
        this.route = route;
        this.data = data;
        this.eventCallback = eventCallback;
        reqTicks = DateTime.Now.Ticks;
    }

    public RequestInfo(string route,string data, EventDispatcher.EventCallback eventCallback)
    {
        this.dataType = DataType.Type_String;
        this.route = route;
        this.data = data;
        this.eventCallback = eventCallback;
        reqTicks = DateTime.Now.Ticks;
    }
}

//该类主要用于与服务器建立链接
public class ConnectionManager : MonoBehaviour
{
    private const int REQUEST_TIME_OUT = 10 * 10000000;

    private static ConnectionManager  connManager;
    private PomeloClient  pclient;

    /// <summary>
    /// 网络状态
    /// </summary>
    private ConnectStatus connectStatus = ConnectStatus.UNCONNECT;

    private bool connectStatusChangedTag = false;

    private Action<bool> connectResultCallback = null;

    /// <summary>
    /// 是否客户端主动关闭连接
    /// </summary>
    private bool activeCloseConnect = false;

    /// <summary>
    /// 断线重连次数
    /// </summary>
    private int autoReConnectCount = 0;

    /// <summary>
    /// 数据请求队列，当需要向服务器请求数据时先将请求加入队列，等得到相应的数据后再将其移出队列，用于断线重连后重新请求断线前要请求的数据
    /// </summary>
    private List<RequestInfo> requestList = new List<RequestInfo>();

    private ConnectionManager()
    {
    }

    /// <summary>
    /// 获取ConnectionManager单例对象
    /// </summary>
    /// <returns>The instance.</returns>
    public static ConnectionManager GetInstance()
    {
        if (connManager == null)
        {
            connManager = (PublicGameObject.publicGameObj).AddComponent<ConnectionManager>();
        }
        return connManager;
    }

    private void Update()
    {
        if(connectStatusChangedTag)
        {
            ConnectStatusChanged();
            connectStatusChangedTag = false;
        }
    }

    public ConnectStatus GetConnectStatus()
    {
        return connectStatus;
    }

    public void PrepareDisconnect()
    {
        activeCloseConnect = true;
    }

    /// <summary>
    /// 与服务器器建立连接
    /// </summary>
    /// <param name="conResult">Con result.</param>
    public void Connect(Action<bool> conResult)
    {
        connectResultCallback = conResult;
        if (pclient != null && conResult != null)
        {
            Debug.Log("(pclient != null && conResult != null)");
            InvokeConnectResult(true);
            return;
        }

        ChangeConnectStatus(ConnectStatus.CONNECTING);
        EventDispatcher.Instance().RegistEventListener(NetDataRequestTip.NET_REQUEST_START, NetDataRequestTip.startReq);
        EventDispatcher.Instance().RegistEventListener(NetDataRequestTip.NET_REQUEST_END, NetDataRequestTip.endReq);
        EventDispatcher.Instance().RegistEventListener(NetDataRequestTip.NET_CONNECT_CLOSE, NetDataRequestTip.ConnectClose);
        EventDispatcher.Instance().DispatchEvent(NetDataRequestTip.NET_REQUEST_START,null);
        
        pclient = new PomeloClient();
        pclient.NetWorkStateChangedEvent += OnGateServerNetWorkStateChange;
        try{
            pclient.initClient(CGNetConst.HOST,CGNetConst.POST,()=>{
                pclient.connect(null,conData1=>{
                    Debug.Log("Gate服务器连接成功！");
                    pclient.request(CGNetConst.ROUTE_LOGIN,delegate(JsonObject jsonObject) {
                        Debug.Log("PARA_CODE = " + jsonObject [CGNetConst.PARA_CODE]);
                        if (Convert.ToInt32(jsonObject [CGNetConst.PARA_CODE]) == CGNetConst.RESULT_CODE_SUC)
                        {
                            Disconnect(true);
                            Debug.Log("与Gate服务器断开连接，准备连接游戏服务器……");
                            Debug.Log("host=" + (string)jsonObject [CGNetConst.PARA_HOST] + "   port=" + jsonObject [CGNetConst.PARA_PORT]);
                            pclient = new PomeloClient();
                            pclient.NetWorkStateChangedEvent += OnNetWorkStateChange;
                            pclient.initClient((string)jsonObject [CGNetConst.PARA_HOST], Convert.ToInt32(jsonObject [CGNetConst.PARA_PORT]),()=>{
                                pclient.connect(null,conData2=>{
                                    ChangeConnectStatus(ConnectStatus.CONNECTED);
                                    Debug.Log("游戏服务器连接成功！");

//                                    if(conResult != null)
//                                    {
//                                        conResult(true);
//                                    }
                                });
                            });                     
                        }else
                        {
                            Debug.Log("Connect faild!");
                            InvokeConnectResult(false);
//                            if(conResult != null)
//                            {
//                                conResult(false);
//                            }
                        }
                    });
                });
            });
        }catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    private void CheckRequestOutTime()
    {
        if(connectStatus != ConnectStatus.CONNECTED)return;
        for(int i=0; i<requestList.Count; i++)
        {
            long delay = DateTime.Now.Ticks - requestList[i].reqTicks;
            if(delay>REQUEST_TIME_OUT)
            {
                Debug.Log("Request time out!----------->"+requestList[i].route);
                requestList[i].reqTicks = DateTime.Now.Ticks;
                Disconnect(false);
            }
        }
    }

    private void OnGateServerNetWorkStateChange(NetWorkState state)
    {
        Debug.Log("GateServerNetState------------------>"+state.ToString());
        switch(state)
        {
            case NetWorkState.CLOSED:
                break;
            case NetWorkState.CONNECTING:

                break;
            case NetWorkState.CONNECTED:
                break;
            case NetWorkState.DISCONNECTED:
                if(!activeCloseConnect)
                {
                    ChangeConnectStatus(ConnectStatus.ERROR);
                }
                break;
            case NetWorkState.TIMEOUT:
                ChangeConnectStatus(ConnectStatus.TIMEOUT);
                break;
            case NetWorkState.ERROR:
                ChangeConnectStatus(ConnectStatus.ERROR);
                break;
        }
    }
    
    private void OnNetWorkStateChange(NetWorkState state)
    {
        Debug.Log("GameServerNetState------------------>"+state.ToString());
        switch(state)
        {
            case NetWorkState.CLOSED:
                break;
            case NetWorkState.CONNECTING:
                break;
            case NetWorkState.CONNECTED:
                break;
            case NetWorkState.TIMEOUT:
                ChangeConnectStatus(ConnectStatus.TIMEOUT);
                break;
            case NetWorkState.DISCONNECTED:
                ChangeConnectStatus(ConnectStatus.DISCONNECT);
                break;
            case NetWorkState.ERROR:
                ChangeConnectStatus(ConnectStatus.ERROR);
                break;
            default:
                break;
        }
    }

    private void ChangeConnectStatus(ConnectStatus status)
    {
        connectStatus = status;
        connectStatusChangedTag = true;
    }

    private void ConnectStatusChanged()
    {
        Debug.Log("ConnectStatus changed:"+connectStatus.ToString());
        switch(connectStatus)
        {
            case ConnectStatus.UNCONNECT:
                break;
            case ConnectStatus.CONNECTING:
                break;
            case ConnectStatus.CONNECTED:
                ConnectedToGameServer();
                break;
            case ConnectStatus.DISCONNECT:
                DisconnectFromServer();
                break;
            case ConnectStatus.TIMEOUT:
            case ConnectStatus.ERROR:
                InvokeConnectResult(false);
                ShowNetError();
                break;
            default:
                break;
        }
    }

    private void InvokeConnectResult(bool result)
    {
        if(connectResultCallback != null)
        {
            connectResultCallback.Invoke(result);
            connectResultCallback = null;
        }
    }
    
    private void ConnectedToGameServer()
    {
        EventDispatcher.Instance().DispatchEvent(NetDataRequestTip.NET_REQUEST_END, true);
        InvokeConnectResult(true);

        activeCloseConnect = false;

        //如果未绑定过SessionID，需要绑定SessionID
        //Player pInfo = PlayerDataManager.GetInstance().GetPlayerInfo();
        //if(pInfo != null)
        //{
        //    string deviceID = SystemInfo.deviceUniqueIdentifier;
        //    SessionBindReq sessionBindReq = new SessionBindReq();
        //    sessionBindReq.BindSession(deviceID,delegate(EventBase eb) {
        //        autoReConnectCount = 0;
        //        CommonResult<long> commonResult = (CommonResult<long>)eb.eventValue;
        //        PublicTimer.ResetServerTime(commonResult.data);

        //        //处理断线重连之前未能完成的请求
        //        if(requestList.Count>0)
        //        {
        //            //Todo:处理断线重连之前未能完成的请求
        //            for(int i=0; i<requestList.Count; i++)
        //            {
        //                requestList[i].request.Invoke();
        //            }
        //        }
        //    });
        //}

        //注册并开始监听推送消息.(仅限一次开启)
        Debug.Log("注册并开始监听推送消息");
//        PushMsgReciver.GetInstance().StartReciver();
//        ChatMsgReciver.GetInstance().StartReciver();
    }
    
    private void DisconnectFromServer()
    {
//        EventDispatcher.Instance().clearnAllRegistedCallBack();

        InvokeConnectResult(false);

        if(!activeCloseConnect)
        {
            Debug.Log("autoReconnectCount:"+autoReConnectCount.ToString());
            if(autoReConnectCount<1-requestList.Count)
            {
                EventDispatcher.Instance().clearnAllRegistedCallBack();
                autoReConnectCount ++;
                ReconnectToServer();
            }else
            {
                ShowNetError();
            }
        }
    }
    
    private void ReConnectToServer()
    {
        Connect(null);
    }
    
    private void ShowNetError()
    {
        Debug.Log(">>>>>>>>>>>>>>>>> ConnectClose! <<<<<<<<<<<<<<");
        EventDispatcher.Instance().DispatchEvent(NetDataRequestTip.NET_REQUEST_END, true);
        EventDispatcher.Instance().DispatchEvent(NetDataRequestTip.NET_CONNECT_CLOSE,null);
    }
    
    public void Disconnect(bool active)
    {
        activeCloseConnect = active;
        pclient.disconnect();
    }

    void ReconnectToServer()
    {
        if(connectStatus == ConnectStatus.CONNECTING)return;
        Debug.Log("========ReconnectToServer====" + System.DateTime.Now);
        Connect(null);
    }

    /// <summary>
    /// 数据请求 只能发送简单的数据类型
    /// Requests the data.
    /// </summary>
    /// <param name="route">Route.</param>
    /// <param name="data">Data.</param>
    /// <param name="eventCallback">Event callback.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public void RequestData<T>(string route, JsonObject data, EventDispatcher.EventCallback eventCallback)
    {       
        RequestInfo reqInfo = new RequestInfo(route,data,eventCallback);
        reqInfo.request = ()=>{Request<T>(reqInfo);};
        requestList.Add(reqInfo);
        Request<T>(reqInfo);
    }

    /// <summary>
    /// Requests the data by json string.
    /// 如果结构中有自定义类的话必须用这个方法。不然会造成请求发送不出去，原因是由于jsonObject对象的toString（）方法不能吧自定义类转化
    /// </summary>
    /// <param name="route">Route.</param>
    /// <param name="jsonData">Json data.</param>
    /// <param name="eventCallback">Event callback.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public void RequestDataByJsonStr<T>(string route, string jsonData, EventDispatcher.EventCallback eventCallback)
    {       
        RequestInfo reqInfo = new RequestInfo(route,jsonData,eventCallback);
        reqInfo.request = ()=>{Request<T>(reqInfo);};
        requestList.Add(reqInfo);
        Request<T>(reqInfo);
    }

    private void Request<T>(RequestInfo reqInfo)
    {
        if(reqInfo == null)return;
        try
        {
            EventDispatcher.Instance().RegistEventListener(reqInfo.route,reqInfo.eventCallback);
            EventDispatcher.Instance().RegistEventListener(NetDataRequestTip.NET_REQUEST_END, NetDataRequestTip.endReq);
            NetDataRequestTip.startReq(null);
            
            Action<JsonObject> requestCallback = delegate(JsonObject obj) {

                Debug.Log("Route:"+reqInfo.route);
                Debug.Log("Date:"+obj.ToString());
                if(!requestList.Remove(reqInfo))
                {
                    Debug.Log("Can not found request info in list!");
                }
                
                CommonResult<T> commonResult = AnalysisData<T>(obj);
                if(requestList.Count<=0)
                {
                    EventDispatcher.Instance().DispatchEvent(NetDataRequestTip.NET_REQUEST_END, true);
                }
                EventDispatcher.Instance().DispatchEvent(reqInfo.route, commonResult);
            };
            
            if(reqInfo.dataType == RequestInfo.DataType.Type_JsonObject)
            {
                pclient.request(reqInfo.route,(JsonObject)reqInfo.data,requestCallback);
            }else if(reqInfo.dataType == RequestInfo.DataType.Type_String)
            {
                pclient.requestByJsonStr(reqInfo.route,(string)reqInfo.data,requestCallback);
            }else
            {
                Debug.Log("Request info data type is error!");
                return;
            }
            
        }catch(Exception e)
        {
            Debug.LogException(e);
        }
    }

    /// <summary>
    /// 监听服务器主动对送消息
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="action">Action.</param>0
    public void ReceivePushMsg<T>(string eventName, EventDispatcher.EventCallback eventCallback)
    {
        if (pclient == null)
        {
            Debug.Log("pclient is null");
        }
        Debug.Log("=============ReceivePushMsgReceivePushMsgReceivePushMsg==================");
        pclient.on(eventName, delegate (JsonObject resData)
        {
            EventDispatcher.Instance().RegistEventListener(eventName, eventCallback);
            Debug.Log("pushData=" + resData);
            CommonResult<T> commonResult = AnalysisData<T>(resData);
            EventDispatcher.Instance().DispatchEvent(eventName, commonResult);
        });
    }

    /*
    数据封装
    */
    private CommonResult<T> AnalysisData<T>(JsonObject resData)
    {
        CommonResult<T> temp = null;
        try
        {
            string jstr = resData.ToString();
            //Debug.Log (typeof (T) + " Json String is " + jstr);
            temp = JsonMapper.ToObject<CommonResult<T>>(jstr);
        } catch (Exception e)
        {
            Debug.LogException(e);
        }
        return temp;
    }   
    
}
