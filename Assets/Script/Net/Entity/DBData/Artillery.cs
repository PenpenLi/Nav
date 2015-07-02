//作者:鲁力源
//时间：2015/06/22
//功能描述：小兵信息数据结构
//备注：字段与数据表字段名称保持一致升级所需物品1数量
using UnityEngine;
using System.Collections;

public class Artillery{
    public int Id { set; get; } //id
    public int SkLevel { set; get; }//技能等级
    public int Build { set; get; }//营地外形
    public int SkIcon { set; get; }//技能图标
    public int AttEff { set; get;}//攻击特效
    public int Missile { set; get; }//子弹特效
    public int UnderEff { set; get; }//被击特效
    public int UnderPos { set; get; }//被击位置
    public int AttSound { set; get; }//攻击音效
    public int UnderSound { set; get; }//被击音效
    public double AtkRange { set; get; }//伤害范围
    public double Atk { set; get; }//技能伤害
    public double CD { set; get; }//CD时间
    public double StifTime { set; get; }//僵直时间
}
