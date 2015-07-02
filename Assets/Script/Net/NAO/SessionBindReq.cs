using System.Collections;
using System;
using SimpleJson;
using UnityEngine;
using Pomelo.DotNetClient;
using Pomelo;
using NetManager;
using LitJson;
using System.Collections.Generic;

namespace NetManager
{
    public class SessionBindReq
    {
        private const string DEVICE_ID = "DEVICEID";
        public void BindSession(string deviceID, EventDispatcher.EventCallback eventCallback)
        {
            JsonObject msg = new JsonObject();
            msg[DEVICE_ID] = deviceID;
            ConnectionManager.GetInstance().RequestData<long>(CGNetConst.ROUTE_SESSIONBIND, msg, eventCallback);
        }
    }
}
