/*创建者：师小强
 * 时间：2015/7/13 AM 1:29
 * 说明: 获得玩家建筑信息
*/

public class BuildingInfo
{
    public int POSID { set; get; }//位置ID
    public int B_ID { set; get; }//建筑ID 如果是-1 为当前坐标点上无建筑
    public string B_STRAT_TIME { set; get; } //记录开始时间点，“-1”代表没有进行升级操作
}