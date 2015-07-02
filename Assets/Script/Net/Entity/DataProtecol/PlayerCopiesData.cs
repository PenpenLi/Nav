using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NormalCopiesData
{
    public int copiesId{set;get;}
    public int star{set;get;}
}

public class SpecialCopiesData
{
    public int copiesId{set;get;}
    public int hasEnt{set;get;}
}

public class PlayerCopiesData
{
    public List<NormalCopiesData> star{set;get;}
    public List<SpecialCopiesData> entry{set;get;}
    public List<int> chapter{set;get;}
}
