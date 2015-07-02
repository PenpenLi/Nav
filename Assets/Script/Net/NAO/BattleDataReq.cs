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
    public class BattleDataReq
    {
        private const string KEY_GUID = "GUID";
        private const string KEY_COPYID = "COPIESID";
        private const string KEY_PROPSKILL = "PROPS";
        private const string KEY_SUPPORT = "FRIEND";

        public void GetBattleData(string guid,int copyID,List<int> propSkills,int supportPlayerID,EventDispatcher.EventCallback eventCallback)
        {
            JsonObject msg = new JsonObject();
            msg[KEY_GUID] = guid;
            msg[KEY_COPYID] = copyID;
            msg[KEY_PROPSKILL] = propSkills;
            msg[KEY_SUPPORT] = supportPlayerID;
            ConnectionManager.GetInstance().RequestData<StageMonsterData>(CGNetConst.ROUTE_BATTLE_READY, msg, eventCallback);
        }
    }
}
