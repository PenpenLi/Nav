/*创建者：师小强
 * time：2015/7/13 AM 1:29
 * Function: Get Building info
*/

public class GetBuildingInfo 
{
    public int IdAtlas { set; get; }//用于从本地库中查找图集
    public int IdPos { set; get; }//用于查找目标位置
    public int Lev { set; get; }//用于设置当前等级的houseIcon
}