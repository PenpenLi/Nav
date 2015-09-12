/*创建者：师小强
 * 时间：2015/7/13 AM 1:29
 * 说明: 获得玩家建筑信息
*/

public class BuildingInfo
{
    public int ID;
    public int PLAYER_ID;
    public int POS_ID { set; get; }//位置ID
    public int B_ID { set; get; }//建筑ID 如果是-1 为当前坐标点上无建筑
    public int ITEMS_COUNT{set;get;}//建筑已生产的资源数量
    public long STRAT_TIME { set; get; } //记录开始时间点，“-1”代表没有进行升级操作
}