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
    public class BattleSettleDataReq
    {
        private const string KEY_GUID = "GUID";
        private const string KEY_COPYID = "COPIESID";
        private const string KEY_WIN = "ISWIN";
        private const string KEY_STAR = "STAR";
        private const string KEY_PROPS = "PROPS";

        public void BattleSettle(string guid,int copyID,int isWin,int star,List<int> propSkills,EventDispatcher.EventCallback eventCallback)
        {
            JsonObject msg = new JsonObject();
            msg[KEY_GUID] = guid;
            msg[KEY_COPYID] = copyID;
            msg[KEY_WIN] = isWin;
            msg[KEY_STAR] = star;
            msg[KEY_PROPS] = propSkills;
            ConnectionManager.GetInstance().RequestData<BattleSettleData>(CGNetConst.ROUTE_BATTLE_SETTLE, msg, eventCallback);
        }
    }
}
