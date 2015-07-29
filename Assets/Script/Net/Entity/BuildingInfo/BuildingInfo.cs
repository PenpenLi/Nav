﻿/*创建者：师小强
 * 时间：2015/7/13 AM 1:29
 * 说明: 获得玩家建筑信息
*/

public class GetBuildingInfo 
{
    public int IdPos { set; get; }//位置ID
    public int IdBuilding { set; get; }//建筑ID
    public int Lev { set; get; }//建筑等级
    public long UpTimes { set; get; } //当前建造状态“-1”为当前未升级，如果有升级直接发送当前升级已消耗的时间
    public int IdProduct { set; get; }//当前是否有生产“-1”为当前没有产物，如果有发送物品ID
    public long ItemNu { set; get; }//产物当前数量
}