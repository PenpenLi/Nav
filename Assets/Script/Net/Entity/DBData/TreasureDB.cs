using UnityEngine;
using System.Collections;

public class TreasureDB {
	public int Id{set;get;}            //宝物ID
	public string Name{set;get;}       //宝物名称
	public string Desc{set;get;}       //宝物描述
	public int Icon{set;get;}          //宝物图标
	public string ChapName{set;get;}   //章节名称
	public int FrB1Id{set;get;}        //第一个位置所需材料
	public int FrB2Id{set;get;}        //第二个位置所需材料
	public int FrB3Id{set;get;}        //第三个位置所需材料
}
