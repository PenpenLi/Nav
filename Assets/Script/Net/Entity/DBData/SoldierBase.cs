//作者:鲁力源
//时间：2015/05/22
//功能描述：小兵信息数据结构
//备注：字段与数据表字段名称保持一致
public class SoldierBase
{
    public int Id { set; get; }//ID
    public int BaseId { set; get; }//小兵基础ID
    public string Prefix{ set; get; }//名称前缀
    public string Name { set; get; }//名称
    //1 肉盾型 近战，血高附近抗伤害                  
    //2 平衡型 属性中庸，可以承担一部分伤害                  
    //3 攻击型 高额单体攻击，血量较少                 
    //4 范围型 范围输出，血量中庸                   
    //5 速度型 移动速度和攻击速度较快                 
    //6 支援型                     
    //7 特殊型 无法上阵参与战斗，经验卡            此类型卡均为绿色        
    //8 特殊型 无法上阵参与战斗，做经验卡的同时提高LvMAX         此类型卡均为绿色    
    public int Type { set; get; }

    public int Star { set; get; }//兵星级
    public int MaxLv { set; get; }//初始最大等级
    public double Atk { set; get; }//攻击力（使用时需要除以100)
    public double HP { set; get; }//生命值（使用时需要除以100）
    public int Food { set; get; }//消耗
    public double AtkS { set; get; }//攻击速度
    public double WS { set; get; }//移动速度
    public int AtkD { set; get; }//攻击距离
    public int AtkRange { set; get; }//攻击范围
    public double CD { set; get; }//产兵CD（使用时需要除以100）
    public int AtkType { set; get; }//0近身攻击 1范围型近身攻击 2区域攻击 3范围区域攻击 4弧线型的远距离攻击 5未实现 6火枪攻击 7两路型范围近身攻击
    public int SkillId { set; get; }//技能ID
    public int BuffId { set; get; }//BuffID
    public int FoodStep { set; get; }//消耗步长增长
    public int ProExp { set; get; }//新卡提供的经验值
    public int Item1Id { set; get; }//升级所需物品1基础ID
    public int Num1 { set; get; }//升级所需物品1数量
    public int Item2Id { set; get; }//升级所需物品2基础ID
    public int Num2 { set; get; }//升级所需物品2数量
    public int Item3Id { set; get; }//升级所需物品3基础ID
    public int Num3 { set; get; }//升级所需物品3数量
    public int Item4Id { set; get; }//升级所需物品4基础ID
    public int Num4 { set; get; }//升级所需物品4数量
    public int Item5Id { set; get; }//升级所需物品5基础ID
    public int Num5 { set; get; }//升级所需物品5数量
    public string Skl { set; get; }//骨骼动画ID
    public int Head { set; get; }//头像
    public int AttEff { set; get; }//攻击特效
    public int FlightPath { set; get; }//飞行路径类型
    public double FlightSpeed { set; get; }//飞行速度
    public int AttSound { set; get; }//攻击音效
	public int HitSound { set; get; }//命中音效
    public int UnderEff { set; get; }//受击特效
    public int UnderPos { set; get; }//受击位置
    public int UnderSound { set; get; }//受击音效
    public int Sort { set; get;}//默认排序的优先级
    public double Offset { set; get; }//战斗计算的偏移点
    public double AttObjectX { set; get; }//远程攻击的发射点偏移
    public double AttObjectY { set; get; }//远程攻击的发射点偏移

    public SoldierBase Clone()
    {
        return (SoldierBase)this.MemberwiseClone();
    }

}
