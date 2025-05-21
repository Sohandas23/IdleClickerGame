using System;
using System.Collections.Generic;

[Serializable]
public class SaveData 
{
    public float coins;
    public Dictionary<string, int> upgradeLevels = new();
    public long lastSessionTicks;
}
