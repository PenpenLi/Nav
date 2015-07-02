//×÷Õß:Â³Á¦Ô´
//Ê±¼ä£º2015/05/27
//¹¦ÄÜÃèÊö£º¹ÖÎïÐÅÏ¢Êý¾Ý½á¹¹
//±¸×¢£º×Ö¶ÎÓëÊý¾Ý±í×Ö¶ÎÃû³Æ±£³ÖÒ»ÖÂ
public class MonsterBase
{
    public int Id{ get; set; } //id
    public string Name{ get; set; }//Ãû×Ö
    public int IsBoss{ get; set; }//ÊÇ·ñboss
    public int Type{ get; set; }//ÀàÐÍ
    public double Atk{ get; set; }//¹¥»÷Á¦
	public double HP{ get; set; }//ÑªÁ¿
	public double AtkS{ get; set; }//¹¥»÷ËÙ¶È
	public double WS{ get; set; }//ÒÆ¶¯ËÙ¶È
    public int AtkD{ get; set; }//¹¥»÷¾àÀë
    public int AtkRange{ get; set; }//¹¥»÷·¶Î§
    public int AtkType{ get; set; }//¹¥»÷ÀàÐÍ
    public int SkillId{ get; set; }//¼¼ÄÜID
    public int BuffId{ get; set; }//BuffID
    public string Skl{ get; set; }//¶¯»­ÎÄ¼þµØÖ·
	public string Desc{ get; set; }//Í·ÏñID
    public int AttEff{ get; set; }//¹¥»÷ÌØÐ§
    public int FlightPath { set; get; }//·ÉÐÐÂ·¾¶ÀàÐÍ
    public double FlightSpeed { set; get; }//·ÉÐÐËÙ¶È
    public int AttSound{ get; set; }//¹¥»÷ÒôÐ§
	public int HitSound { set; get; }//????
    public int UnderEff{ get; set; }//±»»÷ÌØÐ§
    public int UnderPos{ get; set; }//±»»÷µÄ°ó¶¨µã
    public int UnderSound{ get; set; }//±»»÷ÒôÐ§
    public int Sort { set; get; }//Ä¬ÈÏÅÅÐòµÄÓÅÏÈ¼¶
    public double Offset { set; get; }//Õ½¶·¼ÆËãµÄÆ«ÒÆµã
    public double AttObjectX { set; get; }//Ô¶³Ì¹¥»÷µÄ·¢ÉäµãÆ«ÒÆ
    public double AttObjectY { set; get; }//Ô¶³Ì¹¥»÷µÄ·¢ÉäµãÆ«ÒÆ
    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public MonsterBase GetBaseData()
    {
        return this;
    }
}