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
    public class CommonDataReq
    {
        private const string KEY_GUID = "GUID";

        public void GetPlayerSoldiers(string guid, EventDispatcher.EventCallback eventCallback)
        {
            JsonObject msg = new JsonObject();
            msg[KEY_GUID] = guid;
            ConnectionManager.GetInstance().RequestData<List<PlayerSoldierData>>(CGNetConst.ROUTE_SOLDIER, msg, eventCallback);
        }

        public void GetPlayerCopiesData(string guid,EventDispatcher.EventCallback eventCallback)
        {
            JsonObject msg = new JsonObject();
            msg[KEY_GUID] = guid;
            ConnectionManager.GetInstance().RequestData<PlayerCopiesData>(CGNetConst.ROUTE_COPIES, msg, eventCallback);
        }

		public void GetTreasureData(string guid,EventDispatcher.EventCallback eventCallback)
		{
			JsonObject msg = new JsonObject();
			msg[KEY_GUID] = guid;
			ConnectionManager.GetInstance().RequestData<List<TreasureIntData>>(CGNetConst.ROUTE_Treasure, msg, eventCallback);
		}
    }
}
