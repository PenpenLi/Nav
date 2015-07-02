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
    public class SoldierDataReq
    {
        private const string KEY_GUID = "GUID";
        private const string SOURCE_SOLDIER_ID = "PSID";
        private const string RESOURCE_SOLDIER_ID = "ESID";

        public void SellSoldiers(string guid,List<int> soldiersID,EventDispatcher.EventCallback eventCallback)
        {
            JsonObject msg = new JsonObject();
            msg[KEY_GUID] = guid;
            msg[SOURCE_SOLDIER_ID] = soldiersID;
            ConnectionManager.GetInstance().RequestData<CommonResultData>(CGNetConst.ROUTE_SOLDIER_SELL, msg, eventCallback);
        }

        public void UpdateLockState(string guid,int soldierID,EventDispatcher.EventCallback eventCallback)
        {
            JsonObject msg = new JsonObject();
            msg[KEY_GUID] = guid;
            msg[SOURCE_SOLDIER_ID] = soldierID;
            ConnectionManager.GetInstance().RequestData<CommonResultData>(CGNetConst.ROUTE_SOLDIER_LOCK, msg, eventCallback);
        }

        public void CompoundSoldier(string guid,int sourceSoldierID,List<int> resSoldiersID,EventDispatcher.EventCallback eventCallback)
        {
            JsonObject msg = new JsonObject();
            msg[KEY_GUID] = guid;
            msg[SOURCE_SOLDIER_ID] = sourceSoldierID;
            msg[RESOURCE_SOLDIER_ID] = resSoldiersID;
            ConnectionManager.GetInstance().RequestData<CommonResultData>(CGNetConst.ROUTE_SOLDIER_COMPOUND, msg, eventCallback);
        }
    }
}
