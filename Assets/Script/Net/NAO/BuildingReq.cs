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
    class BuildingReq
    {
        private const string GUID = "guID";
        private const string PosID = "posID";
        private const string OldTime = "oldTime";
        public void GetItemBase(string guID, string posID,EventDispatcher.EventCallback eventCallback)
        {
            JsonObject msg = new JsonObject();
            msg[GUID] = guID;
            msg[GUID] = posID;
            ConnectionManager.GetInstance().RequestData<PlayerInfo>(CGNetConst.ROUTE_BUILDING_UPDATE, msg, eventCallback);
        }
        
    }

}
