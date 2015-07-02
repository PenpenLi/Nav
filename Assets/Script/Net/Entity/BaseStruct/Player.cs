//作者:张伟刚
//时间：2015/05/22
//功能描述：玩家基础数据结构
//备注：
using System.Collections.Generic;

public class Player 
{
	public int Id{set; get;}//玩家ID
    public int Type{set;get;}//0:普通 1:战斗大师 2:超级大师
	public string DevId{set; get;}//设备号
	public string GUID{set; get;}//玩家唯一标识符
	public int HeadId{set; get;}//头像ID
	public string UName{set; get;}//用户名
	public string Nick{set; get;}//昵称
    public int Diamond{set; get;}//钻石
	public int Gold{set; get;}//金钱
	public int Friend{set;get;}//友情点
	public int Level{set; get;}//等级
	public int CurExp{set; get;}//当前经验
	public int CurPower{set; get;}//当前体力
	public int MaxPower{set; get;}//最大体力
    public int SpPower{set;get;}//
	public int LastTime{set; get;}//上次登录时间
	public int AArms1{set;get;}//当前配置的A兵营1
	public int AArms2{set;get;}//当前配置的A兵营2
	public int AArms3{set;get;}//当前配置的A兵营3
	public int AArms4{set;get;}//当前配置的A兵营4
	public int AArms5{set;get;}//当前配置的A兵营5
	public int BArms1{set;get;}//当前配置的A兵营1
	public int BArms2{set;get;}//当前配置的A兵营2
	public int BArms3{set;get;}//当前配置的A兵营3
	public int BArms4{set;get;}//当前配置的A兵营4
	public int BArms5{set;get;}//当前配置的A兵营5

    public int MainArms{set;get;}//DEFAULT '0' 当前主阵型'

	public int DupNow{set; get;}//副本进度
	public int Locked{set; get;}//玩家是否被锁定
	public int NeedTime{set;get;}//玩家上次体力回复时间

    public int UpSpLv{set;get;}//升级=> 上升速度
    public int UpMaxLv{set;get;}//升级=>最大粮草
    public int UpSkLv{set;get;}//升级=>技能等级
    public int UpCityLv{set;get;}//升级=>城墙
    public int UpPlLv{set;get;}//升级=>击退小兵掠夺粮草数
    public int UpCdLv{set;get;}//升级=>减少Cd时间
    public int UpAtkLv{set;get;}//升级=>增加攻击力
    public int UpPoLv{set;get;}//升级=>增加体力

    public int PropSk1{set;get;}
    public int PropSk2{set;get;}
    public int PropSk3{set;get;}
    public int PropSk4{set;get;}

    public int CitySkillLevel { set; get; }

    public Player()
    {
        Id = 0;
        DevId = "fuckmeplease";
        GUID = "ok let's go";
        HeadId = 1;
        UName = "dragons";
        Nick = "神兽";
        Diamond = 0;
        Gold = 0;
        Friend = 0;
        Level = 1;
        CurExp = 0;
        CurPower = 0;
        MaxPower = 0;
        LastTime = 0;
        AArms1 = 1;
        AArms2 = 1;
        AArms3 = 1;
        AArms4 = 1;
        AArms5 = 1;
        BArms1 = 1;
        BArms2 = 1;
        BArms3 = 1;
        BArms4 = 1;
        BArms5 = 1;
        MainArms = 0;
        DupNow = 1;
        Locked = 0;
        NeedTime = 0;
        UpSpLv = 0;
        UpMaxLv = 0;
        UpSkLv = 0;
        UpCityLv = 0;
        UpPlLv = 0;
        UpCdLv = 0;
        UpAtkLv = 0;
        UpPoLv = 0;
        PropSk1 = 0;
        PropSk2 = 0;
        PropSk3 = 0;
        PropSk4 = 0;
        CitySkillLevel = 1;
    }

}
