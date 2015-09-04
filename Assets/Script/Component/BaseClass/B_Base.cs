
/*创建：师小强
 *时间：2015.8.24
 *说明：用来接收本地库中建筑基础信息表数据
 */

public class B_Base
{
    public int ID { set; get; }
    public int B_TYPE { set; get; }//建筑类型ID
    public string B_NAME { set; get; }//建筑名字
    public string B_ICON_NAME { set; get; }//建筑图标名字
    public string B_ATLAS_NAME { set; get; }//建筑图集名字
    public int B_LEV { set; get; }//建筑等级
    public int MAX_LEV { set; get; }//建筑最大等级
    public int LEV_UP_TIME { set; get; }//建筑升级所需秒数
    public int ITEM_ID { set; get; }//建筑产物ID
    public int ITEMS_MAX_COUNT { set; get; }//产量最大值
    public int ITEMS_NEED_TIME { set; get; }//生产最大值所需时间
    public int BUY_NEED_GOLD { set; get; }//1级购买与后续升级所需金币数量
    public int NEED_TIME1 { set; get; }//升级所需物资1
    public int COUNT1 { set; get; }//升级所需1号物资数量
    public int NEED_TIME2 { set; get; }//升级所需物资2
    public int COUNT2 { set; get; }//升级所需2号物资数量
    public int NEED_TIME3 { set; get; }//升级所需物资3
    public int COUNT3 { set; get; }//升级所需3号物资数量
    public string BUILD_STR { set; get; }//建筑描述

}
