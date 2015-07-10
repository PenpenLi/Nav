/* 创建者：师小强
 * 时间：2015/7/9 13:22
 * 作用：房子信息
 */

public class HouseInfo {
    public int Id {set;get;}//house ID
	public string HouseName{set;get;}//house name   
    public int Level {set;get;}//the current level house
    public int NeedTime {set;get;} //the current up level need time
    public int NeedGold {set;get;}//the current up level need gold
    public string HouseIconName {set;get;} //name of house icon
	public UIAtlas HouseIconAtlas {set;get;} //atlas of house icon
	}