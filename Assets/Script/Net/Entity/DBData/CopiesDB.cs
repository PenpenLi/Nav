//作者:鲁力源
//时间：2015/05/22
//功能描述：副本的关卡信息数据结构
//备注：字段与数据表字段名称保持一致

using System;

public class CopiesDB {

    public int Id { get; set; }//id
    public string DupName{ get; set;}//副本名称
    public int DupType { get; set; }//副本类型
    public int DupChap { get; set; }//副本章节 普通副本：章节数 特殊副本：副本模块
    public int DupDiff { get; set; }//副本难度 普通副本：0表示小关卡；1表示大关卡 特殊副本：1-6个难度关卡
    public int IsBoos { get; set; }//是否BOSS(0：小关卡 1：BOSS关卡)
    public int UseCount { get; set; }//挑战次数
    public int UsePower { get; set; }//所需体力
    public int BgSize { get; set; }//背景大小
    public int FightBg { get; set; }//战斗背景的ID
    public int FightMus { get; set; }//战斗的背景音乐
    public int EneBlood { get; set; }//敌营血量
    public int PepExp { get; set; }//获得主公经验
    public int ArmsExp { get; set; }//获得小兵经验
    public int GetGold { get; set; }//金币奖励
}
