/*创建者：鲁力源
 * 时间：2015/05/30 01：25
 * 功能说明：舰船基础属性
 * 备注：
 */

public class ShipBase {
    public int id { set; get; }//ID
    public string name { set; get; }//名称
    public int attack { set; get; }//攻击力
    public int defense { set; get; }//防御力
    public int speed { set; get; }//行船速度
    public int dodge { set; get; }//闪避
    public float attackspeed { set; get; }//攻击速度
    public int weight { set; get; }//船体载重
    public int attackskilltype { set; get; }//攻击的类型 主要是指线路
	
}
