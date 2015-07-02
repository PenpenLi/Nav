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
    public class TeamDataReq
    {
        private const string KEY_GUID = "GUID";
        private const string KEY_MAINTEAM = "MAINARM";
        private const string KEY_A1 = "A1ID";
        private const string KEY_A2 = "A2ID";
        private const string KEY_A3 = "A3ID";
        private const string KEY_A4 = "A4ID";
        private const string KEY_A5 = "A5ID";
        private const string KEY_B1 = "B1ID";
        private const string KEY_B2 = "B2ID";
        private const string KEY_B3 = "B3ID";
        private const string KEY_B4 = "B4ID";
        private const string KEY_B5 = "B5ID";

        public void SaveFormation(string guid,int mainTeam,int[] teamA,int[] teamB,EventDispatcher.EventCallback eventCallback)
        {
            JsonObject msg = new JsonObject();
            msg[KEY_GUID] = guid;
            msg[KEY_MAINTEAM] = mainTeam;
            msg[KEY_A1] = teamA[0];
            msg[KEY_A2] = teamA[1];
            msg[KEY_A3] = teamA[2];
            msg[KEY_A4] = teamA[3];
            msg[KEY_A5] = teamA[4];
            msg[KEY_B1] = teamB[0];
            msg[KEY_B2] = teamB[1];
            msg[KEY_B3] = teamB[2];
            msg[KEY_B4] = teamB[3];
            msg[KEY_B5] = teamB[4];
            ConnectionManager.GetInstance().RequestData<CommonResultData>(CGNetConst.ROUTE_TEAM_SAVE, msg, eventCallback);
        }
    }
}
