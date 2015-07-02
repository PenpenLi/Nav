using System;

//剧情配置
public class StoryConfig
{
    public StoryConfig(int ID ,int stageid,int heroid,int position,double triggertime,string content)
    {
        id = ID;
        Stageid = stageid;
        Heroid = heroid;
        Position = position;
        Triggertime = triggertime;
        Content = content;
    }

    public StoryConfig()
    {

    }

	public int id{ get; set;}
    public int Stageid{ get; set;}
    public int Heroid{ get; set;}
    public int Position{ get; set;}
    public double Triggertime{ get; set;}
    public string Content{ get; set;}
}
 

