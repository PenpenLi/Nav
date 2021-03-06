using System;

namespace ConfigConst
{
		public class ResponseCode
		{
				public const int SUCCESS = 200;
				public const int FAIL = 500;
		}

		public class GameUIConfig
		{
				public const int None = 0;
				public const int Home = 1;
				public const int Fight = 2;
				public const int Duplication = 4;
				public const int Areana = 16;
				public const int Activity = 32;
                public const int Barrack = 64;
                public const int Tower = 128;
		}

		public class BaseConfig
		{
				public const int BattleArrayMaxLength = 6;
				public const int CardMaxLevel = 12;
				public const int CardMaxQuality = 6;

				public class SutraType
				{
						public const int None = 0;
						public const int Weapon = 1;
						public const int Armor = 2;
						public const int Assist = 3;
				}

				public const int WeaponSlotNo = 1;
				public const int ArmorSlotNo = 2;
				public const int Assist1SlotNo = 3;
				public const int Assist2SlotNo = 4;
		}

		public class DataStatusFromServer
		{
				public const int STATUS_0 = 0;
				public const int STATUS_1 = 1;
				public const int STATUS_2 = 2;
				public const int STATUS_3 = 2;
		}


}
