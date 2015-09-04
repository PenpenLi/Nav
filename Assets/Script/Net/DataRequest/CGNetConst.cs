
using System;

namespace NetManager
{

    public class CGNetConst
    {
        //-------------------------CONNECT-------------------------//
		//gate server
//		public const string gateHost = "192.168.1.245";
//		public const int gatePost = 3014;
        //内网服务器

        public const string HOST = "192.168.31.194";     //250服务器
        public const int POST = 3010;

        //public const string HOST = "192.168.1.243";     //杨伟服务器
        //public const int POST = 3010;

//        public const string HOST = "192.168.1.246";  //王磊服务器
//        public const int POST = 3010;

        //public const string HOST = "192.168.1.245";  //鲁力源服务器
        //public const int POST = 3010;



        //-------------------------ROUTE-------------------------//
        public const string ROUTE_LOGIN = "connector.entryHandler.entry";

		public const string ROUTE_CONNECTOR = "gate.gateHandler.entry";
		//-------------------------Login--------------------------//
        //快速登录使用的包头
		public const string ROUTE_QUICKLOGIN="connector.quickLoginHandle.entry";

        public const string ROUTE_SESSIONBIND = "connector.quickLoginHandle.sessionBind";

        //玩家所有的士兵
        public const string ROUTE_SOLDIER = "connector.sendPlayerSoldierHandler.sendPlayerSoldier";

        //玩家副本信息
        public const string ROUTE_COPIES = "connector.entryCopiesHandler.getCopiesData";

        //玩家队伍保存
        public const string ROUTE_TEAM_SAVE = "connector.soldierOperationHandler.saveFormation";

        //升级
        public const string ROUTE_UPGRADE = "connector.upgradeHandler.clickUpgrade";

        //士兵
        public const string ROUTE_SOLDIER_COMPOUND = "connector.soldierOperationHandler.addSoldierExp";
        public const string ROUTE_SOLDIER_SELL = "connector.soldierOperationHandler.sellSoldier";
        public const string ROUTE_SOLDIER_LOCK = "connector.soldierOperationHandler.lockSoldier";

		//宝物
		public const string ROUTE_Treasure = "connector.treasureHandler.sendPlayerTreasure";
        //领取章节奖励
        public const string ROUTE_CHAPTER_REWARD = "connector.entryCopiesHandler.getChapterReward";

        //战斗准备
        public const string ROUTE_BATTLE_READY = "connector.entryCopiesHandler.readyForFight";

        //战斗结算
        public const string ROUTE_BATTLE_SETTLE = "connector.checkoutFightHandler.fightOver";

        //服务器时间
        public const string ROUTE_SERVER_TIME = "connector.quickLoginHandle.sendSystemTime";


        //临时测试用
        public const string ROUTE_GMTOOL_ADD_SOLDIER = "connector.GMToolsHandler.temporaryAddSoldier";
        public const string ROUTE_GMTOOL_ADD_POW = "connector.GMToolsHandler.addPower";

        //sq start----------------------------------------------------------
        //建筑
        public const string ROUTE_BUILDING_UPDATE = "";
        public const string ROUTE_BUILDING_UPGRADE = "";

        //sq end------------------------------------------------------------

		public const string PARA_HOST = "host";
		public const string PARA_PORT = "port";
		public const string PARA_CODE = "code";
		public static int RESULT_CODE_SUC = 200;
		public static int RESULT_CODE_FAIL = 500;
    }

    public enum ConnectStatus
    {
        UNCONNECT,
        CONNECTING,
        CONNECTED,
        DISCONNECT,
        TIMEOUT,
        ERROR
    }
}

