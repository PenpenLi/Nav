/*创建者：师小强
 * 时间：2015/7/28 AM 1:29
 * 说明: 获得玩家信息
*/

public class PlayerInfo
{
    public int Id { set; get; }//玩家ID
	public string devId{ set; get; }//设备ID
    public int IdHead { set; get; }//玩家头像ID
	public string PlayerName{ set; get; }//玩家用户名
	public string Psw{set; get;}//玩家密码
    public string nick { set; get; }//玩家昵称
    public int Lv { set; get; }//玩家等级
    public long PNu { set; get; }//人口数量
    public long GNu { set; get; } //金币数量
    public long GemNn { set; get; }//宝石数量
    public long IronNn { set; get; }//铁锭数量
    public long GunpowderNu { set; get; }//火药数量
    public long LignumNu { set; get; }//木材数量
    public long PetroleumNu { set; get; }//石油数量
    public long CoatNu { set; get; }//上衣数量
    public long PantsNu { set; get; }//裤子数量
    public long RumNu { set; get; }//朗姆酒数量
    public long FruitNu { set; get; }//水果数量
    public long FlourNu { set; get; }//面粉数量
}