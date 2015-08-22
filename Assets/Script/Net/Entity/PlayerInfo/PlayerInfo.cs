/*创建者：师小强
 * 时间：2015/7/28 AM 1:29
 * 说明: 获得玩家信息
*/

public class PlayerInfo
{
    public int Id { set; get; }//玩家ID
    public int IdHead { set; get; }//玩家头像ID
	public string PlayerName{ set; get; }//玩家用户名
	public string Psw{set; get;}//玩家密码
    public string nick { set; get; }//玩家昵称
    public int Lv { set; get; }//玩家等级
    public int PNu { set; get; }//人口数量
    public int GNu { set; get; } //金币数量
    public int GemNn { set; get; }//宝石数量
    public int IronNn { set; get; }//铁锭数量
    public int GunpowderNu { set; get; }//火药数量
    public int LignumNu { set; get; }//木材数量
    public int PetroleumNu { set; get; }//石油数量
    public int CoatNu { set; get; }//上衣数量
    public int PantsNu { set; get; }//裤子数量
    public int RumNu { set; get; }//朗姆酒数量
    public int FruitNu { set; get; }//水果数量
    public int FlourNu { set; get; }//面粉数量
    public string GUID { set; get; }//玩家唯一识别码
}