//作者:张伟刚
//时间：2015/06/12
//功能描述：副本掉落表
//备注：字段与数据表字段名称保持一致;

public enum GdsType
{
    Pow = 0,//体力
    SpecialPow = 1,// 特殊体力
    Diamond = 2,//钻石
    Gold = 3,//金币
    FriendShip = 4,//友情点
    Soldier = 5,//士兵
    PropSkill = 6,//技能道具
    Item = 7,//物品
    Treasure = 8//宝物
}

public class CopiesItem
{
    public int Id{set;get;}//掉落ID 
    public int GoodsId{set;get;}//掉落物ID
    public int GdsType{set;get;}//掉落物类型
    public int GdsCount{set;get;}//掉落数量
    public int CopiesId{set;get;}//掉落副本
}
