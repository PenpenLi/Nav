using System;
using System.Collections;
using UnityEngine;
using Pomelo.DotNetClient;
using Pomelo;
using NetManager;
using SimpleJson;
using LitJson;
using System.Collections.Generic;

namespace NetManager
{
    public class UpgradeDataReq
    {
        private const string KEY_GUID = "GUID";
        private const string KEY_TYPE = "TYPE";

        public void Upgrade(string guid,int type,EventDispatcher.EventCallback eventCallback)
        {
            JsonObject msg = new JsonObject();
            msg[KEY_GUID] = guid;
            msg[KEY_TYPE] = type;
            ConnectionManager.GetInstance().RequestData<CommonResultData>(CGNetConst.ROUTE_UPGRADE, msg, eventCallback);
        }
    }
}
