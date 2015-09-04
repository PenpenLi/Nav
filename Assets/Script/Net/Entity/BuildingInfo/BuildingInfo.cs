/*创建者：师小强
 * 时间：2015/7/13 AM 1:29
 * 说明: 获得玩家建筑信息
*/

public class BuildingInfo
{
    public int PosID { set; get; }//位置ID
    public int Btype { set; get; }//建筑ID 如果是-1 为当前坐标点上无建筑
    public int BuildingID { set; get; }//建筑ID 
    public int Lev { set; get; }//建筑等级
    public string BeginUpTimes { set; get; } //记录开始时间点，“-1”代表没有进行升级操作
    public int ItemNu { set; get; }//产物当前数量
}