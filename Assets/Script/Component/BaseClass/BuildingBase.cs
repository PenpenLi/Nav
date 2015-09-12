
/*创建：师小强
 *时间：2015.8.24
 *说明：用来接收本地库中建筑基础信息表数据
 */

public class bbase
{
    public int ID { set; get; }
    public string Name { set; get; }//建筑名字
    public string Icon { set; get; }//建筑图标名字
    public string Atlas { set; get; }//建筑图集名字
    public int Lv { set; get; }//建筑等级
    public int MxaLv { set; get; }//建筑最大等级
    public int UpgradeTime { set; get; }//建筑升级所需秒数
    public int ItemID { set; get; }//建筑产物ID
    public int MaxItem { set; get; }//产量最大值
    public int NeedTime { set; get; }//生产最大值所需时间
    public int Cost { set; get; }//造价
    public int NeedID1 { set; get; }//升级所需物资1
    public int Count1 { set; get; }//升级所需1号物资数量
    public int NeedID2 { set; get; }//升级所需物资2
    public int Count2 { set; get; }//升级所需2号物资数量
    public int NeedID3 { set; get; }//升级所需物资3
    public int Count3 { set; get; }//升级所需3号物资数量
    public string InfoStr { set; get; }//建筑描述

}
