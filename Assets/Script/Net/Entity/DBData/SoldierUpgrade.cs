//作者:张伟刚
//时间：2015/06/2
//功能描述：士兵升级所需经验和供给经验
//备注：字段与数据表字段名称保持一致
using System;

public class SoldierUpgrade
{
    public int Id{set;get;}//等级
    public int Exp{set;get;}//当前等级升到下级所需的经验
    public int TotalExp{set;get;}//当前等级被吞噬时提供的经验
}
