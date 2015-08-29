
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
    public double LEV_UP_TIME { set; get; }//建筑升级所需秒数
    public int ITEM_ID { set; get; }//建筑产物ID
    public int ITEMS_MAX_COUNT { set; get; }//产量最大值
    public int ITEMS_TIME { set; get; }//生产最大值所需时间
    public int NEED_GOLD { set; get; } //1级建筑所需金币，1级以上升级所需金币
    public string BUILD_STR { set; get; }//建筑描述
}
