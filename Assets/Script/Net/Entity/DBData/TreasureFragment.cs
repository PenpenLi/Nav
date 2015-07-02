using UnityEngine;
using System.Collections;

public class TreasureFragment
{
	public int Id{set;get;}        //宝物碎片ID
	public string Name{set;get;}   //宝物碎片名称
	public int Icon{set;get;}      //宝物碎片图标
	public string Desc{set;get;}   //宝物碎片描述
	public int BaseId{set;get;}    //碎片位置
	public int Star{set;get;}      //碎片星级
	public int TrId{set;get;}      //碎片合成宝物ID
	public string DropCop{set;get;}    //掉落关卡
}
