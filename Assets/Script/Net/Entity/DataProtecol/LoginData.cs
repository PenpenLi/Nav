using System.Collections.Generic;
public class LoginData
{
	public int status{set;get;}
	public PlayerInfo player{set;get;}
    public long curTime{set;get;}
    public List<BuildingInfo> BI { set; get; }
}