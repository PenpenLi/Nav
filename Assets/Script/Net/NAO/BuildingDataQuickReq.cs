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
    class BuildingDataQuickReq
    {
        private const string GUID = "guID";
        public void GetBuilingBase(string guID, EventDispatcher.EventCallback eventCallback)
        {
            JsonObject msg = new JsonObject();
            msg[GUID] = guID;
            ConnectionManager.GetInstance().RequestData<BuildingInfo>(CGNetConst.ROUTE_BUILDING_UPDATE, msg, eventCallback);
        }
        
    }
}
