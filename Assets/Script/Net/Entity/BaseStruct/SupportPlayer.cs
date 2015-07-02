using UnityEngine;
using System.Collections;

public enum SupportType
{
    Friend,
    Mercenary
}

public class SupportPlayer
{
    public SupportType type;
    public int playerID;
    public int level;
    public int headImage;
    public string nickName;
    public int cost;
    public int surplusTime;
}
