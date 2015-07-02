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
    public class GMTool
    {
        public void AddSoldier(int playerID,EventDispatcher.EventCallback eventCallback)
        {
            JsonObject msg = new JsonObject();
            msg["playerId"] = playerID;
            ConnectionManager.GetInstance().RequestData<PlayerSoldierData>(CGNetConst.ROUTE_GMTOOL_ADD_SOLDIER, msg, eventCallback);
        }

        public void AddPlayerPow(int playerID,int num,EventDispatcher.EventCallback eventCallback)
        {
            JsonObject msg = new JsonObject();
            msg["playerId"] = playerID;
            msg["num"] = num;
            ConnectionManager.GetInstance().RequestData<CommonResultData>(CGNetConst.ROUTE_GMTOOL_ADD_POW,msg,eventCallback);
        }
    }
}
